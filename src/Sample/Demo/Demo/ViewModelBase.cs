using Prism.Mvvm;
using Prism.Navigation;

namespace TransitionApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        protected INavigationService NavigationService { get; private set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual void Initialize(INavigationParameters parameters)
        { 
        }
    }
}
