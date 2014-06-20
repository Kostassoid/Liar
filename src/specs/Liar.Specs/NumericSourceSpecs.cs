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
using Kostassoid.Liar.Sequence;
using System.Linq;
using System;

namespace Kostassoid.Liar.Specs
{
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class NumericSourceSpecs
	{
		[Subject(typeof(SequentialNumericSource))]
		[Tags("Unit")]
		public class when_iterating_over_sequential_source
		{
			static long[] _sequence;

			Because of = () =>
			{
				var source = new SequentialNumericSource(13);

				_sequence = Enumerable
					.Range(0, 1000)
					.Select(_ => (long)source.GetNext())
					.ToArray();
			};

			It should_be_ordered = () => _sequence.ShouldEqual(_sequence.OrderBy(i => i).ToArray());

			It should_be_sequential = () => _sequence.Sum().ShouldEqual(Enumerable.Range(13, 1000).Sum());

			It should_start_with_seed = () => _sequence[0].ShouldEqual(13);
		}

		[Subject(typeof(RandomNumericSource))]
		[Tags("Unit")]
		public class when_iterating_over_random_source
		{
			static long[] _sequence;

			Because of = () =>
			{
				var source = new RandomNumericSource(13);

				_sequence = Enumerable
					.Range(0, 1000)
					.Select(_ => (long)source.GetNext())
					.ToArray();
			};

			It should_not_be_ordered = () => _sequence.ShouldNotEqual(_sequence.OrderBy(i => i).ToArray());

			It should_not_be_sequential = () => _sequence.Sum().ShouldNotEqual(Enumerable.Range(13, 1000).Sum());
		}

		[Behaviors]
		public class NumericSourceBehavior
		{
			protected static Func<int, NumericSource> SourceBuilder;

			private It should_produce_same_sequence_from_same_seed = () =>
			{
				var source1 = SourceBuilder(13);
				var source2 = SourceBuilder(13);
				var sequence1 = Enumerable
					.Range(0, 1000)
					.Select(_ => source1.GetNext())
					.ToArray();
				var sequence2 = Enumerable
					.Range(0, 1000)
					.Select(_ => source2.GetNext())
					.ToArray();
				sequence1.ShouldEqual(sequence2);
			};

			private It should_produce_different_sequence_from_different_seed = () =>
			{
				var source1 = SourceBuilder(13);
				var source2 = SourceBuilder(666);
				var sequence1 = Enumerable
					.Range(0, 1000)
					.Select(_ => source1.GetNext())
					.ToArray();
				var sequence2 = Enumerable
					.Range(0, 1000)
					.Select(_ => source2.GetNext())
					.ToArray();
				sequence1.ShouldNotEqual(sequence2);
			};

			private It should_produce_same_sequence_after_reset = () =>
			{
				var source = SourceBuilder(13);
				var sequence1 = Enumerable
					.Range(0, 1000)
					.Select(_ => source.GetNext())
					.ToArray();
				source.Reset();
				var sequence2 = Enumerable
					.Range(0, 1000)
					.Select(_ => source.GetNext())
					.ToArray();
				sequence1.ShouldEqual(sequence2);
			};

		}

		[Subject(typeof(SequentialNumericSource))]
		[Tags("Unit")]
		public class when_generating_using_sequential_source
		{
			protected static Func<int, NumericSource> SourceBuilder;

			Establish context = () =>
			{
				SourceBuilder = seed => new SequentialNumericSource(seed);
			};

			#pragma warning disable 0169
			Behaves_like<NumericSourceBehavior> correct_numeric_source;
			#pragma warning restore 0169
		}

		[Subject(typeof(RandomNumericSource))]
		[Tags("Unit")]
		public class when_generating_using_random_source
		{
			protected static Func<int, NumericSource> SourceBuilder;

			Establish context = () =>
			{
				SourceBuilder = seed => new RandomNumericSource(seed);
			};

			#pragma warning disable 0169
			Behaves_like<NumericSourceBehavior> correct_numeric_source;
			#pragma warning restore 0169
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
