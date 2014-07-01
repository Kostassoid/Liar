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
using System.IO;
using System.IO.Compression;

namespace Kostassoid.Liar.Generators.Text
{
	public static class ChainDictionaryHelper
	{
		public static ChainDictionary LoadFromFile(string filePath)
		{
			ChainDictionary dictionary;
			using (Stream stream = new FileStream (filePath, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				using (GZipStream compressionStream = new GZipStream (stream, CompressionMode.Decompress))
				{
					dictionary = ChainDictionary.LoadFrom(compressionStream);
				}
				stream.Close ();
			}
			return dictionary;
		}

		public static void SaveToFile(this ChainDictionary dictionary, string filePath)
		{
			using (Stream stream = new FileStream (filePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (GZipStream compressionStream = new GZipStream (stream, CompressionMode.Compress))
				{
					dictionary.SaveTo (compressionStream);
				}
				stream.Close ();
			}
		}

		public static void LearnFromFile(this ChainDictionary dictionary, string filepath, ISplitter splitter)
		{
			dictionary.LearnFrom(File.ReadLines (filepath), splitter);
		}
	}
}

