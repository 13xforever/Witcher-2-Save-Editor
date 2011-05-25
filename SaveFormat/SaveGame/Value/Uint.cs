using System;
using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Uint : Base
	{
		public Uint(byte[] value)
		{
			type = PrimitiveType.Uint;
			this.value = BitConverter.ToUInt32(value, 0);
		}

		public uint value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}