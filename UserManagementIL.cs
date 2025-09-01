using EmployeeModelPackage;

public interface UserManagementIL
{
    RegisterModel GetRegistrationDetails(string UserName, string Password);

    // Add this if missing:
    bool SaveEmployee(RegisterModel model);
    bool IsUserExists(string UserName);
}
