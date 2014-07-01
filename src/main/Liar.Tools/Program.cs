using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO.Compression;
using Kostassoid.Liar.Randomization;
using Kostassoid.Liar.Generators.Text;

namespace Liar.Tools
{
	class MainClass
	{
		public static void ShowUsage()
		{
			Console.WriteLine ("Liar Tools\n");
			Console.WriteLine ("Create dictionary: -c <dictionary.file> <source.file>");
			Console.WriteLine ("Generate: -g <dictionary.file> <length>");
		}

		static void Create (string dictionaryFile, string sourceFile)
		{
			var dictionary = new ChainDictionary ();

			dictionary.LearnFromFile (sourceFile, new WordSplitter());
			dictionary.SaveToFile (dictionaryFile);
		}

		static void Generate (string dictionaryFile, int length)
		{
			var dictionary = ChainDictionaryHelper.LoadFromFile (dictionaryFile);

			Console.WriteLine(dictionary.Generate (length, new DefaultRandomSource()));
		}

		public static void Main (string[] args)
		{
			if (args.Length != 3) {
				ShowUsage ();
				return;
			}

			var command = args [0];
			var dictionaryFile = args [1];

			switch (command) {
			case "-c":
				Create (dictionaryFile, args [2]);
				break;
			case "-g":
				Generate (dictionaryFile, int.Parse (args [2]));
				break;
			default:
				ShowUsage ();
				break;
			}
		}
	}
}
