//-----------------------------------------------------------------------
// <copyright file="HierarchicalOrder.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// A three of <see cref="HierarchicalOrder"/> object describes the order of the <see cref="FullMethodDescription"/> objects inside a BDD scenario call chain.
// Every object of <see cref="HierarchicalOrder"/> indicates the relative position of a method.
// </summary>
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
    /// A three of <see cref="HierarchicalOrder"/> object describes the order of the <see cref="FullMethodDescription"/> objects inside a BDD scenario call chain.
    /// Every object of <see cref="HierarchicalOrder"/> indicates the relative position of a method.
    /// </summary>
    /// <seealso cref="System.IComparable{HudDimension.BDDExtensionForUnityTestTools.HierarchicalOrder}" />
    public class HierarchicalOrder : IComparable<HierarchicalOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalOrder"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public HierarchicalOrder(uint order)
        {
            this.Order = order;
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The value that indicates the ordinal position compared to the others in the same level of the three.
        /// </value>
        private uint Order { get; set; }

        /// <summary>
        /// Gets or sets the nested hierarchical order.
        /// </summary>
        /// <value>
        /// The HierarchicalOrder object at the immediate top level.
        /// </value>
        private HierarchicalOrder NestedHierarchicalOrder { get; set; }

        /// <summary>
        /// Adds the <paramref name="lastElement"/> <see cref="HierarchicalOrder"/> as last element of the tree.
        /// </summary>
        /// <param name="lastElement">The last element.</param>
        public void AddAsLastElementHierarchicalOrder(HierarchicalOrder lastElement)
        {
            if (this.NestedHierarchicalOrder != null)
            {
                this.NestedHierarchicalOrder.AddAsLastElementHierarchicalOrder(lastElement);
            }
            else
            {
                this.NestedHierarchicalOrder = lastElement;
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>0 if the objects are equals, -1 if <see cref="this"/> is lower than <paramref name="other"/> and 1 if <see cref="this"/> is greater than <paramref name="other"/> </returns>
        public int CompareTo(HierarchicalOrder other)
        {
            if (this.Order < other.Order)
            {
                return -1;
            }

            if (this.Order > other.Order)
            {
                return +1;
            }

            if (this.NestedHierarchicalOrder == null && other.NestedHierarchicalOrder == null)
            {
                return 0;
            }

            if (this.NestedHierarchicalOrder == null && other.NestedHierarchicalOrder != null)
            {
                return 1;
            }

            if (this.NestedHierarchicalOrder != null && other.NestedHierarchicalOrder == null)
            {
                return -1;
            }

            return this.NestedHierarchicalOrder.CompareTo(other.NestedHierarchicalOrder);
        }
    }
}
