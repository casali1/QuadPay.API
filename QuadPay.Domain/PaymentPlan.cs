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


        public PaymentPlan(decimal amount, int installmentCount = 4, int installmentIntervalDays = 14) {
            // TODO
            //Installment 
            InitializeInstallments(amount, installmentCount, installmentIntervalDays);
        }

        // Installments are paid in order by Date
        public Installment NextInstallment()
        {
            // TODO


            return new Installment();
        }

        public Installment FirstInstallment()
        {
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

        public decimal OustandingBalance()
        {
            // TODO


            return 0;
        }

        public decimal AmountPastDue(DateTime currentDate)
        {
            // TODO


            return 0;
        }

        public IList<Installment> PaidInstallments()
        {
            var paidInstallments = new List<Installment>();
            //paidInstallments.
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

            for (int i=1; i < CountOfInstallations; i++)
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

        public decimal MaximumRefundAvailable() {
            // TODO
            return 0;
        }

        // We only accept payments matching the Installment Amount.
        public void MakePayment(decimal amount, Guid installmentId)
        {
            var activeAccount = Installments.Where(x => x.Id == installmentId);

            foreach(var singleInstallment in activeAccount)
            {
                if (singleInstallment.IsPaid)
                {
                    singleInstallment.IsPaid = true;
                    break;
                }
            }
        }




        // Returns: Amount to refund via PaymentProvider
        public decimal ApplyRefund(Refund refund)
        {
            // TODO
            return 0;
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