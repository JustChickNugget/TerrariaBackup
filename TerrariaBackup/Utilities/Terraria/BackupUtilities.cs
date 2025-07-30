using TerrariaBackup.Structs.Terraria;
using TerrariaBackup.Other;
using System.IO;
using System.Windows;

namespace TerrariaBackup.Utilities.Terraria;

/// <summary>
/// Backup utilities for Terraria.
/// </summary>
public static class BackupUtilities
{
    /// <summary>
    /// Start backup operation. First, find players and worlds according to the data the user provided. Then create the
    /// main directory where user stores backups (in addition, remove restricted characters from the directory name,
    /// replacing them with '_', if the user entered them). Then copy everything that was found to the backup directory,
    /// creating a directory structure like Terraria does.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory.</param>
    /// <param name="backupPath">Path to the backup directory.</param>
    /// <param name="backupDirectoryName">Name of the backup directory.</param>
    /// <param name="selectedPlayers">String of selected players (separated by ';').</param>
    /// <param name="selectedWorlds">String of selected worlds (separated by ';').</param>
    public static async void Backup(string terrariaPath, string backupPath, string backupDirectoryName, string selectedPlayers, string selectedWorlds)
    {
        try
        {
            if (string.IsNullOrEmpty(terrariaPath))
                throw new ArgumentNullException(null, "Terraria path cannot be null or empty.");
            
            if (string.IsNullOrEmpty(backupPath))
                throw new ArgumentNullException(null, "Backup path cannot be null or empty.");
            
            if (string.IsNullOrEmpty(backupDirectoryName))
                throw new ArgumentNullException(null, "Backup directory name cannot be null or empty.");
            
            await Task.Run(() =>
            {
                List<Player> players = DataLoader.FindPlayers(terrariaPath, selectedPlayers);
                List<World> worlds = DataLoader.FindWorlds(terrariaPath, selectedWorlds);
            
                string backupDirectoryPath =
                    Path.Combine(backupPath, string.Join('_', backupDirectoryName.Split(Path.GetInvalidFileNameChars())));

                if (Directory.Exists(backupDirectoryPath))
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                        "Backup directory already exists. Do you want to delete it?",
                        "Backup directory exists",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);

                    if (messageBoxResult == MessageBoxResult.Yes)
                        Directory.Delete(backupDirectoryPath, true);
                    else
                        return;
                }
            
                string playersPath = Path.Combine(backupDirectoryPath, Constants.PlayersDirectoryName);
                string worldsPath = Path.Combine(backupDirectoryPath, Constants.WorldsDirectoryName);
            
                Directory.CreateDirectory(backupDirectoryPath);
                Directory.CreateDirectory(playersPath);
                Directory.CreateDirectory(worldsPath);
            
                foreach (Player player in players)
                {
                    foreach (string playerFile in player.Files)
                        File.Copy(playerFile, Path.Combine(playersPath, Path.GetFileName(playerFile)));

                    if (player.MapFiles.Count > 0)
                    {
                        string playerMapsPath = Path.Combine(playersPath, player.Name);
                        Directory.CreateDirectory(playerMapsPath);
                    
                        foreach (string playerMapFile in player.MapFiles)
                            File.Copy(playerMapFile, Path.Combine(playerMapsPath, Path.GetFileName(playerMapFile)));
                    }
                }

                foreach (World world in worlds)
                {
                    foreach (string worldFile in world.Files)
                        File.Copy(worldFile, Path.Combine(worldsPath, Path.GetFileName(worldFile)));

                    if (world.SubworldFiles.Count > 0)
                    {
                        string subworldsPath = Path.Combine(worldsPath, world.Name);
                        Directory.CreateDirectory(subworldsPath);
                    
                        foreach (string subworldFile in world.SubworldFiles)
                            File.Copy(subworldFile, Path.Combine(subworldsPath, Path.GetFileName(subworldFile)));
                    }
                }
            });
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
}