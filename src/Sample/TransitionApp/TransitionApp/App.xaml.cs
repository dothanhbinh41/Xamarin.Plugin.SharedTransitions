using Plugin.SharedTransitions;
using Prism;
using Prism.Ioc;
using TransitionApp.ViewModels;
using TransitionApp.Views;
using TransitionApp.Views.Collectionview;
using TransitionApp.Views.Image;
using TransitionApp.Views.Layout;
using TransitionApp.Views.Listview;
using TransitionApp.Views.Main;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TransitionApp
{ 
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //Standard navigationpage
            await NavigationService.NavigateAsync($"{nameof(SharedTransitionNavigationPage)}/{nameof(TransitionApp.Views.Main.MainPage)}");

            //Tabbed Page
            //await NavigationService.NavigateAsync(nameof(MainTabbedPage));

            //Master-Detail
            //await NavigationService.NavigateAsync(nameof(MainMasterDetailPage));

            //await NavigationService.NavigateAsync(nameof(HomePage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SharedTransitionNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
            containerRegistry.RegisterForNavigation<ImageFromPage>();
            containerRegistry.RegisterForNavigation<ImageToPage>();
            containerRegistry.RegisterForNavigation<LayoutFromPage>();
            containerRegistry.RegisterForNavigation<LayoutToPage>();
            containerRegistry.RegisterForNavigation<ListViewFromPage, ListViewFromPageViewModel>();
            containerRegistry.RegisterForNavigation<ListViewToPage, CollectionviewToPageViewModel>();
            containerRegistry.RegisterForNavigation<CollectionviewFromPage, CollectionviewFromPageViewModel>();
            containerRegistry.RegisterForNavigation<CollectionviewToPage, CollectionviewToPageViewModel>();
        }
    }
}
