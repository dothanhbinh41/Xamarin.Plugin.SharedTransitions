using System.Collections.Generic;
using Prism.AppModel;
using Prism.Navigation;
using TransitionApp.Models;

namespace TransitionApp.ViewModels
{
    public class CollectionviewToPageViewModel : ViewModelBase  
    {
        private List<DogModel> _dogs;
        public List<DogModel> Dogs
        {
            get => _dogs;
            set => SetProperty(ref _dogs, value);
        }

        private DogModel _selectedDog;
        public DogModel SelectedDog
        {
            get => _selectedDog;
            set => SetProperty(ref _selectedDog, value);
        }

        public CollectionviewToPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            SelectedDog = parameters.GetValue<DogModel>("selectedDog");
        }
    }
}
