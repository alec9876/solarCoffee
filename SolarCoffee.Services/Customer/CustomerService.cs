using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDBContext _context;
        public CustomerService(SolarDBContext context)
        {
            _context = context;
        }
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "New Customer Added",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Message = $"New Customer Added: {ex.Message} - {ex.StackTrace} ",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            var now = DateTime.UtcNow;

            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Customer not found.",
                    Time = now,
                    Data = false
                };
            }

            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Message = $"Customer Created",
                    Time = now,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"{ex.StackTrace}",
                    Time = now,
                    Data = false
                };
            }
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _context.Customers
                .Include(c => c.PrimaryAdress)
                .OrderBy(c => c.LastName)
                .ToList();
        }

        public Data.Models.Customer GetById(int id)
        {
            return _context.Customers.Include(c => c.PrimaryAdress).FirstOrDefault(c => c.Id == id);
        }
    }
}
