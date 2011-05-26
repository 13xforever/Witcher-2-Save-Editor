using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaveFormat.SaveGame
{
	public class W2Save
	{
		public string header;				// 4 bytes, SAVY
		public int unknown1;				// 4 bytes, 2
		public int unknown2;				// 4 bytes, 1
		public List<Section> section;		//(0x48c-12)/36

		internal static W2Save Read(Stream stream)
		{
			var result = new W2Save();
			var tmp = new byte[4];
			stream.FillInBuffer(tmp);
			result.header = Encoding.UTF8.GetString(tmp).TrimEnd(char.MinValue);
			tmp = new byte[4];
			stream.FillInBuffer(tmp);
			result.unknown1 = BitConverter.ToInt32(tmp, 0);
			stream.FillInBuffer(tmp);
			result.unknown2 = BitConverter.ToInt32(tmp, 0);
			result.section = Section.Read(stream).ToList();

			foreach (var sec in result.section)
				sec.ReadData(stream);
			return result;
		}
	}
}