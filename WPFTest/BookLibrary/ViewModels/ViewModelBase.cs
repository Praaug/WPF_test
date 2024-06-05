using ServerCommunication.Classes;
using ServerCommunication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookLibrary.ViewModels
{

    /// <summary>
    /// Base class for all ViewModels
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected IServerConnector _serverConnector = new ServerConnector();
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected Image? LoadImage(string path)
        {
            try
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage(new Uri("pack://application:,,,Resources/Images/" + path));
                image.Source = bi;

                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}