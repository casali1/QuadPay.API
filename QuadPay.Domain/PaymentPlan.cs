﻿using System;
using System.Collections.Generic;

namespace QuadPay.Domain
{
    public class PaymentPlan
    {
        public Guid Id { get; }
        public IList<Installment> Installments { get; }
        public IList<Refund> Refunds { get; }
        public DateTime OriginationDate { get; }
        public PaymentPlan(decimal amount, int installmentCount = 4, int installmentIntervalDays = 14) {
            // TODO
            InitializeInstallments();
        }

        // Installments are paid in order by Date
        public Installment NextInstallment() {
            // TODO
            return new Installment();
        }

        public Installment FirstInstallment() {
            // TODO
            return new Installment();
        }

        public decimal OustandingBalance() {
            // TODO
            return 0;
        }

        public decimal AmountPastDue(DateTime currentDate) {
            // TODO
            return 0;
        }

        public IList<Installment> PaidInstallments() {
            // TODO
            return new List<Installment>();
        }

        public IList<Installment> DefaultedInstallments() {
            // TODO
            return new List<Installment>();
        }

        public IList<Installment> PendingInstallments() {
            // TODO
            return new List<Installment>();
        }

        public decimal MaximumRefundAvailable() {
            // TODO
            return 0;
        }

        // We only accept payments matching the Installment Amount.
        public void MakePayment(decimal amount, Guid installmentId) {

        }

        // Returns: Amount to refund via PaymentProvider
        public decimal ApplyRefund(Refund refund) {
            // TODO
            return 0;
        }

        // First Installment always occurs on PaymentPlan creation date
        private void InitializeInstallments() {
            // TODO
        }
    }
}