using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class DepositAccount : IAccount
{
    public DepositAccount()
    {
        Percent = 0;
        Money = 0;
        CurrentPercentSum = 0;
        InitialAmount = 0;
        AccountID = Guid.NewGuid();
    }

    public decimal TransactionRestrictionLimit { get; set; }
    public decimal Percent { get; set; }
    public DateTime CurrentTime { get; set; }
    public DateTime AccountCreationTime { get; set; }
    public Guid AccountID { get; }
    public decimal Money { get; internal set; }
    internal DateTime RestrictionsDuration { get; set; }
    internal decimal CurrentPercentSum { get; set; }
    internal decimal InitialAmount { get; set; }
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
        Percent = config.DebitPercent / 365;
        Money = 0;
        CurrentPercentSum = 0;
        InitialAmount = 0;
        RestrictionsDuration = DateTime.Now.Add(config.RestrictionDuration);
    }

    public void WithdrawMoney(decimal amount)
    {
        if (DateTime.Compare(RestrictionsDuration, CurrentTime) >= 0) throw new Exception();
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
        if (TransactionRestrictionLimit == -1)
        {
            TransactionRestrictionUsed = 0;
        }

        InitialAmount += amount;
    }

    public void RevertTopupDepositInitialAmount(decimal amount)
    {
        if (TransactionRestrictionLimit == -1)
        {
            TransactionRestrictionUsed = 0;
        }

        InitialAmount -= amount;
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
        CurrentTime = dateTime;
        return true;
    }

    public override string ToString()
    {
        return string.Concat("\tAccount type: ", "Deposit account\n\t", "Money: ", Money, "\t\nOperations restriction ends in: ", RestrictionsDuration, "\t\nAccount creation amount: ", InitialAmount, "\t\nAccount ID: ", AccountID);
    }
}