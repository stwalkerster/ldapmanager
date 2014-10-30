// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionBookmark.cs" company="Simon Walker">
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
//   The connection bookmark.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LdapManager.Models
{
    using Caliburn.Micro;

    /// <summary>
    /// The connection bookmark.
    /// </summary>
    public class ConnectionBookmark : IHaveDisplayName
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ConnectionBookmark"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="bindDn">
        /// The bind DN.
        /// </param>
        /// <param name="bindPassword">
        /// The bind password.
        /// </param>
        /// <param name="baseDn">
        /// The base DN.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public ConnectionBookmark(string host, int port, string bindDn, string bindPassword, string baseDn, int version, string displayName)
        {
            this.Port = port;
            this.BindDn = bindDn;
            this.BindPassword = bindPassword;
            this.BaseDn = baseDn;
            this.Version = version;
            this.Host = host;
            this.DisplayName = displayName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the base DN.
        /// </summary>
        public string BaseDn { get;  set; }

        /// <summary>
        /// Gets the bind DN.
        /// </summary>
        public string BindDn { get;  set; }

        /// <summary>
        /// Gets the bind password.
        /// </summary>
        public string BindPassword { get;  set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public int Version { get;  set; }

        #endregion

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}