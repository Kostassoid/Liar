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

	public class PrimitiveDefaultGenerators :
		IGeneratorOf<bool>,
		IGeneratorOf<byte>,
		IGeneratorOf<short>,
		IGeneratorOf<ushort>,
		IGeneratorOf<int>,
		IGeneratorOf<uint>,
		IGeneratorOf<string>
	{
		int IGeneratorOf<int>.GetNext(Session session)
		{
			return session.Random.Next(int.MinValue, int.MaxValue);
		}

		uint IGeneratorOf<uint>.GetNext(Session session)
		{
			return (uint)session.Random.Next(int.MinValue, int.MaxValue);
		}

		byte IGeneratorOf<byte>.GetNext(Session session)
		{
			return (byte)session.Random.Next(byte.MinValue, byte.MaxValue);
		}

		short IGeneratorOf<short>.GetNext(Session session)
		{
			return (short)session.Random.Next(short.MinValue, short.MaxValue);
		}

		ushort IGeneratorOf<ushort>.GetNext(Session session)
		{
			return (ushort)session.Random.Next(ushort.MinValue, ushort.MaxValue);
		}

		public bool GetNext(Session session)
		{
			return session.Random.Next(2) == 0;
		}

		string IGeneratorOf<string>.GetNext(Session session)
		{
			return Guid.NewGuid().ToString("n");
		}
	}
}