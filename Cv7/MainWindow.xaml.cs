using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cv7;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Customer> Customers { get; set; } =
    [
        new() { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
        new() { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 25 },
        new() { Id = 3, FirstName = "Sam", LastName = "Brown", Age = 40 }
    ];
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void AddCustomer(object sender, RoutedEventArgs e)
    {
        Customers.Add(new Customer()
        {
            Id = Customers.Last().Id + 1,
            FirstName = "New",
            LastName = "Customer",
            Age = 20
        });
    }

    private void RemoveCustomer(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var customer = (Customer)btn.DataContext;
        if (customer != null)
        {
            Customers.Remove(customer);
        }
    }

    private void AnonymizaCustomer(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var customer = (Customer)btn.DataContext;
        if (customer != null)
        {
            customer.FirstName = "***";
            customer.LastName = "***";
        }
    }

    private void EditCustomer(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        var customer = (Customer)btn.DataContext;
        var dialog = new EditDialog(customer);
        dialog.ShowDialog();
    }
}