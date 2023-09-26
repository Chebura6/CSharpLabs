namespace Banks.ClientBuilder;

public interface INameBuilder
{
    ISurnameBuilder WithName(string name);
}