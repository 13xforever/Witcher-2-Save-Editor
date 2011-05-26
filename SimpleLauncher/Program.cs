using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SaveFormat.Dzip;
using SaveFormat.SaveGame;
using SaveFormat.SaveGame.Node;
using SaveFormat.SaveGame.Value;
using Base = SaveFormat.SaveGame.Node.Base;

namespace SimpleLauncher
{
	static class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				ShowHelp();
				return;
			}
			if (!File.Exists(args[0]))
			{
				Console.WriteLine("'{0}' could not be found.", args[0]);
				return;
			}

			Unpack(args[0]);
			Console.WriteLine("done");
		}

		private static void ShowHelp()
		{
			var exe = Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase);
			Console.WriteLine("Usage: {0} \"path_to_file.dzip\"", exe);
			Console.WriteLine("Or just drag & drop file.dzip unto {0}", exe);
		}

		private static void Unpack(string filename)
		{
			var oldTitle = Console.Title;
			using (var stream = File.OpenRead(filename))
			{
				Console.Title = "Unpacking " + filename;
				W2Dzip pack = W2Dzip.Read(stream);
				var outPath = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
				pack.UnpackAll(stream, outPath);
			}
			Console.Title = oldTitle;
		}

		private static void ParseSave(string filename)
		{
			var save = W2SaveReader.Read(filename);
			if (save.header != "SAVY") Console.WriteLine("Not a Witcher 2 save file.");

			var questEntries = save.section.First(s => s.name == "questLogBlock").data.children;

			var trackedEntryId = (CGuid)(questEntries.FindValueByName("trackedEntry").value);
			var trackedPhaseId = (CGuid)(questEntries.FindValueByName("trackedPhase").value);

			var entry = questEntries.GetQuestLogBlock(trackedEntryId.value);
			var phase = questEntries.GetQuestLogBlock(trackedPhaseId.value);
		}

		private static Aval FindValueByName(this IEnumerable<Base> sequence, string valueName)
		{
			return (Aval)sequence.First(n => n.type == NodeType.AVAL && n.name == valueName);
		}

		private static Blck GetQuestLogBlock(this IEnumerable<Base> list, Guid id)
		{
			return (from statusBlock in list.OfType<Blck>()
			        where statusBlock.name == "statusBlock"
			        where (
			              	from phaseStatus in statusBlock.children.OfType<Aval>()
			              	where phaseStatus.name == "status"
			              	select ((CGuid) ((SQuestLogPhaseStatus) (phaseStatus.value)).value["guid"]).value
			              ).First() == id
			        select statusBlock).First();
		}
	}
}
