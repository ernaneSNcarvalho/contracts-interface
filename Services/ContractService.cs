using System;
using Contracts.Entities;

namespace Contracts.Services
{
    class ContractService
    {
        private IOnlinePaymentService _ionlinePaymentService;
        public ContractService(IOnlinePaymentService onlinePaymentService)
        {
            _ionlinePaymentService = onlinePaymentService;
        }
        public void ProcessContract(Contract contract, int months)
        {
            double basicQuota = contract.TotalValue / months;
            for (int i = 1; i <= months; i++)
            {
                DateTime date = contract.Date.AddMonths(i);
                double updatedQuota = basicQuota + _ionlinePaymentService.Interest(basicQuota, i);
                double fullQuota = updatedQuota + _ionlinePaymentService.PaymentFee(updatedQuota);
                contract.AddInstallment(new Installment(date, fullQuota));
            }
        }
    }
}