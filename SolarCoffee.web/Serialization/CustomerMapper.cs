using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public class CustomerMapper
    {

        // Serializes a customer data model to a CustomerVM
        public static CustomerViewModel SerializeCustomer(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        //Serializes a CustomerVM to a customer data model
        public static Customer SerializeCustomer(CustomerViewModel customer)
        {
            return new Customer
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        //Maps a customeraddress data model to a customeraddressVM
        public static CustomerAddressViewModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressViewModel
            {
                Id = address.Id,
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }

        //Maps a customeraddressVM to customeraddress data model
        public static CustomerAddress MapCustomerAddress(CustomerAddressViewModel address)
        {
            return new CustomerAddress
            {
                CreatedOn = address.CreatedOn,
                UpdatedOn = address.UpdatedOn,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }
    }
}
