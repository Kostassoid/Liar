namespace Kostassoid.Liar
{
	using System;

	public interface IGenerator
	{
		object GetNext(GeneratorContext context);
	}

	public interface IGenerator<out T> : IGenerator
	{
		new T GetNext(GeneratorContext context);
	}

	public class GeneratorContext
	{
		
	}

	public class Int32Generator : IGenerator<int>
	{
		public int GetNext(GeneratorContext context)
		{
			throw new NotImplementedException();
		}

		object IGenerator.GetNext(GeneratorContext context)
		{
			return GetNext(context);
		}
	}
}