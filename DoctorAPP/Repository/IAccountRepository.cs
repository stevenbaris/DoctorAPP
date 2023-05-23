using DoctorAPP.ViewModel;

namespace DoctorAPP.Repository
{
    public interface IAccountRepository
    {
        Task<bool> SignUpUser(RegisterUserViewModel user);
        Task<string> SignInUser(LoginUserViewModel loginUserViewModel);
    }
}
