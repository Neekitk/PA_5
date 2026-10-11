using System;
using System.IO;
using System.Security.Cryptography;

namespace DigitalUniversityArchive.Services;

public class FileEncryptionService
{
    public void EncryptFile(string sourcePath, string destinationPath)
    {
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine("Исходный файл не найден.");
            return;
        }

        byte[] key = RandomNumberGenerator.GetBytes(32);
        byte[] iv = RandomNumberGenerator.GetBytes(16);

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using FileStream inputFile = new FileStream(
            sourcePath,
            FileMode.Open,
            FileAccess.Read);

        using FileStream outputFile = new FileStream(
            destinationPath,
            FileMode.Create,
            FileAccess.Write);

        outputFile.Write(iv, 0, iv.Length);

        using CryptoStream cryptoStream = new CryptoStream(
            outputFile,
            aes.CreateEncryptor(),
            CryptoStreamMode.Write);

        inputFile.CopyTo(cryptoStream);

        Console.WriteLine("Файл успешно зашифрован.");
    }
}