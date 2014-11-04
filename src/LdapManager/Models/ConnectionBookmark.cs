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
    using System.DirectoryServices.Protocols;
    using System.Net;

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
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="baseDn">
        /// The base DN.
        /// </param>
        /// <param name="networkCredential">
        /// The network Credential.
        /// </param>
        /// <param name="authType">
        /// The authentication type.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public ConnectionBookmark(
            string displayName, 
            string host, 
            int port, 
            string baseDn, 
            NetworkCredential networkCredential, 
            AuthType authType = AuthType.Basic, 
            int version = 3)
        {
            this.DisplayName = displayName;
            this.Host = host;
            this.Port = port;
            this.NetworkCredential = networkCredential;
            this.BaseDn = baseDn;
            this.Version = version;
            this.AuthType = authType;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ConnectionBookmark"/> class.
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="baseDn">
        /// The base DN.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public ConnectionBookmark(string displayName, string host, int port, string baseDn, int version = 3)
        {
            this.Port = port;
            this.BaseDn = baseDn;
            this.Version = version;
            this.Host = host;
            this.DisplayName = displayName;
            this.AuthType = AuthType.Anonymous;
            this.NetworkCredential = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        public AuthType AuthType { get; set; }

        /// <summary>
        /// Gets or sets the base DN.
        /// </summary>
        public string BaseDn { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the network credential.
        /// </summary>
        public NetworkCredential NetworkCredential { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the protocol version.
        /// </summary>
        public int Version { get; set; }

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
            return this.DisplayName;
        }

        #endregion
    }
}