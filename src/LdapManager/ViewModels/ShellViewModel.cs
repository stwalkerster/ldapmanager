// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="Simon Walker">
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
//   The shell view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    using Castle.Core.Internal;
    using Castle.Core.Logging;

    using LdapManager.Models;
    using LdapManager.Services.Interfaces;
    using LdapManager.TypedFactories;
    using LdapManager.ViewModels.Interfaces;

    /// <summary>
    /// The shell view model.
    /// </summary>
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        #region Fields

        /// <summary>
        /// The bookmark service.
        /// </summary>
        private readonly IBookmarkService bookmarkService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The connection service factory.
        /// </summary>
        private readonly IConnectionServiceFactory connectionServiceFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="bookmarkService">
        /// The bookmark Service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="connectionServiceFactory">
        /// The connection Service Factory.
        /// </param>
        public ShellViewModel(IBookmarkService bookmarkService, ILogger logger, IConnectionServiceFactory connectionServiceFactory)
        {
            this.bookmarkService = bookmarkService;
            this.logger = logger;
            this.connectionServiceFactory = connectionServiceFactory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the bookmarks.
        /// </summary>
        public IEnumerable<ConnectionBookmark> Bookmarks
        {
            get
            {
                return this.bookmarkService.GetBookmarks();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The close connection.
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        public void CloseConnection(ManagerViewModel viewModel)
        {
            this.logger.Debug("Sending close request to VM " + viewModel);
            viewModel.TryClose();
        }

        /// <summary>
        /// The open connection.
        /// </summary>
        public void OpenConnection()
        {
            // this.logger.Debug("");
            this.Bookmarks.ForEach(
                x =>
                {
                    var managerViewModel = new ManagerViewModel(x, this.connectionServiceFactory);

                    try
                    {
                        this.ActivateItem(managerViewModel);
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat(ex, "Error opening managerviewmodel for connection {0}", x.DisplayName);
                        managerViewModel.TryClose();
                        MessageBox.Show(ex.Message, x.DisplayName);
                    }
                });
        }

        #endregion
    }
}