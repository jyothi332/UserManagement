

using UserManagement.Models.Business_Layer;
using EmployeeModelPackage;

namespace UserManagement.Models.Application_Layer
{
    public class UserManagementAL : UserManagementIL
    {
        private readonly UserManagementBL _bl;

        public UserManagementAL()
        {
            _bl = new UserManagementBL();
        }

        public RegisterModel GetRegistrationDetails(string UserName, string Password)
        {
            return _bl.GetRegistrationDetails(UserName, Password);
        }

        public bool SaveEmployee(RegisterModel model) // <== This method must exist
        {
            return _bl.SaveEmployee(model);
        }
        public bool IsUserExists(string UserName)
        {
            return _bl.IsUserExists(UserName);
        }
    }
}
