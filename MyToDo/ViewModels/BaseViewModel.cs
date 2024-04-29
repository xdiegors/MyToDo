using CommunityToolkit.Mvvm.ComponentModel;

namespace MyToDo.ViewModels
{
    [ObservableObject]
    public abstract partial class BaseViewModel
    {       
        public INavigation Navigation { get; set; }
    }
}
