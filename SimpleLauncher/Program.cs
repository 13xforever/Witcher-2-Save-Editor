using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaveFormat;

namespace SimpleLauncher
{
	class Program
	{
		static void Main(string[] args)
		{
			var save = W2SaveReader.Read(@"D:\Documents\Witcher 2\gamesaves\QuickSave.sav");
			if (save.header != "SAVY") Console.WriteLine("Not a Witcher 2 save file.");
			Console.WriteLine("done");
			Console.ReadKey();
		}
	}
}
