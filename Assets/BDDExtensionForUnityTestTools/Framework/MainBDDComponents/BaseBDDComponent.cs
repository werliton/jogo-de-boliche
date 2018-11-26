//-----------------------------------------------------------------------
// <copyright file="BaseBDDComponent.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the base class for <see cref="StaticBDDComponent"/> and <see cref="DynamicBDDComponent"/> components.
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[module: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the base class for <see cref="StaticBDDComponent"/> and <see cref="DynamicBDDComponent"/> components.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class BaseBDDComponent : MonoBehaviour
    {
        /// <summary>
        /// The list of the formal errors found in the BDD Component.
        /// </summary>
        [HideInInspector]
        public List<UnityTestBDDError> Errors = new List<UnityTestBDDError>();

        /// <summary>
        /// This boolean field marks the component when the error checking operations are executing.
        /// </summary>
        [HideInInspector]
        public bool Checking = true;
    }
}
