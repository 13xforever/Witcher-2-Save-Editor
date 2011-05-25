using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Uint8 : Base
	{
		public Uint8(byte[] value)
		{
			type = PrimitiveType.Uint8;
			this.value = value[0];
		}

		public byte value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}