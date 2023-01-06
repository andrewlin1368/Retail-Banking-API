using Microsoft.AspNetCore.Mvc;
using Retail_Banking_API.Models;

namespace Retail_Banking_API.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerInterface customerInterface;
        public CustomerController(ICustomerInterface customerInterface)
        {
            this.customerInterface = customerInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllCustomers()
        {
            (List<Customer> ?, string) data= customerInterface.GetAllCustomers();
            if (data.Item1 == default)
            {
                if (!data.Item2.Contains("<!DOCTYPE html>")) TempData["Response"] = data.Item2;
                else TempData["Response"] = "Error @ my.retailbanking.com.";
            }
            return View(data.Item1);
        }

        public IActionResult GetCustomer()
        {
            return View();
        }

        public IActionResult ShowCustomer(int cid, bool ssn = false, int ssnid = 0)
        {
            if (ssn)
            {
                (Customer?, string) ssndata = customerInterface.GetCustomerBySSN(ssnid);
                if (ssndata.Item2.Contains("<!DOCTYPE html>"))
                {
                    TempData["Response"] = "Error @ my.retailbanking.com.";
                    return RedirectToAction("GetCustomer");
                }
                return View(ssndata.Item1);
            }
            (Customer ?,string) data = customerInterface.GetCustomer(cid);
            if (data.Item1 == default)
            {
                if(!data.Item2.Contains("<!DOCTYPE html>")) TempData["Response"] = data.Item2;
                else TempData["Response"] = "Error @ my.retailbanking.com.";
                return RedirectToAction("GetCustomer");
            }
            return View(data.Item1);
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                (Customer?, string) data = customerInterface.AddCustomer(customer);
                if(data.Item1 != default)
                    return RedirectToAction("ShowCustomer", new {cid = 0, ssn = true, ssnid = data.Item1.SSNID});
                else
                {
                    if(data.Item2.Contains("<!DOCTYPE html>")) TempData["Response"] = "Error @ my.retailbanking.com.";
                    else TempData["Response"] = data.Item2;
                }
                return View(customer);
            }
            else
            {
                return View(customer);
            }
        }
    }
}
