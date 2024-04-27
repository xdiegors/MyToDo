using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace MyToDo.ViewModels
{
    [ObservableObject]
    public abstract partial class BaseViewModel
    {       
        public INavigation Navigation { get; set; }
    }
}
