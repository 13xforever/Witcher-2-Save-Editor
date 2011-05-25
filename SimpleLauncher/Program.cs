using System;
using System.Collections.Generic;
using System.Linq;
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
			var save = W2SaveReader.Read(@"D:\Documents\Witcher 2\gamesaves\QuickSave.sav");
			if (save.header != "SAVY") Console.WriteLine("Not a Witcher 2 save file.");

			var questEntries = save.section.First(s => s.name == "questLogBlock").data.children;

			var trackedEntryId = (CGuid)(questEntries.FindValueByName("trackedEntry").value);
			var trackedPhaseId = (CGuid)(questEntries.FindValueByName("trackedPhase").value);

			var entry = questEntries.GetQuestLogBlock(trackedEntryId.value);
			var phase = questEntries.GetQuestLogBlock(trackedPhaseId.value);

			Console.WriteLine("done");
			Console.ReadKey();
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
