namespace TestKomgrip.Models
{
    public class LoginModel
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Position { get; set; }

        public DateTime? LastLogin { get; set; }
        public int? NameId { get; set; }
    }
}
