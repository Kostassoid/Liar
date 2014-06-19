namespace Kostassoid.Liar.Generators
{
	using System;
	using Sequence;

	public class WithDecorator<T> : IGenerator<T>
	{
		readonly IGenerator<T> _generator;
		readonly Action<T> _withAction;

		public WithDecorator(IGenerator<T> generator, Action<T> withAction)
		{
			_generator = generator;
			_withAction = withAction;
		}

		public T GetNext(SequenceGenerator sequence)
		{
			var value = _generator.GetNext(sequence);
			_withAction(value);
			return value;
		}
	}
}