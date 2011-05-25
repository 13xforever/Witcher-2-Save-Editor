namespace SaveFormat.SaveGame.Value
{
	public class AtUint8 : Base
	{
		public AtUint8(byte[] value)
		{
			type = PrimitiveType.AtUint8;
			this.value = value;
		}

		public byte[] value; //0A 00 00 00   85   Uint8   FF FF __ __ __ __ __ __ __ __ __ __
	}
}