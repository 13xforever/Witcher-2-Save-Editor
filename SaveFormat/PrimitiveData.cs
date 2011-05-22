using System.Collections.Generic;

namespace SaveFormat
{
	public class PrimitiveData
	{
		public PrimitiveType type;
	}

	public class AtUint8:PrimitiveData
	{
		public uint value;
	}

	public class Uint8:PrimitiveData
	{
		public byte[] value = new byte[12];
	}

	public class String:PrimitiveData
	{
		public string value;
	}
	
	public class CGuid:PrimitiveData
	{
		public byte[] value;
	}

	public enum PrimitiveType
	{
		AtUint8,
		Uint8,
		String,
		CGUID
	}
}