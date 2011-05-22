using System;

namespace SaveFormat
{
	public class UnknownNodeTypeException : FormatException
	{
		public UnknownNodeTypeException(string message):base(message)
		{
		}
	}
	public class UnknownValueTypeException : FormatException
	{
		public UnknownValueTypeException(string message):base(message)
		{
		}
	}

	public class UnknownNodeFlagException : FormatException
	{
	}
	
	public class UnknownValueFlagException : FormatException
	{
	}
}