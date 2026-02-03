using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace apikey
{
    public static class ApiKeyManager
    {
        private static readonly string AppDataPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LlamaService");

        private static readonly string KeyFilePath =
            Path.Combine(AppDataPath, "apikey.dat");

        public static string GetOrCreateApiKey()
        {
            Directory.CreateDirectory(AppDataPath);

            if (!File.Exists(KeyFilePath))
            {
                var key = GenerateApiKey();
                SaveEncryptedKey(key);
                return key;
            }

            return LoadEncryptedKey();
        }

        public static string RegenerateApiKey()
        {
            var key = GenerateApiKey();
            SaveEncryptedKey(key);
            return key;
        }

        private static string GenerateApiKey(int length = 32)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        private static void SaveEncryptedKey(string key)
        {
            byte[] plain = Encoding.UTF8.GetBytes(key);

            byte[] encrypted =
                System.Security.Cryptography.ProtectedData.Protect(
                    plain,
                    null,
                    DataProtectionScope.LocalMachine);

            File.WriteAllBytes(KeyFilePath, encrypted);
        }

        private static string LoadEncryptedKey()
        {
            byte[] encrypted = File.ReadAllBytes(KeyFilePath);

            byte[] decrypted =
                System.Security.Cryptography.ProtectedData.Unprotect(
                    encrypted,
                    null,
                    DataProtectionScope.LocalMachine);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
