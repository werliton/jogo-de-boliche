//-----------------------------------------------------------------------
// <copyright file="BDDMethodBaseAttribute.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is a base class for the four types of BDD Methods: <see cref="GenericBDDMethod"/>, <see cref="GivenBaseAttribute"/>, <see cref="WhenBaseAttribute"/>, <see cref="ThenBaseAttribute"/>.
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
    /// This is a base class for the four types of BDD Methods: <see cref="GenericBDDMethod"/>, <see cref="GivenBaseAttribute"/>, <see cref="WhenBaseAttribute"/>, <see cref="ThenBaseAttribute"/>.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IGivenWhenThenDeclaration" />
    [AttributeUsage(System.AttributeTargets.Method)]
    public abstract class BDDMethodBaseAttribute : Attribute, IGivenWhenThenDeclaration
    {
        /// <summary>
        /// Gets the value of the Delay in milliseconds.
        /// </summary>
        /// <returns>The value of the Delay in milliseconds.</returns>
        public abstract uint GetDelay();

        /// <summary>
        /// Gets the execution order.
        /// </summary>
        /// <returns>The execution order.</returns>
        public abstract uint GetExecutionOrder();

        /// <summary>
        /// Gets the string representing the type of the Step Method.
        /// </summary>
        /// <returns>The string representing the type of the Step Method.</returns>
        public abstract string GetStepName();

        /// <summary>
        /// Gets the sentence of the scenario represented by the Step Method.
        /// </summary>
        /// <returns>The sentence of the scenario represented by the Step Method.</returns>
        public abstract string GetStepScenarioText();

        /// <summary>
        /// Gets the value of the timeout in milliseconds.
        /// </summary>
        /// <returns>The value of the timeout in milliseconds.</returns>
        public abstract uint GetTimeout();
    }
}
