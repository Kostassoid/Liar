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

namespace Kostassoid.Liar.Generators
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class Registry
	{
		static readonly IDictionary<Type, IGenerator> Generators
			= new Dictionary<Type, IGenerator>();

		static Registry()
		{
			Register(new PrimitiveDefaultGenerators());
		}

		public static void Register(IGenerator generator)
		{
			foreach (var i in generator.GetType().GetInterfaces())
			{
				if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IGenerator<>))
				{
					continue;
				}

				var target = i.GetGenericArguments().First();
				Generators[target] = generator;
			}
		}

		public static T Generate<T>()
		{
			IGenerator generator;
			if (!Generators.TryGetValue(typeof (T), out generator))
			{
				throw new InvalidOperationException(string.Format("No default generator for type [{0}] is registered", typeof(T).Name));
			}

			return ((IGenerator<T>)generator).GetNext(Session.Current);
		}
	}
}