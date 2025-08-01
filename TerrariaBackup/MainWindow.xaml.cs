using TerrariaBackup.Windows;
using TerrariaBackup.Utilities;
using TerrariaBackup.Other;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace TerrariaBackup;

/// <summary>
/// A window containing the main logic of the application.
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    #region MAIN EVENTS
    
    #region LOCATION
    
    /// <summary>
    /// Go up one directory for Terraria's path.
    /// </summary>
    private void TerrariaPathUpButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string previousDirectory = Path.GetDirectoryName(TerrariaPathTextBox.Text) ?? "";

            if (!string.IsNullOrEmpty(previousDirectory))
                TerrariaPathTextBox.Text = previousDirectory;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    /// <summary>
    /// Open Terraria's directory in the explorer.
    /// </summary>
    private void TerrariaPathOpenButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = TerrariaPathTextBox.Text,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            
            Process.Start(processStartInfo);
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    /// <summary>
    /// Choose Terraria's directory.
    /// </summary>
    private void TerrariaPathChooseButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFolderDialog openFolderDialog = new();

            if (!string.IsNullOrEmpty(TerrariaPathTextBox.Text))
                openFolderDialog.InitialDirectory = TerrariaPathTextBox.Text;

            if (openFolderDialog.ShowDialog() ?? false)
                TerrariaPathTextBox.Text = openFolderDialog.FolderName;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Go up one directory for backup's path.
    /// </summary>
    private void BackupPathUpButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string previousDirectory = Path.GetDirectoryName(BackupPathTextBox.Text) ?? "";

            if (!string.IsNullOrEmpty(previousDirectory))
                BackupPathTextBox.Text = previousDirectory;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Open backup's directory in the explorer.
    /// </summary>
    private void BackupPathOpenButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = BackupPathTextBox.Text,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            
            Process.Start(processStartInfo);
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Choose backup's directory.
    /// </summary>
    private void BackupPathChooseButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFolderDialog openFolderDialog = new();

            if (!string.IsNullOrEmpty(BackupPathTextBox.Text))
                openFolderDialog.InitialDirectory = BackupPathTextBox.Text;

            if (openFolderDialog.ShowDialog() ?? false)
                BackupPathTextBox.Text = openFolderDialog.FolderName;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    #endregion
    
    #region OPERATIONS
    
    /// <summary>
    /// Show found players and worlds in a Terraria's directory.
    /// </summary>
    private void ShowFoundButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string playersPath = Path.Combine(TerrariaPathTextBox.Text, Constants.PlayersDirectoryName);
            string worldsPath = Path.Combine(TerrariaPathTextBox.Text, Constants.WorldsDirectoryName);
            
            if (!Directory.Exists(playersPath) || !Directory.Exists(worldsPath))
                throw new DirectoryNotFoundException("Players or worlds directory not found.");
            
            BackupWindow backupWindow = new(
                TerrariaPathTextBox.Text,
                BackupPathTextBox.Text,
                BackupDirectoryNameTextBox.Text);
            
            backupWindow.ShowDialog();
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    #endregion
    
    #region MENU EVENTS
    
    /// <summary>
    /// Show information about the application.
    /// </summary>
    private void ApplicationAboutMenuItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            AboutWindow aboutWindow = new();
            aboutWindow.ShowDialog();
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Check for updates using GitHub's API.
    /// </summary>
    private async void ApplicationCheckForUpdatesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bool updatesAvailable = await ToolBox.CheckForUpdates();

            if (updatesAvailable)
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
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }

    /// <summary>
    /// Open developer's GitHub profile page.
    /// </summary>
    private void DeveloperGitHubMenuItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Process.Start(Constants.DeveloperGitHubProcessStartInfo);
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    #endregion
    
    #endregion
    
    #region WINDOW EVENTS
    
    /// <summary>
    /// Set text boxes' content if paths exist.
    /// </summary>
    private void Window_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Directory.Exists(Constants.DefaultTerrariaPath))
                TerrariaPathTextBox.Text = Constants.DefaultTerrariaPath;
            
            if (Directory.Exists(Constants.DefaultBackupPath))
                BackupPathTextBox.Text = Constants.DefaultBackupPath;
        }
        catch (Exception ex)
        {
            ToolBox.PrintException(ex);
        }
    }
    
    #endregion
}