using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace TerrariaBackup.Utilities;

/// <summary>
/// Storage operations.
/// </summary>
public static class StorageHelper
{
    /// <summary>
    /// Get folder from the picker.
    /// </summary>
    /// <param name="storageProvider">Storage provider service object</param>
    /// <param name="startLocation">Start location of the picker</param>
    /// <param name="title">Title of the picker</param>
    /// <returns>If nothing was selected, return null; if a folder was selected, return the local path to that folder.</returns>
    public static async Task<string?> GetFolderPathFromPicker(
        IStorageProvider storageProvider,
        string startLocation,
        string title)
    {
        string suggestedStartLocation;

        if (!string.IsNullOrEmpty(startLocation) && Directory.Exists(startLocation))
        {
            suggestedStartLocation = startLocation;
        }
        else
        {
            suggestedStartLocation = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        IStorageFolder? startLocationStorageFolder =
            await storageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);

        startLocationStorageFolder ??= await storageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Documents);

        FolderPickerOpenOptions folderPickerOpenOptions = new()
        {
            AllowMultiple = false,
            Title = title
        };

        if (startLocationStorageFolder != null)
        {
            folderPickerOpenOptions.SuggestedStartLocation = startLocationStorageFolder;
        }

        IReadOnlyList<IStorageFolder> storageFolders =
            await storageProvider.OpenFolderPickerAsync(folderPickerOpenOptions);

        return storageFolders.Count == 0
            ? null
            : Path.TrimEndingDirectorySeparator(storageFolders[0].Path.LocalPath);
    }
}