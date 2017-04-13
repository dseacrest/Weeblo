using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeblo.Models;
using Weeblo.ViewModels;

namespace Weeblo.Controllers.API
{
    [Route("api/project")] //Sets base route for api
    [Authorize]
    public class ProjectController : Controller 
    {
        private ILogger<ProjectController> _logger;
        private IProjectRepository _repository;

        public ProjectController(IProjectRepository repository, ILogger<ProjectController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        //Sample Get
        //http://localhost:50245/api/project/
        [HttpGet("")] //If empty takes base route for api from above "api/projects", if you add data here it ammends to base route
        public IActionResult Get() //Can also use JsonResult and return Json
        {
            try
            {
                var results = _repository.GetProjectsByUserName(this.User.Identity.Name);
                //  if (true) return BadRequest("Bad things happened"); //400 Result is bad request

                return Ok(Mapper.Map<IEnumerable<ProjectViewModel>>(results)); //200 Result (Ok) is resposnse when success happens, it will return a list so you must call it IEnumerable
            }
            catch (Exception ex)
            {
                //Log error
                _logger.LogError($"Failed to get all projects: {ex}"); //Put $ ahead of string when you insert dynamic variables like {ex}

                return BadRequest("Error occured");
            }

        }

        //Sample Post
        //http://localhost:50245/api/project/
        //Body includes class variables in json
        //[
        //  {
        //    "name": "AA",
        //    "datecreated": "02/16/2017",
        //    "username": "devon"
        //  }
        //]
        [HttpPost("")] //Api post
        public async Task<IActionResult> Post([FromBody]ProjectViewModel theProject)  //Must tell it where you are getting data "[FromBody]", set a separate view model class for projects to have separate validation requirements
       { 

            if (ModelState.IsValid)
            {

                var newProject = Mapper.Map<Project>(theProject); // Save to Database using AutoMapper - Nuget package that allows us to save things to the database
                                                                  //Need to map the class in startup.cs

                newProject.UserName = User.Identity.Name;

                _repository.AddProject(newProject);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/project/{theProject.Name}", Mapper.Map<ProjectViewModel>(newProject));
                }
                else
                {
                    return BadRequest("Failed to save changes to database");
                }

            }
            return BadRequest(ModelState); //Don't do for public api
        }
    }
}
