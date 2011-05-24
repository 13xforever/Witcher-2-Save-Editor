using System;
using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class Int64 : Base
	{
		public Int64(byte[] value)
		{
			type = PrimitiveType.Int64;
			this.value = BitConverter.ToInt64(value, 0);
		}

		public long value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}