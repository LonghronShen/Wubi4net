using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Wubi4net.Utils
{

    internal static class StringUtils
    {

        public const string ChineseRangeRegex = "^[\u3007 \u4e00-\ufa29]+";

        /// <summary>
        /// Indicates if the input consists only Chinese characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsInputAllChinese(string input)
        {
            return Regex.IsMatch(input, ChineseRangeRegex);
        }

        /// <summary>
        /// Indicates if the input consists only Chinese characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsInputAllChinese(char input)
        {
            return Regex.IsMatch(input.ToString(), ChineseRangeRegex);
        }

    }

}
