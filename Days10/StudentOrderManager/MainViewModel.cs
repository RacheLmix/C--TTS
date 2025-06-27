using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StudentOrderManager.Data;
using System.IO;
using CsvHelper;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QPdfContainer = QuestPDF.Infrastructure.IContainer;
using Microsoft.Win32;

namespace StudentOrderManager
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly StudentOrderContext _context = new();
        public ObservableCollection<Student> Students { get; set; } = new();
        public ObservableCollection<Order> Orders { get; set; } = new();

        // Filters
        public string StudentNameFilter { get => _studentNameFilter; set { _studentNameFilter = value; OnPropertyChanged(nameof(StudentNameFilter)); } }
        private string _studentNameFilter = string.Empty;
        public string StudentClassFilter { get => _studentClassFilter; set { _studentClassFilter = value; OnPropertyChanged(nameof(StudentClassFilter)); } }
        private string _studentClassFilter = string.Empty;
        public string OrderCustomerFilter { get => _orderCustomerFilter; set { _orderCustomerFilter = value; OnPropertyChanged(nameof(OrderCustomerFilter)); } }
        private string _orderCustomerFilter = string.Empty;

        // Selected
        public Student SelectedStudent { get => _selectedStudent; set { _selectedStudent = value; OnPropertyChanged(nameof(SelectedStudent)); if (value != null) { EditStudentName = value.Name; EditStudentClass = value.Class; } } }
        private Student _selectedStudent;
        public Order SelectedOrder { get => _selectedOrder; set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); if (value != null) { EditOrderCustomer = value.Customer; EditOrderTotal = value.Total.ToString(); } } }
        private Order _selectedOrder;

        // Edit fields
        public string EditStudentName { get => _editStudentName; set { _editStudentName = value; OnPropertyChanged(nameof(EditStudentName)); } }
        private string _editStudentName = string.Empty;
        public string EditStudentClass { get => _editStudentClass; set { _editStudentClass = value; OnPropertyChanged(nameof(EditStudentClass)); } }
        private string _editStudentClass = string.Empty;
        public string EditOrderCustomer { get => _editOrderCustomer; set { _editOrderCustomer = value; OnPropertyChanged(nameof(EditOrderCustomer)); } }
        private string _editOrderCustomer = string.Empty;
        public string EditOrderTotal { get => _editOrderTotal; set { _editOrderTotal = value; OnPropertyChanged(nameof(EditOrderTotal)); } }
        private string _editOrderTotal = string.Empty;

        // Error messages
        public string StudentError { get => _studentError; set { _studentError = value; OnPropertyChanged(nameof(StudentError)); } }
        private string _studentError = string.Empty;
        public string OrderError { get => _orderError; set { _orderError = value; OnPropertyChanged(nameof(OrderError)); } }
        private string _orderError = string.Empty;

        // Thống kê
        public string TotalOrderByCustomer { get => _totalOrderByCustomer; set { _totalOrderByCustomer = value; OnPropertyChanged(nameof(TotalOrderByCustomer)); } }
        private string _totalOrderByCustomer = string.Empty;

        // Commands
        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand FilterStudentsCommand { get; }
        public ICommand AddOrderCommand { get; }
        public ICommand EditOrderCommand { get; }
        public ICommand DeleteOrderCommand { get; }
        public ICommand FilterOrdersCommand { get; }
        public ICommand ExportPdfCommand { get; }
        public ICommand ExportCsvCommand { get; }

        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(_ => AddStudent());
            EditStudentCommand = new RelayCommand(_ => EditStudent());
            DeleteStudentCommand = new RelayCommand(_ => DeleteStudent());
            FilterStudentsCommand = new RelayCommand(_ => FilterStudents());
            AddOrderCommand = new RelayCommand(_ => AddOrder());
            EditOrderCommand = new RelayCommand(_ => EditOrder());
            DeleteOrderCommand = new RelayCommand(_ => DeleteOrder());
            FilterOrdersCommand = new RelayCommand(_ => FilterOrders());
            ExportPdfCommand = new RelayCommand(_ => ExportPdf());
            ExportCsvCommand = new RelayCommand(_ => ExportCsv());
            LoadData();
        }

        private void LoadData()
        {
            Students.Clear();
            foreach (var s in _context.Students.ToList()) Students.Add(s);
            Orders.Clear();
            foreach (var o in _context.Orders.ToList()) Orders.Add(o);
            UpdateTotalOrderByCustomer();
        }

        // CRUD Student
        private void AddStudent()
        {
            StudentError = string.Empty;
            if (string.IsNullOrWhiteSpace(EditStudentName) || string.IsNullOrWhiteSpace(EditStudentClass))
            {
                StudentError = "Tên và lớp không được để trống!";
                return;
            }
            var s = new Student { Name = EditStudentName, Class = EditStudentClass };
            _context.Students.Add(s);
            _context.SaveChanges();
            LoadData();
            EditStudentName = EditStudentClass = string.Empty;
        }
        private void EditStudent()
        {
            StudentError = string.Empty;
            if (SelectedStudent == null)
            {
                StudentError = "Chọn sinh viên để sửa!";
                return;
            }
            if (string.IsNullOrWhiteSpace(EditStudentName) || string.IsNullOrWhiteSpace(EditStudentClass))
            {
                StudentError = "Tên và lớp không được để trống!";
                return;
            }
            SelectedStudent.Name = EditStudentName;
            SelectedStudent.Class = EditStudentClass;
            _context.Students.Update(SelectedStudent);
            _context.SaveChanges();
            LoadData();
        }
        private void DeleteStudent()
        {
            StudentError = string.Empty;
            if (SelectedStudent == null)
            {
                StudentError = "Chọn sinh viên để xóa!";
                return;
            }
            _context.Students.Remove(SelectedStudent);
            _context.SaveChanges();
            LoadData();
        }
        private void FilterStudents()
        {
            var query = _context.Students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(StudentNameFilter))
                query = query.Where(s => s.Name.Contains(StudentNameFilter));
            if (!string.IsNullOrWhiteSpace(StudentClassFilter))
                query = query.Where(s => s.Class.Contains(StudentClassFilter));
            Students.Clear();
            foreach (var s in query.ToList()) Students.Add(s);
        }

        // CRUD Order
        private void AddOrder()
        {
            OrderError = string.Empty;
            if (string.IsNullOrWhiteSpace(EditOrderCustomer) || string.IsNullOrWhiteSpace(EditOrderTotal) || !decimal.TryParse(EditOrderTotal, out var total))
            {
                OrderError = "Khách và tổng tiền hợp lệ!";
                return;
            }
            var o = new Order { Customer = EditOrderCustomer, Total = total };
            _context.Orders.Add(o);
            _context.SaveChanges();
            LoadData();
            EditOrderCustomer = EditOrderTotal = string.Empty;
        }
        private void EditOrder()
        {
            OrderError = string.Empty;
            if (SelectedOrder == null)
            {
                OrderError = "Chọn đơn hàng để sửa!";
                return;
            }
            if (string.IsNullOrWhiteSpace(EditOrderCustomer) || string.IsNullOrWhiteSpace(EditOrderTotal) || !decimal.TryParse(EditOrderTotal, out var total))
            {
                OrderError = "Khách và tổng tiền hợp lệ!";
                return;
            }
            SelectedOrder.Customer = EditOrderCustomer;
            SelectedOrder.Total = total;
            _context.Orders.Update(SelectedOrder);
            _context.SaveChanges();
            LoadData();
        }
        private void DeleteOrder()
        {
            OrderError = string.Empty;
            if (SelectedOrder == null)
            {
                OrderError = "Chọn đơn hàng để xóa!";
                return;
            }
            _context.Orders.Remove(SelectedOrder);
            _context.SaveChanges();
            LoadData();
        }
        private void FilterOrders()
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(OrderCustomerFilter))
                query = query.Where(o => o.Customer.Contains(OrderCustomerFilter));
            Orders.Clear();
            foreach (var o in query.ToList()) Orders.Add(o);
            UpdateTotalOrderByCustomer();
        }

        // Thống kê
        private void UpdateTotalOrderByCustomer()
        {
            if (!string.IsNullOrWhiteSpace(OrderCustomerFilter))
            {
                var total = _context.Orders.Where(o => o.Customer.Contains(OrderCustomerFilter)).Sum(o => (decimal?)o.Total) ?? 0;
                TotalOrderByCustomer = total.ToString("N0");
            }
            else
            {
                TotalOrderByCustomer = _context.Orders.Sum(o => (decimal?)o.Total)?.ToString("N0") ?? "0";
            }
        }

        // Báo cáo (placeholder)
        private void ExportCsv()
        {
            try
            {
                var dialog = new SaveFileDialog
                {
                    FileName = $"OrderReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == true)
                {
                    using var writer = new StreamWriter(dialog.FileName);
                    using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                    csv.WriteRecords(Orders);
                    OrderError = $"Đã xuất CSV: {dialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                OrderError = $"Lỗi xuất CSV: {ex.Message}";
            }
        }
        private void ExportPdf()
        {
            try
            {
                var dialog = new SaveFileDialog
                {
                    FileName = $"OrderReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == true)
                {
                    var orders = Orders.ToList();
                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Margin(30);
                            page.Header().Text("Báo cáo đơn hàng").FontSize(20).Bold().AlignCenter();
                            page.Content().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50); // ID
                                    columns.RelativeColumn(); // Khách
                                    columns.RelativeColumn(); // Tổng tiền
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(x => CellStyle(x)).Text("ID").Bold();
                                    header.Cell().Element(x => CellStyle(x)).Text("Khách").Bold();
                                    header.Cell().Element(x => CellStyle(x)).Text("Tổng tiền").Bold();
                                });
                                foreach (var o in orders)
                                {
                                    table.Cell().Element(x => CellStyle(x)).Text(o.Id.ToString());
                                    table.Cell().Element(x => CellStyle(x)).Text(o.Customer);
                                    table.Cell().Element(x => CellStyle(x)).Text(o.Total.ToString("N0"));
                                }
                            });
                            page.Footer().AlignRight().Text($"Tổng cộng: {orders.Sum(x => x.Total):N0}");
                        });
                    })
                    .GeneratePdf(dialog.FileName);
                    OrderError = $"Đã xuất PDF: {dialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                OrderError = $"Lỗi xuất PDF: {ex.Message}";
            }

            static QPdfContainer CellStyle(QPdfContainer container) => container.PaddingVertical(2).PaddingHorizontal(5);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // RelayCommand cho ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;
        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object? parameter) => _execute(parameter);
        public event EventHandler? CanExecuteChanged { add { CommandManager.RequerySuggested += value; } remove { CommandManager.RequerySuggested -= value; } }
    }
} 