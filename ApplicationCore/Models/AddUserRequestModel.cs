using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class AddUserRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Password must be between 6 characters and 10 characters long", MinimumLength = 6)]
        //[RegularExpression("^ (?=.*[a - z])(?=.*[A - Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = 
        //    "Password should have minimum 6 with at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }
        public DateTime? JoinedOn { get; set; }
    }
}
