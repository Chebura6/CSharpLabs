using Banks.Models;

namespace Banks.ClientBuilder;

public interface IOptionalBuilder
{
    IOptionalBuilder WithAddress(string address);
    IOptionalBuilder WithPassport(string passport);
    Client Build();
}