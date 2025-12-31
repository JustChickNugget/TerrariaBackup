using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TerrariaBackup.Models;

/// <summary>
/// Progress track model.
/// </summary>
public sealed class ProgressTracker : INotifyPropertyChanged
{
    /// <summary>
    /// Property: progress current value.
    /// </summary>
    public int CurrentValue
    {
        get => _currentValue;
        set
        {
            if (_currentValue == value)
            {
                return;
            }

            _currentValue = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ProgressDecimalValue));
            OnPropertyChanged(nameof(ProgressValueStatusText));
        }
    }

    /// <summary>
    /// Property: progress maximum value.
    /// </summary>
    public int MaximumValue
    {
        get => _maximumValue;
        set
        {
            if (_maximumValue == value)
            {
                return;
            }

            _maximumValue = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Progress decimal value (from 0 to 1).
    /// </summary>
    public double ProgressDecimalValue => MaximumValue == 0 ? 0 : (double)CurrentValue / MaximumValue;

    /// <summary>
    /// Progress value status text.
    /// </summary>
    public string ProgressValueStatusText => $"{CurrentValue} / {MaximumValue}";

    /// <summary>
    /// Field: progress current value.
    /// </summary>
    private int _currentValue;

    /// <summary>
    /// Field: progress maximum value.
    /// </summary>
    private int _maximumValue;

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