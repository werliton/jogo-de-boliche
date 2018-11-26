//-----------------------------------------------------------------------
// <copyright file="ChosenMethods.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class describes the Step Methods for a single BDD Step type.
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
    /// This class describes the Step Methods for a single BDD Step type.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public class ChosenMethods : ICloneable
    {
        /// <summary>
        /// The chosenMethods array, containing the full names of the Step Methods.
        /// </summary>
        private string[] chosenMethodsNames;

        /// <summary>
        /// The chosenMethods parametersIndexes array, containing the parametersIndexes of the Step Methods.
        /// </summary>
        private string[] chosenMethodsParametersIndex;

        /// <summary>
        /// Gets or sets the chosenMethods array, containing the full names of the Step Methods.
        /// </summary>
        /// <value>
        /// The chosenMethods array, containing the full names of the Step Methods.
        /// </value>
        public string[] ChosenMethodsNames
        {
            get
            {
                return this.chosenMethodsNames;
            }

            set
            {
                this.chosenMethodsNames = value;
            }
        }

        /// <summary>
        /// Gets or sets the chosenMethods parametersIndexes array, containing the parametersIndexes of the Step Methods.
        /// </summary>
        /// <value>
        /// The chosenMethods parametersIndexes array, containing the parametersIndexes of the Step Methods.
        /// </value>
        public string[] ChosenMethodsParametersIndex
        {
            get
            {
                return this.chosenMethodsParametersIndex;
            }

            set
            {
                this.chosenMethodsParametersIndex = value;
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public object Clone()
        {
            ChosenMethods newObject = new ChosenMethods();
            newObject.chosenMethodsNames = new string[this.chosenMethodsNames.Length];
            Array.Copy(this.chosenMethodsNames, newObject.chosenMethodsNames, this.chosenMethodsNames.Length);
            newObject.chosenMethodsParametersIndex = new string[this.chosenMethodsParametersIndex.Length];
            Array.Copy(this.chosenMethodsParametersIndex, newObject.chosenMethodsParametersIndex, this.chosenMethodsParametersIndex.Length);
            return newObject;
        }
    }
}