﻿using System;
using System.Diagnostics;

namespace SaveFormat.SaveGame.Value
{
	[DebuggerDisplay("{value}")]
	public class Float : Base
	{
		public Float(byte[] value)
		{
			type = PrimitiveType.Float;
			if (value.Length != 4) throw new ArgumentException("Uint isn't 4 bytes long.");

			this.value = BitConverter.ToDouble(value, 0);
		}

		public double value;
	}
}