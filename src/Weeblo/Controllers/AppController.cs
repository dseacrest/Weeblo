using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weeblo.ViewModels;
using Weeblo.Services;
using Microsoft.Extensions.Configuration;
using Weeblo.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

//All controllers should derive/inherit from "Controller" (need to add Microsoft.AspNetCore.Mvc to project.json)
//Local variables are visible only in the method or block they are declared whereas instance variables can been seen by all methods in the class.
//Underscore variables "_var" is usually used to refer to private member fields

namespace Weeblo.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IProjectRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, ProjectContext context, IProjectRepository repository, ILogger<AppController> logger) //Must add ProjectContext for database
        {
            _mailService = mailService;
            _config = config;
            _repository = repository; //Must add context for database and create private ProjectContext _context; above
            _logger = logger;
        }
        
        //Action is a method that returns a view
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Project()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get projects in Index page: {ex.Message}");
                return Redirect("/error");
            }
        } 

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost] //Action Filter - If someone posts to content this action should be instantiated
        public IActionResult Contact(ContactViewModel model) // Data being passed into action represented as model
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support aol addresses");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From the World!", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
   
            return View();
            
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
