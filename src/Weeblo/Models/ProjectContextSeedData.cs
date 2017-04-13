using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeblo.Models
{
    public class ProjectContextSeedData
    {
        private ProjectContext _context;
        private UserManager<ProjectUser> _userManager;

        public ProjectContextSeedData(ProjectContext context, UserManager<ProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {

            try
            {
                if (await _userManager.FindByEmailAsync("devon@scoutsheet.com") == null)
                {
                    var user = new ProjectUser()
                    {
                        UserName = "devon",
                        Email = "devon@scoutsheet.com"
                    };

                    await _userManager.CreateAsync(user, "Password$1");
                }

                if (!_context.Stories.Any())
                {

                    //Seeding data

                    var firstProject = new Project()
                    {
                        DateCreated = DateTime.UtcNow,
                        Name = "Finish Chrome",
                        UserName = "devon",
                        Stories = new List<Story>()
                    {
                        new Story() { Name = "A", Status="Good", AssignedTo="Devon", Description="Fixit" },
                        new Story() { Name = "B", Status="Good", AssignedTo="Spencer", Description="Fixit" },
                        new Story() { Name = "C", Status="Good", AssignedTo="Devon", Description="Fixit" },
                        new Story() { Name = "D", Status="Bad", AssignedTo="Devon", Description="Fixit" },
                        new Story() { Name = "E", Status="Good", AssignedTo="Jason", Description="Fixit" },
                        new Story() { Name = "F", Status="Good", AssignedTo="Devon", Description="Fixit" }
                    }

                    };

                    _context.Projects.Add(firstProject);

                    _context.Stories.AddRange(firstProject.Stories);


                    var secondProject = new Project()
                    {
                        DateCreated = DateTime.UtcNow,
                        Name = "Finish Spreadsheet",
                        UserName = "devon",
                        Stories = new List<Story>()
                    {
                        new Story() { Name = "G", Status="Good", AssignedTo="Devon", Description="Fixit" },
                        new Story() { Name = "H", Status="Good", AssignedTo="Spencer", Description="Fixit" },
                        new Story() { Name = "I", Status="Good", AssignedTo="Devon", Description="Fixit" },
                        new Story() {  Name = "J", Status="Bad", AssignedTo="Devon", Description="Fixit" },
                        new Story() {  Name = "K", Status="Good", AssignedTo="Jason", Description="Fixit" },
                        new Story() {  Name = "L", Status="Good", AssignedTo="Devon", Description="Fixit" }
                    }

                    };

                    _context.Projects.Add(secondProject);

                    _context.Stories.AddRange(secondProject.Stories);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {

            }

        }
    }
}

