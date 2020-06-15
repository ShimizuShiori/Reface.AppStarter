using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    public static class StringExtensions
    {

        public static IEnumerable<string> Split(this string text, string splitter)
        {

            if (text == null) return Enumerable.Empty<string>();

            if (splitter == null) splitter = "";
            if (splitter == "")
                return text.Select(c => c.ToString());

            int wordStartAt = 0;
            int splitterIndex = text.IndexOf(splitter);

            if (splitterIndex == -1)
            {
                return new string[] { text };
            }

            List<string> result = new List<string>();
            while (true)
            {
                result.Add(text.Substring(wordStartAt, splitterIndex - wordStartAt));

                wordStartAt = splitterIndex + splitter.Length;
                splitterIndex = text.IndexOf(splitter, wordStartAt);

                if (splitterIndex == -1)
                {
                    result.Add(text.Substring(wordStartAt, text.Length - wordStartAt));
                    break;
                }
            }

            return result;
        }
    }
}
