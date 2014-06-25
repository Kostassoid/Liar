using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

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

			foreach (var l in File.ReadLines (sourceFile))
			{
				var matches = Regex.Matches(l, @"\w+[^\s]*\w+|\w|\.");

				bool atStart = true;
				ChainItem current = null;
				foreach (Match match in matches) {
					var word = match.Value;

					if (word == ".") {
						if (current != null) {
							current.CanFinish = true;
						}
						atStart = true;
						continue;
					}

					var next = dictionary.GetOrAdd (word);

					if (atStart) {
						next.CanStart = true;
						continue;
					}

					if (current != null) {
						current.Register (next.Value);
					}

					current = next;
				}
			}

			SaveDictionary (dictionary, dictionaryFile);
		}

		static void SaveDictionary(ChainDictionary dictionary, string dictionaryFile)
		{
			dictionary.Normalize ();

			/*
			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new FileStream (dictionaryFile, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				formatter.Serialize (stream, dictionary);
				stream.Close ();
			}
			*/

			using (var writer = new System.IO.StreamWriter(dictionaryFile))
			{
				var serializer = new XmlSerializer(dictionary.GetType());
				//var serializer = new DataContractSerializer(typeof(ChainDictionary));

				serializer.Serialize(writer, dictionary);
				writer.Flush();
			}
		}

		static ChainDictionary LoadDictionary(string dictionaryFile)
		{
			/*
			ChainDictionary dictionary;
			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new FileStream (dictionaryFile, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				dictionary = (ChainDictionary)formatter.Deserialize(stream);
				stream.Close ();
			}
			return dictionary;
			*/

			using (var stream = System.IO.File.OpenRead(dictionaryFile))
			{
				var serializer = new XmlSerializer(typeof(ChainDictionary));
				return serializer.Deserialize(stream) as ChainDictionary;
			}
		}

		static void Generate (string dictionaryFile, int length)
		{
			var dictionary = LoadDictionary (dictionaryFile);

			dictionary.Generate (length);
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
