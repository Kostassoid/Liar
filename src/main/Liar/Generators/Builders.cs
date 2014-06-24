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

namespace Kostassoid.Liar.Generators
{
	using System;
	using Generators;
	using Sequence;

	public static class Builders
	{
		public static bool BuildBoolean(IRandomSource source)
		{
			return source.Next (1)[0] % 2 == 0;
		}

		public static byte BuildByte(IRandomSource source)
		{
			return source.Next (1)[0];
		}

		public static short BuildShort(IRandomSource source)
		{
			var b = source.Next (2);
			return (short)(((short)b[0] << 8) | b[1]);
		}

		public static int BuildInt(IRandomSource source)
		{
			var b = source.Next (4);
			return ((int)b[0] << 24) | ((int)b[1] << 16) | ((int)b[2] << 8) | b[3];
		}

		public static long BuildLong(IRandomSource source)
		{
			return ((long)BuildInt (source) << 32) | (uint)BuildInt (source);
		}

		public static decimal BuildDecimal(IRandomSource source)
		{
			byte scale = (byte)(source.Next(1)[0] % 29);
			bool sign = BuildBoolean(source);
			return new decimal(
				BuildInt(source), 
				BuildInt(source), 
				BuildInt(source), 
				sign,
				scale);
		}

		public static float BuildFloat(IRandomSource source)
		{
			float value;
			do
			{
				value = BitConverter.ToSingle (source.Next(4), 0);
			}
			while(float.IsNaN (value) || float.IsInfinity (value));

			return value;
		}

		public static double BuildDouble(IRandomSource source)
		{
			double value;
			do
			{
				value = BitConverter.ToDouble(source.Next(8), 0);
			}
			while(double.IsNaN (value) || double.IsInfinity (value));

			return value;
		}

		public static Guid BuildGuid(IRandomSource source)
		{
			return new Guid(source.Next(16));
		}
	}
}

