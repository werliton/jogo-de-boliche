//-----------------------------------------------------------------------
// <copyright file="GenericBDDMethod.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This attribute can be used on a method in a BDD Component to make it a BDD Method.
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
    /// This attribute can be used on a method in a BDD Component to make it a BDD Method.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BDDMethodBaseAttribute" />
    [AttributeUsage(System.AttributeTargets.Method)]
    public class GenericBDDMethod : BDDMethodBaseAttribute
    {
        /// <summary>
        /// Gets the string representing the type of the Step Method.
        /// </summary>
        /// <returns>The string representing the type of the Step Method.</returns>
        public override string GetStepName()
        {
            return "Generic";
        }

        /// <summary>
        /// Gets the sentence of the scenario represented by the Step Method.
        /// </summary>
        /// <returns>The sentence of the scenario represented by the Step Method.</returns>
        public override string GetStepScenarioText()
        {
            return null;
        }

        /// <summary>
        /// Gets the execution order.
        /// </summary>
        /// <returns>The execution order.</returns>
        public override uint GetExecutionOrder()
        {
            return 0;
        }

        /// <summary>
        /// Gets the value of the Delay in milliseconds.
        /// </summary>
        /// <returns>The value of the Delay in milliseconds.</returns>
        public override uint GetDelay()
        {
            return 0;
        }

        /// <summary>
        /// Gets the value of the timeout in milliseconds.
        /// </summary>
        /// <returns>The value of the timeout in milliseconds.</returns>
        public override uint GetTimeout()
        {
            return 3000;
        }
    }
}
