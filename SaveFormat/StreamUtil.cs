using System;
using System.IO;

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
	}
}