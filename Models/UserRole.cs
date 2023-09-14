namespace cookie_authentication.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
