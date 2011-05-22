namespace SaveFormat.Value
{
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