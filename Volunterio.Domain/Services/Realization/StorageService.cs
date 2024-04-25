﻿using Volunterio.Data.Enums;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Helpers;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Settings.Abstraction;
using Volunterio.Domain.Validators.Runtime;
using FileInfo = Volunterio.Domain.Models.FileInfo;

namespace Volunterio.Domain.Services.Realization;

internal class StorageService(
    IUrlSettings urlSettings
) : IStorageService
{
    public async Task<string> SaveFileAsync(
        byte[] file,
        string fileExtension,
        FolderName folderName,
        CancellationToken cancellationToken = default
    )
    {
        var fileName = $"{Guid.NewGuid()}.{fileExtension.TrimStart('.')}";

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName.Files, folderName, fileName);

        var directory = Path.GetDirectoryName(filePath);

        RuntimeValidator.Assert(directory is not null, StatusCode.DirectoryNotFound);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        await File.WriteAllBytesAsync(filePath, file, cancellationToken);

        return
            $"{urlSettings.WebApiUrl.TrimEnd('/')}/api/v1/files/{JsonBase64Helper.Encode(new FileInfo(fileName, folderName))}";
    }

    public void RemoveFile(
        string fileUrl,
        FolderName folderName,
        CancellationToken cancellationToken = default
    )
    {
        var fileName = fileUrl.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

        if (fileName is null)
        {
            return;
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName.Files, folderName, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public Task<byte[]> GetFileAsync(
        FileInfo info,
        CancellationToken cancellationToken = default
    )
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName.Files, info.FolderName, info.FileName);

        RuntimeValidator.Assert(File.Exists(filePath), StatusCode.FileNotFound);

        return File.ReadAllBytesAsync(filePath, cancellationToken);
    }
}