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

namespace Kostassoid.Liar.Generators.Base
{
	using Generators;
	using Sequence;

	public class BuilderGenerator<T> : IGenerator<T>
	{
		Builder<T> _builder;

		public BuilderGenerator (Builder<T> builder)
		{
			_builder = builder;
		}

		public T GetNext (NumericSource source)
		{
			return _builder(source);
		}
	}
}

