using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    private TimeManager _timeMachine;
    private CentralBank _centralBank;
    private List<decimal> _lowerBeyond;
    private List<decimal> _percents;
    private DepositAccount _deposit;
    private DebitAccount _debit;
    private CreditAccount _credit;

    public BanksTest()
    {
        _timeMachine = new TimeManager();
        _centralBank = new CentralBank(_timeMachine);
        _lowerBeyond = new List<decimal>();
        _percents = new List<decimal>();
        _deposit = new DepositAccount();
        _debit = new DebitAccount();
        _credit = new CreditAccount();
    }

    [Fact]
    public void IsTimeManagerWorks()
    {
        _lowerBeyond.Add(0);
        _lowerBeyond.Add(1000);
        _lowerBeyond.Add(50000);
        _percents.Add(2);
        _percents.Add(4);
        _percents.Add(10);
        Client client = Client.Builder.WithName("ivj").WithSurname("slck").WithAddress("sldks").WithPassport("dcsnsofino").Build();
        Client client1 = Client.Builder.WithName("wfoeije").WithSurname("epomeqp").Build();
        Bank sber = _centralBank.AddBank("Sber", new Config(12, _lowerBeyond, _percents, 5, new TimeSpan(3, 0, 0, 0), 7000, 0.01m));
        Bank tink = _centralBank.AddBank("Tink", new Config(8, _lowerBeyond, _percents, 10, new TimeSpan(3, 0, 0, 0), 7000, 0.01m));
        _centralBank.AddClient(client, sber.Name, _credit);
        _centralBank.AddClient(client1, tink.Name, _debit);
        _centralBank.Transfer(_credit, _debit, 5000);
        _timeMachine.NewDay += _centralBank.CalculatePercentages;
        _timeMachine.NewMonth += _centralBank.ChargeInterest;
        _timeMachine.IncreaseTimeFor1Month();

        // Assert.Equal(8178.082191780821917808219176m, _debit.Money);
    }
}