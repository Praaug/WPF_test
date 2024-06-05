using BookLibrary.Views;
using Common;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookLibrary.ViewModels
{
    /// <summary>
    /// Handles logic and visuals for displaying Book Detail Information
    /// </summary>
    public class BookDetailsViewModel : ViewModelBase
    {
        private Window windowInstance;

        #region Fields
        private string _author;
        public string Author
        {
            get => _author;
            set => SetField(ref _author, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set => SetField(ref _publisher, value);
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set => SetField(ref _releaseDate, value);
        }

        private double _price;
        public double Price
        {
            get => _price;
            set => SetField(ref _price, value);
        }

        private int _pageCount;
        public int PageCount
        {
            get => _pageCount;
            set => SetField(ref _pageCount, value);
        }

        private string _language;
        public string Language
        {
            get => _language;
            set => SetField(ref _language, value);
        }

        private string _edition;
        public string Edition
        {
            get => _edition;
            set => SetField(ref _edition, value);
        }

        private BitmapImage? _image;
        public BitmapImage? Image
        {
            get => _image;
            set => SetField(ref _image, value);
        }

        private string _isbn;
        public string ISBN
        {
            get => _isbn;
            set => SetField(ref _isbn, value);
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set => SetField(ref _genre, value);
        }
        #endregion

        public ICommand CloseCommand => new DelegateCommand(Close, CanClose);

        /// <summary>
        /// Command binding method for determining if the window instance can be closed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanClose(object obj)
        {
            return true;
        }
        /// <summary>
        /// Command binding method for closing the window instance
        /// </summary>
        /// <param name="obj"></param>

        private void Close(object obj)
        {
            windowInstance.Owner.Activate();
            windowInstance.Hide();
        }

        /// <summary>
        /// Constructor that receives all required information for initialization and displays the view
        /// </summary>
        /// <param name="bookViewModel"></param>
        /// <param name="bookDetailsDto"></param>
        public BookDetailsViewModel(BookViewModel bookViewModel, BookDetailsDto bookDetailsDto)
        {
            _author = bookViewModel.Author;
            _title = bookViewModel.Title;
            _image = bookViewModel.Image != null ? bookViewModel.Image : new BitmapImage(new Uri("/Images/Generic.png", UriKind.Relative));

            _publisher = bookDetailsDto.Publisher != null ? bookDetailsDto.Publisher : "-";
            _releaseDate = bookDetailsDto.ReleaseDate != null ? bookDetailsDto.ReleaseDate : default;
            _price = bookDetailsDto.Price;
            _pageCount = bookDetailsDto.PageCount;
            _language = bookDetailsDto.Language;
            _edition = bookDetailsDto.Edition;
            _isbn = bookDetailsDto.ISBN;
            _genre = bookDetailsDto.Genre;

            ShowWindow();
        }

        /// <summary>
        /// Method for displaying the view as a window
        /// </summary>
        public void ShowWindow()
        {
            windowInstance = new Window
            {
                Title = "Book Details View",
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                Content = new BookDetailsView(this),
            };
            windowInstance.ShowDialog();
        }

    }
}
