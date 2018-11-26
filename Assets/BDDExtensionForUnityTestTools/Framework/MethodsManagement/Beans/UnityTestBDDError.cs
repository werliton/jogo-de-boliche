//-----------------------------------------------------------------------
// <copyright file="UnityTestBDDError.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Contains the information for managing the errors detected on the BDD Components.
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
using System.Reflection;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Contains the information for managing the errors detected on the BDD Components.
    /// </summary>
    public class UnityTestBDDError
    {
        /// <summary>
        /// Gets or sets the message containing the description of the error.
        /// </summary>
        /// <value>
        /// The message containing the description of the error.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the component that has generated the error.
        /// </summary>
        /// <value>
        /// The component that has generated the error.
        /// </value>
        public Component Component { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MethodInfo"/> object of the method that has generated the error.
        /// </summary>
        /// <value>
        /// The <see cref="MethodInfo"/> object of the method that has generated the error.
        /// </value>
        public MethodInfo MethodMethodInfo { get; set; }

        /// <summary>
        /// Gets or sets the Step Type that has generated the error.
        /// </summary>
        /// <value>
        /// The Step Type that has generated the error.
        /// </value>
        public Type StepType { get; set; }

        /// <summary>
        /// Gets or sets the index inside the chosenMethods list where the error is generated.
        /// </summary>
        /// <value>
        /// The index inside the chosenMethods list where the error is generated.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the red exclamation mark symbol has to be shown.
        /// </summary>
        /// <value>
        /// <c>true</c> if the red exclamation mark symbol has to be shown, otherwise <c>false</c>.
        /// </value>
        public bool ShowRedExclamationMark { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button for opening the component of the method in the IDE has to be shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the button for opening the component of the method in the IDE has to be shown, otherwise, <c>false</c>.
        /// </value>
        public bool ShowButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the BDD Runner Inspector has to be locked.
        /// </summary>
        /// <value>
        /// <c>true</c> if the BDD Runner Inspector has to be locked, otherwise <c>false</c>.
        /// </value>
        public bool LockRunnerInspectorOnErrors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the building parameters process has to be locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the building parameters process has to be locked, otherwise, <c>false</c>.
        /// </value>
        public bool LockBuildParameters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameters rows inside the BDD Runner Inspector have to be locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the parameters rows inside the BDD Runner Inspector have to be locked, otherwise, <c>false</c>.
        /// </value>
        public bool LockParametersRows { get; set; }
    }
}
