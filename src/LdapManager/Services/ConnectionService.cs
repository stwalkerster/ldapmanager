// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionService.cs" company="Simon Walker">
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
//   The connection service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.DirectoryServices.Protocols;
    using System.Linq;
    using System.Net;

    using LdapManager.Factories;
    using LdapManager.Models;

    using DirectoryAttribute = LdapManager.Models.DirectoryAttribute;

    /// <summary>
    /// The connection service.
    /// </summary>
    public class ConnectionService : Interfaces.IConnectionService
    {
        #region Fields

        /// <summary>
        /// The bookmark.
        /// </summary>
        private readonly ConnectionBookmark bookmark;

        /// <summary>
        /// The directory root.
        /// </summary>
        private DirectoryEntry directoryRoot;

        /// <summary>
        /// The LDAP connection.
        /// </summary>
        private LdapConnection ldapConnection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ConnectionService"/> class.
        /// </summary>
        /// <param name="bookmark">
        /// The bookmark.
        /// </param>
        public ConnectionService(ConnectionBookmark bookmark)
        {
            this.bookmark = bookmark;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the directory root.
        /// </summary>
        public DirectoryEntry DirectoryRoot
        {
            get
            {
                return this.directoryRoot ?? (this.directoryRoot = this.GetRootEntry());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The bind.
        /// </summary>
        public void Bind()
        {
            var ldapDirectoryIdentifier = new LdapDirectoryIdentifier(this.bookmark.Host, this.bookmark.Port);

            switch (this.bookmark.AuthType)
            {
                case AuthType.Basic:
                    var networkCredential = new NetworkCredential(this.bookmark.BindDn, this.bookmark.BindPassword);

                    this.ldapConnection = new LdapConnection(ldapDirectoryIdentifier, networkCredential)
                                              {
                                                  AuthType =
                                                      AuthType
                                                      .Basic
                                              };
                    break;
                case AuthType.Anonymous:
                    this.ldapConnection = new LdapConnection(ldapDirectoryIdentifier) { AuthType = AuthType.Anonymous };
                    break;
                default:
                    throw new NotSupportedException("Specified AuthType is not supported.");
            }

            this.ldapConnection.SessionOptions.ProtocolVersion = this.bookmark.Version;

            this.ldapConnection.Bind();
        }

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
        public IEnumerable<DirectoryAttribute> GetAttributes(string distinguishedName, params string[] attributes)
        {
            var searchRequest = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.Base, attributes);
            var directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse != null)
            {
                var searchResultEntries = directoryResponse.Entries.Cast<SearchResultEntry>().ToList();

                if (searchResultEntries.Count() != 1)
                {
                    throw new Exception("Something went wrong. Retrieved an unexpected number of myself.");
                }

                SearchResultEntry result = searchResultEntries.First();

                var dynamicDictionary = new List<DirectoryAttribute>();

                foreach (object a in result.Attributes)
                {
                    var value = (System.DirectoryServices.Protocols.DirectoryAttribute)((DictionaryEntry)a).Value;

                    dynamicDictionary.AddRange(AttributeFactory.Create(value));
                }

                return dynamicDictionary;
            }

            return new List<DirectoryAttribute>();
        }

        /// <summary>
        /// The fetch children.
        /// </summary>
        /// <param name="distinguishedName">
        /// The distinguished name.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<DirectoryEntry> FetchChildren(string distinguishedName)
        {
            var searchRequest = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.OneLevel);
            var directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse != null)
            {
                var searchResultEntries = directoryResponse.Entries.Cast<SearchResultEntry>().ToList();

                return
                    searchResultEntries.Select(
                        searchResultEntry => DirectoryEntryFactory.Create(searchResultEntry, this));
            }

            return new List<DirectoryEntry>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// The get root entry.
        /// </summary>
        /// <returns>
        /// The <see cref="DirectoryEntry"/>.
        /// </returns>
        private DirectoryEntry GetRootEntry()
        {
            var searchRequest = new SearchRequest(this.bookmark.BaseDn, "(objectClass=*)", SearchScope.Base);
            var directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse != null)
            {
                var searchResultEntries = directoryResponse.Entries.Cast<SearchResultEntry>().ToList();

                if (searchResultEntries.Count() > 2)
                {
                    throw new Exception("More than one root element!?");
                }

                if (!searchResultEntries.Any())
                {
                    throw new Exception("Root not found");
                }

                DirectoryEntry directoryEntry = DirectoryEntryFactory.Create(searchResultEntries.First(), this);
                directoryEntry.IsBase = true;
                return directoryEntry;
            }

            throw new Exception("Root not found");
        }

        public IEnumerable<string> GetBaseDNs()
        {
            var searchRequest = new SearchRequest(string.Empty, "(objectClass=*)", SearchScope.Base, "namingContexts");
            var directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse != null)
            {
                return
                    directoryResponse.Entries[0].Attributes["namingcontexts"].GetValues(typeof(string)).Cast<string>();
            }

            return new List<string>();
        }

        public void GetSchema()
        {
            var searchRequest = new SearchRequest(this.bookmark.BaseDn, "(objectClass=*)", SearchScope.Base, "subschemasubentry");
            var directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse == null)
            {
                return;
            }

            var subschemaBaseDn = directoryResponse.Entries[0].Attributes["subschemasubentry"].GetValues(typeof(string)).Cast<string>().First();
            
            searchRequest = new SearchRequest(subschemaBaseDn, "(objectClass=subschema)", SearchScope.Base, "objectclasses", "attributetypes");
            directoryResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

            if (directoryResponse == null)
            {
                return;
            }

            var objectClasses = directoryResponse.Entries[0].Attributes["objectclasses"].GetValues(typeof(string)).Cast<string>();
            var attributeTypes = directoryResponse.Entries[0].Attributes["attributetypes"].GetValues(typeof(string)).Cast<string>();
        }

        #endregion
    }
}