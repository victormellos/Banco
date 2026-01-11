namespace Banco.Core.ValueObjects;

public class Money
{
    private decimal Amount { get;}

    public Money(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Valor não pode ser negativo");
        }
        Amount = amount;
    }

    public Money AddAmount(Money amount)
    {
        if (amount.IsNegative())
        {
            throw new ArgumentException("Valor inválido");
        }
        return new Money(Amount + amount.Amount);
    }

    public Money SubtractAmount(Money amount)
    {
        if (amount.IsNegative())
        {
            throw new ArgumentException("Valor inválido");
        }

        return new Money(Amount - amount.Amount);
    }
    public bool IsNegative() => Amount < 0; //Função lambda
    public bool IsGreaterThan(Money other) => Amount > other.Amount;
}