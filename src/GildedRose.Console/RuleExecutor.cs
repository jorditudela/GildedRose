using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class RuleExecutor<TItem, TArgs> 
    {
        private List<RuleBase<TItem, TArgs>> rules;/* = new List<RuleBase<TItem, TArgs>>();*/

        //public void Add(RuleBase<TItem, TArgs> rule)
        //{
        //    rules.Add(rule);
        //}

        public void ExecuteRules(TItem item, TArgs args)
        {
            var itemNameProp = item.GetType().GetProperty("Name");
            if (itemNameProp != null)
            {
                IOrderedEnumerable<RuleBase<TItem, TArgs>> applicableRules = 
                    rules.Where(
                        x => x.isMatch(itemNameProp.GetValue(item).ToString())
                    ).OrderBy(x => x.Order);
                foreach (RuleBase<TItem, TArgs> rule in applicableRules)
                {
                    rule.ExecRule(item, args);
                    if (rule.StopExecution)
                        break;
                }
            }
        }

        public RuleExecutor(List<RuleBase<TItem, TArgs>> rules)
        {
            this.rules = rules;
        }
    }
}
