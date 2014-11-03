// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryAttribute.cs" company="Simon Walker">
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
//   The directory attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LdapManager.Models
{
    using System.Linq;

    /// <summary>
    /// The directory attribute.
    /// </summary>
    public class DirectoryAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="DirectoryAttribute"/> class.
        /// </summary>
        /// <param name="fromString">
        /// The from string.
        /// </param>
        public DirectoryAttribute(string fromString)
        {
            var strings = fromString.Split('=');
            this.Name = strings.First();
            this.Value = strings.Last();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DirectoryAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public DirectoryAttribute(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public object Value { get; set; }

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
            return string.Format("{0}={1}", this.Name, this.Value);
        }

        #endregion
    }
}