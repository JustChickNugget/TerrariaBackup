using System.ComponentModel;

namespace TerrariaBackup.Models;

/// <summary>
/// Used for selecting items.
/// </summary>
public sealed class SelectableItem : INotifyPropertyChanged
{
    private readonly string? _name;
    private bool _checked;

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
        set
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