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
using System.Linq;
using Kostassoid.Liar.Randomization;
using Kostassoid.Liar.Generators;

namespace Liar.Generators.Text
{
	[Serializable]
	public class ChainItem
	{
		public string Value { get; private set; }

		public bool CanStart { get; set; }
		public bool CanFinish { get; set; }

		public IList<ChainLink> Links { get; set; }

		public ChainItem (string value)
		{
			Value = value;
			Links = new List<ChainLink> ();
		}

		public string PickNext(IRandomSource random)
		{
			Console.WriteLine ("Picking next for {0} from {1}.", Value, Links.Count);

			var l = Builders.Build<uint>(random) / (double)uint.MaxValue;
			return Links
				.OrderBy (_ => Builders.Build<int>(random))
				.Where (i => i.Probability > l)
				.Select(i => i.Value)
				.FirstOrDefault ();
		}

		public void Register(string value)
		{
			var existing = Links.FirstOrDefault(i => i.Value == value);
			if (existing == null)
			{
				existing = new ChainLink (value);
				Links.Add (existing);
			}

			existing.Probability++;
		}

		public void Normalize ()
		{
			if (!Links.Any ())
				return;

			double max = Links.Max (l => l.Probability);
			foreach (var l in Links)
			{
				l.Probability = l.Probability / max;
			}
		}
	}
}

