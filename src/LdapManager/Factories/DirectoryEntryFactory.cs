// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryEntryFactory.cs" company="Simon Walker">
//   Copyright (C) 2014 Simon Walker
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
//   the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
//   SOFTWARE.
// </copyright>
// <summary>
//   The directory entry factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Factories
{
    using System.DirectoryServices.Protocols;

    using LdapManager.Models;
    using LdapManager.Services.Interfaces;

    /// <summary>
    /// The directory entry factory.
    /// </summary>
    public class DirectoryEntryFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="directoryEntry">
        /// The directory entry.
        /// </param>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <returns>
        /// The <see cref="DirectoryEntry"/>.
        /// </returns>
        public static DirectoryEntry Create(SearchResultEntry directoryEntry, IConnectionService connection)
        {
            var constructed = new DirectoryEntry(directoryEntry.DistinguishedName);

            constructed.ConnectionService = connection;

            foreach (var att in directoryEntry.Attributes)
            {
                //// constructed.Attributes.Add(DirectoryAttributeFactory.Create(att));
            }

            return constructed;
        }

        #endregion
    }
}