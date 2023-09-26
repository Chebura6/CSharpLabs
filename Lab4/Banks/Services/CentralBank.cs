using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private ITimeManager _timeManager;
    public CentralBank(ITimeManager timeManager)
    {
        Banks = new List<Bank>();
        _timeManager = timeManager;
    }

    public delegate void MethodContainer();
    public event MethodContainer NewConfig;
    internal List<Bank> Banks { get; }
    public void AddClient(Client client, string bankName, IAccount account)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(account);

        Bank bank = FindBank(bankName);
        account.AccountCreationTime = _timeManager.CurrentTime;
        if (!string.IsNullOrEmpty(client.Passport) && !string.IsNullOrEmpty(client.Address))
        {
            account.TransactionRestrictionLimit = -1;
        }
        else
        {
            account.TransactionRestrictionLimit = bank.Config.TransactionRestrictionLimit;
        }

        bank.AddAccount(client, account);
    }

    public void ChangeAccountsTime(DateTime dateTime)
    {
        foreach (Bank bank in Banks)
        {
            bank.SetAccountsTime(dateTime);
        }
    }

    public void ChargeInterest()
    {
        foreach (Bank bank in Banks)
        {
            bank.ChargeInterests();
        }
    }

    public void CalculatePercentages()
    {
        foreach (Bank bank in Banks)
        {
            bank.CalculateThePercentageOnTheBalance();
        }
    }

    public Bank AddBank(string name, Config config)
    {
        if (string.IsNullOrEmpty(name)) throw CentralBankException.InvalidBankName();
        ArgumentNullException.ThrowIfNull(config);
        var bank = new Bank(name, config);
        Banks.Add(bank);
        return bank;
    }

    public void ChangeConfig(string bankName, Config config)
    {
        ArgumentNullException.ThrowIfNull(config);
        Bank bank = FindBank(bankName);
        bank.Config = config;
        NewConfig?.Invoke();
    }

    public string GetConfig(string bankName)
    {
        return FindBank(bankName).Config.ToString();
    }

    public bool Transfer(IAccount from, IAccount toAccount, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(toAccount);
        var ctx = new TransactionContext(from, toAccount, amount);
        var withdraw = new TransactionHandler(new WithdrawCommand(ctx));
        var commission = new TransactionHandler(new CommissionCommand(ctx));
        var topup = new TransactionHandler(new TopupCommand(ctx));
        return new TransactionHandlerChain(withdraw, topup).Execute();
    }

    public bool TopupInitialAmount(Guid fromGuid, Guid toGuid, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(fromGuid);
        ArgumentNullException.ThrowIfNull(toGuid);
        var from = FindAccount(fromGuid);
        var to = FindAccount(toGuid);
        var ctx = new TransactionContext(from, to, amount);
        var withdraw = new TransactionHandler(new WithdrawCommand(ctx));
        var commission = new TransactionHandler(new CommissionCommand(ctx));
        var topup = new TransactionHandler(new InitialAmountCommand(ctx));
        return new TransactionHandlerChain(withdraw, commission, topup).Execute();
    }

    public bool Transfer(Guid fromGuid, Guid toGuid, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(fromGuid);
        ArgumentNullException.ThrowIfNull(toGuid);
        var from = FindAccount(fromGuid);
        var to = FindAccount(toGuid);
        var ctx = new TransactionContext(from, to, amount);
        var withdraw = new TransactionHandler(new WithdrawCommand(ctx));
        var commission = new TransactionHandler(new CommissionCommand(ctx));
        var topup = new TransactionHandler(new TopupCommand(ctx));
        return new TransactionHandlerChain(withdraw, commission, topup).Execute();
    }

    public IAccount FindAccount(Guid accountID)
    {
        return Banks.SelectMany(b => b.Accounts)
            .FirstOrDefault(a => a.Account.AccountID == accountID)?
            .Account;
    }

    public void TimeHasBeenDecreased(DateTime dateTime)
    {
        foreach (Bank bank in Banks)
        {
            bank.RemoveAccountsFromFuture(dateTime);
        }
    }

    public string[] GetClientAccountsInfo()
    {
        string totalString = string.Empty;
        foreach (Bank bank in Banks)
        {
             totalString = string.Concat(totalString, bank.ToString());
        }

        totalString = totalString.Substring(1);
        string[] total = totalString.Split("\t");
        return total;
    }

    public IReadOnlyCollection<string> GetBanksInSystem()
    {
        return Banks.Select(x => x.Name)
            .Distinct()
            .ToArray();
    }

    private Bank FindBank(string bankName)
    {
        Bank bank = Banks.FirstOrDefault(x => x.Name.Equals(bankName));
        if (bank is null) throw CentralBankException.InvalidBankName();
        return bank;
    }
}