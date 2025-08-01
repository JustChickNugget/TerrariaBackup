using System.Diagnostics;
using System.IO;

namespace TerrariaBackup.Other;

/// <summary>
/// A static class that contains public variables for access throughout the application.
/// </summary>
public static class Constants
{
    public static string DefaultTerrariaPath { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\My Games\Terraria";
    public static string DefaultBackupPath { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}";
    
    public static string PlayersDirectoryName => "Players";
    public static string WorldsDirectoryName => "Worlds";

    private static string DeveloperLink => "https://github.com/JustChickNugget";
    public static string RepositoryLink { get; } = $"{DeveloperLink}/TerrariaBackup";
    
    public static string ReleasesApiLink =>
        "https://api.github.com/repos/JustChickNugget/TerrariaBackup/releases/latest";
    
    public static ProcessStartInfo DeveloperGitHubProcessStartInfo { get; } = new()
    {
        FileName = DeveloperLink,
        UseShellExecute = true
    };
    
    public static EnumerationOptions DefaultEnumerationOptions { get; } = new()
    {
        IgnoreInaccessible = true,
        RecurseSubdirectories = false
    };
}