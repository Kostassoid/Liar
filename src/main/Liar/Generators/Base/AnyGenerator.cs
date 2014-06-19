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

namespace Kostassoid.Liar.Generators.Base
{
	using System;
	using Generators;
	using Sequence;

	public class AnyGenerator<T> : IGenerator<T>
	{
		public T GetNext (NumericSource source)
		{
			return (T)BuildValue (source);
		}

		// TODO: refactor
		object BuildValue(NumericSource source)
		{
			var t = typeof(T);

			if (t == typeof(sbyte))
			{
				return (sbyte)source.GetNext();
			}

			if (t == typeof(byte))
			{
				return (byte)source.GetNext();
			}

			if (t == typeof(short))
			{
				return (short)source.GetNext();
			}

			if (t == typeof(ushort))
			{
				return (ushort)source.GetNext();
			}

			if (t == typeof(int))
			{
				return (int)source.GetNext();
			}

			if (t == typeof(uint))
			{
				return (uint)source.GetNext();
			}

			if (t == typeof(long))
			{
				return ((long)source.GetNext() << 32) + source.GetNext();
			}

			if (t == typeof(ulong))
			{
				return ((ulong)source.GetNext() << 32) + (ulong)source.GetNext();
			}

			if (t == typeof(bool))
			{
				return (bool)(source.GetNext() % 2 == 0);
			}

			throw new NotImplementedException (string.Format ("No value builder found for [{0}].", t.Name));
		}
	}
}

