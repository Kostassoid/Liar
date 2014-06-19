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

using Kostassoid.Liar.Generators;
using System.Collections.Generic;

namespace Kostassoid.Liar
{
	using System;
	using Syntax;

	internal class Specification<T> : ISpecification<T>
	{
		public IGenerator<T> Generator { get; set; }

		public T Value
		{
			get
			{
				return Generator.GetNext (Session.Current.Source);
			}
		}

		public IEnumerable<T> Sequence
		{
			get
			{
				for (;;)
				{
					yield return Generator.GetNext (Session.Current.Source);
				}
// ReSharper disable FunctionNeverReturns
			}
// ReSharper restore FunctionNeverReturns
		}

		public IValueSyntax<T> With(Action<T> withAction)
		{
			Generator = new WithDecorator<T>(Generator, withAction);

			return this;
		}

		public IValueSyntax<T> Where(Predicate<T> predicate)
		{
			Generator = new WhereDecorator<T>(Generator, predicate);

			return this;
		}

		public Specification (IGenerator<T> generator)
		{
			Generator = generator;
		}
	}
}

