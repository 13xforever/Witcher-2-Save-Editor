using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SaveFormat.SaveGame.Node;

namespace SaveFormat.SaveGame
{
	[DebuggerDisplay("{name}")]
	public class Section
	{
		public string name;
		public int offset;

		public Blck data;

		internal static IEnumerable<Section> Read(Stream stream)
		{
			for (var i = 0; i < 32; i++)
			{
				// ReSharper disable UseObjectOrCollectionInitializer
				var result = new Section();
				// ReSharper restore UseObjectOrCollectionInitializer
				result.name = stream.ReadUtf8String(32).TrimEnd(char.MinValue);
				if (string.IsNullOrEmpty(result.name)) yield break;

				result.offset = stream.ReadInt32();
				yield return result;
			}
		}

		public void ReadData(Stream stream)
		{
			stream.Seek(offset, SeekOrigin.Begin);

			string nodeType = stream.ReadUtf8String(4);
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