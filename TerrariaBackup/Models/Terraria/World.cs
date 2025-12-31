using System.Collections.Generic;

namespace TerrariaBackup.Models.Terraria;

/// <summary>
/// Terraria world data.
/// </summary>
public sealed record World
{
    /// <summary>
    /// Name of the world.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// World data files.
    /// </summary>
    public required List<string> Files { get; init; }

    /// <summary>
    /// Subworld data files.
    /// </summary>
    public required List<string> SubworldFiles { get; init; }
}