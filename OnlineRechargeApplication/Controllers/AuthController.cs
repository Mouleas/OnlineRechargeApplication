using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRechargeApplication.Data;
using OnlineRechargeApplication.Models;

namespace OnlineRechargeApplication.Controllers
{
    public class AuthController : Controller
    {

        private readonly OnlineRechargeApplicationContext _context;

        public AuthController(OnlineRechargeApplicationContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp() 
        {

            return View();
        }
        public ActionResult SignIn() 
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(IFormCollection obj)
        {
            try
            {
                CustomerModel model = new CustomerModel();
                model.CustomerName = obj["name"];
                model.CustomerEmail = obj["email"];
                model.CustomerPhone = obj["phonenumber"];
                model.CountryCode = obj["countrycode"];
                model.CustomerAddress = obj["address"];
                ServiceProviderModel service = new ServiceProviderModel();
                service.ServiceProviderName = obj["serviceprovider"];
                model.ServiceProvider = service;
                model.CustomerPassword = obj["password"];
                string confirmPassword = obj["confirmpassword"];
                if (model.CustomerPassword != confirmPassword)
                {
                    ViewData["err"] = "*Password is incorrect";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("SignIn", "Auth");
                }
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message;
            }
           
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(IFormCollection obj)
        {
            try
            {
                string email = obj["email"];
                CustomerModel? model = _context.CustomerModel.SingleOrDefault(user => user.CustomerEmail == email);
                if (model == null)
                {
                    ViewData["err"] = "*Email not registered";
                    return View();
                }
                else
                {
                    if (model.CustomerPassword != obj["password"])
                    {
                        ViewData["err"] = "*Password does not match";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message;
                return View();
            }
            
            return View();
        }
    }
}
