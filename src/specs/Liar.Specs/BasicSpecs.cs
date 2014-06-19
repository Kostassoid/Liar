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
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class BasicSpecs
	{
		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_default_value
		{
			static int _value;

			Because of = () =>
			{
				_value = A<int>.Empty().Value;
			};

			It should_be_deafult = () => _value.ShouldEqual(default(int));

		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_any_value
		{
			static int _value1;
			static int _value2;
			static int _value3;

			Because of = () =>
			{
				_value1 = A<int>.Any().Value;
				_value2 = A<int>.Any().Value;
				_value3 = A<int>.Any().Value;
			};

			It should_generate_different_values =
				() => ((_value1 != _value2) || (_value1 != _value3)).ShouldBeTrue();
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

			It should_not_be_the_same_as_template = () =>
				_value.ShouldNotBeTheSameAs(_template);

			It should_equal_template_value = () =>
			{
				_value.A.ShouldEqual(_template.A);
				_value.B.ShouldEqual(_template.B);
				_value.C.ShouldEqual(_template.C);
			};
		}

		[Subject(typeof(A<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_instance_using_builder
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

		[Subject(typeof(A<>), "Basic")]
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
