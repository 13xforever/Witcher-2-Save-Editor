namespace SaveFormat.SaveGame.Value
{
	public class GameTime : Base
	{
		public GameTime(byte[] value)
		{
			type = PrimitiveType.GameTime;
			this.value = value;
		}

		public byte[] value;
	}
}