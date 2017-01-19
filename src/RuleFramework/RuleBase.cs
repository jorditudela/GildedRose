﻿using System;
using System.Text.RegularExpressions;

namespace RuleFramework
{
    public class RuleBase<TItem, TArgs>
    {
        public class ItemUpdaterNotSetException : Exception
        {
            public RuleBase<TItem, TArgs> rule;
        };

        public string Name;
        public string Pattern = RulePatternConsts.MatchAll;
        public Action<TItem, TArgs> OnExecRule;
        public int Order = 50;
        public bool StopExecution = false;

        public void ExecRule(TItem item, TArgs args)
        {
            if (OnExecRule == null)
                throw new ItemUpdaterNotSetException()
                {
                    rule = this
                };
            OnExecRule(item, args);
        }

        public bool isMatch(string itemName)
        {
            Regex rgx = new Regex(this.Pattern);
            return rgx.IsMatch(itemName);
        }
    }
}
