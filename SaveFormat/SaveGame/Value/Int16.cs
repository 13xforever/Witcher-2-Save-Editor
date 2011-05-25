using System;
using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Int16 : Base
	{
		public Int16(byte[] value)
		{
			type = PrimitiveType.Int16;
			this.value = BitConverter.ToInt16(value, 0);
		}

		public short value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}