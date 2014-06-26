// Copyright 2014 Konstantin Alexandroff
//   
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//  
//      http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Compression;
using Kostassoid.Liar.Randomization;
using Kostassoid.Liar.Generators;

namespace Liar.Generators.Text
{
	[Serializable]
	public class ChainDictionary
	{
		IDictionary<string, ChainItem> _items;
		string _separator;

		public ChainDictionary ()
		{
			_items = new Dictionary<string, ChainItem> ();
		}

		public ChainItem GetOrAdd(string value)
		{
			ChainItem item;
			if (!_items.TryGetValue (value, out item)) {
				item = new ChainItem (value);
				_items [value] = item;
			}
			return item;
		}

		public void Normalize()
		{
			foreach (var item in _items.Values) {
				item.Normalize ();
			}

			Console.WriteLine("Total : {0}, CanStart: {1}, CanFinish: {2}",
				_items.Count,
				_items.Values.Where(i => i.CanStart).Count(),
				_items.Values.Where(i => i.CanFinish).Count()
			);

			foreach (var i in _items.Values.Where(i => i.CanStart).Take(10)) {
				Console.WriteLine ("CanStart: {0}", i.Value);
			}
		}

		public void LearnFromFile(string filepath, string separator = ".")
		{
			Learn (File.ReadLines (filepath), separator);
		}

		public void LearnFromText(IEnumerable<string> text, string separator)
		{
			Learn (text);
		}

		void Learn(IEnumerable<string> lines, string separator = ".")
		{
			_items.Clear ();
			_separator = separator;

			foreach (var l in lines)
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

					var next = GetOrAdd (word);

					if (!atStart) {
						current.Register (next.Value);
					}
					else
					{
						if (char.IsUpper(next.Value[0]))
							next.CanStart = true;

						atStart = false;
					}

					current = next;
				}
			}

			Normalize ();
		}


		public void SaveToFile(string dictionaryFile)
		{
			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new FileStream (dictionaryFile, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (GZipStream compressionStream = new GZipStream (stream, CompressionMode.Compress))
				{
					formatter.Serialize (compressionStream, this);
				}
				stream.Close ();
			}
		}

		public static ChainDictionary LoadFromFile(string dictionaryFile)
		{
			ChainDictionary dictionary;
			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new FileStream (dictionaryFile, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				using (GZipStream compressionStream = new GZipStream (stream, CompressionMode.Decompress))
				{
					dictionary = (ChainDictionary)formatter.Deserialize (compressionStream);
				}
				stream.Close ();
			}
			return dictionary;
		}

		public string Generate(int maxLength, IRandomSource random)
		{
			ChainItem current = null;
			var result = new StringBuilder();
			int count = 0;

			while (count < maxLength)
			{
				if (current == null)
				{
					current = _items.Values.Where (i => i.CanStart).OrderBy (_ => Builders.Build<int>(random)).First ();
					result.Append (current.Value);
					count++;
					continue;
				}

				if (current.CanFinish && Builders.Build<byte>(random) % 10 < 3)
				{
					result.Append (". ");
					current = null;
					continue;
				}

				var next = current.PickNext(random);
				if (next == null)
				{
					result.Append (". ");
					current = null;
					continue;
				}

				current = _items [next];
				result.Append (" " + current.Value);
				count++;
			}

			return result.ToString ();
		}

	}
}

