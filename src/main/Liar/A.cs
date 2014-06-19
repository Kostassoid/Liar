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

namespace Kostassoid.Liar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Generators.Base;
	using Syntax;

	public class A<T>
	{
		public static IAnySyntax<T> Any()
		{
			return new Specification<T>(new AnyGenerator<T>());
		}

		public static ISpecification<T> Empty()
		{
			return new Specification<T>(new EmptyGenerator<T>());
		}

		public static ISpecification<T> Like(T template)
		{
			return new Specification<T>(new TemplateGenerator<T>(template));
		}

		public static ISpecification<T> As(Builder<T> builder)
		{
			return new Specification<T>(new BuilderGenerator<T>(builder));
		}
	}
}
