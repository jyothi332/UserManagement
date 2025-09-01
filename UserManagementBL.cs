using Dapper;
using EmployeeModelPackage;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace UserManagement.Models.Business_Layer
{
    public class UserManagementBL
    {
        private readonly string _connectionString;

        public UserManagementBL()
        {
            _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false).Build().GetSection("ConnectionStrings:EmployeePortal").Value;
        }

        public RegisterModel GetRegistrationDetails(string UserName, string Password)
        {
            try
            {
                var pass = string.Empty;

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(Password);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);
                    string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    pass = hashedPassword;
                }

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @$"select * from UserInformation where Username = '{UserName}' and Password = '{pass}'";
                    var employees = db.Query<RegisterModel>(query).FirstOrDefault();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching employee list: " + ex.Message);
                return new RegisterModel();
            }
        }
        public bool SaveEmployee(RegisterModel model)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string insertQuery = @"INSERT INTO UserInformation (UserName, Email, PhoneNumber,Password)
                                   VALUES (@UserName, @Email, @PhoneNumber,@Password)";

                    int rowsAffected = db.Execute(insertQuery, new
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Password = model.Password
                    });

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving employee: " + ex.Message);
                return false;
            }
        }
        public bool IsUserExists(string UserName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "SELECT COUNT(*) FROM UserInformation WHERE UserName = @UserName";
                    int count = db.QuerySingle<int>(query, new { UserName });
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


    }
}





