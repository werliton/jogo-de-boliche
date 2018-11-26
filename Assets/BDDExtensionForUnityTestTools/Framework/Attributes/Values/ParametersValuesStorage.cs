//-----------------------------------------------------------------------
// <copyright file="ParametersValuesStorage.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This attribute can be used for making a field a Parameters Value Storage.
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
    /// This attribute can be used for making a field a Parameters Value Storage.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ParametersValuesStorage : Attribute
    {
    }
}
