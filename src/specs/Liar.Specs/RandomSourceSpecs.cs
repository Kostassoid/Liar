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

using Kostassoid.Liar.Randomization;
using System.Linq;
using System;

namespace Kostassoid.Liar.Specs
{
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class RandomSourceSpecs
	{
		[Subject(typeof(DefaultRandomSource))]
		[Tags("Unit")]
		public class when_iterating_over_random_source
		{
			static long[] _sequence;

			Because of = () =>
			{
				var source = new DefaultRandomSource(13);

				_sequence = source.Next(100).Select(b => (long)b).ToArray();
			};

			It should_not_be_ordered = () => _sequence.ShouldNotEqual(_sequence.OrderBy(i => i).ToArray());

			It should_not_be_sequential = () => _sequence.Sum().ShouldNotEqual(Enumerable.Range(13, 100).Sum());
		}

		[Behaviors]
		public class NumericSourceBehavior
		{
			protected static Func<int, IRandomSource> SourceBuilder;

			private It should_produce_same_sequence_from_same_seed = () =>
			{
				var source1 = SourceBuilder(13);
				var source2 = SourceBuilder(13);
				var sequence1 = source1.Next(1000);
				var sequence2 = source2.Next(1000);
				sequence1.ShouldEqual(sequence2);
			};

			private It should_produce_different_sequence_from_different_seed = () =>
			{
				var source1 = SourceBuilder(13);
				var source2 = SourceBuilder(666);
				var sequence1 = source1.Next(1000);
				var sequence2 = source2.Next(1000);
				sequence1.ShouldNotEqual(sequence2);
			};

			private It should_produce_same_sequence_after_reset = () =>
			{
				var source = SourceBuilder(13);
				var sequence1 = source.Next(1000);
				source.Reset();
				var sequence2 = source.Next(1000);
				sequence1.ShouldEqual(sequence2);
			};

		}

		[Subject(typeof(DefaultRandomSource))]
		[Tags("Unit")]
		public class when_generating_using_random_source
		{
			protected static Func<int, IRandomSource> SourceBuilder;

			Establish context = () =>
			{
				SourceBuilder = seed => new DefaultRandomSource(seed);
			};

			#pragma warning disable 0169
			Behaves_like<NumericSourceBehavior> correct_numeric_source;
			#pragma warning restore 0169
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
