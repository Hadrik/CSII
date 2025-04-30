using System.Windows;
using System.Windows.Controls;

namespace Cv7;

public partial class FormRow : UserControl
{
    public required string LabelText { get; set; }

    private static readonly DependencyProperty ValueProperty = DependencyProperty
        .Register(nameof(Value), typeof(string), typeof(FormRow),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public FormRow()
    {
        InitializeComponent();
    }
}