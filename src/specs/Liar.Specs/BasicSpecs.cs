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

namespace Kostassoid.Liar.Specs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Generators;
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class BasicSpecs
	{
		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_default_int
		{
			static int _value;

			Because of = () =>
			{
				_value = A<int>.Empty().Value;
				/*
				 * _v = A<int>.Any().Value;
				 * _v = A<int>.Default().Value;
				 * _v = A<int>.Any().Between(10, 300).Value;
				 * _v = A<int>.Like(66).Value;
				 * _v = A<int>.Any().Between(5, 55).Sequence;
				 * _v = A<int>.As(i => i).Value;
				 * _v = A<int>.As().Pincode().Value;
				 * _v = A<int>.Any().Over(() => Session.Generator).Value;
				 */
			};

			It should_be_deafult = () => _value.ShouldEqual(default(int));

		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_value_instance_using_template
		{
			static int _value;

			Because of = () =>
			{
				_value = A<int>.Like(33).Value;
			};

			It should_equal_template_value = () => _value.ShouldEqual(33);

		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_ref_instance_using_template
		{
			static Boo _value;
			static Boo _template;

			Because of = () =>
			{
				_template = new Boo
				{
					A = 13,
					B = "this",
					C = Guid.NewGuid()
				};

				_value = A<Boo>.Like(_template).Value;
			};

			It should_equal_template_value = () =>
			{
				_value.A.ShouldEqual(_template.A);
				_value.B.ShouldEqual(_template.B);
				_value.C.ShouldEqual(_template.C);
			};
		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_ints_using_generator
		{
			static int _value;

			Because of = () =>
			{
				_value = A<int>.Any().PinCode().Value;
			};

			It should_comply_with_rules = () =>
			{
				_value.ShouldBeGreaterThanOrEqualTo(1000);
				_value.ShouldBeLessThan(10000);
			};

		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_ints_using_rules
		{
			static Boo _value;

			Because of = () =>
			{
				_value = A<Boo>.As(_ => new Boo
					{
						A = A<int>.Any().Value
					})
					.Value;
			};

			It should_comply_with_rules = () => _value.A.ShouldNotEqual(default(int));
		}

		public class Boo
		{
			public int A { get; set; }
			public string B { get; set; }
			public Guid C { get; set; }
		}

	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
