//-----------------------------------------------------------------------
// <copyright file="StepMethodUtilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class gives a utility for getting the string of the Step Method type.
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
using System;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class gives a utility for getting the string of the Step Method type.
    /// </summary>
    public static class StepMethodUtilities
    {
        /// <summary>
        /// Gets the the string of the Step Method type.
        /// </summary>
        /// <typeparam name="T">An implementation of the interface IGivenWhenThenDeclaration.</typeparam>
        /// <returns>The string of the Step Method type.</returns>
        public static string GetStepMethodName<T>() where T : IGivenWhenThenDeclaration
        {
            IGivenWhenThenDeclaration declaration = Activator.CreateInstance(typeof(T), string.Empty) as IGivenWhenThenDeclaration;
            string result = declaration.GetStepName();
            return result;
        }
    }
}
