using Banks.ClientBuilder;
using Banks.Exceptions;

namespace Banks.Models;

public class Client
{
    private Client(string name, string surname, string passport, string address)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) throw ClientException.InvalidParameter();
        if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrEmpty(surname)) throw ClientException.InvalidParameter();
        Name = name;
        Surname = surname;
        Passport = passport;
        Address = address;
    }

    public static INameBuilder Builder => new ClientBuilder();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Passport { get; set; }
    public string Address { get; set; }
    public override string ToString()
    {
        return string.Concat("\tName: ", Name, "\n\tSurname: ", Surname, "\n\tPassport: ", Passport, "\n\tAddress: ", Address);
    }

    private class ClientBuilder : INameBuilder, ISurnameBuilder, IOptionalBuilder
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public ISurnameBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public IOptionalBuilder WithSurname(string surname)
        {
            Surname = surname;
            return this;
        }

        public IOptionalBuilder WithAddress(string address)
        {
            Address = address;
            return this;
        }

        public IOptionalBuilder WithPassport(string passport)
        {
            Passport = passport;
            return this;
        }

        public Client Build()
        {
            return new Client(Name, Surname, Passport, Address);
        }
    }
}