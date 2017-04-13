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
    [Authorize]
    [Route("/api/project/{projectName}/stories")]
    public class StoriesController : Controller
    {
        private ILogger<StoriesController> _logger;
        private IProjectRepository _repository;

        public StoriesController(IProjectRepository repository, ILogger<StoriesController> logger)
        {
            _repository = repository;
            _logger = logger;

        }

        //Sample Get
        //http://localhost:50245/api/project/new%20repository/stories
        [HttpGet("")]
        public IActionResult Get(string projectName)
        {
            try
            { 
            var project = _repository.GetUserProjectByName(projectName, User.Identity.Name);

            return Ok(Mapper.Map<IEnumerable<StoryViewModel>>(project.Stories.OrderBy(s => s.Name).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stories.");
            }

            return BadRequest("Failed to get stops.");
        }

        //Sample Post
        //http://localhost:50245/api/project/new%20repository/stories
        //Body includes class variables in json
        //[
        //  {
        //    "name": "AA",
        //    "description": "Drag and drop feature",
        //    "status": "Planned",
        //    "assignedTo": "jeff"
        //  }
        //]
        [HttpPost("")]
        public async Task<IActionResult> Post(string projectName, [FromBody]StoryViewModel vm) //Add task when doing anything asycn
        {
            try
            {
                //If vm valid
                if (ModelState.IsValid)  // Checks to see if the data types and annotations/requirement are met
                {
                    var newStory = Mapper.Map<Story>(vm);
                

                    //Lookup geocode

                    //Save to database

                    _repository.AddStory(projectName, newStory, User.Identity.Name);

                    if(await _repository.SaveChangesAsync())
                    {
                    return Created($"/api/project/{projectName}/stories/{newStory}",
                        Mapper.Map<StoryViewModel>(newStory));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new story: {0}", ex);
            }

            return BadRequest("Failed to save new story2. ");

        }

    }
}
