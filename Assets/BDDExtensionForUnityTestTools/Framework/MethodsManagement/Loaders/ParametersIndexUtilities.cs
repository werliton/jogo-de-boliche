//-----------------------------------------------------------------------
// <copyright file="ParametersIndexUtilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// A collection of utilities for manipulating the parameters indexes.
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// A collection of utilities for manipulating the parameters indexes.
    /// </summary>
    public class ParametersIndexUtilities
    {
        /// <summary>
        /// The argumentseparator character.
        /// </summary>
        private const string ARGUMENTSEPARATOR = ",";

        /// <summary>
        /// The attributesseparator character.
        /// </summary>
        private const string ATTRIBUTESSEPARATOR = ".";

        /// <summary>
        /// Builds the parameter index for the parameter identified by the method parameters.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="componentName">Name of the component.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterId">The id that makes the parameter full name unique.</param>
        /// <param name="parameterValueStorageName">The name of the Parameters Values Storage field.</param>
        /// <param name="index">The index of the parameter value inside the Parameters Values Storage field.</param>
        /// <returns>The complete index string of the parameter.</returns>
        public string BuildParameterIndex(Type parameterType, string componentName, string methodName, string parameterName, string parameterId, string parameterValueStorageName, int index)
        {
            string result = ";";
            result += parameterType.FullName;
            result += ARGUMENTSEPARATOR;
            result += componentName;
            result += ATTRIBUTESSEPARATOR;
            result += methodName;
            result += ATTRIBUTESSEPARATOR;
            result += parameterName;
            result += ATTRIBUTESSEPARATOR;
            result += parameterId;
            result += ARGUMENTSEPARATOR;
            result += parameterValueStorageName;
            result += ATTRIBUTESSEPARATOR;
            result += "Array.data[";
            result += index;
            result += "]";
            return result;
        }

        /// <summary>
        /// Gets the previously stored type of the parameter from the index string.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The stored type of the parameter.</returns>
        public string GetParameterType(string parameterIndex)
        {
            string result = null;
            string partialResult = parameterIndex;
            if (parameterIndex.StartsWith(";"))
            {
                partialResult = parameterIndex.Substring(1, parameterIndex.Length - 1);
            }

            result = partialResult.Split(',')[0];
            return result;
        }

        /// <summary>
        /// Gets the name of the component from the index string.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The name of the component.</returns>
        public string GetComponentName(string parameterIndex)
        {
            return parameterIndex.Split(',')[1].Split('.')[0];
        }

        /// <summary>
        /// Gets the name of the method from the index string.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The name of the method.</returns>
        public string GetMethodName(string parameterIndex)
        {
            return parameterIndex.Split(',')[1].Split('.')[1];
        }

        /// <summary>
        /// Gets the name of the parameter from the index string.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The name of the parameter.</returns>
        public string GetParameterName(string parameterIndex)
        {
            return parameterIndex.Split(',')[1].Split('.')[2];
        }

        /// <summary>
        /// Gets the parameter id from the index string.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The parameter id.</returns>
        public string GetParameterId(string parameterIndex)
        {
            return parameterIndex.Split(',')[1].Split('.')[3];
        }

        /// <summary>
        /// Gets the parameter value storage location.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The parameter value storage location.</returns>
        public string GetParameterValueStorageLocation(string parameterIndex)
        {
            return parameterIndex.Split(',')[2];
        }

        /// <summary>
        /// Gets the index of the parameter value storage location.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The index of the parameter value storage location.</returns>
        public int GetParameterValueStorageLocationIndex(string parameterIndex)
        {
            string parameterValueStorageLocation = this.GetParameterValueStorageLocation(parameterIndex);
            string indexString = parameterValueStorageLocation.Split(']')[0].Split('[')[1];
            return int.Parse(indexString);
        }

        /// <summary>
        /// Gets the full name of the parameter.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The full name of the parameter.</returns>
        public string GetParameterFullName(string parameterIndex)
        {
            return parameterIndex.Split(',')[1];
        }

        /// <summary>
        /// Gets the full name of the method.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The full name of the method.</returns>
        public string GetMethodFullName(string parameterIndex)
        {
            return this.GetParameterFullName(parameterIndex).Split('.')[0] + "." + this.GetParameterFullName(parameterIndex).Split('.')[1];
        }

        /// <summary>
        /// Gets the name of the parameter value storage field.
        /// </summary>
        /// <param name="parameterIndex">Index of the parameter.</param>
        /// <returns>The name of the parameter value storage field.</returns>
        public string GetParameterValueStorageName(string parameterIndex)
        {
            return this.GetParameterValueStorageLocation(parameterIndex).Split('.')[0];
        }

        /// <summary>
        /// Gets the parameters index list by dividing them from the parametersIndexes string for the whole method parameters.
        /// </summary>
        /// <param name="parametersIndexes">The parameters indexes.</param>
        /// <returns>The parameters index list.</returns>
        public string[] GetParametersIndexList(string parametersIndexes)
        {
            string[] result = null;
            List<string> resultList = new List<string>();
            if (parametersIndexes == null || parametersIndexes.Equals(string.Empty))
            {
                result = new string[0];
            }
            else
            {
                string[] stringSplitted = parametersIndexes.Split(';');
                foreach (string element in stringSplitted)
                {
                    if (!element.Equals(string.Empty))
                    {
                        resultList.Add(element);
                    }
                }

                result = resultList.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Gets the full name of the parameter giving its elements.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterId">The parameter identifier.</param>
        /// <returns>The full name of the parameter.</returns>
        public string GetParameterFullName(string componentName, string methodName, string parameterName, string parameterId)
        {
            return componentName + "." + methodName + "." + parameterName + "." + parameterId;
        }
    }
}
