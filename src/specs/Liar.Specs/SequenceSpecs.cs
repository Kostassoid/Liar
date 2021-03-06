﻿// Copyright 2014 Konstantin Alexandroff
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

namespace Kostassoid.Liar.Specs
{
	using System.Collections.Generic;
	using System.Linq;
	using Generators;
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class SequenceSpecs
	{
		[Subject(typeof(A<>), "Sequence")]
		[Tags("Unit")]
		public class when_generating_sequence
		{
			static IList<int> _values;

			Because of = () =>
			{
				_values = A<int>.Any().Sequence.Take(10).ToList();
			};

			It should_produce_different_values = () => _values.Distinct().Count().ShouldBeGreaterThan(1);

			It should_produce_enough_values = () => _values.Count.ShouldEqual(10);
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
