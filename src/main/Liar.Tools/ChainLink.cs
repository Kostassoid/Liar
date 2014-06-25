using System;

namespace Liar.Tools
{
	[Serializable]
	public class ChainLink
	{
		public string Value { get; private set; }

		public double Probability { get; set; }

		public ChainLink (string value)
		{
			this.Value = value;
		}
	}
}

