using System.IO;

namespace SaveFormat.SaveGame.Node
{
	public class Rots: Base
	{
		public Rots()
		{
			type = NodeType.ROTS;
		}

		public byte[] data = new byte[4];

		public static Rots Read(Stream stream)
		{
			var result = new Rots();
			stream.FillInBuffer(result.data);
			return result;
		}
	}
}