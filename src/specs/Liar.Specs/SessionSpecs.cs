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

namespace Kostassoid.Liar.Specs
{
	using Machine.Specifications;

	// ReSharper disable InconsistentNaming
	// ReSharper disable UnusedMember.Local
	public class SessionSpecs
	{
		[Subject(typeof(Session))]
		[Tags("Unit")]
		public class when_getting_current_session
		{
			static Session _session;

			Because of = () =>
			{
				_session = Session.Current;
			};

			It should_be_created_once = () => _session.ShouldBeTheSameAs(Session.Current);

		}

		[Subject(typeof(Session))]
		[Tags("Unit")]
		public class when_starting_session
		{
			static Session _session;

			Because of = () =>
			{
				_session = Session.Current;
				Session.Start();
			};

			It should_create_new_session = () => _session.ShouldNotBeTheSameAs(Session.Current);
		}

		[Subject(typeof(Session))]
		[Tags("Unit")]
		public class when_resetting_session
		{
			static byte[] _seq1;
			static byte[] _seq2;

			Because of = () =>
			{
				Session.Start();
				_seq1 = Session.Current.Source.Next(10);

				Session.Current.Reset();
				_seq2 = Session.Current.Source.Next(10);
			};

			It should_reset_source = () => _seq1.ShouldEqual(_seq2);
		}

	}

	// ReSharper restore InconsistentNaming
	// ReSharper restore UnusedMember.Local
}
