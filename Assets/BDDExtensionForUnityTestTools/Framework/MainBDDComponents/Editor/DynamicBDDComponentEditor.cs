//-----------------------------------------------------------------------
// <copyright file="DynamicBDDComponentEditor.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class is the editor for the Dynamic Component custom inspector.
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
    /// This class is the editor for the Dynamic Component custom inspector.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BaseBDDComponentEditor" />
    [CustomEditor(typeof(DynamicBDDComponent), true)]
    public class DynamicBDDComponentEditor : BaseBDDComponentEditor
    {
        /// <summary>
        /// The path of the BDD Component image.
        /// </summary>
        private string customMainTexturePath = @"HudDimensionDynamicComponentSprite.png";

        /// <summary>
        /// Called when [enable].
        /// </summary>
        private void OnEnable()
        {
            this.MainTexturePath = this.customMainTexturePath;
        }
    }
}
