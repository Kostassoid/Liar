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

using System.Collections.Generic;
using System.Linq;

namespace Kostassoid.Liar.Generators
{
	using System;
	using Generators;
	using Randomization;

	public static class Builders
	{
		static IDictionary<Type, Builder<object>> _builders = new Dictionary<Type, Builder<object>> ();

		static Builders()
		{
			Reset ();
		}

		public static void Reset()
		{
			_builders.Clear();
			_builders [typeof(sbyte)] = s => (sbyte)BuildByte (s);
			_builders [typeof(byte)] = s => (byte)BuildByte (s);
			_builders [typeof(short)] = s => (short)BuildShort(s);
			_builders [typeof(ushort)] = s => (ushort)BuildShort(s);
			_builders [typeof(int)] = s => (int)BuildInt (s);
			_builders [typeof(uint)] = s => (uint)BuildInt (s);
			_builders [typeof(long)] = s => (long)BuildLong (s);
			_builders [typeof(ulong)] = s => (ulong)BuildLong (s);
			_builders [typeof(float)] = s => BuildFloat(s);
			_builders [typeof(double)] = s => BuildDouble(s);
			_builders [typeof(decimal)] = s => BuildDecimal(s);
			_builders [typeof(bool)] = s => BuildBoolean (s);
			_builders [typeof(Guid)] = s => BuildGuid(s);
		}

		public static void Register<T>(Builder<T> builder)
		{
			// TODO: not nice
			_builders [typeof(T)] = s => builder(s);
		}

		public static T Build<T>(IRandomSource source)
		{
			var t = typeof(T);

			Builder<object> builder;
			if (!_builders.TryGetValue (t, out builder))
			{
				throw new NotSupportedException (string.Format ("No value builder found for [{0}].", t.Name));
			}

			return (T) builder (source);
		}

		static bool BuildBoolean(IRandomSource source)
		{
			return source.Next (1)[0] % 2 == 0;
		}

		static byte BuildByte(IRandomSource source)
		{
			return source.Next (1)[0];
		}

		static short BuildShort(IRandomSource source)
		{
			var b = source.Next (2);
			return (short)(((short)b[0] << 8) | b[1]);
		}

		static int BuildInt(IRandomSource source)
		{
			var b = source.Next (4);
			return ((int)b[0] << 24) | ((int)b[1] << 16) | ((int)b[2] << 8) | b[3];
		}

		static long BuildLong(IRandomSource source)
		{
			return ((long)BuildInt (source) << 32) | (uint)BuildInt (source);
		}

		static decimal BuildDecimal(IRandomSource source)
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

		static float BuildFloat(IRandomSource source)
		{
			float value;
			do
			{
				value = BitConverter.ToSingle (source.Next(4), 0);
			}
			while(float.IsNaN (value) || float.IsInfinity (value));

			return value;
		}

		static double BuildDouble(IRandomSource source)
		{
			double value;
			do
			{
				value = BitConverter.ToDouble(source.Next(8), 0);
			}
			while(double.IsNaN (value) || double.IsInfinity (value));

			return value;
		}

		static Guid BuildGuid(IRandomSource source)
		{
			return new Guid(source.Next(16));
		}
	}
}

