using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ShortCleanLinqExtensions.src.Extensions
{
    public static class StringExtension
    {
        #region VARIABLES

        private static Dictionary<string, Dictionary<string, string>> _snakeCache = new Dictionary<string, Dictionary<string, string>>();

        #endregion VARIABLES

        /// <summary>
        ///  the method converts the given string to Title Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string Title(this string text)
        {
            string cultureName = CultureInfo.CurrentCulture.Name ?? "en-US";

            var textinfo = new CultureInfo(cultureName, false).TextInfo;

            return textinfo.ToTitleCase(text);
        }

        /// <summary>
        ///  The method generates a URL friendly "slug" from the given string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string Stug(this string text, string separator = "-")
        {
            var title = text;

            // Convert all dashes/underscores into separator
            var flip = separator == "-" ? "_" : "-";

            title = Regex.Replace(title, "[" + Regex.Escape(flip) + "]+", separator, RegexOptions.Compiled);

            // Replace dictionary words
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "@", "at" }
            };
            if (dictionary != null)
            {
                foreach (var pair in dictionary)
                {
                    dictionary[pair.Key] = separator + pair.Value + separator;
                }

                title = title.Replace(dictionary.Keys?.ToString() ?? string.Empty, dictionary.Values.ToString());
            }

            // Remove all characters that are not the separator, letters, numbers, or whitespace
            title = Regex.Replace(title, "[^" + Regex.Escape(separator) + "\\p{L}\\p{N}\\s]+", "", RegexOptions.Compiled).ToLower();

            // Replace all separator characters and whitespace by a single separator
            title = Regex.Replace(title, "[" + Regex.Escape(separator) + "\\s]+", separator, RegexOptions.Compiled);

            return title.Trim(separator.ToCharArray());
        }

        /// <summary>
        /// The mask method masks a portion of a string with a repeated character, and may be used to obfuscate segments of strings such as email addresses and phone numbers:
        /// </summary>
        /// <param name="str"></param>
        /// <param name="character"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="encoding"></param>
        /// <returns>string</returns>
        public static string Mask(this string str, string character, int index, int? length = null, string encoding = "UTF-8")
        {
            if (character == "")
            {
                return str;
            }

            string segment = length != null ? str.Substring(index, length.Value) : str.Substring(index);

            if (segment == "")
            {
                return str;
            }

            int strlen = Encoding.GetEncoding(encoding).GetByteCount(str);
            int startIndex = index;

            if (index < 0)
            {
                startIndex = index < -strlen ? 0 : strlen + index;
            }

            string start = str.Substring(0, startIndex);
            int segmentLen = Encoding.GetEncoding(encoding).GetByteCount(segment);
            string end = str.Substring(startIndex + segmentLen);

            return start + new string(character[0], segmentLen) + end;
        }

        /// <summary>
        /// The snake method converts the given string to snake_case:
        /// </summary>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <returns>string</returns>
        public static string Snake(this string value, string delimiter = "_")
        {
            string key = value;

            if (_snakeCache.ContainsKey(key) && _snakeCache[key].ContainsKey(delimiter))
            {
                return _snakeCache[key][delimiter];
            }

            if (!string.IsNullOrEmpty(value) && !value.All(char.IsLower))
            {
                value = Regex.Replace(value, @"\s+", string.Empty);
                value = Regex.Replace(value, @"(.)(?=[A-Z])", "$1" + delimiter);
                value = value.ToLowerInvariant();
            }

            string result = value;

            if (_snakeCache.ContainsKey(key))
            {
                _snakeCache[key].Add(delimiter, result);
            }
            else
            {
                _snakeCache.Add(key, new Dictionary<string, string> { { delimiter, result } });
            }

            return result;
        }

        /// <summary>
        /// The Limit method truncates the given string to the specified length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <param name="end"></param>
        /// <returns>string</returns>
        public static string Limit(this string value, int limit = 100, string end = "...")
        {
            if (value != null && Encoding.UTF8.GetByteCount(value) <= limit)
            {
                return value;
            }

            string truncated = value != null ? value.Substring(0, limit) : "";

            if (truncated.Length > 0)
            {
                truncated = truncated.Remove(truncated.LastIndexOf(" ", StringComparison.Ordinal));
            }

            return $"{truncated}{end}";
        }

        /// <summary>
        /// The method determines if the given string is valid JSON:
        /// </summary>
        /// <param name="value"></param>
        /// <returns>bool</returns>
        public static bool IsJson(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                var result = JsonSerializer.Deserialize<dynamic>(value);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}