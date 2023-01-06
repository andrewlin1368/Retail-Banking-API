using System;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Retail_Banking_API.Models
{
    public class CustomerRepo : ICustomerInterface
    {
        public (Customer?, string) AddCustomer(Customer customer)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://my.retailbanking.com/api/")
            };
            var response = client.PostAsJsonAsync("api/AddCustomerWihoutAuthorization", customer);
            var result = response.Result;
            StreamReader reader = new(result.Content.ReadAsStreamAsync().Result);
            if (result.IsSuccessStatusCode)
            {
                return (customer,"");
            }
            return (default, reader.ReadToEnd());
        }

        public (List<Customer>?,string) GetAllCustomers()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://my.retailbanking.com/api/")
            };
            var response = client.GetAsync("api/getallcustomers");
            var result = response.Result;
            StreamReader reader = new(result.Content.ReadAsStreamAsync().Result);
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<List<Customer>>();
                return (data.Result,"");
            }
            return (default,reader.ReadToEnd());
        }

        public (Customer?,string) GetCustomer(int cid)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://my.retailbanking.com/api/")
            };
            var response = client.GetAsync($"api/getcustomer/{cid}");
            var result = response.Result;
            StreamReader reader = new(result.Content.ReadAsStreamAsync().Result);
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Customer>();
                return (data.Result, "");
            }
            return (default,reader.ReadToEnd());
        }

        public (Customer?, string) GetCustomerBySSN(int ssnid)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://my.retailbanking.com/api/")
            };
            var response = client.GetAsync($"api/getcustomerbyssn/{ssnid}");
            var result = response.Result;
            StreamReader reader = new(result.Content.ReadAsStreamAsync().Result);
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Customer>();
                return (data.Result, "");
            }
            return (default, reader.ReadToEnd());
        }
    }
}
