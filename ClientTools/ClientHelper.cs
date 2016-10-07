using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClientTools
{
    public static class ClientHelper
    {
        private static char[] delimiterChars = { ',', ';' };

        public static bool IsClientHeaderRow(this Client client)
        {
            return (
                client.FirstName.CompareTo("FirstName") == 0 &&
                client.LastName.CompareTo("LastName") == 0 &&
                client.Address.CompareTo("Address") == 0 &&
                client.PhoneNumber.CompareTo("PhoneNumber") == 0
                );
        }

        internal static bool ParseRaw(string rawData, out string firstName, out string lastName, out string address, out string phoneNumber)
        {
            firstName = string.Empty;
            lastName = string.Empty;
            address = string.Empty;
            phoneNumber = string.Empty;

            // Input string cannot be empty
            if (string.IsNullOrEmpty(rawData))
                return false;

            // Split delimted string
            var items = rawData.Split(delimiterChars);

            // Expected count is 4
            if (items.Count() != 4)
            {
                return false;
            }

            firstName = items[0].Trim();
            lastName = items[1].Trim();
            address = items[2].Trim();
            phoneNumber = items[3].Trim();

            return true;
        }

        public static List<Client> ReadCSVFile(string filename, out string error)
        {
            error = string.Empty;

            if (!File.Exists(filename))
            {
                error = "Unable to access file.";
                return null;
            }
            else
            {
                var result = new List<Client>();

                using (var reader = new StreamReader(File.OpenRead(filename)))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        try
                        {
                            var client = new Client(line);

                            if (!client.IsClientHeaderRow())
                                result.Add(client);
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                            return result;
                        }
                    }
                }

                return result;
            }
        }

        public delegate string OutputFileDelegate(List<Client> clients, string filename);

        public static string OutputFirstNameLastNameFrequency(List<Client> clients, string filename)
        {
            string result = string.Empty;

            try
            {
                var outputFilename = GenerateOutputFilename(filename, "_NameFrequency");

                // Take Firstname and Lastname and make one list (concat) and then group the new list.
                // Order by Count descending and then by name ascending.
                var query = clients
                    .Select(r => r.FirstName)
                    .Concat(clients.Select(r => r.LastName))
                    .GroupBy(r => r)
                    .Select(r => new { Name = r.Key, Count = r.Count() })
                    .OrderByDescending(r => r.Count)
                    .ThenBy(r => r.Name);

                using (StreamWriter writer = new StreamWriter(outputFilename))
                {
                    foreach (var item in query)
                        writer.WriteLine("{0}, {1}", item.Name, item.Count);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string OutputAddressesSortedByStreetName(List<Client> clients, string filename)
        {
            string result = string.Empty;

            try
            {
                var outputFilename = GenerateOutputFilename(filename, "_Addresses");

                // Use the Client class helper methods to sort by street name and then by street number.
                var query = clients
                    .OrderBy(r => r.StreetName)
                    .ThenBy(r => r.StreetNumber)
                    .Select(r => new { Address = r.Address });

                // TODO: Incorrect Address fields will be displayed at the top of the list and not sorted. Fix handling in Client class - Helper Properties.

                using (StreamWriter writer = new StreamWriter(outputFilename))
                {
                    foreach (var item in query)
                        writer.WriteLine(item.Address);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        // Output the text file(s) to the same directory as the input file
        internal static string GenerateOutputFilename(string inputFilename, string addToFilename)
        {
            var path = Path.GetDirectoryName(inputFilename);
            var filename = Path.GetFileNameWithoutExtension(inputFilename);

            return Path.Combine(path, filename + addToFilename + ".txt");
        }
    }
}
