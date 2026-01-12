namespace Banco.Core.Utils;

public class AuthService //serviço de autenticação 
{
    private readonly Save _save; 
    
    public AuthService(Save save)
    {
        _save = save;
    }
    public bool TryLogin(string email, string password)
    {
        var customer = _save.GetCustomerByEmail(email);
        if (customer == null) return false;

        return customer.CheckPassword(password: password);
    }
}