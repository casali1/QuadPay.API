using System;

namespace QuadPay.Domain {
    public class Refund {

        public Guid Id { get; set; }
        public string IdempotencyKey { get; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public Refund(decimal amount)
        {
            Amount = amount;
        }

    }
}