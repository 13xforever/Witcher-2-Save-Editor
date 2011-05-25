﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public static W2Dzip Read(Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);

			var result = new W2Dzip();
			var tmp = new byte[4];
			stream.Read(tmp, 0, tmp.Length);
			result.header = Encoding.UTF8.GetString(tmp);
			stream.Read(tmp, 0, tmp.Length);
			result.version = BitConverter.ToInt32(tmp, 0);
			stream.Read(tmp, 0, tmp.Length);
			result.fileCount = BitConverter.ToInt32(tmp, 0);
			stream.Read(tmp, 0, tmp.Length);
			result.userId = BitConverter.ToInt32(tmp, 0);
			tmp = new byte[8];
			stream.Read(tmp, 0, tmp.Length);
			result.metaOffset = BitConverter.ToInt64(tmp, 0);
			stream.Read(tmp, 0, tmp.Length);
			result.unknown = BitConverter.ToInt64(tmp, 0);
			stream.Seek(result.metaOffset, SeekOrigin.Begin);
			result.fileEntry = FileEntry.Read(stream, result.fileCount);
			return result;
		}

		public void UnpackAll(Stream stream, string baseDirectory)
		{
			var digits = (int) Math.Ceiling(Math.Log10(fileCount));
			string mask = "{0,"+digits+"}/{1}: {2} ";
			for (int i = 0; i < fileEntry.Count; i++)
			{
				var entry = fileEntry[i];
				Console.Write(mask, i+1, fileCount, entry.filename);
				try
				{
					stream.Seek(entry.offset, SeekOrigin.Begin);
					var tmp = new byte[4];
					stream.Read(tmp, 0, tmp.Length);
					var localOffset = BitConverter.ToInt32(tmp, 0);
					stream.Seek(entry.offset + localOffset, SeekOrigin.Begin);

					var outFilename = Path.Combine(baseDirectory, entry.filename);
					var outDirectory = Path.GetDirectoryName(outFilename);
					if (!Directory.Exists(outDirectory))
						Directory.CreateDirectory(outDirectory);
					using (var outStream = File.OpenWrite(outFilename))
						stream.CopyTo(outStream, entry.length - localOffset);

					var c = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("ok");
					Console.ForegroundColor = c;
				}
				catch (Exception e)
				{
					var c = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("failed: " + e.Message);
					Console.ForegroundColor = c;
				}

			}
		}
	}
}