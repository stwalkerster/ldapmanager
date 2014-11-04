// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerViewModel.cs" company="Simon Walker">
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
//   The manager view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using LdapManager.Models;
    using LdapManager.Services;
    using LdapManager.Services.Interfaces;
    using LdapManager.TypedFactories;
    using LdapManager.ViewModels.Interfaces;

    /// <summary>
    /// The manager view model.
    /// </summary>
    public class ManagerViewModel : Screen, IManagerViewModel, IDisposable
    {
        #region Fields

        /// <summary>
        /// The bookmark.
        /// </summary>
        private readonly ConnectionBookmark bookmark;

        /// <summary>
        /// The connection service factory.
        /// </summary>
        private readonly IConnectionServiceFactory connectionServiceFactory;

        /// <summary>
        /// The connection service.
        /// </summary>
        private IConnectionService connectionService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ManagerViewModel"/> class.
        /// </summary>
        /// <param name="bookmark">
        /// The bookmark.
        /// </param>
        /// <param name="connectionServiceFactory">
        /// The connection Service Factory.
        /// </param>
        public ManagerViewModel(ConnectionBookmark bookmark, IConnectionServiceFactory connectionServiceFactory)
        {
            this.bookmark = bookmark;
            this.connectionServiceFactory = connectionServiceFactory;
            this.connectionService = this.connectionServiceFactory.Create(bookmark);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the directory root.
        /// </summary>
        public DirectoryEntry DirectoryRoot { get; set; }

        /// <summary>
        /// Gets the directory tree.
        /// </summary>
        public IEnumerable<DirectoryEntry> DirectoryTree
        {
            get
            {
                return new List<DirectoryEntry> { this.DirectoryRoot };
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.bookmark.DisplayName;
            }

            set
            {
                this.bookmark.DisplayName = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.connectionServiceFactory.Release(this.connectionService);
            this.connectionService = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on deactivate.
        /// </summary>
        /// <param name="close">
        /// The close.
        /// </param>
        protected override void OnDeactivate(bool close)
        {
            if (close)
            {
                // Closing logic here
            }
        }

        /// <summary>
        /// The on initialize.
        /// </summary>
        protected override void OnInitialize()
        {
            this.connectionService.Bind();
            this.DirectoryRoot = this.connectionService.DirectoryRoot;

            ((ConnectionService)this.connectionService).GetSchema();
        }

        #endregion
    }
}