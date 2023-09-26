using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class DebitAccount : IAccount
{
    public DebitAccount()
    {
        Percent = 0;
        Money = 0;
        CurrentPercentSum = 0;
        AccountID = Guid.NewGuid();
    }

    public decimal TransactionRestrictionLimit { get; set; }
    public decimal Percent { get; set; }
    public DateTime AccountCreationTime { get; set; }
    public Guid AccountID { get; }
    public decimal Money { get; internal set; }
    internal decimal CurrentPercentSum { get; set; }
    internal decimal TransactionRestrictionUsed { get; set; }
    internal decimal CommissionForTransactions { get; set; }
    public void CalculatePercentages()
    {
        CurrentPercentSum += Money * Percent;
    }

    public void ChargeInterests()
    {
        Money += CurrentPercentSum;
        CurrentPercentSum = 0;
    }

    public void FillWithData(Config config)
    {
        Percent = config.DebitPercent / 365m;
        Money = 0;
        CurrentPercentSum = 0;
        CommissionForTransactions = config.CommissionForTransactions;
    }

    public void WithdrawMoney(decimal amount)
    {
        if (TransactionRestrictionLimit == -1)
        {
            TransactionRestrictionUsed = 0;
        }
        else
        {
            if (TransactionRestrictionUsed + amount > TransactionRestrictionLimit) throw AccountException.LimitExceeded();
            TransactionRestrictionUsed += amount;
        }

        if (Money < amount) throw new Exception();
        Money -= amount;
    }

    public void TopupMoney(decimal amount)
    {
        if (TransactionRestrictionLimit == -1)
        {
            TransactionRestrictionUsed = 0;
        }
        else
        {
            if (TransactionRestrictionUsed + amount > TransactionRestrictionLimit) throw AccountException.LimitExceeded();
            TransactionRestrictionUsed += amount;
        }

        Money += amount;
    }

    public void TopupDepositInitialAmount(decimal amount)
    {
        Money += 0;
    }

    public void RevertTopupDepositInitialAmount(decimal amount)
    {
        Money -= 0;
    }

    public void RemoveCommission(decimal amount)
    {
        if (Money < amount) throw new Exception();
        Money -= amount * CommissionForTransactions;
    }

    public void ReturnCommission(decimal amount)
    {
        Money += amount * CommissionForTransactions;
    }

    public bool SetTime(DateTime dateTime)
    {
        return false;
    }

    public override string ToString()
    {
        return string.Concat("\tAccount type:", "Debit account\n\t", "Money: ", Money, "\t\nTransaction restriction limit: ", TransactionRestrictionLimit, "\t\nUsed transaction restriction: ", TransactionRestrictionUsed, "\t\nAccount ID: ", AccountID);
    }
}