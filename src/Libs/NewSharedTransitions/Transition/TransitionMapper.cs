﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Plugin.SharedTransitions
{
    /// <summary>
    /// TransitionMapper implementation
    /// </summary>
    /// <seealso cref="ITransitionMapper" />
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class TransitionMapper : ITransitionMapper
    {
        readonly Lazy<List<TransitionMap>> _transitionStack = new Lazy<List<TransitionMap>>(() => new List<TransitionMap>());

        public IReadOnlyList<TransitionMap> TransitionStack => _transitionStack.Value;

        public IReadOnlyList<TransitionDetail> GetMap(Page page, string selectedGroup, bool ignoreGroup = false)
        {
            var p = GetCurrentPage(page);
            if (ignoreGroup)
                return TransitionStack.Where(x => x.Page == p)
                           .Select(x => x.Transitions.ToList())
                           .FirstOrDefault() ?? new List<TransitionDetail>();

            return TransitionStack.Where(x => x.Page == p)
                           .Select(x => x.Transitions.Where(tr=>tr.TransitionGroup == selectedGroup).ToList())
                           .FirstOrDefault() ?? new List<TransitionDetail>();
        }

        public void AddOrUpdate(Page page, string transitionName, string transitionGroup, bool isLightSnapshot, View view, object nativeView)
        { 
            var p = GetCurrentPage(page);
            var transitionMap = _transitionStack.Value.FirstOrDefault(x => x.Page == p);

            if (transitionMap == null)
            {
	            _transitionStack.Value.Add(
                    new TransitionMap
                    {
                        Page = p,
                        Transitions = new List<TransitionDetail> {CreateTransition(transitionName, transitionGroup, isLightSnapshot, view, nativeView)}
                    }
                );
            }
            else
            {
	            var transitionDetail = transitionMap.Transitions.FirstOrDefault(x => x.View == view);
	            if (transitionDetail == null)
	            {
		            transitionDetail = CreateTransition(transitionName, transitionGroup, isLightSnapshot, view, nativeView);
		            transitionMap.Transitions.Add(transitionDetail);

		            ClearMapStackForElementRecycling(transitionMap, transitionDetail);
	            }
	            else if (transitionDetail.TransitionName != transitionName ||
	                     transitionDetail.TransitionGroup != transitionGroup && transitionGroup != null)
	            {
		            transitionDetail.TransitionName  = transitionName;
		            transitionDetail.TransitionGroup = transitionGroup;

		            ClearMapStackForElementRecycling(transitionMap, transitionDetail);
	            }
            }
        }

        public void ClearMapStackForElementRecycling(TransitionMap transitionMap, TransitionDetail transitionDetail)
        {
	        var alreadyexisting = transitionMap.Transitions.Where(x =>
		        x.TransitionName == transitionDetail.TransitionName && x.TransitionGroup == transitionDetail.TransitionGroup &&
		        x.View != transitionDetail.View).ToList();

	        foreach (var transition in alreadyexisting)
		        transitionMap.Transitions.Remove(transition);
        }

        public void Remove(Page page, View view)
        {
            var p = GetCurrentPage(page);
            var transitionMap = _transitionStack.Value.FirstOrDefault(x=>x.Page == p);
            transitionMap?.Transitions.Remove(transitionMap.Transitions.FirstOrDefault(x=>x.View == view));
        }

        public void RemoveFromPage(Page page)
        {
            var p = GetCurrentPage(page);
            _transitionStack.Value.Remove(_transitionStack.Value.FirstOrDefault(x => x.Page == GetCurrentPage(p)));
        }

        public TransitionDetail CreateTransition(string transitionName,string transitionGroup, bool isLightSnapshot, View view, object nativeView)
        {
            return new TransitionDetail
            {
	            TransitionName  = transitionName,
	            TransitionGroup = transitionGroup,
	            View            = view,
	            IsDirty         = false,
	            IsLightSnapshot = isLightSnapshot,
				NativeView      = new WeakReference(nativeView)
            };
        }

        Page GetCurrentPage(Page p)
        {
            switch (p)
            {
                case TabbedPage tab: 
                    return tab.CurrentPage;
                default:
                    return p;
            }
        }
    }
}
