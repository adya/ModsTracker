using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SMT.Utils
{
    static class StringUtils
    {
        public static string NonNull(string str) { return (str == null ? "" : str); }
    }

    static class MathUtils
    {
        public static int Clamp(int min, int value, int max)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);
            return (value < min ? min : (value > max ? max : value));
        }
        public static float Clamp(float min, float value, float max)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);
            return (value < min ? min : (value > max ? max : value));
        }
        public static bool Within(int min, int value, int max)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);
            return (min <= value && value <= max);
        }
        public static bool Within(float min, float value, float max)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);
            return (min <= value && value <= max);
        }
    }

    static class ColorUtils
    {
        public static Color ChangeLightness(this Color color, float correctionFactor, bool preserveAlpha = true)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            red += (255 - red) * correctionFactor;
            green += (255 - green) * correctionFactor;
            blue += (255 - blue) * correctionFactor;

            return Color.FromArgb((preserveAlpha ? color.A : (byte)255), (byte)ClampColor(red), (byte)ClampColor(green), (byte)ClampColor(blue));
        }

        private static float ClampColor(float value)
        {
            return MathUtils.Clamp(0, value, 255);
        }

    }

    static class ChromeUtils
    {
        public class ChromeBookmark
        {
            public ChromeBookmark(string url, string name)
            {
                URL = url;
                Name = name;
            }

            public string URL { get; private set; }
            public string Name { get; private set; }
        }

        public const string DDChromeBookmarks = "chromium/x-bookmark-entries";
        public const string DDText = "Text";

        public static ChromeBookmark[] ReadBookmarks(byte[] rawData)
        {
            var bookmarks = new List<ChromeBookmark>();
            using (BinaryReader br = new BinaryReader(new MemoryStream(rawData)))
            {
                int size = br.ReadInt32();
                int headerSize = br.ReadInt32();
                br.ReadBytes(FullByteBlock(headerSize * 2)); // skip header. Header contains 2-byte chars.
                return ReadItems(br).ToArray();
            }
        }

        private static List<ChromeBookmark> ReadItems(BinaryReader br)
        {
            var items = new List<ChromeBookmark>();
            int itemsCount = br.ReadInt32(); // number of stored items
            for (int i = 0; i < itemsCount; i++)
            {
                bool isBookmark = br.ReadInt32() == 1; // flag indicating whether the next stored item has url or not
                if (isBookmark)
                    items.Add(ReadBookmark(br));
                else
                    items.AddRange(ReadBookmarkFolder(br));
            }
            return items;
        }

        private static ChromeBookmark ReadBookmark(BinaryReader br)
        {
            int urlSize = br.ReadInt32();   // read size of the url
            string url = Encoding.UTF8.GetString(br.ReadBytes(urlSize)); // read actually url
            br.ReadBytes(FullByteBlockRemainder(urlSize)); // skip remainder of the byte block
            int detailsSize = br.ReadInt32() * 2; // x2 because names use Unicode which takes 2-bytes per char.
            var name = Encoding.Unicode.GetString(br.ReadBytes(detailsSize)); // Read bookmark name. Details has 2-byte chars as well.
            br.ReadBytes(FullByteBlockRemainder(detailsSize)); // skip remainder of the byte block
            int bookmarkIndex = br.ReadInt32(); // read, as guessed, bookmark index or id.
            int unknownInt2 = br.ReadInt32();  // not yet discovered, probably begin flag.
            int auxParamsCount = br.ReadInt32(); // read number of aux params

            for (int j = 0; j < auxParamsCount; j++)
            {
                int paramNameSize = br.ReadInt32(); // read size of param name
                br.ReadBytes(FullByteBlock(paramNameSize)); // skip that name
                int paramValueSize = br.ReadInt32(); // read size of param value
                br.ReadBytes(FullByteBlock(paramValueSize)); // skip that value
            }
            return new ChromeBookmark(url, name);
        }

        private static List<ChromeBookmark> ReadBookmarkFolder(BinaryReader br)
        {
            int unknownInt = br.ReadInt32();  // not yet discovered, empty 4-byte block for folder
            int folderNameSize = br.ReadInt32() * 2; // x2 because names use Unicode which takes 2-bytes per char.
            string folderName = Encoding.Unicode.GetString(br.ReadBytes(FullByteBlock(folderNameSize))); // skip name. Name contains 2-byte chars.
            int folderContentSize = br.ReadInt32();
            br.ReadInt32(); // skip 1st unknown empty 4-byte block 
            br.ReadInt32(); // skip 2nd unknown empty 4-byte block 
            return ReadItems(br);
        }

        /// <summary>
        /// Ceils given number of bytes to the closest multiple of 4 (to get full 32-bits [4-bytes] block).
        /// </summary>
        /// <param name="blockSize">Size of the block in bytes to ceil.</param>
        /// <returns></returns>
        private static int FullByteBlock(int blockSize)
        {
            return (int)(Math.Ceiling(blockSize / 4.0) * 4.0);
        }

        /// <summary>
        /// Calculates difference between given size of block and it's ceiled 4-bytes block.
        /// </summary>
        /// <param name="blockSize">Size of the block in bytes to get diffrerence of.</param>
        /// <returns></returns>
        private static int FullByteBlockRemainder(int blockSize)
        {
            return FullByteBlock(blockSize) - blockSize;
        }
    }
}
