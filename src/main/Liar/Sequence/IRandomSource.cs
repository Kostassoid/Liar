using System;
using System.Collections.Generic;

namespace Kostassoid.Liar
{
	public interface IRandomSource
	{
		void Reset();

		byte[] Next (int count);
	}
}

