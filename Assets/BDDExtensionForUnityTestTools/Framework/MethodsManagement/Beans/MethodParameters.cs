//-----------------------------------------------------------------------
// <copyright file="MethodParameters.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Contains the collection of the parameters of a Step Method.
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
namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Contains the collection of the parameters of a Step Method.
    /// </summary>
    public class MethodParameters
    {
        /// <summary>
        /// Gets or sets the array of <see cref="MethodParameter"/> objects. Each element represents a parameter of a Step Method.
        /// </summary>
        /// <value>
        /// The array of <see cref="MethodParameter"/> objects.
        /// </value>
        public MethodParameter[] Parameters { get; set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            if (this.Parameters == null)
            {
                return 0;
            }

            int result = 0;
            for (int index = 0; index < this.Parameters.Length; index++)
            {
                result += this.Parameters[index] == null ? 0 : this.Parameters[index].GetHashCode();
            }

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

            MethodParameters methodParameters = (MethodParameters)obj;
            if (this.Parameters == null && methodParameters.Parameters == null)
            {
                return true;
            }

            if ((this.Parameters != null && methodParameters.Parameters == null)
                ||
              (this.Parameters == null && methodParameters.Parameters != null))
            {
                return false;
            }

            if (this.Parameters.Length != methodParameters.Parameters.Length)
            {
                return false;
            }

            foreach (MethodParameter methodParmeter in this.Parameters)
            {
                bool cycleResult = false;
                foreach (MethodParameter innerMethodParameter in methodParameters.Parameters)
                {
                    if (methodParmeter.Equals(innerMethodParameter))
                    {
                        cycleResult = true;
                    }
                }

                if (cycleResult == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
