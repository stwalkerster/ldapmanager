// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookmarkService.cs" company="Simon Walker">
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
//   The bookmark service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LdapManager.Services
{
    using System.Collections.Generic;

    using LdapManager.Models;
    using LdapManager.Services.Interfaces;

    /// <summary>
    /// The bookmark service.
    /// </summary>
    public class BookmarkService : IBookmarkService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get bookmarks.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ConnectionBookmark}"/>.
        /// </returns>
        public IEnumerable<ConnectionBookmark> GetBookmarks()
        {
            return new List<ConnectionBookmark>
                       {
                           new ConnectionBookmark(
                               "directory.srv.stwalkerster.net",
                               389,
                               "uid=testuser,ou=People,dc=helpmebot,dc=org,dc=uk",
                               "testuser",
                               "dc=helpmebot,dc=org,dc=uk",
                               3,
                               "stw@dir"),
                               new ConnectionBookmark(
                               "directory.srv.stwalkerster.net",
                               389,
                               "uid=testuser,ou=People,dc=helpmebot,dc=org,dc=uk",
                               "testuser",
                               "dc=helpmebot,dc=org,dc=uk",
                               3,
                               "testuser@dir"),
                               new ConnectionBookmark(
                               "directory.srv.stwalkerster.net",
                               389,
                               "uid=testuser,ou=People,dc=helpmebot,dc=org,dc=uk",
                               "testuser",
                               "dc=helpmebot,dc=org,dc=uk",
                               3,
                               "random other account with a longer name"),
                       };
        }

        #endregion
    }
}