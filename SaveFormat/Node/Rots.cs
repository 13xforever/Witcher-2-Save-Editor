using System.IO;

namespace SaveFormat.Node
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
			stream.Read(result.data, 0, result.data.Length);
			return result;
		}
	}
}