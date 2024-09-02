using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Myfirstmvcapplication.Models
{
    public class SignUpModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastNmae { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }    
    }
}