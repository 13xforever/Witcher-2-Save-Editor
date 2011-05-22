using System.Diagnostics;
using System.IO;
using System.Text;
using System;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public abstract class Base
	{
		public PrimitiveType type;
		public bool unknown;

		public static Base Read(Stream stream)
		{
			var b = (byte)stream.ReadByte();
			bool f = (b & 0x80) == 0x80;
			if (!f) throw new UnknownValueFlagException();

			var length = b & 0x7f;
			var tmp = new byte[length];
			stream.Read(tmp, 0, tmp.Length);
			var valueType = Encoding.UTF8.GetString(tmp);
			tmp = new byte[4];
			stream.Read(tmp, 0, tmp.Length);
			var valueLength = BitConverter.ToInt32(tmp, 0);
			tmp = new byte[valueLength];
			stream.Read(tmp, 0, tmp.Length);

			switch (valueType)
			{
				case "Uint8":
					return new Uint8(tmp);
				case "Uint":
					return new Uint(tmp);
				case "Uint64":
					return new Uint64(tmp);
				case "String":
					return new String(tmp);
				case "CGUID":
					return new CGuid(tmp);
				case "Bool":
					return new Bool(tmp);
				default:
					return new UnknownValueType(tmp){valueTypeName = valueType};
			}
		}
	}
}