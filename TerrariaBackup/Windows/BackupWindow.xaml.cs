using TerrariaBackup.Utilities;
using TerrariaBackup.Utilities.Terraria;
using TerrariaBackup.Models;
using System.Collections.ObjectModel;
using System.Windows;

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
    public static ProgressTracker BackupProgressTracker { get; } = new();

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
    private void PlayersSelectAllButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem player in Players)
                player.Checked = true;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Deselect all players.
    /// </summary>
    private void PlayersDeselectAllButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem player in Players)
                player.Checked = false;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Select all worlds.
    /// </summary>
    private void WorldsSelectAllButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem world in Worlds)
                world.Checked = true;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Deselect all worlds.
    /// </summary>
    private void WorldsDeselectAllButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            foreach (SelectableItem world in Worlds)
                world.Checked = false;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    /// <summary>
    /// Start backup operation.
    /// </summary>
    private async void BackupButton_Click(object sender, RoutedEventArgs e)
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
                throw new ArgumentNullException(null, "Select at least one player or world.");

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
}