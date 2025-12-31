using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TerrariaBackup.Models.Terraria;
using TerrariaBackup.Other;

namespace TerrariaBackup.Utilities.Terraria;

/// <summary>
/// Backup utilities for Terraria.
/// </summary>
public static class BackupUtilities
{
    /// <summary>
    /// Start backup operation.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory</param>
    /// <param name="backupPath">Path to the backup directory</param>
    /// <param name="backupDirectoryName">Name of the backup directory</param>
    /// <param name="selectedPlayers">List of selected players</param>
    /// <param name="selectedWorlds">List of selected worlds</param>
    /// <param name="progressCallback">Progress callback</param>
    public static async Task Backup(
        string terrariaPath,
        string backupPath,
        string backupDirectoryName,
        List<string?> selectedPlayers,
        List<string?> selectedWorlds,
        Action<int, int>? progressCallback = null)
    {
        if (string.IsNullOrEmpty(terrariaPath))
        {
            throw new ArgumentNullException(null, "Terraria path cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(backupPath))
        {
            throw new ArgumentNullException(null, "Backup path cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(backupDirectoryName))
        {
            throw new ArgumentNullException(null, "Backup directory name cannot be null or empty.");
        }

        await Task.Run(() =>
        {
            List<Player> players = DataLoader.FindPlayers(terrariaPath, selectedPlayers);
            List<World> worlds = DataLoader.FindWorlds(terrariaPath, selectedWorlds);

            int copiedFiles = 0;

            int totalFiles =
                players.Sum(player => player.Files.Count + player.MapFiles.Count) +
                worlds.Sum(world => world.Files.Count + world.SubworldFiles.Count);

            progressCallback?.Invoke(copiedFiles, totalFiles);

            string backupDirectoryNameClean =
                string.Join('_', backupDirectoryName.Split(Path.GetInvalidFileNameChars()));

            string backupDirectoryPath = Path.Combine(backupPath, backupDirectoryNameClean);

            if (Directory.Exists(backupDirectoryPath))
            {
                throw new IOException("Directory already exists.");
            }

            string playersPath = Path.Combine(backupDirectoryPath, Constants.PlayersDirectoryName);
            string worldsPath = Path.Combine(backupDirectoryPath, Constants.WorldsDirectoryName);

            Directory.CreateDirectory(backupDirectoryPath);
            Directory.CreateDirectory(playersPath);
            Directory.CreateDirectory(worldsPath);

            foreach (Player player in players)
            {
                foreach (string playerFile in player.Files)
                {
                    File.Copy(playerFile, Path.Combine(playersPath, Path.GetFileName(playerFile)));
                    progressCallback?.Invoke(++copiedFiles, totalFiles);
                }

                if (player.MapFiles.Count <= 0)
                {
                    continue;
                }

                string playerMapsPath = Path.Combine(playersPath, player.Name);
                Directory.CreateDirectory(playerMapsPath);

                foreach (string playerMapFile in player.MapFiles)
                {
                    File.Copy(playerMapFile, Path.Combine(playerMapsPath, Path.GetFileName(playerMapFile)));
                    progressCallback?.Invoke(++copiedFiles, totalFiles);
                }
            }

            foreach (World world in worlds)
            {
                foreach (string worldFile in world.Files)
                {
                    File.Copy(worldFile, Path.Combine(worldsPath, Path.GetFileName(worldFile)));
                    progressCallback?.Invoke(++copiedFiles, totalFiles);
                }

                if (world.SubworldFiles.Count <= 0)
                {
                    continue;
                }

                string subworldsPath = Path.Combine(worldsPath, world.Name);
                Directory.CreateDirectory(subworldsPath);

                foreach (string subworldFile in world.SubworldFiles)
                {
                    File.Copy(subworldFile, Path.Combine(subworldsPath, Path.GetFileName(subworldFile)));
                    progressCallback?.Invoke(++copiedFiles, totalFiles);
                }
            }
        });
    }
}