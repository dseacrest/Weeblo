using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeblo.ViewModels
{
    // This view model is setup to create separate validation for ProjectClass
    public class ProjectViewModel
    {
        [Required]
        [StringLength(100, MinimumLength =5)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
