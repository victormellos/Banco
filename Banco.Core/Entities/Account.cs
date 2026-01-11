using Banco.Core.ValueObjects;

namespace Banco.Core.Entities;

public class Account
{
    public Guid Id { get;private set; }
    
    public Customer Owner { get;private set; }
    
    public Money Balance { get;private set; }

    public Account(Customer owner, Money balance)
    {
        Id = Guid.NewGuid();
        
        Owner = owner;
        Balance = balance;
    }

    public void Deposit(Money amount)
    {
        if (amount.IsNegative())
        {
            throw new ArgumentException("Valor inválido");
        }
        Balance = Balance.AddAmount(amount);
    }

    public void Withdraw(Money amount)
    {
        if (amount.IsGreaterThan(Balance))
        {
            throw new ArgumentException("Valor não pode ser maior que saldo");
        }
        if (amount.IsNegative())
        {
            throw new ArgumentException("Valor não pode ser negativo");
        }
        Balance = Balance.SubtractAmount(amount);
        
    }
}