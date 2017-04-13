using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Weeblo.Models
{
    public class ProjectUser: IdentityUser
    {
        public string Name { get; set; }
    }
}