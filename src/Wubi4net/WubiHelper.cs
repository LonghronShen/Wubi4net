using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Wubi4net.Exceptions;
using Wubi4net.Format;
using Wubi4net.Utils;

namespace Wubi4net
{

    /// <summary>
    /// WubiHelper.
    /// </summary>
    public static class WubiHelper
    {

        public const OutputStyle DefaultOutputStyle = OutputStyle.UpperCase;

        private static List<WubiItem> wubiItems;

        static WubiHelper()
        {
            wubiItems = ResourceLoader.LoadWubiItems();
        }

        public static List<string> ConvertSingleCharacter(char input, bool multiCodes = false, OutputStyle outputStyle = DefaultOutputStyle)
        {
            var charString = input.ToString();
            if (!StringUtils.IsInputAllChinese(input.ToString()))
            {
                throw new InvalidWubiFormatException($"The given character '{charString}' is not a Chinese character.");
            }
            var codes = wubiItems
                .Where(x => x.Character == charString)
                .Select(x => x.WubiCodes)
                .FirstOrDefault();
            if (codes != null)
            {
                var formattedCodes = codes
                    .Select(x => outputStyle == OutputStyle.LowerCase ? x.ToLower() : x.ToUpper())
                    .OrderByDescending(x => x.Length);
                if (multiCodes)
                {
                    return formattedCodes.ToList();
                }
                else
                {
                    return formattedCodes.Take(1).ToList();
                }
            }
            throw new Exception("Failed to find the matching wubi code for the given character.");
        }

        public static string ConvertPhraseAsSingleCode(string input, OutputStyle outputStyle = DefaultOutputStyle)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
            var characters = input.ToCharArray();
            switch (characters.Length)
            {
                case 1:
                    return ConvertSingleCharacter(characters[0], false, outputStyle).FirstOrDefault();
                case 2:
                    return characters.Select(x => ConvertSingleCharacter(x, false, outputStyle).First().Substring(0, 2)).Aggregate((a, b) => a + b);
                case 3:
                    return characters.Select((x, i) => ConvertSingleCharacter(x, false, outputStyle).First().Substring(0, i == 0 ? 1 : i)).Aggregate((a, b) => a + b);
                case 4:
                    return characters
                        .Where((x, i) => i < 3 && i == characters.Length - 1)
                        .Select(x => ConvertSingleCharacter(x, false, outputStyle).First().Substring(0, 1)).Aggregate((a, b) => a + b);
                default:
                    throw new ArgumentException("The input phrase string length must be less than 5.");
            }
        }

#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static List<string> GeneralConvert(string input, bool multiCodes = false, OutputStyle outputStyle = DefaultOutputStyle)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
            if (input.Length == 1)
            {
                return ConvertSingleCharacter(input[0], multiCodes, outputStyle);
            }
            else
            {
                return new List<string>()
                {
                    ConvertPhraseAsSingleCode(input, outputStyle)
                };
            }
        }

        public static List<List<string>> Wubi(string input, bool multiCode = false, bool single = true, OutputStyle outputStyle = OutputStyle.LowerCase, Func<string, bool, List<string>> segmentation = null)
        {
            var list = new List<List<string>>();

            if (single)
            {
                var chineseChars = segmentation?.Invoke(input, single) ?? SegmentationUtils.SingleCharacterSegementaion(input);
                foreach (var item in chineseChars)
                {
                    if (item.Length == 1 && StringUtils.IsInputAllChinese(item))
                    {
                        list.Add(GeneralConvert(item, multiCode, outputStyle));
                    }
                    else
                    {
                        list.Add(new List<string>() { item });
                    }
                }
            }
            else
            {
                var chineseChars = segmentation?.Invoke(input, single) ?? SegmentationUtils.CombinationSegmentaion(input);
                foreach (var item in chineseChars)
                {
                    if (item.Length == 1 && StringUtils.IsInputAllChinese(item))
                    {
                        list.Add(new List<string>()
                        {
                            ConvertPhraseAsSingleCode(item, outputStyle)
                        });
                    }
                    else
                    {
                        list.Add(new List<string>() { item });
                    }
                }
            }

            return list;
        }

    }

}
