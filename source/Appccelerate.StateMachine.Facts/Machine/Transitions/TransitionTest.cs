﻿//-------------------------------------------------------------------------------
// <copyright file="TransitionTest.cs" company="Appccelerate">
//   Copyright (c) 2008-2014
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.Machine.Transitions
{
    using FakeItEasy;
    using Xunit;

    public class TransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        public TransitionTest()
        {
            this.Source = Builder<States, Events>.CreateState().Build();
            this.Target = Builder<States, Events>.CreateState().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.Testee.Source = this.Source;
            this.Testee.Target = this.Target;
        }

        [Fact]
        public void EntersDestinationState()
        {
            this.Testee.Fire(this.TransitionContext);

            A.CallTo(() => this.Target.Entry(this.TransitionContext)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void ExitsSourceState()
        {
            this.Testee.Fire(this.TransitionContext);

            A.CallTo(() => this.Source.Exit(this.TransitionContext)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void NotifiesTransitionBeginOnTransitionContext()
        {
            this.Testee.Fire(this.TransitionContext);

            A.CallTo(() => this.TransitionContext.OnTransitionBegin()).MustHaveHappened();
        }
    }
}