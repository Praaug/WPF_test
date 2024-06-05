using System.Windows.Controls;
using BookLibrary.ViewModels;

namespace BookLibrary.Views
{


    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
            DataContext = new LibraryViewModel();
        }
    }
}