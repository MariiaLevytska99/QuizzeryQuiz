using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Quizz.Services;
using Quizz.ViewModels;
using Quizz.Views;

namespace Quizz.ViewModel
{
    public class CategoryLevelsViewModel : BaseViewModel
    {
        #region parameters
        private readonly LevelService _levelService;
        public int categoryId { get; set; }
        public ObservableCollection<LevelViewModel> _levels;

        public INavigation Navigation { get; set; }
        public ObservableCollection<LevelViewModel> Levels
        {
            get { return _levels; }
            set
            {
                _levels = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region comands

        private ICommand _playCommand;

        public ICommand PlayCommand => _playCommand ?? (_playCommand = new Command(async level =>
        {
            var model = level as LevelViewModel;
            if (!model.IsBlock)
            {
                _levelService.StartLevel(model.Id);
                this.Navigation.PushAsync(new Level(model.Id));
            }

        }));

        #endregion

        public CategoryLevelsViewModel(int categoryIdParam)
        {
            categoryId = categoryIdParam;
            _levelService = new LevelService();
            LoadLevels();
        }


        #region methods

        private async Task LoadLevels()
        {
            var levels = new List<LevelViewModel>();
            var result = await _levelService.Get(categoryId);
            foreach (var level in result)
            {
                levels.Add(new LevelViewModel
                {
                    Id = level.Id,
                    Number = level.LevelNumber,
                    Score = level.Score,
                    PointsToUnlock = level.PointsToUnlock,
                    IsBlock = level.IsBlock
                });
            }

            Levels = new ObservableCollection<LevelViewModel>(levels);
        }
        #endregion
    }
}
