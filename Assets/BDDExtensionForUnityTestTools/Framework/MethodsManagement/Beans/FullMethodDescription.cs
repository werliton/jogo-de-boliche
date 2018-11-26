//-----------------------------------------------------------------------
// <copyright file="FullMethodDescription.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
////
// <summary>
// The full information of a Step Method.
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
    /// The full information of a Step Method.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BaseMethodDescription" />
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.MethodDescription" />
    /// <seealso cref="System.IComparable{HudDimension.BDDExtensionForUnityTestTools.FullMethodDescription}" />
    public class FullMethodDescription : MethodDescription, IComparable<FullMethodDescription>
    {
        /// <summary>
        /// The identifier from the potential <see cref="CallBefore"/> attribute.
        /// </summary>
        private string id = string.Empty;

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>
        /// The delay after which the method has to be executed.
        /// </value>
        public uint Delay { get; set; }

        /// <summary>
        /// Gets or sets the time out.
        /// </summary>
        /// <value>
        /// In case of <see cref="AssertionResultRetry"/> response, it is timeout after which the method has not to be executed again.
        /// </value>
        public uint TimeOut { get; set; }

        /// <summary>
        /// Gets or sets the succession order.
        /// </summary>
        /// <value>
        ///  A integer value >0 that marks the order of the execution of the Methods if called with a <see cref="CallBefore"/> attribute.
        /// </value>
        public uint SuccessionOrder { get; set; }

        /// <summary>
        /// Gets or sets the main method.
        /// </summary>
        /// <value>
        /// The <see cref="FullMethodDescription"/> of the method that has configured the <see cref="CallBefore"/> attribute indicating this method.
        /// </value>
        public FullMethodDescription MainMethod { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier from the potential <see cref="CallBefore"/> attribute.
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
                 (this.Parameters == null ? 0 : this.Parameters.GetHashCode())
                 +
                 this.Delay.GetHashCode()
                 +
                 this.TimeOut.GetHashCode()
                 +
                 this.SuccessionOrder.GetHashCode()
                 +
                 (this.MainMethod == null ? 0 : this.MainMethod.GetHashCode())
                 +
                 (this.Id == null ? 0 : this.Id.GetHashCode());
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

            FullMethodDescription method = (FullMethodDescription)obj;
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
                ((this.Parameters == null && method.Parameters == null) || this.Parameters.Equals(method.Parameters))
                &&
                this.Delay.Equals(method.Delay)
                &&
                this.TimeOut.Equals(method.TimeOut)
                &&
                this.SuccessionOrder.Equals(method.SuccessionOrder)
                &&
                ((this.MainMethod == null && method.MainMethod == null) || this.MainMethod.Equals(method.MainMethod))
                &&
                ((this.Id == null && method.Id == null) || this.Id.Equals(method.Id)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>0 if the objects are equals, -1 if <see cref="this"/> is lower than <paramref name="other"/> and 1 if <see cref="this"/> is greater than <paramref name="other"/> </returns>
        public int CompareTo(FullMethodDescription other)
        {
            HierarchicalOrder mainHierarchicalOrder = this.GetHierarchicalOrder();
            HierarchicalOrder otherHierarchicalOrder = other.GetHierarchicalOrder();
            return mainHierarchicalOrder.CompareTo(otherHierarchicalOrder);
        }

        /// <summary>
        /// Calculates the hierarchical order using the <see cref="this.SuccessionOrder"/> value of its parents.
        /// </summary>
        /// <returns>Returns the root of the tree formed by <see cref="HierarchicalOrder"/> objects.</returns>
        private HierarchicalOrder GetHierarchicalOrder()
        {
            HierarchicalOrder result;
            HierarchicalOrder hierarchicalOrder = new HierarchicalOrder(this.SuccessionOrder);
            if (this.MainMethod != null)
            {
                result = this.MainMethod.GetHierarchicalOrder();
                result.AddAsLastElementHierarchicalOrder(hierarchicalOrder);
            }
            else
            {
                result = hierarchicalOrder;
            }

            return result;
        }
    }
}
