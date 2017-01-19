using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleFramework
{
    public class RuleExecutor<TItem, TArgs>
    {
        private List<RuleBase<TItem, TArgs>> rules;
        private Func<TItem, string> onGetKey = null;

        public void ExecuteRules(TItem item, TArgs args)
        {
            bool stopFound = false;
            selectRules(item).ToList().ForEach(rule => {
                if (! stopFound) rule.ExecRule(item, args);
                stopFound |= rule.StopExecution;
            });
        }

        private IOrderedEnumerable<RuleBase<TItem, TArgs>> selectRules(TItem item)
        {
            IEnumerable<RuleBase<TItem, TArgs>> selected; 
            if (onGetKey != null)
            {
                selected = rules.Where(
                        x => x.isMatch(onGetKey(item))
                );
            }
            else
            {
                selected = rules.Where(
                    x => x.Pattern == ".*"
                );
            }
            return selected.OrderBy(x => x.Order);
        }

        public RuleExecutor(List<RuleBase<TItem, TArgs>> rules) : this(rules, null){ }

        public RuleExecutor(List<RuleBase<TItem, TArgs>> rules, Func<TItem, string> onGetKey)
        {
            this.rules = rules;
            this.onGetKey = onGetKey;
        }
    }
}
