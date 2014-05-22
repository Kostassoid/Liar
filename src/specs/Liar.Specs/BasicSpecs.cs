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
		[Subject(typeof(Imagine<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_default_int
		{
			static int _value;

			Because of = () =>
			{
				_value = Imagine<int>.Default();
			};

			It should_be_deafult = () => _value.ShouldEqual(default(int));

		}

		[Subject(typeof(Imagine<>), "Basic")]
		[Tags("Unit")]
		public class when_generating_ints_using_template
		{
			static int _value;

			Because of = () =>
			{
				_value = Imagine<int>.Like(33);
			};

			It should_equal_template_value = () => _value.ShouldEqual(33);

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
