using System;
using System.Collections.Generic;
using System.Linq;

namespace Liar.Tools
{
	public static class EnumerableEx
	{
		public static IEnumerable<T> Random<T>(this IEnumerable<T> source)
		{
			return source.OrderBy (_ => Guid.NewGuid ());
		}

	}
}

