namespace TerrariaBackup.Structs.Terraria;

/// <summary>
/// Contains the world's name, the world's files found, and the subworld's files.
/// </summary>
public readonly struct World
{
    public required string Name { get; init; }
    public required List<string> Files { get; init; }
    public required List<string> SubworldFiles { get; init; }
}