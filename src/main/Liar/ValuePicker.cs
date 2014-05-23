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
	using Generators;
	using Tools;

	internal class ValuePicker<T> : IValuePicker<T>
	{
		public T Any()
		{
			return Registry.Generate<T>();
		}

		public T Default()
		{
			return default(T);
		}

		public T Like(T template)
		{
			return template.Copy();
		}

		public T As(Builder<T> builder)
		{
			return builder();
		}

		public IGeneratorPicker<T> As()
		{
			return new GeneratorPicker<T>();
		}
	}
}