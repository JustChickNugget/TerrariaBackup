using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TerrariaBackup.Other;
using TerrariaBackup.Utilities;
using TerrariaBackup.Windows;

namespace TerrariaBackup;

/// <summary>
/// Main window of the application.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// A constructor of the main window.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    #region Main events

    /// <summary>
    /// Go up one directory in the Terraria directory's path.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void TerrariaPathUpButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TerrariaPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Terraria path is null or empty.");
            }

            string directoryName = Path.GetDirectoryName(TerrariaPathTextBox.Text) ?? "";

            if (!string.IsNullOrEmpty(directoryName))
            {
                TerrariaPathTextBox.Text = directoryName;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathUpButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathUpButton_OnClick));
        }
    }

    /// <summary>
    /// Open Terraria directory.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void TerrariaPathOpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TerrariaPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Terraria path is null or empty.");
            }

            ProcessStartInfo processStartInfo = new()
            {
                FileName = TerrariaPathTextBox.Text,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            Process.Start(processStartInfo);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathOpenButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathOpenButton_OnClick));
        }
    }

    /// <summary>
    /// Select Terraria directory.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void TerrariaPathSelectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TerrariaPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Terraria path is null or empty.");
            }

            string? terrariaPath = await StorageHelper.GetFolderPathFromPicker(
                StorageProvider,
                TerrariaPathTextBox.Text,
                "Select Terraria directory");

            if (string.IsNullOrEmpty(terrariaPath))
            {
                return;
            }

            TerrariaPathTextBox.Text = terrariaPath;
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathSelectButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(TerrariaPathSelectButton_OnClick));
        }
    }

    /// <summary>
    /// Go up one directory in the backup directory's path.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void BackupPathUpButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Backup path is null or empty.");
            }

            string directoryName = Path.GetDirectoryName(BackupPathTextBox.Text) ?? "";

            if (!string.IsNullOrEmpty(directoryName))
            {
                BackupPathTextBox.Text = directoryName;
            }
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(BackupPathUpButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(BackupPathUpButton_OnClick));
        }
    }

    /// <summary>
    /// Open backup directory.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void BackupPathOpenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Backup path is null or empty.");
            }

            ProcessStartInfo processStartInfo = new()
            {
                FileName = BackupPathTextBox.Text,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            Process.Start(processStartInfo);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(BackupPathOpenButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(BackupPathOpenButton_OnClick));
        }
    }

    /// <summary>
    /// Select backup directory.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void BackupPathSelectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Backup path is null or empty.");
            }

            string? backupPath = await StorageHelper.GetFolderPathFromPicker(
                StorageProvider,
                BackupPathTextBox.Text,
                "Select backup directory");

            if (string.IsNullOrEmpty(backupPath))
            {
                return;
            }

            BackupPathTextBox.Text = backupPath;
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(BackupPathSelectButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(BackupPathSelectButton_OnClick));
        }
    }

    /// <summary>
    /// Set default value for the backup directory name.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void BackupDirectoryDefaultButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            BackupDirectoryNameTextBox.Text = "TerrariaBackup";
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(BackupDirectoryDefaultButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(BackupDirectoryDefaultButton_OnClick));
        }
    }

    /// <summary>
    /// Show found players and worlds in the Terraria directory.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    /// <exception cref="ArgumentNullException">Text box values are null or empty.</exception>
    /// <exception cref="DirectoryNotFoundException">Terraria player or world directories not found</exception>
    private async void ShowFoundDataButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TerrariaPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Terraria path is null or empty.");
            }

            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                throw new ArgumentNullException(null, "Backup path is null or empty.");
            }

            if (string.IsNullOrEmpty(BackupDirectoryNameTextBox.Text))
            {
                throw new ArgumentNullException(null, "Backup directory name is null or empty.");
            }

            string playersPath = Path.Combine(TerrariaPathTextBox.Text, Constants.PlayersDirectoryName);
            string worldsPath = Path.Combine(TerrariaPathTextBox.Text, Constants.WorldsDirectoryName);

            if (!Directory.Exists(playersPath))
            {
                throw new DirectoryNotFoundException("Player directory not found.");
            }

            if (!Directory.Exists(worldsPath))
            {
                throw new DirectoryNotFoundException("World directory not found.");
            }

            BackupWindow backupWindow = new(
                TerrariaPathTextBox.Text,
                BackupPathTextBox.Text,
                BackupDirectoryNameTextBox.Text);

            await backupWindow.ShowDialog(this);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(ShowFoundDataButton_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(ShowFoundDataButton_OnClick));
        }
    }

    #endregion

    #region Menu events

    /// <summary>
    /// Show "About" window.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void AboutApplicationMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Version? version = Assembly.GetExecutingAssembly().GetName().Version;

            if (version == null)
            {
                throw new ArgumentNullException(null, "Failed to get version of the application.");
            }

            NuggetLib.Views.Windows.AboutWindow aboutWindow = new(
                "Terraria Backup",
                "Backup your Terraria players and worlds",
                Constants.DeveloperLink,
                Constants.RepositoryLink,
                version);

            await aboutWindow.ShowDialog(this);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(AboutApplicationMenuItem_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(AboutApplicationMenuItem_OnClick));
        }
    }

    /// <summary>
    /// Check for updates.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void CheckForUpdatesMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Version? version = Assembly.GetExecutingAssembly().GetName().Version;

            if (version == null)
            {
                throw new ArgumentNullException(null, "Failed to get version of the application.");
            }

            NuggetLib.Views.Windows.UpdateCheckWindow updateCheckWindow = new(
                Constants.LatestReleaseLink,
                Constants.LatestReleaseApiLink,
                version);

            await updateCheckWindow.ShowDialog(this);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(CheckForUpdatesMenuItem_OnClick));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(CheckForUpdatesMenuItem_OnClick));
        }
    }

    #endregion

    #region Window events

    /// <summary>
    /// Handle window's load.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void Window_OnLoaded(object? sender, RoutedEventArgs e)
    {
        try
        {
            /*
             * The preprocessor "GITHUBSCREENSHOT" is used for creating screenshots where the username won't be visible.
             * Make sure to select a new solution configuration to make it work.
             */

#if GITHUBSCREENSHOT
            TerrariaPathTextBox.Text = Constants.DefaultTerrariaPath.Replace(Environment.UserName, "user");
            BackupPathTextBox.Text = Constants.DefaultBackupPath.Replace(Environment.UserName, "user");
#else
            if (Directory.Exists(Constants.DefaultTerrariaPath))
            {
                TerrariaPathTextBox.Text = Constants.DefaultTerrariaPath;
            }

            if (Directory.Exists(Constants.DefaultBackupPath))
            {
                BackupPathTextBox.Text = Constants.DefaultBackupPath;
            }
#endif
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(Window_OnLoaded));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(Window_OnLoaded));
        }
    }

    /// <summary>
    /// Handle user's input.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void Window_OnKeyDown(object? sender, KeyEventArgs e)
    {
        try
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            e.Handled = true;
            ShowFoundDataButton_OnClick(sender, e);
        }
        catch (Exception exception)
        {
            NuggetLib.Core.Utilities.DebugLogger.LogException(
                exception,
                nameof(MainWindow),
                nameof(Window_OnKeyDown));

            await NuggetLib.Views.Services.ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(MainWindow),
                nameof(Window_OnKeyDown));
        }
    }

    #endregion
}