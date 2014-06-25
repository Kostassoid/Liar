using System;
using System.Collections.Generic;
using System.Linq;

namespace Liar.Tools
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

		public string PickNext(Random random)
		{
			var l = random.NextDouble ();
			return Links
				.OrderBy (_ => random.Next())
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

