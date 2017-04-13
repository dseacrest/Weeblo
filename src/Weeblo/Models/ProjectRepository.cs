using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeblo.Models
{
    public class ProjectRepository: IProjectRepository
    {
        private ProjectContext _context;
        private ILogger<ProjectRepository> _logger;

        public ProjectRepository(ProjectContext context, ILogger<ProjectRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddProject(Project project)
        {
            _context.Add(project);
        }

        public void AddStory(string projectName, Story newStory, string username)
        {
            var project = GetUserProjectByName(projectName, username);

            if (project != null)
            {
                project.Stories.Add(newStory);
                _context.Stories.Add(newStory);
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            _logger.LogInformation("Getting all projects from database.");
            return _context.Projects.ToList();
        }

        public Project GetProjectByName(string projectName)
        {
            return _context.Projects 
               .Include(t=> t.Stories)
               .Where(t => t.Name == projectName) //lambda expression
               .FirstOrDefault();
        }

        public IEnumerable<Project> GetProjectsByUserName(string name)
        {
            return _context
                .Projects
                .Include(t=> t.Stories)
                .Where(t => t.UserName == name)
                .ToList();
        }

        public Project GetUserProjectByName(string projectName, string username)
        {
            return _context
                .Projects
                .Include(t => t.Stories)
                .Where(t => t.Name == projectName && t.UserName == username)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;  //Awaits operation and then compares result
        }

    }
}
