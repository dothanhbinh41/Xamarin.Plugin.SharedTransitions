﻿using System.Collections.Generic;
using Prism.Commands;
using Prism.Navigation;
using TransitionApp.Models;
using TransitionApp.Views;
using TransitionApp.Views.Collectionview;

namespace TransitionApp.ViewModels
{
    public class CollectionviewFromPageViewModel : ViewModelBase
    {
        private List<DogModel> _dogs;
        public List<DogModel> Dogs
        {
            get => _dogs;
            set => SetProperty(ref _dogs, value);
        }

        private int _selectedDogId;
        public int SelectedDogId
        {
            get => _selectedDogId;
            set => SetProperty(ref _selectedDogId, value);
        }

        public DelegateCommand<DogModel> NavigateDogCommand { get; set; }

        public CollectionviewFromPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigateDogCommand = new DelegateCommand<DogModel>(async (selectedDog) =>
            {
               SelectedDogId = selectedDog.Id;
               
               var navParam = new NavigationParameters {{nameof(selectedDog), selectedDog}}; 
               await navigationService.NavigateAsync($"{nameof(CollectionviewToPage)}",navParam);
            });

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                              "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                              "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            Dogs = new List<DogModel>
            {
	            new DogModel {Id =1, Title = "Christmas Dog",    Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/17638/production/_124800859_gettyimages-817514614.jpg", Description = description},
	            new DogModel {Id =2, Title = "Cute Dog",         Image = "https://i.ytimg.com/vi/MPV2METPeJU/maxresdefault.jpg",      Description = description},
	            new DogModel {Id =3, Title = "Lazy Dog",         Image = "https://www.princeton.edu/sites/default/files/styles/half_2x/public/images/2022/02/KOA_Nassau_2697x1517.jpg",      Description = description},
	            new DogModel {Id =4, Title = "What the Dog??!?", Image = "https://i.insider.com/5484d9d1eab8ea3017b17e29?width=600&format=jpeg",  Description = description},
            };
        }
    }
}
