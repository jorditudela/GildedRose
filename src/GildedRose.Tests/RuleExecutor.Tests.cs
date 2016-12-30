using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class TestRuleExecutor
    {
        [Fact]
        public void Execute_rules_in_order()
        {
            RuleExecutor<Item, int[]> ruleEx = new RuleExecutor<Item, int[]>(
                new List<RuleBase<Item, int[]>>()
                {
                    new RuleBase<Item, int[]>()
                    {
                        Order = 50,
                        Name = "Multiply by 2",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = args[0] * 2;
                        }
                    },

                    new RuleBase<Item, int[]>()
                    {
                        Order = 1,
                        Name = "Initialize to 3",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = 3;
                        }
                    }
                }
            );

            int[] arg = new int[1];
            arg[0] = 1;
            ruleEx.ExecuteRules(new Item() { Name = "An Item" }, arg);
            Assert.Equal(6, arg[0]);
        }

        [Fact]
        public void Execute_Only_First_Rule()
        {
            RuleExecutor<Item, int[]> ruleEx = new RuleExecutor<Item, int[]>(
                new List<RuleBase<Item, int[]>>()
                {
                    new RuleBase<Item, int[]>()
                    {
                        Order = 50,
                        Name = "Multiply by 2",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = args[0] * 2;
                        }
                    },
                    new RuleBase<Item, int[]>()
                    {
                        Order = 1,
                        Name = "Initialize to 3",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = 3;
                        },
                        StopExecution = true
                    }
                }
            );
            int[] arg = new int[1];
            arg[0] = 1;
            ruleEx.ExecuteRules(new Item() { Name = "An Item" }, arg);
            Assert.Equal(3, arg[0]);
        }

        [Fact]
        public void Select_Rules_When_TItem_Has_NOT_a_Name_Property()
        {
            RuleExecutor<Object, int[]> ruleEx = new RuleExecutor<Object, int[]>(
                new List<RuleBase<Object, int[]>>()
                {
                    new RuleBase<Object, int[]>()
                    {
                        Order = 50,
                        Name = "Multiply by 2",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = args[0] * 2;
                        }
                    },
                    new RuleBase<Object, int[]>()
                    {
                        Order = 1,
                        Pattern = "special.*",
                        Name = "Initialize to 3",
                        OnExecRule = (item, args) =>
                        {
                            args[0] = 3;
                        }
                    }
                }
            );
            int[] arg = new int[1];
            arg[0] = 1;
            ruleEx.ExecuteRules(new Object(), arg);
            Assert.Equal(2, arg[0]);
        }
    }
}
