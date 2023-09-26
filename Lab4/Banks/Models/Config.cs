using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class Config
{
    private SortedDictionary<decimal, decimal> _percentsForDepositAccounts;
    public Config(decimal debitPercent, List<decimal> lowerBounds, List<decimal> percents, decimal creditCommission, TimeSpan restrictionDuration, decimal transactionRestrictionLimit, decimal commissionForTransactions)
    {
        ArgumentNullException.ThrowIfNull(restrictionDuration);
        ArgumentNullException.ThrowIfNull(lowerBounds);
        ArgumentNullException.ThrowIfNull(percents);
        if (debitPercent < 0) throw ConfigException.NegativePercent();
        if (creditCommission < 0) throw ConfigException.NegativePercent();
        if (transactionRestrictionLimit < 0) throw ConfigException.NegativeLimit();
        if (commissionForTransactions < 0) throw ConfigException.NegativePercent();
        if (lowerBounds.Count != percents.Count) throw ConfigException.InvalidArgumentsInConstructor();
        _percentsForDepositAccounts = new SortedDictionary<decimal, decimal>();
        for (int i = 0; i < lowerBounds.Count; ++i)
        {
            _percentsForDepositAccounts.Add(lowerBounds[i], percents[i]);
        }

        DebitPercent = debitPercent;
        CreditCommission = creditCommission;
        RestrictionDuration = restrictionDuration;
        TransactionRestrictionLimit = transactionRestrictionLimit;
        CommissionForTransactions = commissionForTransactions;
    }

    public Bank Bank { get; }
    internal decimal DebitPercent { get; }
    internal decimal CreditCommission { get; }
    internal TimeSpan RestrictionDuration { get; }
    internal decimal TransactionRestrictionLimit { get; }
    internal decimal CommissionForTransactions { get; }
    public override string ToString()
    {
        return $"Debit interest on the balance: {DebitPercent}\nCredit commission: {CreditCommission}\nDeposit percents:\n{GetStringDepositConfig()}\nDeposit account time restriction duration: {RestrictionDuration.ToString()}\nCommission for transaction: {CommissionForTransactions}";
    }

    internal decimal GetDepositPercent(decimal amount)
    {
        var previousPair = new KeyValuePair<decimal, decimal>(-1, -1);
        foreach (KeyValuePair<decimal, decimal> pair in _percentsForDepositAccounts)
        {
            if (pair.Equals(_percentsForDepositAccounts.Last())) return pair.Value;
            if (pair.Key > amount) return previousPair.Value;
            previousPair = pair;
        }

        return 0;
    }

    private string GetStringDepositConfig()
    {
        return string.Join("\n", _percentsForDepositAccounts.Select(p => $"{p.Key}+ - {p.Value}"));
    }
}