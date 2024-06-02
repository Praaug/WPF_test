using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BookLibrary.Models;
using Common;
using ServerCommunication.Interfaces;

namespace BookLibrary.ViewModels;

public class LibraryViewModel : ViewModelBase
{
    private ObservableCollection<BookViewModel> _books = new ObservableCollection<BookViewModel>();

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

    private bool CanDoRefreshBooks(object obj)
    {
        return true;
    }

    private void DoRefreshBooks(object obj)
    {
        var bookDtos = _serverConnector.GetBooks();
        ConvertBooks(bookDtos);
    }

    private void ConvertBooks(List<BookDto> source)
    {
        // load default Image
        BitmapImage? defaultImage = new BitmapImage(new Uri("/Images/Generic.png", UriKind.Relative));
        
        Books.Clear();
        
        foreach (var bookDto in source)
        {
            BitmapImage? coverImage = new BitmapImage(new Uri("/Images/" + bookDto.ImagePath, UriKind.Relative));

            Books.Add(new BookViewModel()
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Id = bookDto.Id,
                Image = coverImage,
            });
        }
    }
}