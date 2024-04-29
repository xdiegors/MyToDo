using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Models;
using MyToDo.Repositories;

namespace MyToDo.ViewModels
{
    public partial class ItemViewModel : BaseViewModel
    {
        [ObservableProperty]
        TodoItem item;

        private readonly ITodoItemRepository _repository;
        public ItemViewModel(ITodoItemRepository repository)
        {
            _repository = repository;
            Item = new TodoItem()
            {
                Due = DateTime.Now.AddDays(1)
            };
        }

        [RelayCommand]
        public async Task SaveASync()
        {
            await _repository.AddOrUpdateAsync(Item);
            await Navigation.PopAsync();
        }
    }
}
