using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Resources
{
    public class SaveFeedbackResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }
        public bool ContactMe { get; set; }
    }
}
