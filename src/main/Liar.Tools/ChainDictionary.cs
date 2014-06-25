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
		}

		public string Generate(int maxCount)
		{
			var r = new Random ();

			var current = _items.Values.Where (i => i.CanStart).OrderBy(_ => Guid.NewGuid()).First();
			var result = new StringBuilder(current.Value);
			int count = 1;

			while (count < maxCount)
			{
				var next = current.PickNext(r);
				if (next == null)
					break;

				current = _items[next];

				result.Append(" " + current.Value);
				count++;
			}

			return result.ToString ();
		}

	}
}

