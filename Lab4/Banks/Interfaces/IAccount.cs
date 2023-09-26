using Banks.Models;

namespace Banks.Interfaces;

public interface IAccount
{
    decimal TransactionRestrictionLimit { get; set; }
    DateTime AccountCreationTime { get; set; }
    Guid AccountID { get; }
    decimal Money { get; }
    void CalculatePercentages();
    void ChargeInterests();
    void FillWithData(Config config);
    void WithdrawMoney(decimal amount);
    void TopupMoney(decimal amount);
    void TopupDepositInitialAmount(decimal amount);
    void RevertTopupDepositInitialAmount(decimal amount);
    void RemoveCommission(decimal amount);
    void ReturnCommission(decimal amount);
    bool SetTime(DateTime dateTime);
}