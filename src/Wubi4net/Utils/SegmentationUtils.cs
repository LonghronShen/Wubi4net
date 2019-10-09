using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Wubi4net.Utils
{

    internal static class SegmentationUtils
    {

        /// <summary>
        /// Make the Chinese characters as continues as possible.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static List<string> CombinationSegmentaion(string input)
        {
            var currentWord = string.Empty;
            var list = new List<string>();
            var flag = 0;

            void AppendCurrentCharacter(char c, int flag_to_check)
            {
                if (flag == flag_to_check)
                {
                    currentWord += c;
                }
                else
                {
                    list.Add(currentWord);
                    flag = flag_to_check;
                    currentWord = c.ToString();
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (StringUtils.IsInputAllChinese(c))
                {
                    if (i == 0)
                    {
                        flag = 0;
                    }
                    AppendCurrentCharacter(c, 0);
                }
                else
                {
                    if (i == 0)
                    {
                        flag = 1;
                    }
                    AppendCurrentCharacter(c, 1);
                }
            }

            list.Add(currentWord);
            return list;
        }

        /// <summary>
        /// Make each Chinese character seperated.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static List<string> SingleCharacterSegementaion(string input)
        {
            var list = new List<string>();
            var currentCharacter = string.Empty;

            foreach (var c in input)
            {
                if (StringUtils.IsInputAllChinese(c))
                {
                    if (!string.IsNullOrEmpty(currentCharacter))
                    {
                        list.Add(currentCharacter);
                        currentCharacter = "";
                    }
                    else
                    {
                        currentCharacter += c;
                    }
                }
            }

            return list;
        }

    }

}