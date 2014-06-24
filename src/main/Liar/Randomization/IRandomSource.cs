using System;
using System.Collections.Generic;

namespace Kostassoid.Liar.Randomization
{
	public interface IRandomSource
	{
		void Reset();

		byte[] Next (int count);
	}
}

