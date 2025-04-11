using SDSI.Access.Interface;
using System.Data.SqlClient;

namespace SDSI.Access.DBconnections
{
    public class User : IUser
    {
       
            public int insertUser(string firstName, string lastName, string email, string userName, string password, string phoneNumber = "")
            {
            string query = "INSERT INTO Users (FirstName, LastName, Email, UserName, Password, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @UserName, @Password, @PhoneNumber)";
            // Insert user into database
            var cnnection = new SqlConnection("Server=APH10112\\SQLEXPRESS;Database=Accessservice;Trusted_Connection=True;");
            cnnection.Open();
            try
            {
                var command = new SqlCommand(query, cnnection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.ExecuteNonQuery();
                return 200;
            }
            catch (SqlException e)
            {
                return e.Number;
            }
            finally
            {
                cnnection.Close();
            }
        }
       public int checkUser(string userName, string password)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
            // Check if user exists in database
            var cnnection = new SqlConnection("Server=APH10112\\SQLEXPRESS;Database=Accessservice;Trusted_Connection=True;");
            cnnection.Open();
            try
            {
                var command = new SqlCommand(query, cnnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    return 200;
                }
                else
                {
                    return 404;
                }
            }
            catch (SqlException e)
            {
                return e.Number;
            }
            finally
            {
                cnnection.Close();
            }
        }

        public string checkUser(string userName)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName";
            // Check if user exists in database
            var cnnection = new SqlConnection("Server=APH10112\\SQLEXPRESS;Database=Accessservice;Trusted_Connection=True;");
            cnnection.Open();
            try
            {
                var command = new SqlCommand(query, cnnection);
                command.Parameters.AddWithValue("@UserName", userName);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader["Password"] as string;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException e)
            {
                return e.ToString();
            }
            finally
            {
                cnnection.Close();
            }
        }

    }
}
