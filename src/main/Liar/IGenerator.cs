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

namespace Kostassoid.Liar
{
	using System;

	public interface IGenerator
	{
		object GetNext(GeneratorContext context);
	}

	public interface IGenerator<out T> : IGenerator
	{
		new T GetNext(GeneratorContext context);
	}

	public class GeneratorContext
	{
		
	}

	public class Int32Generator : IGenerator<int>
	{
		public int GetNext(GeneratorContext context)
		{
			throw new NotImplementedException();
		}

		object IGenerator.GetNext(GeneratorContext context)
		{
			return GetNext(context);
		}
	}
}