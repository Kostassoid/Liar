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

namespace Kostassoid.Liar.Syntax
{
	using System;
	using System.Collections.Generic;

	public interface IValueSyntax<out T>
	{
		T Value { get; }
		IEnumerable<T> Sequence { get; }

		IValueSyntax<T> With(Action<T> withAction);
		IValueSyntax<T> Where(Predicate<T> predicate);
	}
}

