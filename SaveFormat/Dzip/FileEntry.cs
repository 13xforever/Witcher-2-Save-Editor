using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SaveFormat.Dzip
{
	public class FileEntry
	{
		public string filename; //0-terminated
		public long unknown; //crc?
		public long decompressedLength;
		public long offset;
		public long compressedLength;


		public static List<FileEntry> Read(Stream stream, int count)
		{
			var result = new List<FileEntry>(count);
			for (var i=0;i<count;i++)
			{
				var entry = new FileEntry();
				var tmp = new byte[2];
				stream.Read(tmp, 0, tmp.Length);
				var filenameLength = BitConverter.ToInt16(tmp, 0);
				tmp = new byte[filenameLength];
				stream.Read(tmp, 0, tmp.Length);
				entry.filename = Encoding.UTF8.GetString(tmp).TrimEnd(char.MinValue);
				tmp = new byte[8];
				stream.Read(tmp, 0, tmp.Length);
				entry.unknown = BitConverter.ToInt64(tmp, 0);
				stream.Read(tmp, 0, tmp.Length);
				entry.decompressedLength = BitConverter.ToInt64(tmp, 0);
				stream.Read(tmp, 0, tmp.Length);
				entry.offset = BitConverter.ToInt64(tmp, 0);
				stream.Read(tmp, 0, tmp.Length);
				entry.compressedLength = BitConverter.ToInt64(tmp, 0);
				result.Add(entry);
			}
			return result;
		}

		public override string ToString()
		{
			return filename;
		}
	}
}