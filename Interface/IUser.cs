using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace SDSI.Access.Interface
{
    public interface IUser  
    {

        public int insertUser(string firstName, string lastName, string email, string userName, string password, string phoneNumber = "");

        public int checkUser(string userName, string password);

        public string checkUser(string userName);
        

    }
}
