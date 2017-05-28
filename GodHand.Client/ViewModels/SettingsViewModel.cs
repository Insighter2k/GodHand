using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace GodHand.Client.ViewModels
{
    public class SettingsViewModel : Screen, IFlyout
    {
        protected SettingsViewModel()
        {
            DisplayName = "Settings";
            Header = DisplayName;
            Position = Position.Right;
        }

        public static Screen Create() => new SettingsViewModel();

        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                if (value == _header)
                {
                    return;
                }

                _header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (value.Equals(_isOpen))
                {
                    return;
                }

                _isOpen = value;
                NotifyOfPropertyChange(() => IsOpen);
            }
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set
            {
                if (value == _position)
                {
                    return;
                }

                _position = value;
                NotifyOfPropertyChange(() => Position);
            }
        }


      
    }
}
