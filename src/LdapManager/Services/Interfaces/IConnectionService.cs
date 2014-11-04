// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConnectionService.cs" company="Simon Walker">
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
//   The ConnectionService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Services.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;

    using LdapManager.Models;

    /// <summary>
    /// The ConnectionService interface.
    /// </summary>
    public interface IConnectionService
    {
        #region Public Properties

        /// <summary>
        /// Gets the directory root.
        /// </summary>
        DirectoryEntry DirectoryRoot { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The bind.
        /// </summary>
        void Bind();

        #endregion

        /// <summary>
        /// The get attributes.
        /// </summary>
        /// <param name="distinguishedName">
        /// The distinguished name.
        /// </param>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        /// <returns>
        /// The <see cref="List{DirectoryAttribute}"/>.
        /// </returns>
        IEnumerable<DirectoryAttribute> GetAttributes(string distinguishedName, params string[] attributes);

        /// <summary>
        /// The fetch children.
        /// </summary>
        /// <param name="distinguishedName">
        /// The distinguished name.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<DirectoryEntry> FetchChildren(string distinguishedName);

        IEnumerable<string> GetBaseDNs();
    }
}