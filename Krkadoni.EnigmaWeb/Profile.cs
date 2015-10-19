using System.ComponentModel;
using System.Runtime.CompilerServices;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    public class Profile : IProfile, INotifyPropertyChanged
    {
        private string _address;
        private EnigmaType _enigma;
        private int _httpPort;
        private string _name;
        private string _password;
        private bool _useSsl;
        private string _username;
        private int _streamingPort;

        public Profile()
        {
            _name = string.Empty;
            _username = "root";
            _password = "dreambox";
            _address = "192.168.1.1";
            _enigma = EnigmaType.Enigma2;
            _httpPort = 80;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public EnigmaType Enigma
        {
            get { return _enigma; }
            set
            {
                if (value == _enigma) return;
                _enigma = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public int HttpPort
        {
            get { return _httpPort; }
            set
            {
                if (value == _httpPort) return;
                _httpPort = value;
                OnPropertyChanged();
            }
        }

        public bool UseSsl
        {
            get { return _useSsl; }
            set
            {
                if (value.Equals(_useSsl)) return;
                _useSsl = value;
                OnPropertyChanged();
            }
        }

        public int StreamingPort
        {
            get { return _streamingPort; }
            set
            {
                if (value.Equals(_streamingPort)) return;
                _streamingPort = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}