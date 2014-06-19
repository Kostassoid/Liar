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
	using Tools;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class ToolsSpecs
	{
		[Subject(typeof(ObjectExtensions))]
		[Tags("Unit")]
		public class when_cloning_value
		{
			static int _source;
			static int _cloned;

			Because of = () =>
			{
				_source = 55;
				_cloned = _source.Copy();
			};

			It should_copy_value = () => _cloned.ShouldEqual(_source);
		}

		[Subject(typeof(ObjectExtensions))]
		[Tags("Unit")]
		public class when_cloning_struct
		{
			struct Boo
			{
				// ReSharper disable NotAccessedField.Local
				public int A;
				public string B;
				public Guid C;
				// ReSharper restore NotAccessedField.Local
			}

			static Boo _source;
			static Boo _cloned;

			Because of = () =>
			{
				_source = new Boo
				{
					A = 13,
					B = "Zzz",
					C = Guid.NewGuid()
				};
				_cloned = _source.Copy();
			};

			It should_copy_value = () => _cloned.ShouldEqual(_source);

			It should_not_be_the_same_as_source = () => _cloned.ShouldNotBeTheSameAs(_source);
		}

		[Subject(typeof(ObjectExtensions))]
		[Tags("Unit")]
		public class when_cloning_object
		{
			class Boo
			{
				// ReSharper disable NotAccessedField.Local
				public int A;
				public string B;
				public Guid C;
				// ReSharper restore NotAccessedField.Local
			}

			static Boo _source;
			static Boo _cloned;

			Because of = () =>
			{
				_source = new Boo
				{
					A = 13,
					B = "Zzz",
					C = Guid.NewGuid()
				};
				_cloned = _source.Copy();
			};

			It should_clone_values = () =>
			{
				_cloned.A.ShouldEqual(_source.A);
				_cloned.B.ShouldEqual(_source.B);
				_cloned.C.ShouldEqual(_source.C);
			};

			It should_not_be_the_same_as_source = () => _cloned.ShouldNotBeTheSameAs(_source);
		}

		[Subject(typeof(ObjectExtensions))]
		[Tags("Unit")]
		public class when_deep_cloning_object
		{
			class Boo
			{
				// ReSharper disable NotAccessedField.Local
				public Boo Nested;
				// ReSharper restore NotAccessedField.Local
			}

			static Boo _source;
			static Boo _cloned;

			Because of = () =>
			{
				_source = new Boo
				{
					Nested = new Boo
					{
						Nested = new Boo()
					}
				};
				_cloned = _source.Copy();
			};

			It should_clone_values = () =>
			{
				_cloned.Nested.ShouldNotBeNull();
				_cloned.Nested.Nested.ShouldNotBeNull();
				_cloned.Nested.Nested.Nested.ShouldBeNull();
			};

			It should_not_be_the_same_as_source = () =>
			{
				_cloned.Nested.ShouldNotBeTheSameAs(_source.Nested);
				_cloned.Nested.Nested.ShouldNotBeTheSameAs(_source.Nested.Nested);
			};
		}

	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
