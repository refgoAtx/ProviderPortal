using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Model
{
    public class Members
    {

        [Key]
        public int Id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [ForeignKey("RoleId")]
        public virtual string? RoleID { get; set; }
        public virtual IdentityRole? IdentityRole { get; set; }
    }

    public class Root
    {
        public string PID { get; set; }
        public string Rename { get; set; }
        public string Create { get; set; }
        public string Read { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
    public class Rootstring
    {
        public string artist { get; set; }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
