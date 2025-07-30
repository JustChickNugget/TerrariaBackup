using System.ComponentModel;

namespace TerrariaBackup.Models;

/// <summary>
/// Used for selecting items.
/// </summary>
public sealed class SelectableItem : INotifyPropertyChanged
{
    private readonly string? _name;
    private readonly bool _checked;

    public string? Name
    {
        get => _name;
        init
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public bool Checked
    {
        get => _checked;
        init
        {
            _checked = value;
            OnPropertyChanged(nameof(Checked));
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}