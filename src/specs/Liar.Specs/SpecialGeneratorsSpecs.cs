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
	using Generators;
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class SpecialGeneratorsSpecs
	{
		[Subject(typeof(PinCodeGenerator))]
		[Tags("Unit")]
		public class when_generating_pincode
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
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
