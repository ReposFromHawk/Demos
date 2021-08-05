using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(500)]
        public string JwtToken { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}
