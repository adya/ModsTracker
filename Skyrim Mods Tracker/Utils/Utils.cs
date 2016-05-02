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

    static class ChromeUtils
    {
        public static List<string> ReadBookmarksURL(byte[] rawData)
        {
            List<string> urls = new List<string>();
            using (BinaryReader br = new BinaryReader(new MemoryStream(rawData)))
            {
                int size = br.ReadInt32();
                int header = br.ReadInt32();
                br.ReadBytes(FullByteBlock(header * 2)); // skip header. Header contains 2-byte chars.
                long bookmarksCount = br.ReadInt64();
                for (int i = 0; i < bookmarksCount; i++)
                {
                    int unknownInt = br.ReadInt32(); // not yet discovered, probably begin flag.
                    int urlSize = br.ReadInt32();   // read size of the url
                    urls.Add(Encoding.UTF8.GetString(br.ReadBytes(urlSize))); // read actually url
                    br.ReadBytes(FullByteBlockRemainder(urlSize)); // skip remainder of the byte block
                    int detailsSize = br.ReadInt32(); // read size of bookmark's details
                    br.ReadBytes(FullByteBlock(detailsSize * 2)); // skip it. Details has 2-byte chars as well.
                    long bookmarkIndex = br.ReadInt64(); // read, as guessed, bookmark index or id.
                    long auxParamsCount = br.ReadInt64(); // read number of aux params

                    for (int j = 0; j < auxParamsCount; j++)
                    {
                        int paramNameSize = br.ReadInt32(); // read size of param name
                        br.ReadBytes(FullByteBlock(paramNameSize)); // skip that name
                        int paramValueSize = br.ReadInt32(); // read size of param value
                        br.ReadBytes(FullByteBlock(paramValueSize)); // skip that value
                    }

                }
            }
            return urls;
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
