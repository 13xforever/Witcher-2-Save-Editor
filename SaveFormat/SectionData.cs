using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SaveFormat
{
	public class DataNode
	{
		public NodeType type;
		public bool unknown;
		public short nameLength;
		public string nodeName;
	}

	public class BlckNode: DataNode
	{
		public BlckNode()
		{
			type = NodeType.BLCK;
		}

		public int nodeLength;
		public List<DataNode> children;

		public static BlckNode Read(Stream stream)
		{
			var result = new BlckNode();
			var b = (byte)stream.ReadByte();
			result.unknown = (b & 80) == 80;
			if (!result.unknown) throw new UnknownBlckFlag();

			return result;
		}
	}

	public class AvalNode: DataNode
	{
		public AvalNode()
		{
			type = NodeType.AVAL;
		}

		public List<PrimitiveData> children;
	}

	public enum NodeType
	{
		BLCK = 0,
		AVAL = 1,
	}
}