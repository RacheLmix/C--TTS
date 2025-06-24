using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public class TodoViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context = new();

    // Danh sách gốc để lọc/tìm kiếm
    private List<TodoItem> AllTodos = new();
    public ObservableCollection<TodoItem> TodoList { get; set; } = new();

    private TodoItem _selectedItem = new() { Deadline = DateTime.Now };
    public TodoItem SelectedItem
    {
        get => _selectedItem;
        set { _selectedItem = value; OnPropertyChanged(); }
    }

    // Thuộc tính cho filter và search
    private int _filterIndex;
    public int FilterIndex
    {
        get => _filterIndex;
        set { _filterIndex = value; OnPropertyChanged(); ApplyFilterAndSearch(); }
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); ApplyFilterAndSearch(); }
    }

    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public TodoViewModel()
    {
        _context.Database.EnsureCreated();
        LoadTodos();

        AddCommand = new RelayCommand(AddTodo);
        UpdateCommand = new RelayCommand(UpdateTodo, () => SelectedItem?.Id > 0);
        DeleteCommand = new RelayCommand(DeleteTodo, () => SelectedItem?.Id > 0);
    }

    private void LoadTodos()
    {
        AllTodos = _context.TodoItems.OrderBy(t => t.Deadline).ToList();
        ApplyFilterAndSearch();
    }

    private void ApplyFilterAndSearch()
    {
        IEnumerable<TodoItem> filtered = AllTodos;
        // Lọc theo trạng thái
        switch (FilterIndex)
        {
            case 1: // Hoàn thành
                filtered = filtered.Where(t => t.IsCompleted);
                break;
            case 2: // Chưa hoàn thành
                filtered = filtered.Where(t => !t.IsCompleted);
                break;
        }
        // Lọc theo từ khoá
        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(t => t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        TodoList.Clear();
        foreach (var item in filtered)
            TodoList.Add(item);
    }

    private void AddTodo()
    {
        var newItem = new TodoItem
        {
            Title = SelectedItem.Title,
            Deadline = DateTime.Now,
            IsCompleted = SelectedItem.IsCompleted
        };
        _context.TodoItems.Add(newItem);
        _context.SaveChanges();
        LoadTodos();
        SelectedItem = new TodoItem { Deadline = DateTime.Now };
    }

    private void UpdateTodo()
    {
        _context.TodoItems.Update(SelectedItem);
        _context.SaveChanges();
        LoadTodos();
    }

    private void DeleteTodo()
    {
        _context.TodoItems.Remove(SelectedItem);
        _context.SaveChanges();
        LoadTodos();
        SelectedItem = new TodoItem { Deadline = DateTime.Now };
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
