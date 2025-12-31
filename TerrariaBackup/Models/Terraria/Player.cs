using System.Collections.Generic;

namespace TerrariaBackup.Models.Terraria;

/// <summary>
/// Terraria player data.
/// </summary>
public sealed record Player
{
    /// <summary>
    /// Name of the player.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Player data files.
    /// </summary>
    public required List<string> Files { get; init; }

    /// <summary>
    /// Player map data files.
    /// </summary>
    public required List<string> MapFiles { get; init; }
}