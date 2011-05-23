using System;

namespace SaveFormat.Value
{
	public class CGuid : Base
	{
		public CGuid(byte[] value)
		{
			type = PrimitiveType.CGUID;
			this.value = new Guid(value);
		}

		public Guid value;

		public override string ToString()
		{
			return value.ToString();
		}
	}
}