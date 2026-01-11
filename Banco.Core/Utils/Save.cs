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

        public bool TryLogin(string email, string password)
        {
            var customer = _customers.Find(c => c.Email == email);
            if (customer == null) return false;

            return Password.VerifyPassword(password, customer.PasswordHash);
        }

        public bool DoesSaveExist()
        {
            return File.Exists(_jsonPath) && !string.IsNullOrWhiteSpace(File.ReadAllText(_jsonPath));
        }

        private void LoadCustomers()
        {
            if (!File.Exists(_jsonPath))
            {
                _customers = new List<Customer>();
                return;
            }

            string json = File.ReadAllText(_jsonPath);
            _customers = JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
        }

        private void SaveCustomers()
        {
            string json = JsonSerializer.Serialize(_customers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonPath, json);
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            SaveCustomers();
        }
    }
}