using System;
using System.IO;
using System.Text;

namespace SaveFormat
{
	public static class StreamUtil
	{
		public static void CopyTo(this Stream input, Stream output, long length)
		{
			var buffer = new byte[Math.Min(length, 32768)];

			int read;
			while ((read = input.Read(buffer, 0, (int) Math.Min(length, buffer.Length))) > 0 && length > 0)
			{
				output.Write(buffer, 0, read);
				length -= read;
			}
		}

		public static int FillInBuffer(this Stream stream, byte[] buffer, int offset, int length)
		{
			int bytesRead, totalRead = 0;
			while ((bytesRead = stream.Read(buffer, offset + totalRead, length - totalRead)) > 0)
				totalRead += bytesRead;
			return totalRead;
		}
	
		public static int FillInBuffer(this Stream stream, byte[] buffer, int length)
		{
			int bytesRead, totalRead = 0;
			while ((bytesRead = stream.Read(buffer, totalRead, length - totalRead)) > 0)
				totalRead += bytesRead;
			return totalRead;
		}
	
		public static int FillInBuffer(this Stream stream, byte[] buffer)
		{
			return stream.FillInBuffer(buffer, buffer.Length);
		}

		public static sbyte ReadInt8(this Stream stream)
		{
			return (sbyte) stream.ReadByte();
		}
	
		public static ushort ReadUInt16(this Stream stream)
		{
			var tmp = new byte[2];
			stream.FillInBuffer(tmp);
			return BitConverter.ToUInt16(tmp, 0);
		}
	
		public static short ReadInt16(this Stream stream)
		{
			var tmp = new byte[2];
			stream.FillInBuffer(tmp);
			return BitConverter.ToInt16(tmp, 0);
		}

		public static uint ReadUInt32(this Stream stream)
		{
			var tmp = new byte[4];
			stream.FillInBuffer(tmp);
			return BitConverter.ToUInt32(tmp, 0);
		}

		public static int ReadInt32(this Stream stream)
		{
			var tmp = new byte[4];
			stream.FillInBuffer(tmp);
			return BitConverter.ToInt32(tmp, 0);
		}

		public static ulong ReadUInt64(this Stream stream)
		{
			var tmp = new byte[8];
			stream.FillInBuffer(tmp);
			return BitConverter.ToUInt64(tmp, 0);
		}

		public static long ReadInt64(this Stream stream)
		{
			var tmp = new byte[8];
			stream.FillInBuffer(tmp);
			return BitConverter.ToInt64(tmp, 0);
		}

		public static byte[] ReadBytes(this Stream stream, int length)
		{
			var tmp = new byte[length];
			stream.FillInBuffer(tmp);
			return tmp;
		}
	
		public static string ReadUtf8String(this Stream stream, int length)
		{
			var tmp = new byte[length];
			stream.FillInBuffer(tmp);
			return Encoding.UTF8.GetString(tmp);
		}
	}
}