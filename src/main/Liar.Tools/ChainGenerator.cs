using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Liar.Tools
{
	public class ChainGenerator
	{
		IList<ChainItem> _items;

		string _separator;

		public ChainGenerator (IList<ChainItem> items, string separator)
		{
			_items = items;
			_separator = separator;
		}

		public string Generate(int maxCount)
		{
			var r = new Random ();
			var result = new StringBuilder();
			int count = 0;

			/*
			var current = _root;

			while (count < maxCount)
			{
				current = current.PickNext(r);
				if (current == null)
					break;

				result.Append(current.Value + _separator);
				count++;
			}
			*/

			return result.ToString ();
		}
	}
}

