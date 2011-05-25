using System;
using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Uint16 : Base
	{
		public Uint16(byte[] value)
		{
			type = PrimitiveType.Uint16;
			this.value = BitConverter.ToUInt16(value, 0);
		}

		public ushort value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}