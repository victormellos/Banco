using System.Text.Json;
using Banco.Core.Entities;

namespace Banco.Core.Utils
{
    public class Save
    {
        private readonly string _jsonPath = "Dados.json";
        private List<Customer> _customers;
        
        public Save()
        {
            LoadCustomers();
        }

        private void LoadCustomers() // de json pra classe
        {
            if (!File.Exists(_jsonPath))
            {
                _customers = new List<Customer>();
                return;
            }

            string json = File.ReadAllText(_jsonPath);
            _customers = JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
        }

        private void SaveCustomers() // de classe pra json
        {
            string json = JsonSerializer.Serialize(_customers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonPath, json);
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            SaveCustomers();
        }
        
        public Customer? GetCustomerByEmail(string email)
        {
            Customer? customerByMail =  _customers.FirstOrDefault(c => c.Email == email);
            Console.WriteLine("");
        }

        
        
    }
}