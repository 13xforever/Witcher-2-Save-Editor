using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class Int8 : Base
	{
		public Int8(byte[] value)
		{
			type = PrimitiveType.Int8;
			this.value = (sbyte)value[0];
		}

		public sbyte value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}