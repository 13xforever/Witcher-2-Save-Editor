using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SaveFormat.SaveGame.Node
{
	public class Kcup: Base
	{
		public Kcup()
		{
			type = NodeType.KCUP;
		}

		public Base data;

		public static Kcup Read(Stream stream)
		{
			var result = new Kcup();
			var tmp = new byte[4];
			stream.FillInBuffer(tmp);
			string magicString = Encoding.UTF8.GetString(tmp);
			if (magicString != "STOR")
				throw new InvalidOperationException("Unknown token for KCUP node: "+magicString);
			result.data = Aval.Read(stream).First();
			return result;
		}
	}
}