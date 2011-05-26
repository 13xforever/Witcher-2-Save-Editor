using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SaveFormat.SaveGame.Node
{
	[DebuggerDisplay("{value}", Name="{name}")]
	public class Aval: Base
	{
		public Aval()
		{
			type = NodeType.AVAL;
		}

		public Value.Base value;

		public static IEnumerable<Base> Read(Stream stream)
		{
			while(stream.Position<stream.Length)
			{
				var nodeType = stream.ReadUtf8String(4);
				switch (nodeType)
				{
					case "AVAL":
						{
							var result = new Aval();
							var b = (byte)stream.ReadByte();
							result.unknown = (b & 0x80) == 0x80;
							if (!result.unknown) throw new UnknownNodeFlagException();

							result.nameLength = (short)(b & 0x7f);
							result.name = stream.ReadUtf8String(result.nameLength);
							result.value = Value.Base.Read(stream);

							yield return result;
							break;
						}
					case "BLCK":
						{
							yield return Blck.Read(stream);
							break;
						}
					case "ROTS":
						{
							yield return Rots.Read(stream);
							break;
						}
					case "KCUP":
						{
							yield return Kcup.Read(stream);
							break;
						}
					default:
						throw new UnknownNodeTypeException(nodeType);
				}
			}
		}
	}
}