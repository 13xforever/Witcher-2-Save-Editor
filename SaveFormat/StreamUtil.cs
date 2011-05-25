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

	}
}