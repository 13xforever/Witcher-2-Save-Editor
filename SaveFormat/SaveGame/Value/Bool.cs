using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
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

		public override string ToString()
		{
			return value.ToString();
		}
	}
}