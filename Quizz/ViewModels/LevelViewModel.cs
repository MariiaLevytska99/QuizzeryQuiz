using System.Windows.Input;

namespace Quizz.ViewModels
{
    public class LevelViewModel : BaseViewModel
    {
        #region parameters

        public int _id { get; set; }
        public int _number { get; set; }
        public int _pointsToUnlock { get; set; }
        public int _score { get; set; }
        public bool _isBlock { get; set; }
        public string _image { get; set; }

        public ICommand ForceCloseCommand { get; set; }

        #endregion

        #region get set

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public int PointsToUnlock
        {
            get => _pointsToUnlock;
            set
            {
                _pointsToUnlock = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public bool IsBlock
        {
            get => _isBlock;
            set
            {
                _isBlock = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
