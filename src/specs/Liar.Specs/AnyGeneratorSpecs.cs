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
		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_sbyte
		{
			static sbyte[] _values;

			Because of = () =>
			{
				_values = A<sbyte>.Any().Sequence.Take(100).ToArray();
			};

			It should_generate_different_values =
				() => _values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				_values.Min ().ShouldBeLessThan (sbyte.MinValue / 2);
				_values.Max ().ShouldBeGreaterThan (sbyte.MaxValue / 2);
			};
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_byte
		{
			static byte[] _values;

			Because of = () =>
			{
				_values = A<byte>.Any().Sequence.Take(100).ToArray();
			};

			It should_generate_different_values =
				() => _values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				_values.Min ().ShouldBeLessThan (byte.MaxValue / 4);
				_values.Max ().ShouldBeGreaterThan (byte.MaxValue / 4 * 3);
			};
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_short
		{
			static short[] _values;

			Because of = () =>
			{
				_values = A<short>.Any().Sequence.Take(100).ToArray();
			};

			It should_generate_different_values =
				() => _values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				_values.Min ().ShouldBeLessThan (short.MinValue / 2);
				_values.Max ().ShouldBeGreaterThan (short.MaxValue / 2);
			};
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_int
		{
			static int[] _values;

			Because of = () =>
			{
				_values = A<int>.Any().Sequence.Take(100).ToArray();
			};

			It should_generate_different_values =
				() => _values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				_values.Min ().ShouldBeLessThan (int.MinValue / 2);
				_values.Max ().ShouldBeGreaterThan (int.MaxValue / 2);
			};
		}

		[Subject(typeof(AnyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_any_long
		{
			static long[] _values;

			Because of = () =>
			{
				_values = A<long>.Any().Sequence.Take(100).ToArray();
			};

			It should_generate_different_values =
				() => _values.Distinct ().Count ().ShouldBeGreaterThan (10);

			It should_generate_spread_values = () =>
			{
				_values.Min ().ShouldBeLessThan (long.MinValue / 2);
				_values.Max ().ShouldBeGreaterThan (long.MaxValue / 2);
			};
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
