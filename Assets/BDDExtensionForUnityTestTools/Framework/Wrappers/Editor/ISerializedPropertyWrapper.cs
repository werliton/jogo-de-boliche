//-----------------------------------------------------------------------
// <copyright file="ISerializedPropertyWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the interface for the SerializedProperty wrapper.
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
    /// This is the interface for the SerializedProperty wrapper.
    /// </summary>
    public interface ISerializedPropertyWrapper
    {
        /// <summary>
        /// Gets the SerializedProperty object.
        /// </summary>
        /// <returns>The SerializedProperty object.</returns>
        SerializedProperty GetProperty();
    }
}
