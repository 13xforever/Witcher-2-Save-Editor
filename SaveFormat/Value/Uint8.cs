namespace SaveFormat.Value
{
	public class Uint8 : Base
	{
		public Uint8(byte[] value)
		{
			type = PrimitiveType.Uint8;
			this.value = value[0];
		}

		public byte value;  //0E
	}
}