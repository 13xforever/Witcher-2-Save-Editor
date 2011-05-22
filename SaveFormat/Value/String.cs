using System;
using System.Diagnostics;
using System.Text;

namespace SaveFormat.Value
{
	[DebuggerDisplay("{value}")]
	public class String : Base
	{
		public String(byte[] value)
		{
			type = PrimitiveType.String;
			isUnicode = (value[0] & 0x80) != 0x80;
			bool lengthExtension = (value[0] & 0x40) == 0x40;

			var dataIndex = 1;
			int expectedLength = value[0] & 0x3f;
			if (lengthExtension)
			{
				dataIndex = 2;
				expectedLength = expectedLength | (value[1] << 6);
			}
			if (isUnicode) expectedLength <<= 1;

			if (value.Length - dataIndex != expectedLength)
				throw new InvalidOperationException(string.Format("Expected string length is {0} byte(s), but there's {1} byte(s) of data", expectedLength, value.Length - dataIndex));
			if (isUnicode)
				Encoding.Unicode.GetString(value, dataIndex, value.Length - dataIndex);
			else
				Encoding.UTF8.GetString(value, dataIndex, value.Length - dataIndex);
		}

		public string value;
		public bool isUnicode;
	}
}