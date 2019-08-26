using System;

namespace QuadPay.Domain
{
    public class Installment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        private InstallmentStatus InstallmentStatus;
        public string PaymentReference { get; }
        // Date this Installment was marked 'Paid'
        public DateTime SettlementDate { get; }

        public Installment(/* TODO */) {
            // TODO
        }

        public bool IsPaid { 
            get {
                // TODO
                return true;
            }
        }

        public bool IsDefaulted { 
            get {
                // TODO
                return true;
            }
        }

        public bool IsPending { 
            get {
                // TODO
                return true;
            }
        }

        public void SetPaid(string paymentReference) {
            // TODO
        }
    }

    public enum InstallmentStatus {
        Pending = 0, // Not yet paid
        Paid = 1, // Can be either paid with a charge, or covered by a refund
        Defaulted = 2 // Charge failed
    }
}