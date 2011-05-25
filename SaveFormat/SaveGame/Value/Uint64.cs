using System;
using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Uint64 : Base
	{
		public Uint64(byte[] value)
		{
			type = PrimitiveType.Uint64;
			this.value = BitConverter.ToUInt64(value, 0);
		}

		public ulong value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}