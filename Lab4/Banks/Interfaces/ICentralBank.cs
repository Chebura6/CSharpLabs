using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface ICentralBank
{
    void AddClient(Client client, string bankName, IAccount account);
    Bank AddBank(string name, Config config);
    void CalculatePercentages();
    void ChargeInterest();
    public void ChangeConfig(string bankName, Config config);
    string GetConfig(string bankName);
    bool Transfer(IAccount from, IAccount toAccount, decimal amount);
    bool Transfer(Guid fromGuid, Guid toGuid, decimal amount);
}