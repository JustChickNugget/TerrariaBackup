namespace TerrariaBackup.Structs.Terraria;

/// <summary>
/// Contains the player's name, the player's files found, and the files found containing map data for the player.
/// </summary>
public readonly struct Player
{
    public required string Name { get; init; }
    public required List<string> Files { get; init; }
    public required List<string> MapFiles { get; init; }
}