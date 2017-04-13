using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Weeblo.Models
{
    public class ProjectContext : IdentityDbContext<ProjectUser> //Sets context for database (DbContext), must add package for Microsoft.EntityFrameworkCore  //To store Authentication must use IdentityDbContext
    {
        private IConfigurationRoot _config;

        public ProjectContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;

        }

        public DbSet<Project> Projects { get; set; } //Gives us a class that we can execute Linq queries against
        public DbSet<Story> Stories { get; set; }

        //Must also add to configure services in startup.cs file
        //Must also add to AppController in AppController.cs

        //must override onconfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:ProjectContextConnection"]);  //Pass through connection string
        }


    }
}
