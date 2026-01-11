using Banco.Core.Entities;
using Banco.Core.Utils;
using Banco.Core.ValueObjects;

namespace Banco.Core.UI;

public class Program
{
    static void Main()
    {
        var save = new Save();
        Console.WriteLine("""
                          ===================
                          ~ ~ BEM VINDO! ~ ~
                          ===================
                          """);
        
        string jsonPath = "Dados.json";
        string jsonContent = File.ReadAllText(jsonPath);

        if (string.IsNullOrEmpty(jsonContent))
        {
            Console.WriteLine("Por favor selecione uma opção:\n1 - Criar Conta\n2 - Entrar em sua Conta");
            var op = Console.ReadLine();
            switch (op)
            {
                case "1":
                    string nome = ReadNonEmptyString("Digite Nome do Conta: ");
                    string senha = ReadNonEmptyString("Digite sua Senha: ");
                    string email = ReadNonEmptyString("Digite seu email: ", 1);
                    string phonenumber = ReadNonEmptyString("Digite seu telefone: ", 2);
                    
                    var currentCustomer = new Customer(name:nome, email:email, phoneNumber:phonenumber, passwordHash:senha);
                    var currentAccount = new Account(owner:currentCustomer, new Money(0));
                    
                    save.AddCustomer(currentCustomer);
                    
                    break;
                case "2":
                    Console.Write("Digite o seu Email: ");
                    var email_login = Console.ReadLine();
                    
                    break;
                default:
                    Console.WriteLine("Operação inválida, Digite 1 ou 2!");
                    break;
            }
        }
    }
    static string ReadNonEmptyString(string prompt, int type=0)
    {
        string? input;
        switch (type)
        {
            case 0:
                do
                {
                    Console.Write(prompt);
                    input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Valor inválido! Tente novamente.");
                    }
                } while (string.IsNullOrWhiteSpace(input));

                return input;


            case 1:

                do
                {
                    Console.Write(prompt);
                    input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input) || !input.Contains('@'))
                    {
                        Console.WriteLine("Valor inválido! Tente novamente.");
                    }
                } while (string.IsNullOrWhiteSpace(input));

                return input;
            
            
            case 2:                                                              
                                                                          
                do                                                                  
                {                                                                   
                    Console.Write(prompt);                                          
                    input = Console.ReadLine();                                     
                    if (string.IsNullOrWhiteSpace(input) || input.Length < 10)
                    {                                                               
                        Console.WriteLine("Valor inválido! Tente novamente.");      
                    }                                                               
                } while (string.IsNullOrWhiteSpace(input));                         
                                                                          
                return input;                                                       

            case 3:
                do                                                              
                {                                                               
                    Console.Write(prompt);                                      
                    input = Console.ReadLine();                                 
                    if (string.IsNullOrWhiteSpace(input) || input.Length < 6)  
                    {                                                           
                        Console.WriteLine("A senha deve ter pelo menos 6 caracteres!");  
                    }                                                           
                } while (string.IsNullOrWhiteSpace(input));     
                
                return input;
            
            default:           
                return "ERRO";
        }
       
       
    }

    void LoggedIn(Customer customerAtual, Account currentAccount)
    {
        string op;
        do
        {
            Console.Write($"""
                          ==================================
                          ~ BEM VINDO {customerAtual.Name}! ~  
                          ==================================
                          
                          1- CHECAR SALDO
                          2- DEPOSITAR DINHEIRO
                          3- SACAR DINHEIRO
                          4- SAIR
                          >> 
                          """);
            decimal amount;
            Money money;

            op = Console.ReadLine(); 
            switch (op)
            {
                case "1":
                    Console.WriteLine($"SEU SALDO ATUAL: {currentAccount.Balance}");
                    break;
                case "2":
                    Console.Write("Digite o valor a ser depositado: ");
                    if (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
                {
                        throw new ArgumentException("Valor inválido! Operação cancelada.");
                }
                    money = new Money(amount);
                    currentAccount.Deposit(money);
                    break;
                    
                case "3":
                    Console.Write("Digite o valor a ser sacado: ");

                    if (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                    {
                        Console.WriteLine("Valor inválido. Operação cancelada.");
                        break;
                    }
                    money = new Money(amount);
                    currentAccount.Withdraw(money);
                    break;
                
                case "4":
                    Console.WriteLine($"Até a próxima, {customerAtual.Name}!");
                    break;
                
                default:
                    Console.WriteLine("Operação Inválida, Digite um número válido!");
                    break;
            }
        } while (op != "4");

    }
    
}