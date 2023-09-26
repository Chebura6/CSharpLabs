using Banks.Models;

namespace Banks.ClientBuilder;

public interface ISurnameBuilder
{
    IOptionalBuilder WithSurname(string surname);
}