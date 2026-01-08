using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TerrariaBackup.Models;
using TerrariaBackup.Utilities.Terraria;
using TerrariaBackup.ViewModels;

namespace TerrariaBackup.Windows;

/// <summary>
/// Window for the Terraria data backup.
/// </summary>
public partial class BackupWindow : Window
{
    /// <summary>
    /// View model of the backup window.
    /// </summary>
    private BackupViewModel BackupViewModel { get; } = new();

    /// <summary>
    /// Terraria path.
    /// </summary>
    private string TerrariaPath { get; }

    /// <summary>
    /// Backup path.
    /// </summary>
    private string BackupPath { get; }

    /// <summary>
    /// Backup directory name.
    /// </summary>
    private string BackupDirectoryName { get; }

    /// <summary>
    /// A constructor of the backup window.
    /// </summary>
    public BackupWindow(string terrariaPath, string backupPath, string backupDirectoryName)
    {
        InitializeComponent();
        DataContext = BackupViewModel;

        TerrariaPath = terrariaPath;
        BackupPath = backupPath;
        BackupDirectoryName = backupDirectoryName;
    }

    #region Main events

    /// <summary>
    /// Select all players.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void PlayersSelectAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Players == null)
            {
                throw new ArgumentNullException(null, "Collection of players is null.");
            }

            foreach (SelectableItem player in BackupViewModel.Players)
            {
                player.IsSelected = true;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(PlayersSelectAllButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(PlayersSelectAllButton_OnClick));
        }
    }

    /// <summary>
    /// Deselect all players.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void PlayersDeselectAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Players == null)
            {
                throw new ArgumentNullException(null, "Collection of players is null.");
            }

            foreach (SelectableItem player in BackupViewModel.Players)
            {
                player.IsSelected = false;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(PlayersDeselectAllButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(PlayersDeselectAllButton_OnClick));
        }
    }

    /// <summary>
    /// Select all worlds.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void WorldsSelectAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Worlds == null)
            {
                throw new ArgumentNullException(null, "Collection of worlds is null.");
            }

            foreach (SelectableItem world in BackupViewModel.Worlds)
            {
                world.IsSelected = true;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(WorldsSelectAllButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(WorldsSelectAllButton_OnClick));
        }
    }

    /// <summary>
    /// Deselect all worlds.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void WorldsDeselectAllButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Worlds == null)
            {
                throw new ArgumentNullException(null, "Collection of worlds is null.");
            }

            foreach (SelectableItem world in BackupViewModel.Worlds)
            {
                world.IsSelected = false;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(WorldsDeselectAllButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(WorldsDeselectAllButton_OnClick));
        }
    }

    /// <summary>
    /// Backup Terraria data.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void BackupButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Players == null)
            {
                throw new ArgumentNullException(null, "Collection of players is null.");
            }

            if (BackupViewModel.Worlds == null)
            {
                throw new ArgumentNullException(null, "Collection of worlds is null.");
            }

            List<string?> selectedPlayers = BackupViewModel.Players
                .Where(player => player.IsSelected)
                .Select(player => player.Name)
                .ToList();

            List<string?> selectedWorlds = BackupViewModel.Worlds
                .Where(world => world.IsSelected)
                .Select(world => world.Name)
                .ToList();

            if (selectedPlayers.Count == 0 && selectedWorlds.Count == 0)
            {
                throw new ArgumentNullException(null, "Select at least one player or one world.");
            }

            await BackupUtilities.Backup(
                TerrariaPath,
                BackupPath,
                BackupDirectoryName,
                selectedPlayers,
                selectedWorlds,
                BackupViewModel.ProgressCallback);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(BackupButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(BackupButton_OnClick));
        }
    }

    /// <summary>
    /// Close window.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(CloseButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(CloseButton_OnClick));
        }
    }

    #endregion

    #region Window events

    /// <summary>
    /// Handle window load.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void Window_OnLoaded(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (BackupViewModel.Players == null)
            {
                throw new ArgumentNullException(null, "Collection of players is null.");
            }

            if (BackupViewModel.Worlds == null)
            {
                throw new ArgumentNullException(null, "Collection of worlds is null.");
            }

            List<string> playerNames = DataLoader.LoadPlayerNames(TerrariaPath);
            List<string> worldNames = DataLoader.LoadWorldNames(TerrariaPath);

            foreach (string playerName in playerNames)
            {
                BackupViewModel.Players.Add(new SelectableItem
                {
                    Name = playerName,
                    IsSelected = true
                });
            }

            foreach (string worldName in worldNames)
            {
                BackupViewModel.Worlds.Add(new SelectableItem
                {
                    Name = worldName,
                    IsSelected = true
                });
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(Window_OnLoaded));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(Window_OnLoaded));
        }
    }

    /// <summary>
    /// Handle user input.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void Window_OnKeyDown(object? sender, KeyEventArgs e)
    {
        try
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            e.Handled = true;
            BackupButton_OnClick(sender, e);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(BackupWindow),
                nameof(Window_OnKeyDown));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(BackupWindow),
                nameof(Window_OnKeyDown));
        }
    }

    #endregion
}