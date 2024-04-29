using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Models;
using MyToDo.Repositories;
using MyToDo.Views;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<TodoItemViewModel> items;

        [ObservableProperty]
        TodoItemViewModel selectedItem;

        [ObservableProperty]
        bool showAll;

        private readonly IServiceProvider _services;
        private readonly ITodoItemRepository _repository;
        public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
        {
            _repository = repository;
            _services = services;
           
            _repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            _repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());
            Task.Run(async () => await LoadDataAsync());
        }

        partial void OnSelectedItemChanging(TodoItemViewModel value)
        {
            if (value == null)
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await NavigateToItemAsync(value);
            });
        }

        private async Task NavigateToItemAsync(TodoItemViewModel item)
        {
            var itemView = _services.GetRequiredService<ItemView>();
            var vm =  itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            itemView.Title = "Edit todo item";

            await Navigation.PushAsync(itemView);
        }

        private async Task LoadDataAsync()
        {
            var items = await _repository.GetItemsAsync();

            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(x => CreateTodoItemViewModel(x));

            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        [RelayCommand]
        private async Task ToggleFilterAsync()
        {
            ShowAll = !ShowAll;
            await LoadDataAsync();
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged (object sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }
                Task.Run(async () => await _repository.UpdateItemAsync(item.Item));
            }
        }

        [RelayCommand]
        public async Task AddItemAsync()
        {
            await Navigation.PushAsync(_services.GetRequiredService<ItemView>());
        }
    }     
}