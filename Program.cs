using System;

namespace apikey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string command = args[0].ToLowerInvariant();

            switch (command)
            {
                case "view":
                    Console.WriteLine("API Key:");
                    Console.WriteLine(ApiKeyManager.GetOrCreateApiKey());
                    break;

                case "regen":
                    Console.WriteLine("New API Key:");
                    Console.WriteLine(ApiKeyManager.RegenerateApiKey());
                    break;

                default:
                    ShowHelp();
                    break;
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("LlamaApiKey CLI");
            Console.WriteLine("Commands:");
            Console.WriteLine("  view   - Show API key");
            Console.WriteLine("  regen  - Generate new API key");
        }
    }
}
