using System.IO;
using TerrariaBackup.Other;
using TerrariaBackup.Structs.Terraria;

namespace TerrariaBackup.Utilities.Terraria;

/// <summary>
/// Data loader for Terraria.
/// </summary>
public static class DataLoader
{
    /// <summary>
    /// Get all player names from the Terraria's directory.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory.</param>
    /// <returns>List of player names.</returns>
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
    /// Get all world names from the Terraria's directory.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria directory.</param>
    /// <returns>List of world names.</returns>
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
    /// <param name="terrariaPath">Path to the Terraria directory.</param>
    /// <param name="playerText">Player names provided by user.</param>
    /// <returns>List of found players.</returns>
    public static List<Player> FindPlayers(string terrariaPath, string playerText)
    {
        List<Player> players = [];
        string playersPath = Path.Combine(terrariaPath, Constants.PlayersDirectoryName);

        string[] playerNames = playerText
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .ToArray();

        foreach (string playerName in playerNames)
        {
            string playerMapsPath = Path.Combine(playersPath, playerName);

            List<string> playerFiles = Directory
                .GetFiles(playersPath, $"{playerName}.*plr*", Constants.DefaultEnumerationOptions)
                .ToList();

            List<string> playerMapFiles = [];

            if (Directory.Exists(playerMapsPath))
            {
                playerMapFiles = Directory
                    .GetFiles(playerMapsPath, "*", Constants.DefaultEnumerationOptions)
                    .ToList();
            }

            players.Add(new Player
            {
                Name = playerName,
                Files = playerFiles,
                MapFiles = playerMapFiles
            });
        }

        return players;
    }

    /// <summary>
    /// Find worlds by selected world names.
    /// </summary>
    /// <param name="terrariaPath">Path to the Terraria's directory.</param>
    /// <param name="worldText">World names provided by user.</param>
    /// <returns>List of found worlds.</returns>
    public static List<World> FindWorlds(string terrariaPath, string worldText)
    {
        List<World> worlds = [];
        string worldsPath = Path.Combine(terrariaPath, Constants.WorldsDirectoryName);

        string[] worldNames = worldText
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .ToArray();

        foreach (string worldName in worldNames)
        {
            string subworldsPath = Path.Combine(worldsPath, worldName);

            List<string> worldFiles = Directory
                .GetFiles(worldsPath, $"{worldName}.*wld*", Constants.DefaultEnumerationOptions)
                .ToList();

            List<string> subworldFiles = [];

            if (Directory.Exists(subworldsPath))
            {
                subworldFiles = Directory
                    .GetFiles(subworldsPath, "*", Constants.DefaultEnumerationOptions)
                    .ToList();
            }

            worlds.Add(new World
            {
                Name = worldName,
                Files = worldFiles,
                SubworldFiles = subworldFiles
            });
        }

        return worlds;
    }
}