//-----------------------------------------------------------------------
// <copyright file="MethodDescription.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// The information of a Step Method.
// </summary>
// <seealso cref="HudDimension.UnityTestBDD.BaseMethodDescription" />
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
namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// The information of a Step Method.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BaseMethodDescription" />
    public class MethodDescription : BaseMethodDescription
    {
        /// <summary>
        /// Gets or sets the index of the parameters.
        /// </summary>
        /// <value>
        /// The well formatted string representing the list of the the parameters and the location of their values.
        /// </value>
        public string ParametersIndex { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters of the method.
        /// </value>
        public MethodParameters Parameters { get; set; }

        /// <summary>
        /// Gets the test of the step method replacing the codes %parameterName% with the values of the parameter.
        /// </summary>
        /// <returns>The string of the step method.</returns>
        public string GetDecodifiedText()
        {
            string result = this.Text;
            foreach (MethodParameter parameter in this.Parameters.Parameters)
            {
                result = result.Replace("%" + parameter.ParameterInfoObject.Name + "%", parameter.Value == null ? string.Empty : parameter.Value.ToString());
            }

            return result;
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
                 this.ExecutionOrder.GetHashCode()
                 +
                 (this.ParametersIndex == null ? 0 : this.ParametersIndex.GetHashCode())
                 +
                 (this.Parameters == null ? 0 : this.Parameters.GetHashCode());
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

            MethodDescription method = (MethodDescription)obj;
            if (((this.ComponentObject == null && method.ComponentObject == null) || object.Equals(this.ComponentObject, method.ComponentObject))
                &&
                ((this.Method == null && method.Method == null) || this.Method.Equals(method.Method))
                &&
                ((this.StepType == null && method.StepType == null) || this.StepType.Equals(method.StepType))
                &&
                ((this.Text == null && method.Text == null) || this.Text.Equals(method.Text))
                 &&
                this.ExecutionOrder.Equals(method.ExecutionOrder)
                &&
                ((this.ParametersIndex == null && method.ParametersIndex == null) || this.ParametersIndex.Equals(method.ParametersIndex))
                &&
                ((this.Parameters == null && method.Parameters == null) || this.Parameters.Equals(method.Parameters)))
            {
                return true;
            }

            return false;
        }
    }
}