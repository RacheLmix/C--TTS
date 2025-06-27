using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StudentOrderManager.Data;

namespace StudentOrderManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static StudentOrderContext DbContext;
    public App()
    {
        var optionsBuilder = new DbContextOptionsBuilder<StudentOrderContext>();
        optionsBuilder.UseMySql(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ServerVersion.AutoDetect(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));
        DbContext = new StudentOrderContext(optionsBuilder.Options);
    }
}

