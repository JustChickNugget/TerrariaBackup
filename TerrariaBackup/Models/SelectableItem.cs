using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TerrariaBackup.Models;

/// <summary>
/// Selectable item model.
/// </summary>
public sealed class SelectableItem : INotifyPropertyChanged
{
    /// <summary>
    /// Property: item name.
    /// </summary>
    public string? Name
    {
        get => _name;
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Property: is item selected?
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value)
            {
                return;
            }

            _isSelected = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Field: item name.
    /// </summary>
    private string? _name;

    /// <summary>
    /// Field: is item selected?
    /// </summary>
    private bool _isSelected;

    /// <summary>
    /// Property changed event handler.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Handle on property changed event.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}