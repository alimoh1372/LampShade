using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _0_Framework.Application
{
    public static class GenerateSlug
    {
        public static string Slugify(this string phrase)
        {
            
            string s = phrase.RemoveDiacritics().ToLower();
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s]", "");//Remove invalid charachter;
            s = Regex.Replace(s, @"\s+", " ").Trim();//Single Space
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();
            s = Regex.Replace(s, @"\s", "-");//Insert hyphens(-)
            s = Regex.Replace(s, @"\u2009", "-");// replace half space if exist
            return s.ToLower();

        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}