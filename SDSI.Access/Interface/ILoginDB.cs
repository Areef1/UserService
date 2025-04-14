namespace SDSI.Access.Interface
{
    public interface ILoginDB
    {
        public bool Login(string userName, string password);
        public bool Signup(string firstName, string lastName, string email, string userName, string password, string phoneNumber = "");
        public bool ForgetPasswrd(string userName);

    }
}
