using System;
using System.IO;

namespace TerrariaBackup.Other;

/// <summary>
/// A static class that contains public constant variables for access throughout the application.
/// </summary>
public static class Constants
{
    /// <summary>
    /// A static constructor.
    /// </summary>
    static Constants()
    {
        if (OperatingSystem.IsWindows())
        {
            DefaultTerrariaPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "My Games",
                "Terraria");
        }
        else if (OperatingSystem.IsMacOS())
        {
            DefaultTerrariaPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Library",
                "Application Support",
                "Terraria");
        }
        else if (OperatingSystem.IsLinux())
        {
            DefaultTerrariaPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local",
                "share",
                "Terraria");
        }
        else
        {
            DefaultTerrariaPath = "";
        }
    }

    /// <summary>
    /// Default Terraria path.
    /// </summary>
    public static string DefaultTerrariaPath { get; }

    /// <summary>
    /// Default backup path.
    /// </summary>
    public static string DefaultBackupPath { get; } =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}";

    /// <summary>
    /// Terraria player directory name.
    /// </summary>
    public static string PlayersDirectoryName => "Players";

    /// <summary>
    /// Terraria world directory name.
    /// </summary>
    public static string WorldsDirectoryName => "Worlds";

    /// <summary>
    /// Link to the developer's page.
    /// </summary>
    public static string DeveloperLink => "https://github.com/JustChickNugget";

    /// <summary>
    /// Link to the application's repository.
    /// </summary>
    public static string RepositoryLink { get; } = $"{DeveloperLink}/TerrariaBackup";

    /// <summary>
    /// API link used to retrieve latest release's information.
    /// </summary>
    public static string LatestReleaseApiLink =>
        "https://api.github.com/repos/JustChickNugget/TerrariaBackup/releases/latest";

    /// <summary>
    /// Default enumeration options for enumerating files / directories.
    /// </summary>
    public static EnumerationOptions DefaultEnumerationOptions { get; } = new()
    {
        IgnoreInaccessible = true,
        RecurseSubdirectories = false
    };
}