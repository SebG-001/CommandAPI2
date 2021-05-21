using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos
{
    public class CommandUpdateDto
    {
        [MaxLength(250)]
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}
