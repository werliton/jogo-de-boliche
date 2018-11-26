//-----------------------------------------------------------------------
// <copyright file="BaseMethodDescription.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// The base information of a Step Method.
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
    /// The base information of a Step Method.
    /// </summary>
    public class BaseMethodDescription
    {
        /// <summary>
        /// Gets or sets the component object.
        /// </summary>
        /// <value>
        /// The <see cref="Component"/> containing the Step Method.
        /// </value>
        public Component ComponentObject { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The <see cref="MethodInfo"/> for the Step Method.
        /// </value>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text <see cref="string"/> of the Step Method. It has to be in the form Given-When-Then, omitting the beginning words "Given", "When", "Then", "and".
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the step.
        /// </summary>
        /// <value>
        /// The Step Method Type. It can be <see cref="GivenBaseAttribute"/>, <see cref="WhenBaseAttribute"/> or <see cref="ThenBaseAttribute"/>
        /// </value>
        public Type StepType { get; set; }

        /// <summary>
        /// Gets or sets the execution order.
        /// </summary>
        /// <value>
        /// An integer value >0 that marks the order of the execution of the Step Methods for a Static scenario.
        /// </value>
        public uint ExecutionOrder { get; set; }

        /// <summary>
        /// Gets the full name of the step method in the form "ComponentName.MethodName".
        /// </summary>
        /// <param name="type">The <see cref="Type"/> object of the BDD Component containing the method.</param>
        /// <param name="method">The <see cref="MethodInfo"/> of the Method.</param>
        /// <returns><see cref="string"/> "ComponentName.MethodName".</returns>
        public static string GetFullName(Type type, MethodInfo method)
        {
            return type.Name + "." + GetMethodNameFromSignature(method);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return GetFullName();
        }

        /// <summary>
        /// Gets the full name of the Step Method in the form "ComponentName.MethodName".
        /// </summary>
        /// <returns><see cref="string"/> "ComponentName.MethodName".</returns>
        public virtual string GetFullName()
        {
            return GetFullName(this.ComponentObject.GetType(), this.Method);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int result =
                 (this.ComponentObject == null ? 0 : this.ComponentObject.GetHashCode())
                 +
                 (this.Method == null ? 0 : this.Method.GetHashCode())
                 +
                 (this.StepType == null ? 0 : this.StepType.GetHashCode())
                 +
                 (this.Text == null ? 0 : this.Text.GetHashCode())
                 +
                 this.ExecutionOrder.GetHashCode();

            return result;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BaseMethodDescription method = (BaseMethodDescription)obj;
            if (((this.ComponentObject == null && method.ComponentObject == null) || UnityEngine.Object.Equals(this.ComponentObject, method.ComponentObject))
                &&
                ((this.Method == null && method.Method == null) || this.Method.Equals(method.Method))
                &&
                ((this.StepType == null && method.StepType == null) || this.StepType.Equals(method.StepType))
                &&
                ((this.Text == null && method.Text == null) || this.Text.Equals(method.Text))
                 &&
                this.ExecutionOrder.Equals(method.ExecutionOrder))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the name of the method from its MethodInfo object.
        /// </summary>
        /// <param name="method">The method of which the name is wanted.</param>
        /// <returns><see cref="string"/>MethodName.</returns>
        private static string GetMethodNameFromSignature(MethodInfo method)
        {
            string result = string.Empty;
            string firstStep = method.ToString().Substring(method.ToString().IndexOf(' ') + 1);
            result = firstStep.Substring(0, firstStep.IndexOf('('));
            return result;
        }
    }
}
