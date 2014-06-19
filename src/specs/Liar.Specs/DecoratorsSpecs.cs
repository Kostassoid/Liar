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

namespace Kostassoid.Liar.Specs
{
	using System;
	using Machine.Specifications;
	using Generators.Base;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class DecoratorsSpecs
	{
		[Subject("Decorators")]
		[Tags("Unit")]
		public class when_alternating_instance_with_action
		{
			static Boo _value;

			Because of = () =>
			{
				_value = A<Boo>.As(_ => new Boo
					{
						A = A<int>.Any().Value
					})
					.With(v => v.A = 13)
					.Value;
			};

			It should_comply_with_rules = () => _value.A.ShouldEqual(13);
		}

		[Subject("Decorators")]
		[Tags("Unit")]
		public class when_filtering_values_with_predicate
		{
			static int[] _values;

			Because of = () =>
			{
				_values = A<int>.Any().Where(i => i % 2 == 0).Sequence.Take(100).ToArray();
			};

			It should_comply_with_rules = () => _values.ShouldEachConformTo(i => i % 2 == 0);
		}

	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
