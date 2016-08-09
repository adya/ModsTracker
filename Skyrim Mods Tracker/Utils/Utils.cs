using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool Within(int min, int value, int max)
        {
            min = Math.Min(min, max);
            max = Math.Max(min, max);
            return (min <= value && value <= max);
        }
    }

    static class ChromeUtils
    {
        public const string DDChromeBookmarks = "chromium/x-bookmark-entries";
        public const string DDText = "Text";

        public static List<string> ReadBookmarksURL(byte[] rawData)
        {
            List<string> urls = new List<string>();
            using (BinaryReader br = new BinaryReader(new MemoryStream(rawData)))
            {
                int size = br.ReadInt32();
                int headerSize = br.ReadInt32();
                br.ReadBytes(FullByteBlock(headerSize * 2)); // skip header. Header contains 2-byte chars.
                ReadItems(br, urls);
            }
            return urls;
        }

        private static void ReadItems(BinaryReader br, List<string> urls)
        {
            int itemsCount = br.ReadInt32(); // number of stored items
            for (int i = 0; i < itemsCount; i++)
                ReadItem(br, urls);
        }

        private static void ReadItem(BinaryReader br, List<string> urls)
        {
            bool isUrl = br.ReadInt32() == 1; // flag indicating whether the next stored item has url or not
            if (isUrl) ReadBookmarkURL(br, urls);
            else ReadBookmarkFolder(br, urls);
        }

        private static void ReadBookmarkURL(BinaryReader br, List<string> urls)
        {
            int urlSize = br.ReadInt32();   // read size of the url
            urls.Add(Encoding.UTF8.GetString(br.ReadBytes(urlSize))); // read actually url
            br.ReadBytes(FullByteBlockRemainder(urlSize)); // skip remainder of the byte block
            int detailsSize = br.ReadInt32(); // read size of bookmark's details
            br.ReadBytes(FullByteBlock(detailsSize * 2)); // skip it. Details has 2-byte chars as well.
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
        }

        private static void ReadBookmarkFolder(BinaryReader br, List<string> urls)
        {
            int unknownInt = br.ReadInt32();  // not yet discovered, empty 4-byte block for folder
            int folderNameSize = br.ReadInt32();
            br.ReadBytes(FullByteBlock(folderNameSize * 2)); // skip name. Name contains 2-byte chars.
            int folderContentSize = br.ReadInt32();
            br.ReadInt32(); // skip 1st unknown empty 4-byte block 
            br.ReadInt32(); // skip 2nd unknown empty 4-byte block 
            ReadItems(br, urls);
        }

        private static int FullByteBlock(int blockSize)
        {
            return (int)(Math.Ceiling(blockSize / 4.0) * 4.0);
        }

        private static int FullByteBlockRemainder(int blockSize)
        {
            return FullByteBlock(blockSize) - blockSize;
        }
    }
}
