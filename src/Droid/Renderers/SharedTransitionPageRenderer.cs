using System;
using System.Linq;
using Android.Content;
using Plugin.Transitions.Platforms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Page), typeof(SharedTransitionPageRenderer))]
namespace Plugin.Transitions.Platforms.Android
{
    public class SharedTransitionPageRenderer : PageRenderer
    {
        public SharedTransitionPageRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e); 
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
    }
}
