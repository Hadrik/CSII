using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Cv7;

public class Customer : INotifyPropertyChanged
{
    private string _firstName;
    private string _lastName;
    private int _age;

    public int Id { get; set; }

    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        set
        {
            _firstName = value;
            Changed();
        }
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        set
        {
            _lastName = value;
            Changed();
        }
    }

    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Changed([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}