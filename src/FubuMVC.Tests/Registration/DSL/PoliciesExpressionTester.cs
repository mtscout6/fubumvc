using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using System.Linq;

namespace FubuMVC.Tests.Registration.DSL
{
    [TestFixture]
    public class when_applying_policies_for_wrappers_and_ordering
    {
        private BehaviorGraph graph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Actions.IncludeType<OrderingPolicyController>();

            registry.Policies.Add(policy => {
                policy.Wrap.WithBehavior<OPWrapper1>();
            });

            registry.Policies.Reorder(policy => {
                policy.ThisWrapperBeBefore<OPWrapper1>();
                policy.WhatMustBeAfter = node => node.Category == BehaviorCategory.Authorization;
            });

            graph = BehaviorGraph.BuildFrom(registry);
        }

        
        [Test]
        public void move_behavior_before_authorization()
        {
            // Ordinarily, AuthorizationNode would be before any other behavior wrappers

            var chain = graph.BehaviorFor<OrderingPolicyController>(x => x.M1());
            chain.First().ShouldBeOfType<Wrapper>().BehaviorType.ShouldEqual(typeof(OPWrapper1));
            chain.ToList()[1].ShouldBeOfType<AuthorizationNode>();
        }
    }

    [TestFixture]
    public class when_defining_a_new_reordering_rule_inline
    {
        private BehaviorGraph graph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Actions.IncludeType<OrderingPolicyController>();

            registry.Policies.Add(policy => {
                policy.Wrap.WithBehavior<OPWrapper1>();
            });

            registry.Policies.Reorder(x =>
            {
                x.ThisWrapperBeBefore<OPWrapper1>();
                x.ThisNodeMustBeAfter<AuthorizationNode>();
            });

            graph = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void move_behavior_before_authorization()
        {
            // Ordinarily, AuthorizationNode would be before any other behavior wrappers

            var chain = graph.BehaviorFor<OrderingPolicyController>(x => x.M1());
            chain.First().ShouldBeOfType<Wrapper>().BehaviorType.ShouldEqual(typeof(OPWrapper1));
            chain.ToList()[1].ShouldBeOfType<AuthorizationNode>();
        }
    }

    public class OrderingPolicyController
    {
        [WrapWith(typeof(OPWrapper2), typeof(OPWrapper3))]
        [AllowRole("R1")]
        public void M1(){}
        public void M2(){}
        public void M3(){}
        public void M4(){}
        public void M5(){}
    }

    public class OPWrapper1 : IActionBehavior
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }
    }

    public class OPWrapper2 : IActionBehavior
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }
    }

    public class OPWrapper3 : IActionBehavior
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }
    }
}