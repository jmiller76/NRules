﻿using System;
using System.Diagnostics;

namespace NRules.RuleModel
{
    public static class RuleElementExtensions
    {
        /// <summary>
        /// Matches a rule element to an appropriate action based on the concrete type of the rule element.
        /// Type-safe implementation of discriminated union for rule elements.
        /// </summary>
        /// <param name="element">Rule element to match.</param>
        /// <param name="pattern">Action to invoke on the element if the element is a <see cref="PatternElement"/>.</param>
        /// <param name="aggregate">Action to invoke on the element if the element is an <see cref="AggregateElement"/>.</param>
        /// <param name="group">Action to invoke on the element if the element is a <see cref="GroupElement"/>.</param>
        [DebuggerStepThrough]
        public static void Match(this RuleElement element, Action<PatternElement> pattern, Action<AggregateElement> aggregate, Action<GroupElement> group)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element", "Rule element cannot be null");
            }
            else if (element is PatternElement)
            {
                pattern.Invoke((PatternElement) element);
            }
            else if (element is GroupElement)
            {
                group.Invoke((GroupElement) element);
            }
            else if (element is AggregateElement)
            {
                aggregate.Invoke((AggregateElement) element);
            }
            else
            {
                throw new ArgumentOutOfRangeException("element", string.Format("Unsupported rule element. ElementType={0}", element.GetType()));
            }
        }

        /// <summary>
        /// Matches a group element to an appropriate action based on the concrete type of the group element.
        /// Type-safe implementation of discriminated union for group elements.
        /// </summary>
        /// <param name="element">Group element to match.</param>
        /// <param name="and">Action to invoke on the element if the element is a <see cref="AndElement"/>.</param>
        /// <param name="or">Action to invoke on the element if the element is a <see cref="OrElement"/>.</param>
        /// <param name="not">Action to invoke on the element if the element is a <see cref="NotElement"/>.</param>
        /// <param name="exists">Action to invoke on the element if the element is a <see cref="ExistsElement"/>.</param>
        [DebuggerStepThrough]
        public static void Match(this GroupElement element, Action<AndElement> and, Action<OrElement> or, Action<NotElement> not, Action<ExistsElement> exists)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element", "Rule element cannot be null");
            }
            else if (element is AndElement)
            {
                and.Invoke((AndElement)element);
            }
            else if (element is OrElement)
            {
                or.Invoke((OrElement)element);
            }
            else if (element is NotElement)
            {
                not.Invoke((NotElement)element);
            }
            else if (element is ExistsElement)
            {
                exists.Invoke((ExistsElement)element);
            }
            else
            {
                throw new ArgumentOutOfRangeException("element", string.Format("Unsupported rule group element. ElementType={0}", element.GetType()));
            }
        }
    }
}