using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaveFormat
{
	public static class W2SaveReader
	{
		public static W2Save Read(string filename)
		{
			var result = new W2Save();
			using (var stream = File.OpenRead(filename))
				result = W2Save.Read(stream);
			return result;
		}

		internal static void CopyToChar(this byte[] from, char[] to)
		{
			var copyLength = Math.Min(from.Length, to.Length);
			for (var i = 0; i < copyLength; i++)
				to[i] = (char)from[i];
			for (var i = copyLength; i < to.Length; i++)
				to[i] = char.MinValue;
		}

		internal static string AsString(this char[] array)
		{
			return new StringBuilder().Append(array).ToString().TrimEnd(char.MinValue);
		}
	}
}
