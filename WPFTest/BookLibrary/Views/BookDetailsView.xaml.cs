using BookLibrary.ViewModels;
using System.Windows.Controls;

namespace BookLibrary.Views
{
    /// <summary>
    /// Interaktionslogik für BookDetailsView.xaml
    /// </summary>
    public partial class BookDetailsView : UserControl
    {
        public BookDetailsView(BookDetailsViewModel bookDetailsViewModel)
        {
            InitializeComponent();

            DataContext = bookDetailsViewModel;
        }
    }
}
