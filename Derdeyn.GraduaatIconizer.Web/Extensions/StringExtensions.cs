using System.Collections.Generic;

namespace Derdeyn.GraduaatIconizer.Web.Extensions
{
    public static class StringExtensions
    {
        public static string Remove32BitChars(this string str)
        {
            if (str == null) return "";

            char[] oldchars = str.ToCharArray();
            List<char> newchars = new List<char>(oldchars.Length);

            for (var i = 0; i < oldchars.Length; i++)
            {
                if (!char.IsControl(oldchars[i]) && !char.IsHighSurrogate(oldchars[i]) && !char.IsLowSurrogate(oldchars[i]))
                {
                    newchars.Add(oldchars[i]);
                }
            }
            return new string(newchars.ToArray());
        }
    }
}
