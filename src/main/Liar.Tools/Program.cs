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
			Console.WriteLine ("Create dictionary: -c <dictionary.file> <source.file> <-s|-w>");
			Console.WriteLine ("Generate: -g <dictionary.file> <length>");
		}

		static void Create (string dictionaryFile, string sourceFile, string splitterCode)
		{
			var dictionary = new ChainDictionary ();

			var splitter = ResolveSplitter(splitterCode);

			dictionary.LearnFromFile (sourceFile, splitter);
			dictionary.SaveToFile (dictionaryFile);
		}

		static ISplitter ResolveSplitter(string arg)
		{
			switch (arg) {
			case "-s":
				return new WordSplitter ();
			case "-w":
				return new LetterSplitter ();
			default:
				throw new ArgumentException("Unknown splitter strategy: " + arg);
			}
		}

		static void Generate (string dictionaryFile, int length)
		{
			var dictionary = ChainDictionaryHelper.LoadFromFile (dictionaryFile);

			Console.WriteLine(dictionary.Generate (length, new DefaultRandomSource()));
		}

		public static void Main (string[] args)
		{
			if (args.Length < 3) {
				ShowUsage ();
				return;
			}

			var command = args [0];
			var dictionaryFile = args [1];

			switch (command) {
			case "-c":
				Create (dictionaryFile, args [2], args[3]);
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
