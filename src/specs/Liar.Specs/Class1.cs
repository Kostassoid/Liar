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
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class BasicSpecs
	{
		[Subject(typeof(Imagine), "Basic")]
		[Tags("Unit")]
		public class when_generating_any_int
		{
			static int _value;

			Because of = () =>
			{
				_value = Imagine.Any<int>();
			};

			It should_not_be_deafult = () => _value.ShouldNotEqual(default(int));

		}

	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
