using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Serialization;

namespace MbCore
{
    public static class ParseHelpers
    {
        /// <summary>
        /// Converts a string representation of a title to a corresponding <see cref="BookType"/> value.
        /// </summary>
        /// <param name="title">The title string to be evaluated.</param>
        /// <returns>
        /// A <see cref="BookType"/> enumeration value that represents the type of book derived from the title.
        /// If the title contains multiple matches, <see cref="BookType.Mischbände"/> is returned.
        /// </returns>
        /// <remarks>
        /// The method performs a case-insensitive search for specific key-syllables in the title:
        /// <list type="bullet">
        ///   <item><description>"tauf || geburt " maps to <see cref="BookType.Taufbücher"/>.</description></item>
        ///   <item><description>"trau" maps to <see cref="BookType.Hochzeitsbücher"/>.</description></item>
        ///   <item><description>"misch" maps to <see cref="BookType.Mischbände"/>.</description></item>
        ///   <item><description>"sterb" || "beerd" || "begr" maps to <see cref="BookType.Sterbebücher"/>.</description></item>
        /// </list>
        /// If no match is found, the method returns <see cref="BookType.Verschiedenes"/>.
        /// </remarks>
        public static BookType toBookType(this string title)
        {
            var t = title.ToLower();
            BookType bt = BookType.None;

            if (t.Contains("tauf") || t.Contains("geburt")) bt |= BookType.Taufbücher;
            if (t.Contains("trau")) bt |= BookType.Hochzeitsbücher;
            if (t.Contains("misch")) bt |= BookType.Mischbände;
            if (t.Contains("sterb") || t.Contains("beerd") || t.Contains("begr")) bt |= BookType.Sterbebücher;
            if (bt == BookType.None) bt = BookType.Verschiedenes;
            return ((int)bt & ((int)bt - 1)) == 0 ? bt : BookType.Mischbände;
        }

        /// <summary>
        /// Converts the input string into a safe filename by replacing invalid characters with underscores.
        /// </summary>
        /// <param name="filename">The string to be converted into a safe filename.</param>
        /// <returns>
        /// A string where all invalid filename characters, including spaces, are replaced with underscores.
        /// </returns>
        /// <remarks>
        /// The method uses <see cref="Path.GetInvalidFileNameChars"/> to identify characters that are not allowed in filenames,
        /// appending a space (' ') to this list. Each invalid character in the input string is replaced with an underscore ('_').
        /// </remarks>
        public static string toSafeFilename(this string filename)
        {            
            char[] invalidChars = Path.GetInvalidFileNameChars().Append(' ').ToArray(); 
            return string.Concat(filename.Select(c => invalidChars.Contains(c) ? '_' : c)); // Replace invalid characters with an underscore 
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        internal static T? ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        public static string FindLongestCommonPrefix(List<string> strings)
        {
            if (strings == null || strings.Count == 0)
                return string.Empty;

            // Start with the first string as a candidate prefix
            string prefix = strings[0];

            foreach (string str in strings)
            {
                // Compare prefix with the current string and reduce it as needed
                while (!str.StartsWith(prefix))
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (prefix == string.Empty) return string.Empty;
                }
            }

            return prefix;
        }

    }
}


