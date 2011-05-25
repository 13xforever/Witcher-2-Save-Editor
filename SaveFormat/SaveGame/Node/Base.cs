using System.Diagnostics;

namespace SaveFormat.SaveGame.Node
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