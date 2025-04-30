using System.Windows;

namespace Cv7;

public partial class EditDialog : Window
{
    private Customer _customer;
    
    public EditDialog(Customer customer)
    {
        InitializeComponent();
        DataContext = new Customer
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Age = customer.Age,
        };
        _customer = customer;
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        var c = (Customer)DataContext;
        _customer.FirstName = c.FirstName;
        _customer.LastName = c.LastName;
        _customer.Age = c.Age;
        Close();
    }
}