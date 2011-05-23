using System.Diagnostics;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}", Name="{valueTypeName}")]
	public class UnknownValueType : Base
	{
		public UnknownValueType(byte[] value)
		{
			type = PrimitiveType.Unknown;
			this.value = value;
		}

		public byte[] value;
		public string valueTypeName;

		public override string ToString()
		{
			return valueTypeName;
		}
	}
}