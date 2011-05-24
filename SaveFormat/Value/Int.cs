using System;
using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class Int : Base
	{
		public Int(byte[] value)
		{
			type = PrimitiveType.Int;
			this.value = BitConverter.ToInt32(value, 0);
		}

		public int value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}