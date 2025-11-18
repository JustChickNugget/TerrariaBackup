using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TerrariaBackup.Models;
using TerrariaBackup.Utilities;
using TerrariaBackup.Utilities.Terraria;

namespace TerrariaBackup.Windows;

/// <summary>
/// Window containing found players and worlds from the Terraria's directory. User chooses what to back up.
/// </summary>
public partial class BackupWindow
{
    private string TerrariaPath { get; }
    private string BackupPath { get; }
    private string BackupDirectoryName { get; }

    public ObservableCollection<SelectableItem> Players { get; } = [];
    public ObservableCollection<SelectableItem> Worlds { get; } = [];
    public ProgressTracker BackupProgressTracker { get; } = new();

    private Action<int, int> ProgressCallback { get; }

    public BackupWindow(string terrariaPath, string backupPath, string backupDirectoryName)
    {
        InitializeComponent();
        DataContext = this;

        TerrariaPath = terrariaPath;
        BackupPath = backupPath;
        BackupDirectoryName = backupDirectoryName;

        ProgressCallback = (copiedFiles, totalFiles) =>
        {
            Dispatcher.Invoke(() =>
            {
                BackupProgressTracker.Value = copiedFiles;
                BackupProgressTracker.MaximumValue = totalFiles;
            });
        };
    }

    #region MAIN EVENTS

    /// <summary>
    /// Select all players.
    /// </summary>
    private void PlayersSelectAllButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem player in Players)
            {
                player.Checked = true;
            }
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Deselect all players.
    /// </summary>
    private void PlayersDeselectAllButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem player in Players)
            {
                player.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Select all worlds.
    /// </summary>
    private void WorldsSelectAllButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem world in Worlds)
            {
                world.Checked = true;
            }
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Deselect all worlds.
    /// </summary>
    private void WorldsDeselectAllButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem world in Worlds)
            {
                world.Checked = false;
            }
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Start backup operation.
    /// </summary>
    private async void BackupButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<string?> selectedPlayers = Players
                .Where(player => player.Checked)
                .Select(player => player.Name)
                .ToList();

            List<string?> selectedWorlds = Worlds
                .Where(world => world.Checked)
                .Select(world => world.Name)
                .ToList();

            string selectedPlayersString = string.Join(";", selectedPlayers);
            string selectedWorldsString = string.Join(";", selectedWorlds);

            if (string.IsNullOrEmpty(selectedPlayersString) && string.IsNullOrEmpty(selectedWorldsString))
            {
                throw new ArgumentNullException(null, "Select at least one player or world.");
            }

            await BackupUtilities.Backup(
                TerrariaPath,
                BackupPath,
                BackupDirectoryName,
                selectedPlayersString,
                selectedWorldsString,
                ProgressCallback);
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    #endregion

    #region WINDOW EVENTS

    /// <summary>
    /// Get all players and worlds and put them into list views on window load.
    /// </summary>
    private void Window_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<string> playerNames = DataLoader.LoadPlayerNames(TerrariaPath);
            List<string> worldNames = DataLoader.LoadWorldNames(TerrariaPath);

            foreach (string playerName in playerNames)
            {
                Players.Add(new SelectableItem
                {
                    Name = playerName,
                    Checked = true
                });
            }

            foreach (string worldName in worldNames)
            {
                Worlds.Add(new SelectableItem
                {
                    Name = worldName,
                    Checked = true
                });
            }
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    #endregion

    /// <summary>
    /// Handle user's input. If the user presses 'Enter', the button will be pressed that starts backup process.
    /// </summary>
    private void Window_OnKeyDown(object sender, KeyEventArgs e)
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
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
}