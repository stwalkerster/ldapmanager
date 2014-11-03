// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryEntry.cs" company="Simon Walker">
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
//   The directory entry base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using LdapManager.Services.Interfaces;

    /// <summary>
    /// The directory entry base.
    /// </summary>
    public class DirectoryEntry
    {
        #region Fields

        /// <summary>
        /// The attributes.
        /// </summary>
        private IEnumerable<DirectoryAttribute> attributes;

        /// <summary>
        /// The children.
        /// </summary>
        private IEnumerable<DirectoryEntry> children;

        /// <summary>
        /// The distinguished name.
        /// </summary>
        private string dn;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DirectoryEntry"/> class.
        /// </summary>
        /// <param name="distinguishedName">
        /// The distinguished name.
        /// </param>
        public DirectoryEntry(string distinguishedName)
        {
            this.RelativeDN = new DirectoryAttribute(distinguishedName.Split(new[] { ',' }, 2).First());
            this.DN = distinguishedName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public IEnumerable<DirectoryAttribute> Attributes
        {
            get
            {
                return this.attributes ?? (this.attributes = this.ConnectionService.GetAttributes(this.DN));
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public IEnumerable<DirectoryEntry> Children
        {
            get
            {
                return this.children ?? (this.children = this.ConnectionService.FetchChildren(this.DN));
            }
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public IConnectionService ConnectionService { get; set; }

        /// <summary>
        /// Gets the DN.
        /// </summary>
        public string DN
        {
            get
            {
                return this.dn;
            }

            private set
            {
                this.dn = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether has been resolved.
        /// </summary>
        public bool HasBeenResolved
        {
            get
            {
                return this.children != null;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is base.
        /// </summary>
        public bool IsBase { get; set; }

        /// <summary>
        /// Gets the relative DN.
        /// </summary>
        public DirectoryAttribute RelativeDN { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.IsBase ? this.DN : this.RelativeDN.ToString();
        }

        #endregion
    }
}