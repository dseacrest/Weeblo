using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeblo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        public ICollection<Story> Stories { get; set; } //ICollection allows you to add and remove to a list, IEnumerable only allows you to add to a list
    }
}
