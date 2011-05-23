using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class Uint8 : Base
	{
		public Uint8(byte[] value)
		{
			type = PrimitiveType.Uint8;
			this.value = value[0];
		}

		public byte value;  //0E

		public override string ToString()
		{
			return value.ToString();
		}
	}
}