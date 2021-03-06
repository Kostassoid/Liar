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

using System.Linq;

namespace Kostassoid.Liar.Specs
{
	using System;
	using Generators.Base;
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class EmptyGeneratorSpecs
	{
		[Subject(typeof(EmptyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_empty_value_type
		{
			static int _value;

			Because of = () =>
			{
				_value = A<int>.Empty().Value;
			};

			It should_be_default = () => _value.ShouldEqual(default(int));
		}

		[Subject(typeof(EmptyGenerator<>))]
		[Tags("Unit")]
		public class when_generating_empty_ref_type
		{
			static string _value;

			Because of = () =>
			{
				_value = A<string>.Empty().Value;
			};

			It should_be_null = () => _value.ShouldBeNull();
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
