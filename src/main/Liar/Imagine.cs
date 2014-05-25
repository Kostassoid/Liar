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

namespace Kostassoid.Liar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Imagine<T>
	{
		static readonly IValuePicker<T> Picker = new ValuePicker<T>();

		public static T Default()
		{
			return Picker.Default();
		}

		public static T Like(T template)
		{
			return Picker.Like(template);
		}

		public static T Any()
		{
			return Picker.Any();
		}

		public static T As(Builder<T> builder)
		{
			return Picker.As(builder);
		}

		public static IGeneratorPicker<T> As()
		{
			return new GeneratorPicker<T>();
		}

		public static IEnumerable<T> Seq(Func<IValuePicker<T>, T> valueFunc)
		{
			for (;;)
			{
				yield return valueFunc(new ValuePicker<T>());
			}
		}

		public static IList<T> List(int count, Func<IValuePicker<T>, T> valueFunc)
		{
			return Seq(valueFunc).Take(count).ToList();
		}
	}
}
