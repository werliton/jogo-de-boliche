//-----------------------------------------------------------------------
// <copyright file="MethodParameter.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Contains the value of the parameter of a Step Method and the description of its location.
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
using System.Reflection;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Contains the value of the parameter of a Step Method and the description of its location.
    /// </summary>
    public class MethodParameter
    {
        /// <summary>
        /// Gets or sets the <see cref="ParameterInfo"/> object of the parameter.
        /// </summary>
        /// <value>
        /// The <see cref="ParameterInfo"/> object of the parameter.
        /// </value>
        public ParameterInfo ParameterInfoObject { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>
        /// The value of the parameter.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the parameter location.
        /// </summary>
        /// <value>
        /// The parameter location.
        /// </value>
        public ParameterLocation ParameterLocation { get; set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int result =
                 (this.ParameterInfoObject == null ? 0 : this.ParameterInfoObject.GetHashCode())
                 +
                 (this.Value == null ? 0 : this.Value.GetHashCode())
                 +
                 (this.ParameterLocation == null ? 0 : this.ParameterLocation.GetHashCode());

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

            MethodParameter methodParameter = (MethodParameter)obj;
            if (((this.ParameterInfoObject == null && methodParameter.ParameterInfoObject == null) || object.Equals(this.ParameterInfoObject, methodParameter.ParameterInfoObject))
                &&
                ((this.Value == null && methodParameter.Value == null) || this.Value.Equals(methodParameter.Value))
                &&
                ((this.ParameterLocation == null && methodParameter.ParameterLocation == null) || this.ParameterLocation.Equals(methodParameter.ParameterLocation)))
            {
                return true;
            }

            return false;
        }
    }
}