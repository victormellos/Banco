using Banco.Core.Utils;

namespace Banco.Core.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }
    
    public Customer(string name, string email, string phoneNumber, string  passwordHash)
    {
        Id = Guid.NewGuid();
        UpdateEmail(email);
        Name = name;
        UpdateNumber(phoneNumber);
        SetPassword(passwordHash);
        
    }
    
    void SetPassword(string password)
    {
        PasswordHash = Password.HashPassword(password);
    }

    public bool CheckPassword(string password)
    {
        return Password.VerifyPassword(password, PasswordHash); 
    }
    
    //Caso usuário queira mudar o email ou número de telefone
    
    void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) ||  !newEmail.Contains('@'))
        {
            throw new ArgumentException("Email Inválido");
        }
        Email = newEmail;
    }

    void UpdateNumber(string newNumber)
    {
        var onlyNumbers = "";
        foreach (var number in newNumber)
        {
            if (char.IsDigit(number))
            {
                onlyNumbers += number;
            }
        }

        if (string.IsNullOrWhiteSpace(newNumber) || onlyNumbers.Length < 10) // DDD + numero = 10
        {
            throw new ArgumentException("Número inválido");
        }
        PhoneNumber = onlyNumbers;
    }
}