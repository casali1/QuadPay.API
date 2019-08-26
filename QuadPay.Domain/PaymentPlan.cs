using System;
using System.Collections.Generic;
using System.Linq;

namespace QuadPay.Domain
{
    public class PaymentPlan
    {
        public Guid Id { get; set; }
        public IList<Installment> Installments { get; set; }
        public IList<Refund> Refunds { get; }
        public DateTime OriginationDate { get; }

        public Installment Install;

        public int CountOfInstallations { get; set; }
        public int InstallmentIntervalOfDays { get; set; }


        public PaymentPlan(decimal amount, int installmentCount = 4, int installmentIntervalDays = 14)
        {
            InitializeInstallments(amount, installmentCount, installmentIntervalDays);
        }

        // Installments are paid in order by Date
        public Installment NextInstallment()
        {
            var activeAccount = Installments.Where(x => x.Id == Id);

            foreach (var singleInstallment in activeAccount)
            {
                if (!singleInstallment.IsPaid) return singleInstallment;
            }

            return new Installment();
        }

        public Installment FirstInstallment()
        {
            if (Install.Amount < 0 || CountOfInstallations < 1) throw new ArgumentException();

            var firstInstall = new Installment()
            {
                Id = Install.Id,
                Amount = Install.Amount * 1 / CountOfInstallations,
                Date = Install.Date
            };

            if (Install.IsPending)
            {
                Install.IsPending = false;
                Installments.Add(firstInstall);
                PendingInstallments();
            }

            return firstInstall;
        }

        public decimal OustandingBalance(Guid installmentId)
        {
            var activeAccount = Installments.Where(x => x.Id == installmentId);
            var balance = 0M;
            foreach (var singleInstallment in activeAccount)
            {
                if (!singleInstallment.IsPaid && !singleInstallment.AccountClosed) balance = balance + singleInstallment.Amount;
            }
            return balance;
        }

        public decimal AmountPastDue(DateTime currentDate)
        {
            // TODO


            return 0;
        }

        public IList<Installment> PaidInstallments()
        {
            var paidInstallments = new List<Installment>();
            return new List<Installment>();
        }

        public IList<Installment> DefaultedInstallments()
        {
            // TODO
            return new List<Installment>();
        }

        public IList<Installment> PendingInstallments()
        {
            var intervals = InstallmentIntervalOfDays;

            for (int i = 1; i < CountOfInstallations; i++)
            {
                var pendingInstallment = new Installment()
                {
                    Id = Install.Id,
                    Amount = Install.Amount * 1 / CountOfInstallations,
                    Date = Install.Date.AddDays(intervals)
                };

                Installments.Add(pendingInstallment);
                intervals = intervals + InstallmentIntervalOfDays;
            }

            return Installments;
        }

        public decimal MaximumRefundAvailable()
        {
            // TODO
            return 0;
        }

        // We only accept payments matching the Installment Amount.
        public void MakePayment(decimal amount, Guid installmentId)
        {
            var activeAccount = Installments.Where(x => x.Id == installmentId);

            foreach (var singleInstallment in activeAccount)
            {
                if (!singleInstallment.IsPaid && singleInstallment.Amount == amount)
                {
                    singleInstallment.IsPaid = true;
                    break;
                }
            }
        }

        // Returns: Amount to refund via PaymentProvider
        public decimal ApplyRefund(Guid installmentId)
        {
            var activeAccount = Installments.Where(x => x.Id == installmentId);
            var refund = 0M;

            foreach (var singleInstallment in activeAccount)
            {
                if (singleInstallment.IsPaid && !singleInstallment.AccountClosed)
                {
                    refund = refund + singleInstallment.Amount;
                    singleInstallment.IsPaid = false;
                }
                singleInstallment.AccountClosed = true;
            }

            var closingAccount = activeAccount.FirstOrDefault();
            var refundAccount = new Refund(refund)
            {
                Id = closingAccount.Id,
                Date = DateTime.Now
            };

            return refund;
        }


        // First Installment always occurs on PaymentPlan creation date
        private void InitializeInstallments(decimal amount, int installmentCount, int installmentIntervalDays)
        {
            Id = Guid.NewGuid();
            Install = new Installment()
            {
                Id = Id,
                Amount = amount,
                Date = DateTime.Now
            };

            CountOfInstallations = installmentCount;
            InstallmentIntervalOfDays = installmentIntervalDays;

            Installments = new List<Installment>();
            FirstInstallment();           
        }
    }
}