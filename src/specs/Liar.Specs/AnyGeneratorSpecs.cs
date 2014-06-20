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
				() => Values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				var margin = (T)(((dynamic)Max / 4 - (dynamic)Min / 4));
				/*
				Console.WriteLine("Margin: {0}, comparing {1} with {2} = {3}",
					margin, Values.Min(), (T)((dynamic)Min + margin), Values.Min().CompareTo((T)((dynamic)Min + margin)));
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
				Values = A<sbyte>.Any().Sequence.Take(100).ToArray();
				Min = sbyte.MinValue;
				Max = sbyte.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<sbyte>> numeric_generator;
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
				Values = A<byte>.Any().Sequence.Take(100).ToArray();
				Min = byte.MinValue;
				Max = byte.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<byte>> numeric_generator;
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
				Values = A<short>.Any().Sequence.Take(100).ToArray();
				Min = short.MinValue;
				Max = short.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<short>> numeric_generator;
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
				Values = A<ushort>.Any().Sequence.Take(100).ToArray();
				Min = ushort.MinValue;
				Max = ushort.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<ushort>> numeric_generator;
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
				Values = A<int>.Any().Sequence.Take(100).ToArray();
				Min = int.MinValue;
				Max = int.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<int>> numeric_generator;
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
				Values = A<uint>.Any().Sequence.Take(100).ToArray();
				Min = uint.MinValue;
				Max = uint.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<uint>> numeric_generator;
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
				Values = A<long>.Any().Sequence.Take(100).ToArray();
				Min = long.MinValue;
				Max = long.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<long>> numeric_generator;
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
				Values = A<ulong>.Any().Sequence.Take(100).ToArray();
				Min = ulong.MinValue;
				Max = ulong.MaxValue;
			};

			Behaves_like<NumericGeneratorBehavior<ulong>> numeric_generator;
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
