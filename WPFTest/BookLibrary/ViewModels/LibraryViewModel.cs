using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookLibrary.ViewModels
{
    /// <summary>
    /// Handles all Libary operation like add/remove, checkout/return and show details
    /// </summary>

    public class LibraryViewModel : ViewModelBase
    {
        private ObservableCollection<BookViewModel> _books = new ObservableCollection<BookViewModel>();

        private BookViewModel _currSelectedBook;
        private BookDetailsViewModel _currSelectedBookDetails;

        public BookViewModel SelectedBook
        {
            get => _currSelectedBook;
            set 
            { 
                SetField(ref _currSelectedBook, value);
            }
        }

        public ObservableCollection<BookViewModel> Books
        {
            get => _books;
            set => SetField(ref _books, value);
        }

        public LibraryViewModel() : base()
        {
            DoRefreshBooks(null);
        }

        public ICommand RefreshBooksCommand => new DelegateCommand(DoRefreshBooks, CanDoRefreshBooks);

        public ICommand RemoveBookCommand => new DelegateCommand(RemoveBook, CanIsBookSelected);
        public ICommand AddBookCommand => new DelegateCommand(AddBook, CanAddBook);

        public ICommand ShowDetailsCommand => new DelegateCommand(ShowBookDetail, CanIsBookSelected);

        public ICommand CheckoutBookCommand => new DelegateCommand(CheckoutBook, CanIsBookSelected);
        public ICommand ReturnBookCommand => new DelegateCommand(ReturnBook, CanIsBookSelected);

        private void CheckoutBook(object obj)
        {
            bool checkoutBool = _serverConnector.CheckoutBook(_currSelectedBook.Id);
            if (!checkoutBool)
            {
                MessageBox.Show("A book checkout error occured!", "Book Checkout Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else
            {
                MessageBox.Show("A book has been checkouted!", "Book Checkout Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReturnBook(object obj)
        {
            bool returnBool = _serverConnector.ReturnBook(_currSelectedBook.Id);
            if (!returnBool)
            {
                MessageBox.Show("A book return error occured!", "Book Return Info", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("A book has been returned!", "Book Return Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanDoRefreshBooks(object obj)
        {
            return true;
        }

        private void DoRefreshBooks(object obj)
        {
            var bookDtos = _serverConnector.GetBooks();
            ConvertBooks(bookDtos);
        }
        private void ShowBookDetail(object obj)
        {
            if (_currSelectedBook == null) return;
            _currSelectedBookDetails = new BookDetailsViewModel(_currSelectedBook, _serverConnector.GetBookDetails(_currSelectedBook.Id));
        }

        private bool CanAddBook(object obj)
        {
            return true;
        }

        private void RemoveBook(object obj)
        {
            if (_currSelectedBook == null) return;

            _serverConnector.RemoveBook(_currSelectedBook.Id);
            DoRefreshBooks(obj);
        }

        private bool CanIsBookSelected(object obj)
        {
            return true;
        }

        private void ConvertBooks(List<BookDto> source)
        {
            // load default Image
            BitmapImage? defaultImage = new BitmapImage(new Uri("/Images/Generic.png", UriKind.Relative));

            Books.Clear();

            foreach (var bookDto in source)
            {
                BitmapImage? coverImage = new BitmapImage(new Uri("/Images/" + bookDto.ImagePath, UriKind.Relative));
                string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string filePath = Path.Combine(directory, "/Images/" + bookDto.ImagePath);
                bool doesExist = File.Exists(directory + filePath);

                Books.Add(new BookViewModel()
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Id = bookDto.Id,
                    Image = doesExist ? coverImage : defaultImage
                });
            }
        }

        private bool IsValidUri(String uri)
        {
            try
            {
                new Uri(uri);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void AddBook(object obj)
        {
            BookDetailsDto dummy = new BookDetailsDto()
            {
                Id = 0,
                Author = "TestAuthor",
                Title = "TestTitle",
                ImagePath = "Generic.jpg",
            };
            _serverConnector.AddNewBook(dummy);

            DoRefreshBooks(obj);
        }

    }
}