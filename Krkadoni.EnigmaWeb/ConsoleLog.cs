using System;

namespace Krkadoni.Enigma
{
    public class ConsoleLog : ILog
    {
        private bool _isDebugEnabled;
        private bool _isErrorEnabled;
        private bool _isFatalEnabled;
        private bool _isInfoEnabled;
        private bool _isWarnEnabled;

        public ConsoleLog()
        {
            _isDebugEnabled = true;
            _isErrorEnabled = true;
            _isFatalEnabled = true;
            _isInfoEnabled = true;
            _isWarnEnabled = true;
        }

        public void Debug(object message)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Debug(object message, Exception exception)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        public void DebugFormat(string format, object arg0)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            if (!_isDebugEnabled)
                return;

            System.Diagnostics.Debug.WriteLine(format, arg0, arg1, arg2);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_isDebugEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(String.Format(provider, format), args);
        }

        public void Info(object message)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Info(object message, Exception exception)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        public void InfoFormat(string format, object arg0)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1, arg2);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_isInfoEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(String.Format(provider, format), args);
        }

        public void Warn(object message)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warn(object message, Exception exception)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        public void WarnFormat(string format, object arg0)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1, arg2);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_isWarnEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(string.Format(provider, format), args);
        }

        public void Error(object message)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(object message, Exception exception)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        public void ErrorFormat(string format, object arg0)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1, arg2);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_isErrorEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(string.Format(provider, format), args);
        }

        public void Fatal(object message)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Fatal(object message, Exception exception)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        public void FatalFormat(string format, object arg0)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(format, arg0, arg1, arg2);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (!_isFatalEnabled)
                return;
            System.Diagnostics.Debug.WriteLine(string.Format(provider, format), args);
        }

        public bool IsDebugEnabled
        {
            get { return _isDebugEnabled; }
            private set { _isDebugEnabled = value; }
        }

        public bool IsInfoEnabled
        {
            get { return _isInfoEnabled; }
            private set { _isInfoEnabled = value; }
        }

        public bool IsWarnEnabled
        {
            get { return _isWarnEnabled; }
            private set { _isWarnEnabled = value; }
        }

        public bool IsErrorEnabled
        {
            get { return _isErrorEnabled; }
            private set { _isErrorEnabled = value; }
        }

        public bool IsFatalEnabled
        {
            get { return _isFatalEnabled; }
            private set { _isFatalEnabled = value; }
        }

        public void SetDebug(bool enabled)
        {
            IsDebugEnabled = enabled;
        }

        public void SetInfo(bool enabled)
        {
            IsInfoEnabled = enabled;
        }

        public void SetWarn(bool enabled)
        {
            IsWarnEnabled = enabled;
        }

        public void SetError(bool enabled)
        {
            IsErrorEnabled = enabled;
        }

        public void SetFatal(bool enabled)
        {
            IsFatalEnabled = enabled;
        }
    }
}