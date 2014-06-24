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
	using Sequence;

	public class Session
	{
		static Session _current;

		public static Session Current
		{
			get
			{
				if (_current == null)
				{
					Start();
				}
				return _current;
			}
		}

		public IRandomSource Source
		{
			get;
			private set;
		}

		private Session(IRandomSource source)
		{
			Source = source;
		}

		public static void Start(IRandomSource source)
		{
			_current = new Session(source);
		}

		public static void Start()
		{
			Start(new DefaultRandomSource (666));
		}

		public void Reset()
		{
			Source.Reset();
		}
	}
}