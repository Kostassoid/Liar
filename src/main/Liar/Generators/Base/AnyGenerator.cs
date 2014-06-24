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

using System.Collections.Generic;
using System.Linq;

namespace Kostassoid.Liar.Generators.Base
{
	using System;
	using Generators;
	using Sequence;

	public class AnyGenerator<T> : IGenerator<T>
	{
		static IDictionary<Type, Builder<object>> _builders = new Dictionary<Type, Builder<object>> ();

		static AnyGenerator()
		{
			_builders [typeof(sbyte)] = s => (sbyte)Builders.BuildByte (s);
			_builders [typeof(byte)] = s => (byte)Builders.BuildByte (s);
			_builders [typeof(short)] = s => (short)Builders.BuildShort(s);
			_builders [typeof(ushort)] = s => (ushort)Builders.BuildShort(s);
			_builders [typeof(int)] = s => (int)Builders.BuildInt (s);
			_builders [typeof(uint)] = s => (uint)Builders.BuildInt (s);
			_builders [typeof(long)] = s => (long)Builders.BuildLong (s);
			_builders [typeof(ulong)] = s => (ulong)Builders.BuildLong (s);
			_builders [typeof(float)] = s => Builders.BuildFloat(s);
			_builders [typeof(double)] = s => Builders.BuildDouble(s);
			_builders [typeof(decimal)] = s => Builders.BuildDecimal(s);
			_builders [typeof(bool)] = s => Builders.BuildBoolean (s);
			_builders [typeof(Guid)] = s => Builders.BuildGuid(s);
		}

		public T GetNext (IRandomSource source)
		{
			return (T)BuildValue (source);
		}

		static object BuildValue(IRandomSource source)
		{
			var t = typeof(T);

			Builder<object> builder;
			if (!_builders.TryGetValue (t, out builder))
			{
				throw new NotImplementedException (string.Format ("No value builder found for [{0}].", t.Name));
			}

			return builder (source);
		}
	}
}

