using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using TerrariaBackup.Models;

namespace TerrariaBackup.ViewModels;

/// <summary>
/// A view model for the Terraria data backup.
/// </summary>
public sealed class BackupViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// A constructor for the backup view model.
    /// </summary>
    public BackupViewModel()
    {
        ProgressCallback = (copiedFiles, totalFiles) =>
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                BackupProgressTracker.CurrentValue = copiedFiles;
                BackupProgressTracker.MaximumValue = totalFiles;
            });
        };
    }

    /// <summary>
    /// Property: observable collection of players.
    /// </summary>
    public ObservableCollection<SelectableItem>? Players
    {
        get => _players;
        set
        {
            if (_players == value)
            {
                return;
            }

            _players = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Property: observable collection of worlds.
    /// </summary>
    public ObservableCollection<SelectableItem>? Worlds
    {
        get => _worlds;
        set
        {
            if (_worlds == value)
            {
                return;
            }

            _worlds = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Backup progress tracker.
    /// </summary>
    public ProgressTracker BackupProgressTracker { get; } = new();

    /// <summary>
    /// Backup progress callback.
    /// </summary>
    public Action<int, int>? ProgressCallback { get; }

    /// <summary>
    /// Field: observable collection of players.
    /// </summary>
    private ObservableCollection<SelectableItem>? _players = [];

    /// <summary>
    /// Field: observable collection of worlds.
    /// </summary>
    private ObservableCollection<SelectableItem>? _worlds = [];

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