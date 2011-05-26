﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaveFormat.SaveGame.Node
{
	public class Blck: Base
	{
		public Blck()
		{
			type = NodeType.BLCK;
		}

		public int length;
		public List<Base> children;

		public static Blck Read(Stream stream)
		{
			var result = new Blck();
			var b = (byte)stream.ReadByte();
			result.unknown = (b & 0x80) == 0x80;
			if (!result.unknown) throw new UnknownNodeFlagException();

			result.nameLength = (short)(b & 0x7f);
			var tmp = new byte[result.nameLength];
			stream.FillInBuffer(tmp);
			result.name = Encoding.UTF8.GetString(tmp);
			tmp = new byte[4];
			stream.FillInBuffer(tmp);
			result.length = BitConverter.ToInt32(tmp, 0);
			tmp = new byte[result.length];
			stream.FillInBuffer(tmp);
			using (var memStream = new MemoryStream(tmp))
				result.children = Aval.Read(memStream).ToList();

			return result;
		}
	}
}