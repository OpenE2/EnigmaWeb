using System.ComponentModel;
using System.Runtime.CompilerServices;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    public class BouquetItemService : IBouquetItemService, INotifyPropertyChanged
    {
        private string _name;
        private string _reference;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Reference
        {
            get { return _reference; }
            set
            {
                if (value == _reference) return;
                _reference = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}