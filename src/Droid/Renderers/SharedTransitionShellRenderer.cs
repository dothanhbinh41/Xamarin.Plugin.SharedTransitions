using Android.Content;
using Plugin.Transitions;
using Plugin.Transitions.Platforms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(SharedTransitionShell), typeof(SharedTransitionShellRenderer))]
namespace Plugin.Transitions.Platforms.Android
{
	public class SharedTransitionShellRenderer : ShellRenderer
	{
		public SharedTransitionShellRenderer(Context context) : base(context)
		{
		}

		protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
		{
			return new SharedTransitionShellItemRenderer(this);
		}

	}
}
