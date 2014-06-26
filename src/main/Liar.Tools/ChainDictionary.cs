using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Liar.Tools
{
	[Serializable]
	public class ChainDictionary
	{
		IDictionary<string, ChainItem> _items;

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

		public string Generate(int maxCount)
		{
			var r = new Random ();

			ChainItem current = null;
			var result = new StringBuilder();
			int count = 0;

			while (count < maxCount)
			{
				if (current == null)
				{
					current = _items.Values.Where (i => i.CanStart).OrderBy (_ => Guid.NewGuid ()).First ();
					result.Append (current.Value);
					count++;
					continue;
				}

				if (current.CanFinish && r.Next (10) < 3)
				{
					result.Append (". ");
					current = null;
					continue;
				}

				var next = current.PickNext(r);
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

