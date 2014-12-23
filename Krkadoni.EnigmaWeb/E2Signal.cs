using System.ComponentModel;
using System.Runtime.CompilerServices;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    internal class E2Signal : IE2Signal
    {
        private int _acg;
        private int _ber;
        private decimal _db;
        private int _snr;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Snr
        {
            get { return _snr; }
            set
            {
                if (value == _snr) return;
                _snr = value;
                OnPropertyChanged();
            }
        }

        public int Acg
        {
            get { return _acg; }
            set
            {
                if (value == _acg) return;
                _acg = value;
                OnPropertyChanged();
            }
        }

        public int Ber
        {
            get { return _ber; }
            set
            {
                if (value == _ber) return;
                _ber = value;
                OnPropertyChanged();
            }
        }

        public decimal Db
        {
            get { return _db; }
            set
            {
                if (value == _db) return;
                _db = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}