using System;

namespace SaveFormat
{
	public class UnknownNodeTypeException : FormatException
	{
		public UnknownNodeTypeException(string message):base(message)
		{
		}
	}

	public class UnknownBlckFlag : FormatException
	{
	}
}