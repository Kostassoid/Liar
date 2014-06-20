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
			_builders [typeof(sbyte)] = s => (sbyte)s.GetNext ();
			_builders [typeof(byte)] = s => (byte)s.GetNext ();
			_builders [typeof(short)] = s => (short)s.GetNext ();
			_builders [typeof(ushort)] = s => (ushort)s.GetNext ();
			_builders [typeof(int)] = s => (int)s.GetNext ();
			_builders [typeof(uint)] = s => (uint)s.GetNext ();
			_builders [typeof(long)] = s => ((long)s.GetNext() << 32) + (long)s.GetNext();
			_builders [typeof(ulong)] = s => ((ulong)s.GetNext() << 32) + (ulong)s.GetNext();
			_builders [typeof(float)] = s => BitConverter.ToSingle(BitConverter.GetBytes(s.GetNext()), 0);
			_builders [typeof(double)] = s => BitConverter.Int64BitsToDouble(((long)s.GetNext() << 32) + (long)s.GetNext());
			_builders [typeof(decimal)] = s => BuildDecimal(s);
			_builders [typeof(bool)] = s => s.GetNext() % 2 == 0;
		}

		public T GetNext (NumericSource source)
		{
			return (T)BuildValue (source);
		}

		static object BuildValue(NumericSource source)
		{
			var t = typeof(T);

			Builder<object> builder;
			if (!_builders.TryGetValue (t, out builder))
			{
				throw new NotImplementedException (string.Format ("No value builder found for [{0}].", t.Name));
			}

			return builder (source);
		}

		static decimal BuildDecimal(NumericSource source)
		{
			return 1;
		}
	}
}

