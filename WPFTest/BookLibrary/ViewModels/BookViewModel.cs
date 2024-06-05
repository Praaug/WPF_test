using System;
using System.Data.Common;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookLibrary.ViewModels
{


    public class BookViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        private string _author;
        public string Author
        {
            get => _author;
            set => SetField(ref _author, value);
        }

        private BitmapImage? _image;
        public BitmapImage? Image
        {
            get => _image;
            set => SetField(ref _image, value);
        }

        public BookViewModel()
        {
            _title = String.Empty;
            _author = String.Empty;
            _image = new BitmapImage();
            _id = -1;
        }
    }
}