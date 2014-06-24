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

using System;

namespace Kostassoid.Liar.Generators.Special
{
	using Base;
	using Randomization;
	using Syntax;

	internal static class PinCodeGenerator
	{
		static int Generate(IRandomSource source)
		{
			return Math.Abs(Builders.BuildShort(source)) % 9000 + 1000;
		}

		public static IValueSyntax<int> PinCode(this IAnySyntax<int> syntax)
		{
			var s = syntax as ISpecification<int>;

			s.Generator = new BuilderGenerator<int> (Generate);

			return syntax;
		}
	}
}