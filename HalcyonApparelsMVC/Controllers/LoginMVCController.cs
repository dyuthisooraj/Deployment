using Microsoft.AspNetCore.Mvc;
using HalcyonApparelsMVC.Models;

namespace HalcyonApparelsMVC.Controllers
{
    public class LoginMVCController : Controller
    {
        private readonly IConfiguration _config;

        public LoginMVCController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginMVC loginDetails)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_config["WebConfig:UserApi"]);
            var postTask = client.PostAsJsonAsync("api/Login", loginDetails);
            postTask.Wait();
            var Result = postTask.Result;
            if (!Result.IsSuccessStatusCode)
            {
                ViewData["LoginFlag"] = "Invalid Login";
                return View();
            }
            return RedirectToAction("AccessoryView", "Home");
        }


    }

    
}
