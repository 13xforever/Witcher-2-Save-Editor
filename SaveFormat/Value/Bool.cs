using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class Bool : Base
	{
		public Bool(byte[] value)
		{
			type = PrimitiveType.Bool;
			this.value = value[0] != 0;
		}

		public bool value;
	}
}