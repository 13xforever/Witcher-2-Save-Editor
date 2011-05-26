using System.IO;
using System.Text;
using System;

namespace SaveFormat.SaveGame.Value
{
	public abstract class Base
	{
		public PrimitiveType type;
		public bool unknown;

		public static Base Read(Stream stream)
		{
			var b = (byte)stream.ReadByte();
			bool f = (b & 0x80) == 0x80;
			if (!f) throw new UnknownValueFlagException();

			var valueType = stream.ReadUtf8String(b & 0x7f);
			var tmp = new byte[4];
			stream.FillInBuffer(tmp, 2);
			int valueLength = BitConverter.ToUInt16(tmp, 0);

			if (valueLength == 0xffff)
				valueLength = stream.ReadInt32() - 4; //todo: shouldn't it be 6???
			else
			{
				stream.FillInBuffer(tmp, 2, tmp.Length - 2);
				valueLength = BitConverter.ToInt32(tmp, 0);
			}

			tmp = new byte[valueLength];
			stream.FillInBuffer(tmp);

			switch (valueType)
			{
				case "Uint8":
					return new Uint8(tmp);
				case "Uint16":
					return new Uint16(tmp);
				case "Uint":
					return new Uint(tmp);
				case "Uint64":
					return new Uint64(tmp);
				case "Int8":
					return new Int8(tmp);
				case "Int16":
					return new Int16(tmp);
				case "Int":
					return new Int(tmp);
				case "Int64":
					return new Int64(tmp);
				case "String":
					return new String(tmp);
				case "CGUID":
					return new CGuid(tmp);
				case "Bool":
					return new Bool(tmp);
				case "SQuestLogPhaseStatus":
					return new SQuestLogPhaseStatus(tmp);
				default:
					return new UnknownValueType(tmp){valueTypeName = valueType};
			}
		}
	}
}