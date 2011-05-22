using System.Diagnostics;

namespace SaveFormat.Node
{
	[DebuggerDisplay("{name}")]
	public abstract class Base
	{
		public NodeType type;
		public bool unknown;
		public short nameLength;
		public string name;
	}
}