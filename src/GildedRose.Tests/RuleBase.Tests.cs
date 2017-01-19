using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRose.Console;
using RuleFramework;

namespace GildedRose.Tests
{
    public class TestRuleBase
    {
        [Fact]
        public void ExecRule_without_Implmentation()
        {
            RuleBase<Item, int> rule = new RuleBase<Item, int>();

            Exception e = Assert.Throws<RuleBase<Item, int>.ItemUpdaterNotSetException>
                (() => rule.ExecRule(new Item(), 0));
        }

        [Fact]
        public void ExecRule()
        {
            // Generate a random value to be returned by OnExecRule
            Random rndGen = new Random();
            int rndVal = rndGen.Next(1, 255);
            RuleBase<Item, int[]> rule = new RuleBase<Item, int[]>()
            {
                OnExecRule = (item, args) =>
                {
                    args[0] = rndVal;
                }
            };
            int[] seed = new int[1];
            seed[0] = 0;
            // seed = [0]
            rule.ExecRule(new Item(), seed);
            // After rule execute seed == [rndVal]
            Assert.Equal(rndVal, seed[0]);
        }

        [Fact]
        public void Match_Any_Items()
        {
            RuleBase<Item, int[]> rule = new RuleBase<Item, int[]>()
            {
                Name = "Match all rule"
            };

            Assert.True(rule.isMatch("A Name"));
        }

        [Fact]
        public void Match_Specific_Item()
        {
            RuleBase<Item, int[]> rule = new RuleBase<Item, int[]>()
            {
                Name = "Match all rule",
                Pattern = @"A Name"
            };

            Assert.True(rule.isMatch("A Name"));
        }

        [Fact]
        public void Match_No_Item()
        {
            RuleBase<Item, int[]> rule = new RuleBase<Item, int[]>()
            {
                Name = "Match all rule",
                Pattern = @"noMatch"
            };

            Assert.False(rule.isMatch("A Name"));
        }
    }
}
