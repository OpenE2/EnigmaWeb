using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma
{
    public class WebRequester : IWebRequester, INotifyPropertyChanged
    {
        private readonly CookieContainer _cookies;
        private readonly ILog _log;
        private NetworkCredential _credentials;

        public WebRequester([NotNull] ILog log)
        {
            if (log == null) throw new ArgumentNullException("log");
            _cookies = new CookieContainer();
            _log = log;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task<string> GetResponseAsync(string url, IProfile profile, CancellationToken token)
        {
            try
            {
                if (!string.IsNullOrEmpty(profile.Password))
                {
                    if (_credentials == null)
                        _credentials = !string.IsNullOrEmpty(profile.Password) ? new NetworkCredential(profile.Username, profile.Password) : null;
                }

                url = profile.UseSsl
                    ? string.Format("https://{0}:{1}/{2}", profile.Address, profile.HttpPort, url)
                    : string.Format("http://{0}:{1}/{2}", profile.Address, profile.HttpPort, url);

                _log.DebugFormat("Initializing HttpWebRequest to {0}", url);
                var st = new Stopwatch();
                st.Restart();
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.Proxy = null;
                request.CookieContainer = _cookies;
                request.ContentType = profile.Enigma == EnigmaType.Enigma1 ? "text/html" : "text/xml";
                request.Credentials = _credentials;

                Task<WebResponse> task = request.GetResponseAsync();

                await Task.WhenAny(task, Task.Delay(60000, token));
                token.ThrowIfCancellationRequested();
                var response = (HttpWebResponse) task.Result;

                string result = null;
                if (response != null)
                {
                    using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        Task<string> streamTask = sr.ReadToEndAsync();
                        await Task.WhenAny(streamTask, Task.Delay(60000, token));
                        token.ThrowIfCancellationRequested();
                        result = streamTask.Result;
                        st.Stop();
                        _log.DebugFormat("{0} response is", url);
                        _log.DebugFormat(result);
                    }
                }
                else
                {
                    st.Stop();
                    _log.WarnFormat("Response to {0} is null!", url);
                }

                _log.DebugFormat("HttpWebRequest to {0} took {1} ms", url, st.ElapsedMilliseconds);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;

                throw new WebRequestException(string.Format("HttpWebRequest for {0} failed!", url), ex);
            }
        }

        public async Task<byte[]> GetBinaryResponseAsync(string url, IProfile profile, CancellationToken token)
        {
            try
            {
                if (!string.IsNullOrEmpty(profile.Password))
                {
                    if (_credentials == null)
                        _credentials = !string.IsNullOrEmpty(profile.Password) ? new NetworkCredential(profile.Username, profile.Password) : null;
                }

                url = profile.UseSsl
                    ? string.Format("https://{0}:{1}/{2}", profile.Address, profile.HttpPort, url)
                    : string.Format("http://{0}:{1}/{2}", profile.Address, profile.HttpPort, url);

                _log.DebugFormat("Initializing HttpWebRequest to {0}", url);
                var st = new Stopwatch();
                st.Restart();
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.Proxy = null;
                request.CookieContainer = _cookies;
                request.Credentials = _credentials;

                Task<WebResponse> task = request.GetResponseAsync();

                await Task.WhenAny(task, Task.Delay(60000, token));
                token.ThrowIfCancellationRequested();
                var response = (HttpWebResponse) task.Result;

                byte[] result = null;
                if (response != null)
                {
                    var content = new MemoryStream();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        Task streamTask = responseStream.CopyToAsync(content);
                        await Task.WhenAny(streamTask, Task.Delay(60000, token));
                        token.ThrowIfCancellationRequested();
                        result = content.ToArray();
                        st.Stop();
                        _log.DebugFormat("{0} response length is ", url, content.Length.ToString());
                    }
                }
                else
                {
                    st.Stop();
                    _log.WarnFormat("Response to {0} is null!", url);
                }

                _log.DebugFormat("HttpWebRequest to {0} took {1} ms", url, st.ElapsedMilliseconds);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;

                throw new WebRequestException(string.Format("HttpWebRequest for {0} failed!", url), ex);
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