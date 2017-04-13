using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weeblo.Models
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects();
        IEnumerable<Project> GetProjectsByUserName(string name);
        Project GetProjectByName(string projectName);
        Project GetUserProjectByName(string projectName, string username);

        void AddProject(Project project); //Adds project to database
        void AddStory(string projectName, Story newStory, string username);

        Task<bool> SaveChangesAsync(); //Saves changes to database

        //Must also implement these to repository

    }

}