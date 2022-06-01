using System;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using AndroidX.Fragment.App;
using AndroidX.Transitions;
using Plugin.SharedTransitions.Platforms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;
using ResourceX = Plugin.SharedTransitions.Platforms.Android.Resource;

[assembly: ExportRenderer(typeof(Page), typeof(SharedTransitionPageRenderer))]
namespace Plugin.SharedTransitions.Platforms.Android
{
    public class SharedTransitionPageRenderer : PageRenderer
    {
        long _transitionDuration;
        public SharedTransitionPageRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                UpdateTransitionDuration(e.NewElement);
            }
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (Parent is AView v)
            {
                var fragObj = FragmentManager.FindFragment(v);
                if (fragObj is Fragment fragment)
                {
                    var transition = InflateTransitionInContext();
                    fragment.SharedElementEnterTransition = transition;
                    fragment.SharedElementReturnTransition = transition;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == SharedTransitionNavigationPage.TransitionDurationProperty.PropertyName)
            {
                UpdateTransitionDuration(Element);
            }
        }

        void UpdateTransitionDuration(Page p)
        {
            _transitionDuration = SharedTransitionNavigationPage.GetTransitionDuration(p);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (Element != null)
                {
                    if (Application.Current.MainPage is ISharedTransitionContainer shellPage)
                    {
                        shellPage.TransitionMap.RemoveFromPage(Element);
                    }

                    if (Element.Parent is ISharedTransitionContainer navPage)
                    {
                        navPage.TransitionMap.RemoveFromPage(Element);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            base.Dispose(disposing);
        }

        AndroidX.Transitions.Transition InflateTransitionInContext() => TransitionInflater.From(Context)
                                     .InflateTransition(ResourceX.Transition.navigation_transition)
                                     .SetDuration(_transitionDuration);
    }

}
