using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    public class E1Signal : IE1Signal
    {
        private int _acg;
        private int _ber;
        private bool _lock;
        private int _snr;
        private bool _sync;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Snr
        {
            get { return _snr; }
            set
            {
                if (value == _snr) return;
                _snr = value;
                OnPropertyChanged();
                OnPropertyChanged("CalculatedDb");
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

        public bool Lock
        {
            get { return _lock; }
            set
            {
                if (value.Equals(_lock)) return;
                _lock = value;
                OnPropertyChanged();
            }
        }

        public bool Sync
        {
            get { return _sync; }
            set
            {
                if (value.Equals(_sync)) return;
                _sync = value;
                OnPropertyChanged();
            }
        }

        public decimal CalculatedDb
        {
            get { return Math.Round(Snr/6.5M, 2); }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}