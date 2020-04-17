using Quizz.ServiceModels;
using System.Collections.ObjectModel;
using Quizz.ViewModels;
using System.Threading.Tasks;
using Quiz.Services;

namespace Quizz.ViewModel
{
    public class UsersViewModel : BaseViewModel
    {
        #region parameters

        private readonly UsersService _usersService;

        public ObservableCollection<UsersModel> Users { get; private set; }

        #endregion

        #region methods

        public UsersViewModel()
        {
            _usersService = new UsersService();
            Users = new ObservableCollection<UsersModel>();
            LoadUsers();

        }

        private async Task LoadUsers()
        {
            var users = await _usersService.GetUsers();

            foreach (var user in users)
                Users.Add(user);
        }

        #endregion
    }
}
