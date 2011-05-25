using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SaveFormat.SaveGame.Node;

namespace SaveFormat.SaveGame
{
	[DebuggerDisplay("{name}")]
	public class Section				//36 bytes total
	{
		public string name;					//32 bytes
		public int offset;					//4 bytes

		public Blck data;

		internal static IEnumerable<Section> Read(Stream stream)
		{
			for (var i = 0; i < 32; i++)
			{
				var result = new Section();
				var tmp = new byte[32];
				stream.Read(tmp, 0, tmp.Length);
				result.name = Encoding.UTF8.GetString(tmp).TrimEnd(char.MinValue);
				if (string.IsNullOrEmpty(result.name)) yield break;

				tmp = new byte[4];
				stream.Read(tmp, 0, tmp.Length);
				result.offset = BitConverter.ToInt32(tmp, 0);
				yield return result;
			}
		}

		public void ReadData(Stream stream)
		{
			stream.Seek(offset, SeekOrigin.Begin);

			var tmp = new byte[4];
			stream.Read(tmp, 0, 4);
			string nodeType = Encoding.UTF8.GetString(tmp);
			switch (nodeType)
			{
				case "BLCK":
					{
						data = Blck.Read(stream);
						break;
					}
				default:
					throw new UnknownNodeTypeException(nodeType);
			}
		}
	}
}