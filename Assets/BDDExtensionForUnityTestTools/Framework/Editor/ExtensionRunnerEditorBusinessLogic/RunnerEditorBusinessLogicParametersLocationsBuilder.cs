//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicParametersLocationsBuilder.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic for building the Parameters Locations.
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
using System.Reflection;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic for building the Parameters Locations.
    /// </summary>
    public class RunnerEditorBusinessLogicParametersLocationsBuilder
    {
        /// <summary>
        /// Builds the parameters location.
        /// </summary>
        /// <param name="fullMethodsDescriptionsList">The full methods descriptions list.</param>
        internal void BuildParametersLocation(List<FullMethodDescription> fullMethodsDescriptionsList)
        {
            foreach (FullMethodDescription fullMethodDescription in fullMethodsDescriptionsList)
            {
                fullMethodDescription.ParametersIndex = string.Empty;
            }

            ArrayStorageUtilities arrayStorageUtilities = new ArrayStorageUtilities();
            foreach (FullMethodDescription fullMethodDescription in fullMethodsDescriptionsList)
            {
                if (fullMethodDescription != null)
                {
                    string parametersIndex = string.Empty;
                    foreach (MethodParameter methodParameter in fullMethodDescription.Parameters.Parameters)
                    {
                        FieldInfo arrayStorageFieldInfo = arrayStorageUtilities.GetArrayStorageFieldInfoByType(fullMethodDescription.ComponentObject, methodParameter.ParameterInfoObject.ParameterType);

                        Array array = arrayStorageFieldInfo.GetValue(fullMethodDescription.ComponentObject) as Array;
                        int oldArrayLength = array.Length;
                        int newArrayLength = oldArrayLength + 1;
                        Array newArray = Array.CreateInstance(methodParameter.ParameterInfoObject.ParameterType, newArrayLength);
                        if (array.Length > 0)
                        {
                            array.CopyTo(newArray, 0);
                        }

                        int index = newArray.Length - 1;
                        newArray.SetValue(methodParameter.Value, index);
                        arrayStorageFieldInfo.SetValue(fullMethodDescription.ComponentObject, newArray);
                        parametersIndex += ";" + this.BuildParameterIndex(fullMethodDescription, methodParameter, index, arrayStorageFieldInfo);
                    }

                    this.AddParametersIndex(fullMethodDescription, parametersIndex);
                }
            }
        }

        /// <summary>
        /// Rebuilds the parameters indexes arrays.
        /// </summary>
        /// <param name="fullMethodsDescriptionsList">The full methods descriptions list.</param>
        /// <param name="methodsFullNamesList">The methods full names list.</param>
        /// <returns>The array containing the parameters indexes.</returns>
        internal string[] RebuildParametersIndexesArrays(List<FullMethodDescription> fullMethodsDescriptionsList, string[] methodsFullNamesList)
        {
            int resultIndex = 0;
            string[] result = new string[methodsFullNamesList.Length];
            for (int index = 0; index < fullMethodsDescriptionsList.Count; index++)
            {
                if (fullMethodsDescriptionsList[index] != null && fullMethodsDescriptionsList[index].MainMethod == null)
                {
                    result[resultIndex] = fullMethodsDescriptionsList[index].ParametersIndex;
                    resultIndex++;
                }
            }

            return result;
        }

        /// <summary>
        /// Adds the index of the parameters.
        /// </summary>
        /// <param name="fullMethodDescription">The full method description.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        private void AddParametersIndex(FullMethodDescription fullMethodDescription, string parametersIndex)
        {
            FullMethodDescription baseFullMethodDescription = fullMethodDescription;
            while (baseFullMethodDescription.MainMethod != null)
            {
                baseFullMethodDescription = baseFullMethodDescription.MainMethod;
            }

            baseFullMethodDescription.ParametersIndex += parametersIndex;
        }

        /// <summary>
        /// Builds the index of the parameter.
        /// </summary>
        /// <param name="fullMethodDescription">The full method description.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="index">The index.</param>
        /// <param name="arrayFieldInfo">The array field information.</param>
        /// <returns>A string containing the parameter index.</returns>
        private string BuildParameterIndex(FullMethodDescription fullMethodDescription, MethodParameter parameter, int index, FieldInfo arrayFieldInfo)
        {
            // ";string,BDDComponentForTest.GivenMethod.stringParam.fullId,stringsArrayStorage.Array.data[0];"
            MethodsManagementUtilities methodsManagementUtilities = new MethodsManagementUtilities();
            return parameter.ParameterInfoObject.ParameterType.FullName + "," + fullMethodDescription.GetFullName() + "." +
                parameter.ParameterInfoObject.Name + "." + methodsManagementUtilities.GetMainFullId(fullMethodDescription.MainMethod) + fullMethodDescription.Id  + "," + arrayFieldInfo.Name + ".Array.data[" + index + "]";
        }
    }
}
