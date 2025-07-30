using TerrariaBackup.Structs.API;
using TerrariaBackup.Other;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Windows;

namespace TerrariaBackup.Utilities;

/// <summary>
/// Used for common functions that are repeated and used many times throughout the application.
/// </summary>
public static class ToolBox
{
    /// <summary>
    /// Check for updates using GitHub's API.
    /// </summary>
    public static async void CheckForUpdates()
    {
        try
        {
            await Task.Run(async() =>
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", "TerrariaBackup");
            
                string response = await client.GetStringAsync
                    ("https://api.github.com/repos/JustChickNugget/TerrariaBackup/releases/latest");

                GitHubApiResponse gitHubApiResponse = JsonSerializer.Deserialize<GitHubApiResponse>(response);

                Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                Version latestReleaseVersion = Version.Parse(gitHubApiResponse.TagName + ".0");

                if (latestReleaseVersion.CompareTo(currentVersion) >= 1)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                        "An update is available. Would you like to download the update?",
                        "Update checker",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);
                    
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = Constants.RepositoryLink,
                            UseShellExecute = true
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Current version is up to date.", "Update checker",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
        catch (Exception ex)
        {
            PrintException(ex);
        }
    }
    
    /// <summary>
    /// Print exception if it has occurred.
    /// </summary>
    /// <param name="ex">An object containing the exception stack trace.</param>
    public static void PrintException(Exception ex)
    {
        string title = $"An exception has occurred ({ex.GetType().FullName})";
        
#if DEBUG
        string exceptionString = ex.ToString();
        
        MessageBox.Show(exceptionString, title, MessageBoxButton.OK, MessageBoxImage.Error);
        Debug.WriteLine(exceptionString);
#else
        MessageBox.Show(ex.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
#endif
    }
}