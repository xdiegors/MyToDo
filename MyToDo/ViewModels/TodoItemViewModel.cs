using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Models;

namespace MyToDo.ViewModels
{
    public partial class TodoItemViewModel : BaseViewModel
    {
        public TodoItemViewModel(TodoItem item) => Item = item;

        public event EventHandler ItemStatusChanged;

        [ObservableProperty]
        TodoItem item;

        public string StatusText => Item.Completed ? "Reactivate" : "Completed";

        [RelayCommand]
        void ToggleCompleted()
        {
            Item.Completed = !Item.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
