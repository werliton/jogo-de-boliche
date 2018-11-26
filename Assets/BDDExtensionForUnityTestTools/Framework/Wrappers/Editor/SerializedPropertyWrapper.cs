//-----------------------------------------------------------------------
// <copyright file="SerializedPropertyWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the wrapper for the SerializedProperty.
// </summary>
// 
// <disclaimer>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </disclaimer>
//
// <author>Alessio Langiu</author>
// <email>alessio.langiu@huddimension.co.uk</email>
//-----------------------------------------------------------------------
using UnityEditor;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the wrapper for the SerializedProperty.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.ISerializedPropertyWrapper" />
    public class SerializedPropertyWrapper : ISerializedPropertyWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedPropertyWrapper"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public SerializedPropertyWrapper(SerializedProperty property)
        {
            this.Property = property;
        }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        private SerializedProperty Property { get; set; }

        /// <summary>
        /// Gets the SerializedProperty object.
        /// </summary>
        /// <returns>
        /// The SerializedProperty object.
        /// </returns>
        public SerializedProperty GetProperty()
        {
            return this.Property;
        }
    }
}
