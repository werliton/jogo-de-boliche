//-----------------------------------------------------------------------
// <copyright file="CallBefore.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This attribute can be used on a BDD Method for indicating that another method has to be executed before the BDD Method.
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
    /// This attribute can be used on a BDD Method for indicating that another method has to be executed before the BDD Method.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    public class CallBefore : Attribute
    {
        /// <summary>
        /// A string that can be used to identify the parameters of the called method uniquely.
        /// </summary>
        private string id = string.Empty;

        /// <summary>
        /// The value of the timeout in milliseconds.
        /// </summary>
        private uint timeout = 3000;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallBefore"/> class.
        /// </summary>
        /// <param name="executionOrder">The execution order.</param>
        /// <param name="method">The method.</param>
        public CallBefore(uint executionOrder, string method)
        {
            this.ExecutionOrder = executionOrder;
            this.Method = method;
        }

        /// <summary>
        /// Gets or sets the execution order.
        /// </summary>
        /// <value>
        /// The execution order.
        /// </value>
        public uint ExecutionOrder { get; set; }

        /// <summary>
        /// Gets or sets the value of the delay in milliseconds.
        /// </summary>
        /// <value>
        /// The delay.
        /// </value>
        public uint Delay { get; set; }

        /// <summary>
        /// Gets or sets the string containing the name of the method to execute.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the string that can be used to identify the parameters of the called method uniquely.
        /// </summary>
        /// <value>
        /// The string that can be used to identify the parameters of the called method uniquely.
        /// </value>
        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = string.Empty;
            result += "[CallBefore( " + this.ExecutionOrder + " \"" + this.Method + "\"";
            if (this.Delay != 0)
            {
                result += " Delay =" + this.Delay;
            }

            if (this.Timeout != 0)
            {
                result += " Timeout =" + this.Timeout;
            }

            if (!this.Id.Equals(string.Empty))
            {
                result += " Id =\"" + this.Id + "\"";
            }

            result += ")]";
            return result;
        }
    }
}
