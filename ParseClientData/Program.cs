using ClientTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParseClientData
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check the command line arguments. If not correct, exit the application.
            if (!ArgumentsCorrect(args)) return;

            // Populate the clients from the input file.
            var clients = PopulateClients(args[0]);

            // If clients is null, there was an error. Exit the application.
            if (clients == null) return;                      
            
            // Output Firstname, Lastname frequency file.
            CreateOutput(ClientHelper.OutputFirstNameLastNameFrequency, "Output Firstname, Lastname frequency file... ", args[0], clients);

            // Output addresses file.
            CreateOutput(ClientHelper.OutputAddressesSortedByStreetName, "Output addresses file... ", args[0], clients);
            
            // All done... Press any key to continue.
            Console.WriteLine(Environment.NewLine + "Press any key to continue");
            Console.ReadKey();
        }

        #region Helpers

        /// <summary>
        /// Check the command line arguments. If not correct, print the usage to the console.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool ArgumentsCorrect(string[] args)
        {
            if (args.Count() != 1)
            {
                Console.WriteLine("Usage: ParseClientData <Param1: InputFile>");
                Console.WriteLine();
                Console.ReadKey();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Populate the clients from the input file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static List<Client> PopulateClients(string filename)
        {
            string error = string.Empty;

            Console.WriteLine("Read file: " + filename);

            // Populate the clients from the input file.
            var clients = ClientHelper.ReadCSVFile(filename, out error);

            Console.WriteLine("Done" + Environment.NewLine);

            // If 'error' is not null or empty, something went wrong and the value of 'error' contains the error message.
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Unexpected error reading file: " + filename);
                Console.WriteLine("\t -->" + error);
                Console.WriteLine("Aborting operation..." + Environment.NewLine);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                return null;
            }

            return clients;
        }

        /// <summary>
        /// Output the file for the given OutputFileDelegate
        /// </summary>
        /// <param name="output"></param>
        /// <param name="description"></param>
        /// <param name="filename"></param>
        /// <param name="clients"></param>
        private static void CreateOutput(ClientHelper.OutputFileDelegate output, string description, string filename, System.Collections.Generic.List<Client> clients)
        {
            Console.WriteLine(description);
            var result = output(clients, filename);
            Console.WriteLine(((string.IsNullOrEmpty(result)) ? "Done." :result) + Environment.NewLine);
        }

        #endregion
    }
}
