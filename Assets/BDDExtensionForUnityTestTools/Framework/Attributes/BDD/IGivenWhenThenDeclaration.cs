//-----------------------------------------------------------------------
// <copyright file="IGivenWhenThenDeclaration.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the interface for letting the developer to use the generics.
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
    /// This is the interface for letting the developer to use the generics.
    /// </summary>
    public interface IGivenWhenThenDeclaration
    {
        /// <summary>
        /// Gets the string representing the type of the Step Method.
        /// </summary>
        /// <returns>
        /// The string representing the type of the Step Method.
        /// </returns>
        string GetStepName();

        /// <summary>
        /// Gets the sentence of the scenario represented by the Step Method.
        /// </summary>
        /// <returns>
        /// The sentence of the scenario represented by the Step Method.
        /// </returns>
        string GetStepScenarioText();

        /// <summary>
        /// Gets the execution order.
        /// </summary>
        /// <returns>
        /// The execution order.
        /// </returns>
        uint GetExecutionOrder();

        /// <summary>
        /// Gets the value of the Delay in milliseconds.
        /// </summary>
        /// <returns>
        /// The value of the Delay in milliseconds.
        /// </returns>
        uint GetDelay();

        /// <summary>
        /// Gets the value of the timeout in milliseconds.
        /// </summary>
        /// <returns>
        /// The value of the timeout in milliseconds.
        /// </returns>
        uint GetTimeout();
    }
}
