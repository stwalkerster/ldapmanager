// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeGridConverter.cs" company="Simon Walker">
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
//   The attribute grid converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Castle.Core.Logging;

    using LdapManager.Factories;
    using LdapManager.Models;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// The attribute grid converter.
    /// </summary>
    public class AttributeGridConverter : IValueConverter
    {
        #region Fields

        /// <summary>
        /// The attribute converter.
        /// </summary>
        private readonly AttributeTypeConverter attributeTypeConverter;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="AttributeGridConverter"/> class.
        /// </summary>
        public AttributeGridConverter()
        {
            this.attributeTypeConverter = ServiceLocator.Current.GetInstance<AttributeTypeConverter>();
            this.logger = ServiceLocator.Current.GetInstance<ILogger>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // var attValuePairs = new List<KeyValuePair<string, object>>();

            if (value == null)
            {
                return null;
            }

            var entry = value as DirectoryEntry;
            if (entry == null)
            {
                return null;
            }

            // var attributeCollection = entry.Attributes;
            // if (attributeCollection == null)
            // {
            // return null;
            // }

            // foreach (var attr in attributeCollection)
            // {
            // this.logger.DebugFormat("Found attribute: {0}", attr.Key);

            // foreach (object val in attr.Value.Values)
            // {
            // object dataValue = val;

            // var bytes = val as byte[];
            // if (bytes != null)
            // {
            // dataValue = this.attributeTypeConverter.Convert(bytes);
            // }

            // attValuePairs.Add(new KeyValuePair<string, object>(attr.Key, dataValue));
            // }
            // }

            // this.logger.DebugFormat("Returning {0} pairs", attValuePairs.Count);

            // return attValuePairs;
            return entry.Attributes;
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}