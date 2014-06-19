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
	public class TemplateGeneratorSpecs
	{
		[Subject(typeof(TemplateGenerator<>))]
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

		[Subject(typeof(TemplateGenerator<>))]
		[Tags("Unit")]
		public class when_generating_class_instance_using_template
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
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
