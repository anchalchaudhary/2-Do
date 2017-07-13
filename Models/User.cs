using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^[a-zA-Z0-9.#\s]{7,15}$", ErrorMessage = "Password length must be between 8 to 15. Characters allowed: a-z A-Z 0-9 . #")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Re-Enter Password")]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public Nullable<int> Activated { get; set; }
    }
}