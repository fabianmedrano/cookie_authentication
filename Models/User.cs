using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cookie_authentication.Models
{
    public class User
    {

        public int Id { get; set; }

        [Required, Column(TypeName = "varchar(200)")]
        public string Name { get; set; } = "";

        [Required,Column(TypeName = "varchar(200)")]
        public string Email { get; set; } = "";

        [Required,Column(TypeName = "varchar(200)")]
        public string Password { get; set; } = "";

        [Required,Column(TypeName = "varchar(200)")]
        public string Username { get; set; } = "";



        public ICollection<UserRole> UserRoles { get; set; } =default!;
    }
}
