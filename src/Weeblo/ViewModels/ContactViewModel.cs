using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeblo.ViewModels
{
    public class ContactViewModel
    {

        [Required] // Validation
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(4096,MinimumLength =10)]
        public string Message { get; set; }
    }
}
