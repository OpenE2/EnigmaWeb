using System;
using System.Text;

namespace Krkadoni.Enigma.Parsers
{
    public static class Helpers
    {
        /// <summary>
        ///     Remove illegal XML characters from a string.
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            var buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        ///     Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(char character)
        {
            // == '\t' == 9   
            // == '\n' == 10  
            // == '\r' == 13  
            return (character == 0x9 || character == 0xa || character == 0xd || (character >= 0x20 && character <= 0xd7ff) ||
                    (character >= 0xe000 && character <= 0xfffd) || (character >= 0x10000 && character <= 0x10ffff));
        }
    }
}