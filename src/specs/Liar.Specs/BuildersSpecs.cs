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
using Kostassoid.Liar.Generators;

namespace Kostassoid.Liar.Specs
{
	using System;
	using Machine.Specifications;
	using Generators.Base;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class BuildersSpecs
	{
		[Subject(typeof(Builders))]
		[Tags("Unit")]
		public class when_building_non_registered_type
		{
			static Exception _exception;

			Because of = () =>
			{
				_exception = Catch.Exception(() => Builders.Build<Boo>(Session.Current.Source));
			};

			It should_throw = () => _exception.ShouldBeOfExactType<NotSupportedException> ();
		}

		[Subject(typeof(Builders))]
		[Tags("Unit")]
		public class when_building_using_custom_registered_builder
		{
			static Boo _value;

			Because of = () =>
			{
				Builders.Register<Boo>(s => new Boo {
					A = 13
				});

				_value = Builders.Build<Boo>(Session.Current.Source);
			};

			It should_build_value = () => _value.A.ShouldEqual (13);
		}

		[Subject(typeof(Builders))]
		[Tags("Unit")]
		public class when_resetting_builders
		{
			static Exception _exception;

			Establish context = () =>
			{
				Builders.Register<Boo> (s => new Boo {
					A = 13
				});
			};

			Because of = () =>
			{
				Builders.Reset();

				_exception = Catch.Exception(() => Builders.Build<Boo>(Session.Current.Source));
			};

			It should_reset_builders = () => _exception.ShouldBeOfExactType<NotSupportedException> ();
		}
	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
