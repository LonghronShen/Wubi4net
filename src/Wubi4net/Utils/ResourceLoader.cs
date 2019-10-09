using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Wubi4net.Utils
{

    internal static class ResourceLoader
    {

        public const string ResourceFileName = "unicode_wubi_86.txt";

        public static List<WubiItem> LoadWubiItems(string fileName = ResourceFileName)
        {
#if NET35 || PROFILE336
            var assembly = typeof(ResourceLoader).Assembly;
#else
            var assembly = typeof(ResourceLoader).GetTypeInfo().Assembly;
#endif
            foreach (var item in assembly.GetManifestResourceNames())
            {
                if (item.EndsWith(fileName))
                {
                    using (var stream = assembly.GetManifestResourceStream(item))
                    {
                        return ParseDataStream(stream);
                    }
                }
            }
            throw new FileNotFoundException($"The file '{fileName}' not found.");
        }

#if NETSTANDARD1_0 || PROFILE111
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static List<WubiItem> ParseDataStream(Stream stream)
        {
            using (TextReader tr = new StreamReader(stream))
            {
                var rawText = tr.ReadToEnd();
                var lines = rawText.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var list = lines.Select(line => line.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(fields => new WubiItem()
                    {
                        Unicode = ulong.Parse(fields[0].Trim(), NumberStyles.HexNumber),
                        Character = fields[1].Trim(),
                        WubiCodes = fields.Skip(2).ToList()
                    }).ToList();
                return list;
            }
        }

    }

}