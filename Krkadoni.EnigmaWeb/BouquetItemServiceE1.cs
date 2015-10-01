using System.ComponentModel;
using System.Runtime.CompilerServices;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    public class BouquetItemServiceE1 : BouquetItemService, IBouquetItemServiceE1
    {
        private string _vlcParms;
        private string _reference;

        public string VlcParms
        {
            get { return _vlcParms; }
            set
            {
                if (value == _vlcParms) return;
                _vlcParms = value;
                OnPropertyChanged();
            }
        }
    }
}