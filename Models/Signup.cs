namespace SDSI.Access.Models
{
    public class Signup
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; } = string.Empty;
    }
}
