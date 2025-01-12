using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;

namespace Common.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] ToBytesArray(this object obj)
        {
            try
            {
                return obj != null && obj != (object)DBNull.Value ? (byte[])obj : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static T Decompress<T>(this byte[] obj)
        {
            using (MemoryStream ms = new MemoryStream(obj))
            using (GZipStream gzipStream = new GZipStream(ms, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(gzipStream))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
        public static byte[] DecompressByte(this byte[] obj)
        {
            using (MemoryStream compressedStream = new MemoryStream(obj))
            using (GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(decompressionStream))
            {
                string decompressedJson = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<byte[]>(decompressedJson);
            }

        }
        public static byte[] Compress(this string obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(ms, CompressionMode.Compress))
                using (StreamWriter writer = new StreamWriter(gzipStream))
                    writer.Write(obj);
                return ms.ToArray();
            }
        }
        public static byte[] Compress(this byte[] obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(ms, CompressionMode.Compress))
                using (StreamWriter writer = new StreamWriter(gzipStream))
                    writer.Write(JsonConvert.SerializeObject(obj));
                return ms.ToArray();
            }
        }
    }
}