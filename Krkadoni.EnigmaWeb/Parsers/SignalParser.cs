using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public class SignalParser : IResponseParser<ISignalCommand, ISignalResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public SignalParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<ISignalResponse> ParseAsync(string response, EnigmaType enigmaType)
        {
            try
            {
                if (enigmaType == EnigmaType.Enigma1)
                    return await Task.Run(() => ParseE1(response));

                return await Task.Run(() => ParseE2(response));
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;
                throw new ParsingException(string.Format("Failed to parse response{0}{1}", Environment.NewLine, response), ex);
            }
        }

        protected virtual ISignalResponse ParseE1(string response)
        {
            string searchFor = "type=\"checkbox\" value=\"";
            string sLock = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            sLock = sLock.Substring(0, sLock.IndexOf("\"", StringComparison.Ordinal));
            bool locked = (sLock.ToLower() == "on");
            string sSync = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            sSync = sSync.Substring(sSync.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            sSync = sSync.Substring(0, sSync.IndexOf("\"", StringComparison.Ordinal));
            bool sync = (sSync.ToLower() == "on");
            searchFor = "<td align=\"center\">";
            response = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            string snr = response.Substring(0, response.IndexOf("%", StringComparison.Ordinal));
            response = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            string acg = response.Substring(0, response.IndexOf("%", StringComparison.Ordinal));
            response = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);

            string ber = response.Substring(0, response.IndexOf("<", StringComparison.Ordinal));

            IE1Signal signal = InitializeSignal(snr, acg, ber, locked, sync);

            if (signal == null)
                throw new ParsingException("Failed to parse Enigma1 signal!");

            return _factory.SignalResponse(signal);
        }

        private IE1Signal InitializeSignal(string snr, string acg, string ber, bool locked, bool sync)
        {
            IE1Signal signal = _factory.E1Signal();

            if (snr.Length == 0)
            {
                signal.Snr = -1;
                signal.Acg = -1;
                signal.Ber = -1;
                return signal;
            }

            signal.Lock = locked;
            signal.Sync = sync;
            signal.Acg = int.Parse(acg);
            signal.Snr = int.Parse(snr);
            signal.Ber = int.Parse(ber);

            return signal;
        }

        protected virtual ISignalResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);

            string snr = null;
            string db = null;
            string acg = null;
            string ber = null;

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2snrdb":
                                reader.Read();
                                db = reader.Value;
                                break;
                            case "e2snr":
                                reader.Read();
                                snr = reader.Value;
                                break;
                            case "e2ber":
                                reader.Read();
                                ber = reader.Value;
                                break;
                            case "e2acg":
                                reader.Read();
                                acg = reader.Value;
                                break;
                        }
                    }
                    canRead = reader.Read();
                }
            }

            IE2Signal signal = InitializeSignal(snr, db, acg, ber);

            if (signal == null)
                throw new ParsingException("Failed to parse Enigma2 signal!");

            return _factory.SignalResponse(signal);
        }

        private IE2Signal InitializeSignal(string snr, string db, string acg, string ber)
        {
            IE2Signal signal = _factory.E2Signal();
            string ds = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            string realSnr;
            string realDb;

            snr = snr.Replace("%", "").Trim();
            db = db.ToLower().Replace("db", "").Trim().Replace(",", ds).Replace(".", ds);

            if (snr.Length == 0 || db.Length == 0)
            {
                signal.Db = -1;
                signal.Snr = -1;
                signal.Acg = -1;
                signal.Ber = -1;
                return signal;
            }

            //dB is in percentage, someting is wrong
            if (db.IndexOf("%", StringComparison.Ordinal) > -1)
            {
                //if SNR is in dB we simply switch the values
                if (snr.IndexOf("db", StringComparison.Ordinal) > -1)
                {
                    realSnr = db;
                    realDb = snr;
                    realDb = realDb.Replace("db", "").Trim().Replace(",", ds).Replace(".", ds);
                    realSnr = realSnr.Replace("%", "").Trim();
                }
                else
                {
                    //both dB and SNR are in %, we'll have to calculate dB
                    realSnr = snr;
                    realDb = null;
                }
            }
            else
            {
                realSnr = snr;
                realDb = db;
            }

            //check if snr value is in db
            if (snr.ToLower().IndexOf("db", StringComparison.Ordinal) > -1)
            {
                realSnr = null;
                realDb = db;
            }

            if (realSnr == null && realDb == null)
                throw new ParsingException("Failed to parse Enigma2 signal!");

            if (realSnr != null && realDb != null)
            {
                signal.Snr = int.Parse(realSnr);
                signal.Db = decimal.Parse(realDb);
            }
            else if (realDb != null)
            {
                signal.Db = decimal.Parse(realDb);
                signal.Snr = (int) (signal.Db*6.5M);
            }
            else
            {
                signal.Snr = int.Parse(realSnr);
                signal.Db = Math.Round(signal.Snr/6.5M, 2);
            }
            signal.Acg = int.Parse(acg.Replace("%", "").Trim());
            signal.Ber = int.Parse(ber);
            return signal;
        }
    }
}