using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRechargeApplication.Data;
using OnlineRechargeApplication.Models;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult> SignUp() 
        {
            return _context.ServiceProviderModel != null ?
                         View(await _context.ServiceProviderModel.ToListAsync()) :
                         Problem("Entity set 'OnlineRechargeApplicationContext.ServiceProviderModel'  is null.");
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
                int ServiceProviderId = Convert.ToInt32(obj["serviceprovider"]);
                
                var Service = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == ServiceProviderId);

                model.ServiceProvider = Service;
                Console.WriteLine(model.ServiceProvider.ServiceProviderId);
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
                CustomerModel? customerModel = _context.CustomerModel.SingleOrDefault(user => user.CustomerEmail == email);
                AdminModel? adminModel = _context.AdminModel.SingleOrDefault(user => user.AdminName == email);
                if (adminModel != null) 
                {
                    return RedirectToAction("AdminOps");
                }
                if (customerModel == null)
                {
                    ViewData["err"] = "*Email not registered";
                    return View();
                }
                else
                {
                    if (customerModel.CustomerPassword != obj["password"])
                    {
                        ViewData["err"] = "*Password does not match";
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("CustomerPage", new {email=email} );
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["err"] = ex.Message;
                return View();
            }
        }

        public ActionResult ForgotPassword(IFormCollection obj)
        {
            Console.WriteLine(obj["email"]);
            return View();
        }

        public ActionResult AdminOps()
        {
            return View();
        }
        public async Task<ActionResult> CustomerPage(string email)
        {
            var customerModel = await _context.CustomerModel.Include(x => x.ServiceProvider).FirstOrDefaultAsync(m => m.CustomerEmail == email);

            if (customerModel != null)
            {
                var plans = await _context.PlanModel.Include(x => x.ServiceProvider).Where(m => m.ServiceProvider.ServiceProviderId == customerModel.ServiceProvider.ServiceProviderId).ToListAsync();
                ViewBag.planModel = plans;
            }

            List<SelectedPlanModel> plansSelected = await _context.SelectedPlanModel.Include(x => x.plan).Include(m => m.customer).Where(n => n.customer.CustomerId == customerModel.CustomerId).ToListAsync();

            List<int> selectedPlans = plansSelected.Select(x => x.plan.PlanId).ToList();

            ViewData["email"] = email;
            ViewData["id"] = customerModel.CustomerId;
            ViewData["name"] = customerModel.CustomerName;
            ViewBag.SelectedPlans = selectedPlans;
            return View();
        }

        public async Task<ActionResult> PushPlan(int id, int cid, string email)
        {
            var customerModel = await _context.CustomerModel.Include(x => x.ServiceProvider).FirstOrDefaultAsync(m => m.CustomerId == cid);
            var planModel = await _context.PlanModel.Include(x => x.ServiceProvider).FirstOrDefaultAsync(m => m.PlanId == id);


            SelectedPlanModel model = new SelectedPlanModel();

            model.plan = planModel;
            model.customer = customerModel;
            //model.PlanId = id;
            //model.CustomerId = cid;
            _context.Add(model);
            await _context.SaveChangesAsync();
            //fromPushPlan = true;
            return RedirectToAction("CustomerPage", new { email = email });
        }
    }
}
