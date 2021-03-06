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

namespace Kostassoid.Liar.Generators
{
	using System;
	using Randomization;

	public class WithDecorator<T> : IGenerator<T>
	{
		readonly IGenerator<T> _generator;
		readonly Action<T> _withAction;

		public WithDecorator(IGenerator<T> generator, Action<T> withAction)
		{
			_generator = generator;
			_withAction = withAction;
		}

		public T GetNext(IRandomSource sequence)
		{
			var value = _generator.GetNext(sequence);
			_withAction(value);
			return value;
		}
	}
}