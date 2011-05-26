using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SaveFormat.SaveGame.Node
{
	public class Blck: Base
	{
		public Blck()
		{
			type = NodeType.BLCK;
		}

		public int length;
		public List<Base> children;

		public static Blck Read(Stream stream)
		{
			var result = new Blck();
			var b = (byte)stream.ReadByte();
			result.unknown = (b & 0x80) == 0x80;
			if (!result.unknown) throw new UnknownNodeFlagException();

			result.nameLength = (short)(b & 0x7f);
			result.name = stream.ReadUtf8String(result.nameLength);

			result.length = stream.ReadInt32();
			using (var memStream = new MemoryStream(stream.ReadBytes(result.length)))
				result.children = Aval.Read(memStream).ToList();

			return result;
		}
	}
}