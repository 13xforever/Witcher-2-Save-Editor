using System;
using System.IO;
using System.Linq;

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
			string magicString = stream.ReadUtf8String(4);
			if (magicString != "STOR")
				throw new InvalidOperationException("Unknown token for KCUP node: "+magicString);
			result.data = Aval.Read(stream).First();
			return result;
		}
	}
}