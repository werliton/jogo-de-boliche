//-----------------------------------------------------------------------
// <copyright file="GivenBaseAttribute.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This attribute is the base of a Given Attribute.
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
    /// This attribute is the base of a Given Attribute.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BDDMethodBaseAttribute" />
    [AttributeUsage(System.AttributeTargets.Method)]
    public class GivenBaseAttribute : BDDMethodBaseAttribute
    {
        /// <summary>
        /// The value of the timeout in milliseconds.
        /// </summary>
        private uint timeout = 3000;

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenBaseAttribute"/> class.
        /// </summary>
        /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
        public GivenBaseAttribute(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the value of the Delay in milliseconds.
        /// </summary>
        /// <value>
        /// The value of the Delay in milliseconds.
        /// </value>
        public uint Delay { get; set; }

        /// <summary>
        /// Gets or sets the sentence of the scenario represented by the Step Method.
        /// </summary>
        /// <value>
        /// The sentence of the scenario represented by the Step Method.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value of the timeout in milliseconds.
        /// </summary>
        /// <value>
        /// The value of the timeout in milliseconds.
        /// </value>
        public uint Timeout
        {
            get
            {
                return this.timeout;
            }

            set
            {
                this.timeout = value;
            }
        }

        /// <summary>
        /// Gets the string representing the type of the Step Method.
        /// </summary>
        /// <returns>
        /// The string representing the type of the Step Method.
        /// </returns>
        public override string GetStepName()
        {
            return "Given";
        }

        /// <summary>
        /// Gets the sentence of the scenario represented by the Step Method.
        /// </summary>
        /// <returns>
        /// The sentence of the scenario represented by the Step Method.
        /// </returns>
        public override string GetStepScenarioText()
        {
            return this.Text;
        }

        /// <summary>
        /// Gets the execution order.
        /// </summary>
        /// <returns>
        /// The execution order.
        /// </returns>
        public override uint GetExecutionOrder()
        {
            return 0;
        }

        /// <summary>
        /// Gets the value of the Delay in milliseconds.
        /// </summary>
        /// <returns>
        /// The value of the Delay in milliseconds.
        /// </returns>
        public override uint GetDelay()
        {
            return this.Delay;
        }

        /// <summary>
        /// Gets the value of the timeout in milliseconds.
        /// </summary>
        /// <returns>
        /// The value of the timeout in milliseconds.
        /// </returns>
        public override uint GetTimeout()
        {
            return this.Timeout;
        }
    }
}
