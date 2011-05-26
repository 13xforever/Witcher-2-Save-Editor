using System;
using System.IO;

namespace SaveFormat
{
	public static class Log
	{
		private static readonly StreamWriter log = new StreamWriter(File.Open(DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss'.log'"), FileMode.CreateNew, FileAccess.Write, FileShare.Read));
		private static string buffer;

		public static void Write(string message, params object[] args)
		{
			var line = string.Format(message, args);
			lock (log)
			{
				Console.Write(line);
				buffer = line;
			}
		}

		public static void Success(string message, params object[] args)
		{
			WriteLine(ConsoleColor.Green, false, message, args);
		}

		public static void Warning(string message, params object[] args)
		{
			WriteLine(ConsoleColor.Yellow, true, message, args);
		}
	
		public static void Error(string message, params object[] args)
		{
			WriteLine(ConsoleColor.Red, true, message, args);
		}

		private static void WriteLine(ConsoleColor color, bool logToFile, string message, params object[] args)
		{
			var line = string.Format(message, args);
			lock(log)
			{
				var oldColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
				Console.WriteLine(line);
				Console.ForegroundColor = oldColor;
				if (logToFile)
				{
					log.WriteLine(buffer + line);
					log.Flush();
				}
				buffer = null;
			}
		}
	}
}