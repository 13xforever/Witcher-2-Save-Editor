using System;

namespace SaveFormat.Value
{
	public class Uint : Base
	{
		public Uint(byte[] value)
		{
			type = PrimitiveType.Uint;
			if (value.Length != 4) throw new ArgumentException("Uint isn't 4 bytes long.");

			this.value = BitConverter.ToUInt32(value, 0);
		}

		public uint value;
	}
}