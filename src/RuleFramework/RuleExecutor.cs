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
            foreach (RuleBase<TItem, TArgs> rule in selectRules(item))
            {
                rule.ExecRule(item, args);
                if (rule.StopExecution)
                    break;
            }
        }

        private IOrderedEnumerable<RuleBase<TItem, TArgs>> selectRules(TItem item)
        {
            var itemNameProp = item.GetType().GetProperty("Name");
            IEnumerable<RuleBase<TItem, TArgs>> selected;
            if (itemNameProp != null)
            {
                selected = rules.Where(
                        x => x.isMatch(itemNameProp.GetValue(item).ToString())
                );
            }
            else if (onGetKey != null)
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

        public RuleExecutor(List<RuleBase<TItem, TArgs>> rules)
        {
            this.rules = rules;
        }

        public RuleExecutor(List<RuleBase<TItem, TArgs>> rules, Func<TItem, string> onGetKey)
        {
            this.rules = rules;
            this.onGetKey = onGetKey;
        }
    }
}
