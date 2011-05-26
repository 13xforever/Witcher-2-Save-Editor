using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;

namespace SaveFormat.Dzip
{
	public class W2Dzip
	{
		public string header; //DZIP
		public int version;
		public int fileCount;
		public int userId;
		public long metaOffset;
		public long unknown;
		public List<FileEntry> fileEntry;

		private long estimatedMaximumBufferSize;

		public static W2Dzip Read(Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);

			var result = new W2Dzip();
			var tmp = new byte[4];
			stream.FillInBuffer(tmp);
			result.header = Encoding.UTF8.GetString(tmp);
			stream.FillInBuffer(tmp);
			result.version = BitConverter.ToInt32(tmp, 0);
			stream.FillInBuffer(tmp);
			result.fileCount = BitConverter.ToInt32(tmp, 0);
			stream.FillInBuffer(tmp);
			result.userId = BitConverter.ToInt32(tmp, 0);
			tmp = new byte[8];
			stream.FillInBuffer(tmp);
			result.metaOffset = BitConverter.ToInt64(tmp, 0);
			stream.FillInBuffer(tmp);
			result.unknown = BitConverter.ToInt64(tmp, 0);
			stream.Seek(result.metaOffset, SeekOrigin.Begin);
			result.fileEntry = FileEntry.Read(stream, result.fileCount, out result.estimatedMaximumBufferSize);
			return result;
		}

		public void UnpackAll(Stream stream, string baseDirectory)
		{
			int memoryGateSize = (int)((GC.GetTotalMemory(false) + estimatedMaximumBufferSize) / (1024*1024)) + 1;
			Log.Write("Checking if we have {0} MB of RAM available... ", memoryGateSize);
			try
			{
				new MemoryFailPoint(memoryGateSize);
				Log.Success("ok");
			}
			catch(InsufficientMemoryException)
			{
				Log.Warning("failed. There might be errors.");
			}

			var digits = (int) Math.Ceiling(Math.Log10(fileCount));
			string mask = "{0,"+digits+"}/{1}: {2} ";
			for (int i = 0; i < fileEntry.Count; i++)
			{
				var entry = fileEntry[i];
				Log.Write(mask, i+1, fileCount, entry.filename);
				try
				{
					stream.Seek(entry.offset, SeekOrigin.Begin);
					var tmp = new byte[4];
					stream.FillInBuffer(tmp);
					var localOffset = BitConverter.ToInt32(tmp, 0);
					stream.Seek(entry.offset + localOffset, SeekOrigin.Begin);

					var outFilename = Path.Combine(baseDirectory, entry.filename);
					var outDirectory = Path.GetDirectoryName(outFilename);
					if (!Directory.Exists(outDirectory))
						Directory.CreateDirectory(outDirectory);

					var input = new byte[entry.compressedLength - localOffset];
					var output = new byte[entry.decompressedLength];
					stream.FillInBuffer(input);
					LZF.Decompress(input, input.Length, output, output.Length);
					using (var outStream = File.OpenWrite(outFilename))
						outStream.Write(output, 0, output.Length);

					Log.Success("ok");
				}
				catch (Exception e)
				{
					Log.Error("failed: " + e.Message);
				}

			}
		}
	}
}
