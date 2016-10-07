using System;

namespace ClientTools
{
    /// <summary>
    /// Client class
    /// </summary>
    public class Client
    {
        #region Constructor(s)

        /// <summary>
        /// Default constructor for Client class
        /// </summary>
        public Client()
        { }

        /// <summary>
        /// Initialise the Client object from the line read from a delimited file.
        /// </summary>
        /// <param name="rawData"></param>
        public Client(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
                throw new ArgumentException("Input value cannot be null or empty");

            string firstName, lastName, address, phoneNumber;

            if (!ClientHelper.ParseRaw(rawData, out firstName, out lastName, out address, out phoneNumber))
            {
                throw new ArgumentException("Input string not in the correct format");
            }

            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region Helper Properties

        /// <summary>
        /// Get the street number from the address property
        /// </summary>
        public string StreetNumber
        {
            get
            {
                // TODO: Empty or invalid address will be ignored for now. Get information on how to handle this error as specifications has no information.

                if (string.IsNullOrWhiteSpace(Address) || !Address.Contains(" "))
                    return string.Empty;

                return Address.Substring(0, Address.IndexOf(' ')).Trim();
            }
        }

        /// <summary>
        /// Get the street name from the address property
        /// </summary>
        public string StreetName
        {
            get
            {
                // TODO: Empty or invalid address will be ignored for now. Get information on how to handle this error as specifications has no information.

                if (string.IsNullOrWhiteSpace(Address) || !Address.Contains(" "))
                    return string.Empty;

                return Address.Substring(Address.IndexOf(' ')).Trim();
            }
        }

        #endregion
    }
}
