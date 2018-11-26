//-----------------------------------------------------------------------
// <copyright file="ParameterLocation.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Contains the information about the location of the value for a Step Method parameter.
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

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Contains the information about the location of the value for a Step Method parameter.
    /// </summary>
    public class ParameterLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterLocation"/> class.
        /// </summary>
        public ParameterLocation()
        {
            this.ParameterClassLocation = new ClassLocation();
            this.ParameterArrayLocation = new ArrayLocation();
        }

        /// <summary>
        /// Gets or sets the <see cref="ClassLocation"/> object that describes the component that stores the parameter value.
        /// </summary>
        /// <value>
        /// The <see cref="ClassLocation"/> object.
        /// </value>
        public ClassLocation ParameterClassLocation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ArrayLocation"/> object that describes the position of the parameter value inside an array.
        /// </summary>
        /// <value>
        /// The <see cref="ArrayLocation"/> object.
        /// </value>
        public ArrayLocation ParameterArrayLocation { get; set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int result =
                 (this.ParameterClassLocation == null ? 0 : this.ParameterClassLocation.GetHashCode())
                 +
                 (this.ParameterArrayLocation == null ? 0 : this.ParameterArrayLocation.GetHashCode());

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

            ParameterLocation parameterLocation = (ParameterLocation)obj;
            if (((this.ParameterClassLocation == null && parameterLocation.ParameterClassLocation == null) || this.ParameterClassLocation.Equals(parameterLocation.ParameterClassLocation))
                &&
                ((this.ParameterArrayLocation == null && parameterLocation.ParameterArrayLocation == null) || this.ParameterArrayLocation.Equals(parameterLocation.ParameterArrayLocation)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Describes the component that stores the parameter value.
        /// </summary>
        public class ClassLocation
        {
            /// <summary>
            /// Gets or sets the <see cref="Type"/> object of the component.
            /// </summary>
            /// <value>
            /// The <see cref="Type"/> object.
            /// </value>
            public Type ComponentType { get; set; }

            /// <summary>
            /// Gets or sets the component.
            /// </summary>
            /// <value>
            /// The component.
            /// </value>
            public object ComponentObject { get; set; }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public override int GetHashCode()
            {
                int result =
                     (this.ComponentType == null ? 0 : this.ComponentType.GetHashCode())
                     +
                     (this.ComponentObject == null ? 0 : this.ComponentObject.GetHashCode());

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

                ClassLocation classLocation = (ClassLocation)obj;
                if (((this.ComponentType == null && classLocation.ComponentType == null) || object.Equals(this.ComponentType, classLocation.ComponentType))
                    &&
                    ((this.ComponentObject == null && classLocation.ComponentObject == null) || this.ComponentObject.Equals(classLocation.ComponentObject)))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Describes the position of the parameter value inside an array.
        /// </summary>
        public class ArrayLocation
        {
            /// <summary>
            /// Gets or sets the <see cref="FieldInfo"/> object of the array.
            /// </summary>
            /// <value>
            /// The <see cref="FieldInfo"/> object.
            /// </value>
            public FieldInfo ArrayFieldInfo { get; set; }

            /// <summary>
            /// Gets or sets the name of the array.
            /// </summary>
            /// <value>
            /// The name of the array.
            /// </value>
            public string ArrayName { get; set; }

            /// <summary>
            /// Gets or sets the index of the value inside the array.
            /// </summary>
            /// <value>
            /// The index of the array.
            /// </value>
            public int ArrayIndex { get; set; }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public override int GetHashCode()
            {
                int result =
                     this.ArrayFieldInfo == null ? 0 : this.ArrayFieldInfo.GetHashCode()
                     +
                     this.ArrayName == null ? 0 : this.ArrayName.GetHashCode()
                     +
                     this.ArrayIndex.GetHashCode();

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

                ArrayLocation arrayLocation = (ArrayLocation)obj;
                if (((this.ArrayFieldInfo == null && arrayLocation.ArrayFieldInfo == null) || this.ArrayFieldInfo.Equals(arrayLocation.ArrayFieldInfo))
                    &&
                    ((this.ArrayName == null && arrayLocation.ArrayName == null) || this.ArrayName.Equals(arrayLocation.ArrayName))
                    &&
                    this.ArrayIndex.Equals(arrayLocation.ArrayIndex))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
