//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicData.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains all the data that are used for the Editor of the BDDExtensionRunner component.
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains all the data that are used for the Editor of the BDDExtensionRunner component.
    /// </summary>
    public class RunnerEditorBusinessLogicData
    {
        /// <summary>
        /// The label width in absolute value.
        /// </summary>
        public static readonly float LabelWidthAbsolute = 52;

        /// <summary>
        /// The buttons width in absolute value.
        /// </summary>
        public static readonly float ButtonsWidthAbsolute = 60;

        /// <summary>
        /// The text width in a percent value compared to the whole row width.
        /// </summary>
        public static readonly float TextWidthPercent = 0.40f;

        /// <summary>
        /// This field stores if Unity is building the scripts. It is used to detect when performing a rebuild of the parameters indexes.
        /// </summary>
        public bool IsCompiling;

        /// <summary>
        /// The collection of the BDD objects attached to the Integration Test.
        /// </summary>
        public object[] BDDObjects;

        /// <summary>
        /// The collection of the serialized objects derived from the BDDObjects.
        /// </summary>
        public Dictionary<Type, ISerializedObjectWrapper> SerializedObjects;

        /// <summary>
        /// This field is true if a rebuild of the parameters indexes is required.
        /// </summary>
        internal bool Rebuild;

        /// <summary>
        /// The foldouts value for the first 50 rows of the Given Step Methods.
        /// </summary>
        internal bool[] GivenFoldouts = new bool[50];

        /// <summary>
        /// The foldouts value for the first 50 rows of the When Step Methods.
        /// </summary>
        internal bool[] WhenFoldouts = new bool[50];

        /// <summary>
        /// The foldouts value for the first 50 rows of the Then Step Methods.
        /// </summary>
        internal bool[] ThenFoldouts = new bool[50];

        /// <summary>
        /// The foldouts value for the Options row.
        /// </summary>
        internal bool OptionsFoldout = false;
    }
}