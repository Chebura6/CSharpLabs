using Banks.Console.BanksConsoleApplication;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;
using Spectre.Console;
namespace Banks.Console;

public static class Program
{
    public static void Main()
    {
        // AnsiConsole.Background = Color.Blue;
        var timeMachine = new TimeManager();
        var centralBank = new CentralBank(timeMachine);
        var notifyer = new SpectreConsoleNotifyer();
        var lowerBeyond = new List<decimal>();
        var percents = new List<decimal>();
        lowerBeyond.Add(0);
        lowerBeyond.Add(1000);
        lowerBeyond.Add(50000);
        percents.Add(2);
        percents.Add(4);
        percents.Add(10);
        Client client = Client.Builder.WithName("ivj").WithSurname("slck").WithAddress("sldks").WithPassport("dcsnsofino").Build();
        Client client1 = Client.Builder.WithName("wfoeije").WithSurname("epomeqp").Build();
        var deposit = new DepositAccount();
        var debit = new DebitAccount();
        var credit = new CreditAccount();
        var realTimeManager = new RealTimeManager();
        Bank sber = centralBank.AddBank("Sber", new Config(12, lowerBeyond, percents, 5, new TimeSpan(3, 0, 0, 0), 7000, 0.01m));
        Bank tink = centralBank.AddBank("Tink", new Config(8, lowerBeyond, percents, 10, new TimeSpan(3, 0, 0, 0), 7000, 0.01m));
        centralBank.AddClient(client, sber.Name, credit);
        centralBank.AddClient(client1, tink.Name, debit);
        var clientAccountsParser = new ClientAccountsParser();
        centralBank.NewConfig += notifyer.ConfigHasBeenChanged;
        var clientParams = new List<string> { "name", "surname", "passport" };
        StartMenu.Welcome();
        while (true)
        {
            string option = StartMenu.OperationsMenu();
            try
            {
                switch (option)
                {
                    case "Add Bank":
                        string bankName1 = FillBankParams.GetBankName();
                        var config1 = new Config(FillBankParams.GetDebitPercent(), FillBankParams.GetLowerBounds(), FillBankParams.GetPercents(), FillBankParams.GetCreditCommission(), FillBankParams.GetRestrictionDuration(), FillBankParams.GetTransactionRestrictionLimit(), FillBankParams.GetCommissionForTransactions());
                        centralBank.AddBank(bankName1, config1);
                        AnsiConsole.Write(new Markup("[bold yellow]Bank has been successfully added![/]"));
                        break;
                    case "Add Client":
                        string name = AnsiConsole.Ask<string>("What's client [green]Name[/]?");
                        string surName = AnsiConsole.Ask<string>("What's client [green]surname[/]?");
                        string address = AnsiConsole.Ask<string>("What's client [green]address[/]?");
                        string passport = AnsiConsole.Ask<string>("What's client [green]passport[/]?");

                        var banksTable = new Table();
                        banksTable.AddColumn("Banks");
                        foreach (string bankName in centralBank.GetBanksInSystem())
                        {
                            banksTable.AddRow(bankName);
                        }

                        AnsiConsole.Write(banksTable);
                        string chosenBankName = AnsiConsole.Ask<string>("Which [bold green]bank[/] would you choose?");

                        IAccount accountType = ChooseAccountMenu.ChooseAccount();

                        if (passport == "-")
                        {
                            if (address == "-")
                            {
                                centralBank.AddClient(Client.Builder.WithName(name).WithSurname(surName).Build(), chosenBankName, accountType);
                            }
                            else
                            {
                                centralBank.AddClient(
                                    Client.Builder.WithName(name).WithSurname(surName).WithAddress(address).Build(), chosenBankName, accountType);
                            }
                        }
                        else
                        {
                            if (address == "-")
                            {
                                centralBank.AddClient(
                                    Client.Builder.WithName(name).WithSurname(surName).WithPassport(passport).Build(), chosenBankName, accountType);
                            }
                            else
                            {
                                centralBank.AddClient(
                                    Client.Builder.WithName(name).WithSurname(surName).WithAddress(address).WithPassport(passport).Build(), chosenBankName, accountType);
                            }
                        }

                        AnsiConsole.Write(new Markup($"[green]{name} has been successfully added![/]\n"));

                        if (accountType.GetType() == typeof(DepositAccount))
                        {
                            string isTransaction =
                                AnsiConsole.Ask<string>("Would you like [green]topup[/] your new account?(yes/no)");
                            if (isTransaction is "Yes" or "yes" or "y" or "Y")
                            {
                                clientAccountsParser.Output(centralBank.GetClientAccountsInfo());
                                Guid fromID =
                                    AnsiConsole.Ask<Guid>("Which account do you want to transfer money [blue]from?[/]");
                                Guid toID = accountType.AccountID;
                                decimal amount =
                                    AnsiConsole.Ask<decimal>("What [blue]amount[/] would you like to transfer?");
                                if (centralBank.TopupInitialAmount(fromID, toID, amount))
                                    AnsiConsole.Write(new Markup($"Transaction is [bold green]successful[/]"));
                                else AnsiConsole.Write(new Markup($"Transaction is [bold red]failed[/]\n"));
                            }
                        }

                        break;
                    case "Get config":
                        var banksTable1 = new Table();
                        banksTable1.AddColumn("Banks");
                        foreach (string bankName in centralBank.GetBanksInSystem())
                        {
                            banksTable1.AddRow(bankName);
                        }

                        AnsiConsole.Write(banksTable1);
                        string nameOfBankToFindConfig = AnsiConsole.Ask<string>("What's [green]bank[/]?");
                        AnsiConsole.Write(new Markup(centralBank.GetConfig(nameOfBankToFindConfig)));
                        break;
                    case "Change config":
                        var config = new Config(FillBankParams.GetDebitPercent(), FillBankParams.GetLowerBounds(), FillBankParams.GetPercents(), FillBankParams.GetCreditCommission(), FillBankParams.GetRestrictionDuration(), FillBankParams.GetTransactionRestrictionLimit(), FillBankParams.GetCommissionForTransactions());
                        centralBank.ChangeConfig(FillBankParams.GetBankName(), config);
                        break;
                    case "Make transaction":
                        MakeTransaction.Execute(centralBank);
                        break;
                    case "Get system state":
                        clientAccountsParser.Output(centralBank.GetClientAccountsInfo());
                        break;
                    case "Increase time for 1 hour":
                        realTimeManager.IncreaseTimeFor1Hour();
                        break;
                }
            }
            catch (Exception exception)
            {
                AnsiConsole.Write(new Markup($"[bold red]{exception.Message}[/]"));
            }

            AnsiConsole.WriteLine();
        }
    }
}