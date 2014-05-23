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

namespace Kostassoid.Liar.Tools
{
	using System;

	internal class ArrayTraverse
	{
		public int[] Position;
		readonly int[] _maxLengths;

		public ArrayTraverse(Array array)
		{
			_maxLengths = new int[array.Rank];
			for (int i = 0; i < array.Rank; ++i)
			{
				_maxLengths[i] = array.GetLength(i) - 1;
			}
			Position = new int[array.Rank];
		}

		public bool Step()
		{
			for (int i = 0; i < Position.Length; ++i)
			{
				if (Position[i] < _maxLengths[i])
				{
					Position[i]++;
					for (int j = 0; j < i; j++)
					{
						Position[j] = 0;
					}
					return true;
				}
			}
			return false;
		}
	}
}