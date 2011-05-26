using System.Collections.Generic;
using System.IO;

namespace SaveFormat.Dzip
{
	public class FileEntry
	{
		public string filename; //0-terminated
		public long unknown; //crc?
		public long decompressedLength;
		public long offset;
		public long compressedLength;


		public static List<FileEntry> Read(Stream stream, int count, out long memoryGate)
		{
			memoryGate = 0;
			var result = new List<FileEntry>(count);
			for (var i=0;i<count;i++)
			{
				var entry = new FileEntry();
				var filenameLength = stream.ReadInt16();
				entry.filename = stream.ReadUtf8String(filenameLength).TrimEnd(char.MinValue);
				entry.unknown = stream.ReadInt64();
				entry.decompressedLength = stream.ReadInt64();
				entry.offset = stream.ReadInt64();
				entry.compressedLength = stream.ReadInt64();
				result.Add(entry);
				var estimatedBufferSize = entry.compressedLength + entry.decompressedLength;
				if (memoryGate < estimatedBufferSize) memoryGate = estimatedBufferSize;
			}
			return result;
		}

		public override string ToString()
		{
			return filename;
		}
	}
}