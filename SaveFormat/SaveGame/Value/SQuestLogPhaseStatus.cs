using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class SQuestLogPhaseStatus : Base
	{
		public SQuestLogPhaseStatus(byte[] value)
		{
			type = PrimitiveType.SQuestLogPhaseStatus;

			using (var stream = new MemoryStream(value))
				this.value = ReadAllValues(stream).ToDictionary(kvp => kvp.Key, kvp=>kvp.Value);
				//this.value = ReadAllValues(stream).ToList();
		}

		public Dictionary<string, Base> value;
		//public List<KeyValuePair<string, Base>> value;

		private static IEnumerable<KeyValuePair<string, Base>> ReadAllValues(Stream stream)
		{
			while (stream.Position < stream.Length)
			{
				var b = (byte)stream.ReadByte();
				bool f = (b & 0x80) == 0x80;
				if (!f) throw new UnknownNodeFlagException();

				var fieldName = stream.ReadUtf8String(b & 0x7f);

				yield return new KeyValuePair<string, Base>(fieldName,Read(stream));
			}
		}

		public override string ToString()
		{
			return value.ToString();
		}
	}
}