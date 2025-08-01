using System.ComponentModel;

namespace TerrariaBackup.Models;

/// <summary>
/// Used for tracking progress (like in ProgressBar)
/// </summary>
public sealed class ProgressTracker : INotifyPropertyChanged
{
    private int _value;
    private int _maximumValue;
    
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(ProgressComputedValue));
            OnPropertyChanged(nameof(ProgressStatusText));
        }
    }

    public int MaximumValue
    {
        get => _maximumValue;
        set
        {
            _maximumValue = value;
            OnPropertyChanged(nameof(MaximumValue));
        }
    }

    public double ProgressComputedValue => MaximumValue == 0 ? 0 : (double)Value / MaximumValue;
    public string ProgressStatusText => $"{Value} / {MaximumValue}";
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}