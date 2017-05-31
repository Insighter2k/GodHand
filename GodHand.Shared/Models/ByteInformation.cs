using System.ComponentModel;
using System.Runtime.CompilerServices;
using GodHand.Shared.Annotations;

namespace GodHand.Shared.Models
{
    public class ByteInformation : INotifyPropertyChanged
    {        
        public byte[] ByteValue { get; }
        public int ByteValueLength => ByteValue.Length;
        public int StartPosition { get; }
        public string CurrentValue { get; }

        private string _newValue;
        public string NewValue
        {
            get => _newValue;
            set
            {
                if (value.Length <= ByteValueLength)
                {
                    _newValue = value;
                    OnPropertyChanged(nameof(NewValueLength));
                    OnPropertyChanged(nameof(NewValue));
                }
            }
        }
        public int NewValueLength
        {
            get
            {
                if (NewValue != null) return NewValue.Length;
                return -1;
            }
        }

        public string RomajiTranslation { get; set; }

        public string EnglishTranslation { get; set; }

        private bool _hasChange;
        public bool HasChange
        {
            get => _hasChange;
            set
            {
                _hasChange = value;
                OnPropertyChanged(nameof(HasChange));
            }
        }

        public ByteInformation(byte[] byteValue, int startPosition, string currentValue)
        {
            ByteValue = byteValue;
            StartPosition = startPosition;
            CurrentValue = currentValue;
            NewValue = currentValue;
            HasChange = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
