using TerrariaBackup.Utilities;
using TerrariaBackup.Utilities.Terraria;
using TerrariaBackup.Models;
using System.Collections.ObjectModel;
using System.IO;
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

    public BackupWindow(string terrariaPath, string backupPath, string backupDirectoryName)
    {
        InitializeComponent();
        DataContext = this;

        TerrariaPath = terrariaPath;
        BackupPath = backupPath;
        BackupDirectoryName = backupDirectoryName;
    }

    #region MAIN EVENTS

    /// <summary>
    /// Start backup operation.
    /// </summary>
    private void BackupButton_Click(object sender, RoutedEventArgs e)
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
            
            BackupUtilities.Backup(
                TerrariaPath,
                BackupPath,
                BackupDirectoryName,
                selectedPlayersString,
                selectedWorldsString);

            Close();
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