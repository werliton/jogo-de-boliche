//-----------------------------------------------------------------------
// <copyright file="ISerializedObjectWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the interface for the SerializedObject wrapper.
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
    /// This is the interface for the SerializedObject wrapper.
    /// </summary>
    public interface ISerializedObjectWrapper
    {
        /// <summary>
        /// Finds the SerializedProperty.
        /// </summary>
        /// <param name="parameterLocationString">The parameter location string.</param>
        /// <returns>The SerializedProperty.</returns>
        ISerializedPropertyWrapper FindProperty(string parameterLocationString);

        /// <summary>
        /// Updates the data from the SerializedObject.
        /// </summary>
        void Update();

        /// <summary>
        /// Applies the modified properties to the SerializedObject.
        /// </summary>
        void ApplyModifiedProperties();

        /// <summary>
        /// Gets the serializedObject.
        /// </summary>
        /// <returns>The serializedObject.</returns>
        SerializedObject GetSerializedObject();
    }
}