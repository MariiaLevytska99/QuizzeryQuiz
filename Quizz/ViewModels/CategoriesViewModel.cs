using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using Quizz.Models;
using Quizz.Services;

namespace Quizz.ViewModel
{
    public class CategoriesViewModel : BindableObject
    {

        #region parameters
        private ObservableCollection<Category> _categories;
        private readonly CategoriesService _categoriesService;
        private readonly LevelService _levelService;
        private Category _currentCategory;

        public CategoriesViewModel()
        {
            _categoriesService = new CategoriesService();
            _levelService = new LevelService();
            LoadCategories();
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;

                OnPropertyChanged();
            }
        }

        #endregion

        #region methods

        private async Task LoadCategories()
        {
            var categories = new List<Category>();
            var result = await _categoriesService.Get();
            foreach (var category in result)
            {
                categories.Add(new Category
                {
                    Id = category.Id,
                    Title = category.Category,
                    Image = "category_" + category.Id + ".jpg"
                });
            }

            Categories = new ObservableCollection<Category>(categories);
            CurrentCategory = Categories[0];
        }

        #endregion
    }
}
