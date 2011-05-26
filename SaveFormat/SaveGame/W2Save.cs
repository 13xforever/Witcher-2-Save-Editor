using System.Collections.Generic;
using System.IO;
using System.Linq;

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
			// ReSharper disable UseObjectOrCollectionInitializer
			var result = new W2Save();
			// ReSharper restore UseObjectOrCollectionInitializer
			result.header = stream.ReadUtf8String(4);
			result.unknown1 = stream.ReadInt32();
			result.unknown2 = stream.ReadInt32();
			result.section = Section.Read(stream).ToList();

			foreach (var sec in result.section)
				sec.ReadData(stream);
			return result;
		}
	}
}