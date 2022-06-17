using ASP_View.Models;
using BusinessLogic;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_View.Controllers
{
    public class HomeController : Controller
    {
        UserContainer userContainer = new UserContainer(new UserDAL());

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("UserID", 0);

            return View();
        }

        public IActionResult Privacy(UserModel userModel)
        {
            if((int)HttpContext.Session.GetInt32("UserID") == 0)
            {
                if (userContainer.inloggen(new User(0, userModel.UserName, userModel.Password)).ID > 0)
                {
                    userModel.Id = userContainer.GetUserByName(new User(0, userModel.UserName, userModel.Password)).ID;

                    HttpContext.Session.SetInt32("UserID", userModel.Id);

                    return View(userModel);
                }
                else { return RedirectToAction("Index", "Home"); }
            }

            else { return View(); }
        }

        public IActionResult CreateAccount(UserModel userModel)
        {
            if (userContainer.CheckUser(userModel.UserName))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                userContainer.AddUser(new User(0, userModel.UserName, userModel.Password));
                return RedirectToAction("Index", "Home");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}