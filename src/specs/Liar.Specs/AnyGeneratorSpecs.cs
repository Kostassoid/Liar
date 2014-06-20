// Copyright 2014 Konstantin Alexandroff
//   
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//  
//      http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System.Linq;
using Kostassoid.Liar.Syntax;

namespace Kostassoid.Liar.Specs
{
	using System;
	using Machine.Specifications;
	using Generators.Base;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class AnyGeneratorSpecs
	{
		const int SampleCount = 50000;
		const int UniqueCount = 100;

		[Behaviors]
		public class NumericGeneratorBehavior<T>
			where T : struct, 
		IComparable, 
		IComparable<T>, 
		IConvertible, 
		IEquatable<T>, 
		IFormattable
		{
			protected static T[] Values;
			protected static T Min;
			protected static T Max;

			It should_generate_different_values =
				() => Values.Distinct ().Count ().ShouldBeGreaterThan (UniqueCount);

			It should_generate_spread_values = () =>
			{
				var margin = (T)(((dynamic)Max / 4 - (dynamic)Min / 4));
				/*
				Console.WriteLine("Margin: {0}", margin);
				Console.WriteLine("Comparing min {0} with {1} = {2}",
					Values.Min(), (T)((dynamic)Min + margin), Values.Min().CompareTo((T)((dynamic)Min + margin)));
				Console.WriteLine("Comparing max {0} with {1} = {2}",
					Values.Max(), (T)((dynamic)Max - margin), Values.Max().CompareTo((T)((dynamic)Max - margin)));
				*/
				Values.Min().CompareTo((T)((dynamic)Min + margin)).ShouldBeLessThan(0);
				Values.Max().CompareTo((T)((dynamic)Max - margin)).ShouldBeGreaterThan(0);
			};
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_sbyte
		{
			protected static sbyte[] Values;
			protected static sbyte Min;
			protected static sbyte Max;

			Because of = () =>
			{
				Values = A<sbyte>.Any().Sequence.Take(SampleCount).ToArray();
				Min = sbyte.MinValue;
				Max = sbyte.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<sbyte>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_byte
		{
			protected static byte[] Values;
			protected static byte Min;
			protected static byte Max;

			Because of = () =>
			{
				Values = A<byte>.Any().Sequence.Take(SampleCount).ToArray();
				Min = byte.MinValue;
				Max = byte.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<byte>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_short
		{
			protected static short[] Values;
			protected static short Min;
			protected static short Max;

			Because of = () =>
			{
				Values = A<short>.Any().Sequence.Take(SampleCount).ToArray();
				Min = short.MinValue;
				Max = short.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<short>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_ushort
		{
			protected static ushort[] Values;
			protected static ushort Min;
			protected static ushort Max;

			Because of = () =>
			{
				Values = A<ushort>.Any().Sequence.Take(SampleCount).ToArray();
				Min = ushort.MinValue;
				Max = ushort.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<ushort>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_int
		{
			protected static int[] Values;
			protected static int Min;
			protected static int Max;

			Because of = () =>
			{
				Values = A<int>.Any().Sequence.Take(SampleCount).ToArray();
				Min = int.MinValue;
				Max = int.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<int>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_uint
		{
			protected static uint[] Values;
			protected static uint Min;
			protected static uint Max;

			Because of = () =>
			{
				Values = A<uint>.Any().Sequence.Take(SampleCount).ToArray();
				Min = uint.MinValue;
				Max = uint.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<uint>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_long
		{
			protected static long[] Values;
			protected static long Min;
			protected static long Max;

			Because of = () =>
			{
				Values = A<long>.Any().Sequence.Take(SampleCount).ToArray();
				Min = long.MinValue;
				Max = long.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<long>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_ulong
		{
			protected static ulong[] Values;
			protected static ulong Min;
			protected static ulong Max;

			Because of = () =>
			{
				Values = A<ulong>.Any().Sequence.Take(SampleCount).ToArray();
				Min = ulong.MinValue;
				Max = ulong.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<ulong>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_decimal
		{
			protected static decimal[] Values;
			protected static decimal Min;
			protected static decimal Max;

			Because of = () =>
			{
				Values = A<decimal>.Any().Sequence.Take(SampleCount).ToArray();
				Min = decimal.MinValue;
				Max = decimal.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<decimal>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_float
		{
			protected static float[] Values;
			protected static float Min;
			protected static float Max;

			Because of = () =>
			{
				Values = A<float>.Any().Sequence.Take(SampleCount).ToArray();
				Min = float.MinValue;
				Max = float.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<float>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_double
		{
			protected static double[] Values;
			protected static double Min;
			protected static double Max;

			Because of = () =>
			{
				Values = A<double>.Any().Sequence.Take(SampleCount).ToArray();
				Min = double.MinValue;
				Max = double.MaxValue;
			};

			#pragma warning disable 0169
			Behaves_like<NumericGeneratorBehavior<double>> numeric_generator;
			#pragma warning restore 0169
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_guid
		{
			protected static Guid[] Values;

			Because of = () =>
			{
				Values = A<Guid>.Any().Sequence.Take(SampleCount).ToArray();
			};

			It should_generate_different_values =
				() => Values.Distinct ().Count ().ShouldBeGreaterThan (UniqueCount);
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
