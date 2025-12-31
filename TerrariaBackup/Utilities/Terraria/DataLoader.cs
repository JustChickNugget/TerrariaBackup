using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TerrariaBackup.Models.Terraria;
using TerrariaBackup.Other;

namespace TerrariaBackup.Utilities.Terraria;

/// <summary>
/// Data loader for Terraria.
/// </summary>
public static class DataLoader
{
    /// <summary>
    /// Get all player names from the Terraria directory.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory</param>
    /// <returns>List of player names</returns>
    public static List<string> LoadPlayerNames(string terrariaPath)
    {
        string playersPath = Path.Combine(terrariaPath, Constants.PlayersDirectoryName);

        List<string> foundPlayers = Directory
            .GetFiles(playersPath, "*.*plr*", Constants.DefaultEnumerationOptions)
            .ToList();

        List<string> playerNames = [];

        foreach (string foundPlayer in foundPlayers)
        {
            string playerName = Path.GetFileName(foundPlayer)
                .Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];

            playerNames.Add(playerName);
        }

        playerNames = playerNames.Distinct().ToList();
        return playerNames;
    }

    /// <summary>
    /// Get all world names from the Terraria directory.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory</param>
    /// <returns>List of world names</returns>
    public static List<string> LoadWorldNames(string terrariaPath)
    {
        string worldsPath = Path.Combine(terrariaPath, Constants.WorldsDirectoryName);

        List<string> foundWorlds = Directory
            .GetFiles(worldsPath, "*.*wld*", Constants.DefaultEnumerationOptions)
            .ToList();

        List<string> worldNames = [];

        foreach (string foundWorld in foundWorlds)
        {
            string worldName = Path.GetFileName(foundWorld)
                .Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];

            worldNames.Add(worldName);
        }

        worldNames = worldNames.Distinct().ToList();
        return worldNames;
    }

    /// <summary>
    /// Find players by selected player names.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory</param>
    /// <param name="selectedPlayers">List of selected players</param>
    /// <returns>List of found players</returns>
    public static List<Player> FindPlayers(string terrariaPath, List<string?> selectedPlayers)
    {
        List<Player> players = [];
        string playersPath = Path.Combine(terrariaPath, Constants.PlayersDirectoryName);

        foreach (string? selectedPlayer in selectedPlayers)
        {
            if (string.IsNullOrEmpty(selectedPlayer))
            {
                continue;
            }

            List<string> playerFiles = Directory
                .GetFiles(playersPath, $"{selectedPlayer}.*plr*", Constants.DefaultEnumerationOptions)
                .ToList();

            List<string> playerMapFiles = [];
            string playerMapsPath = Path.Combine(playersPath, selectedPlayer);

            if (Directory.Exists(playerMapsPath))
            {
                playerMapFiles = Directory
                    .GetFiles(playerMapsPath, "*", Constants.DefaultEnumerationOptions)
                    .ToList();
            }

            players.Add(new Player
            {
                Name = selectedPlayer,
                Files = playerFiles,
                MapFiles = playerMapFiles
            });
        }

        return players;
    }

    /// <summary>
    /// Find worlds by selected world names.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory</param>
    /// <param name="selectedWorlds">List of selected worlds</param>
    /// <returns>List of found worlds</returns>
    public static List<World> FindWorlds(string terrariaPath, List<string?> selectedWorlds)
    {
        List<World> worlds = [];
        string worldsPath = Path.Combine(terrariaPath, Constants.WorldsDirectoryName);

        foreach (string? selectedWorld in selectedWorlds)
        {
            if (string.IsNullOrEmpty(selectedWorld))
            {
                continue;
            }

            List<string> worldFiles = Directory
                .GetFiles(worldsPath, $"{selectedWorld}.*wld*", Constants.DefaultEnumerationOptions)
                .ToList();

            List<string> subworldFiles = [];
            string subworldsPath = Path.Combine(worldsPath, selectedWorld);

            if (Directory.Exists(subworldsPath))
            {
                subworldFiles = Directory
                    .GetFiles(subworldsPath, "*", Constants.DefaultEnumerationOptions)
                    .ToList();
            }

            worlds.Add(new World
            {
                Name = selectedWorld,
                Files = worldFiles,
                SubworldFiles = subworldFiles
            });
        }

        return worlds;
    }
}