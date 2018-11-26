//-----------------------------------------------------------------------
// <copyright file="MethodParametersLoader.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class creates the <see cref="MethodParameters"/> object, populating the values of the parameters using the parameters indexes.
// 
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
using System.Collections.Generic;
using System.Reflection;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class creates the <see cref="MethodParameters"/> object, populating the values of the parameters using the parameters indexes.
    /// </summary>
    public class MethodParametersLoader
    {
        /// <summary>
        /// Creates the <see cref="MethodParameters"/> object, populating the values of the parameters using the parameters indexes.
        /// </summary>
        /// <param name="obj">The object containing the method.</param>
        /// <param name="method">The method.</param>
        /// <param name="id">The identifier of the parameters.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        /// <returns>The <see cref="MethodParameters"/> object, populating the values of the parameters using the parameters indexes.</returns>
        public virtual MethodParameters LoadMethodParameters(object obj, MethodInfo method, string id, string parametersIndex)
        {
            List<MethodParameter> parametersList = new List<MethodParameter>();
            ParameterInfo[] parametersInfo = method.GetParameters();
            foreach (ParameterInfo parameterInfo in parametersInfo)
            {
                MethodParameter mp = new MethodParameter();
                mp.ParameterInfoObject = parameterInfo;
                mp.Value = LoadParameterValue(obj, method, parameterInfo, id, parametersIndex);
                mp.ParameterLocation = LoadParameterLocation(obj, method, parameterInfo, id, parametersIndex);
                parametersList.Add(mp);
            }

            MethodParameters result = new MethodParameters();
            result.Parameters = parametersList.ToArray();
            return result;
        }

        /// <summary>
        /// Loads the parameter location.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="method">The method.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="parametersIndex">Index of the parameters.</param>
        /// <returns>The parameter location.</returns>
        private static ParameterLocation LoadParameterLocation(object obj, MethodInfo method, ParameterInfo parameter, string id, string parametersIndex)
        {
            // parametersIndex Format: ;paramtype,className.methodName.paramName.fullId,arrayName.Array.data[index];
            ParametersIndexUtilities parametersIndexUtilities = new ParametersIndexUtilities();
            ParameterLocation result = null;
            if (parametersIndex != null)
            {
                string expectedParameterFullName = parametersIndexUtilities.GetParameterFullName(method.DeclaringType.Name, method.Name, parameter.Name, id);
                string[] parameterIndexes = parametersIndexUtilities.GetParametersIndexList(parametersIndex);
                foreach (string parameterIndex in parameterIndexes)
                {
                    if (!parameterIndex.Equals(string.Empty))
                    {
                        string parameterFullName = parametersIndexUtilities.GetParameterFullName(parameterIndex);
                        if (parameterFullName.Equals(expectedParameterFullName))
                        {
                            string arrayName = parametersIndexUtilities.GetParameterValueStorageName(parameterIndex);

                            int index = parametersIndexUtilities.GetParameterValueStorageLocationIndex(parameterIndex);
                            result = new ParameterLocation();
                            ArrayStorageUtilities arrayStorageUtilities = new ArrayStorageUtilities();
                            FieldInfo field = arrayStorageUtilities.GetArrayStorageFieldInfoByName(obj, arrayName);
                            result.ParameterArrayLocation.ArrayFieldInfo = field;
                            result.ParameterArrayLocation.ArrayIndex = index;
                            result.ParameterArrayLocation.ArrayName = arrayName;
                            result.ParameterClassLocation.ComponentType = obj.GetType();
                            result.ParameterClassLocation.ComponentObject = obj;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Loads the parameter value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="method">The method.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="parametersIndexes">The parameters indexes.</param>
        /// <returns>The parameter value.</returns>
        private static object LoadParameterValue(object obj, MethodInfo method, ParameterInfo parameter, string id, string parametersIndexes)
        {
            // parametersIndex Format: ;paramtype,className.methodName.paramName.fullId,arrayName.Array.data[index];
            object result = null;
            ParametersIndexUtilities parametersIndexUtilities = new ParametersIndexUtilities();
            if (parametersIndexes != null)
            {
                string expectedParameterFullName = parametersIndexUtilities.GetParameterFullName(method.DeclaringType.Name, method.Name, parameter.Name, id);
                string[] parametersIndexesList = parametersIndexUtilities.GetParametersIndexList(parametersIndexes);
                foreach (string parameterIndex in parametersIndexesList)
                {
                    if (!parameterIndex.Equals(string.Empty))
                    {
                        string parameterFullName = parametersIndexUtilities.GetParameterFullName(parameterIndex);
                        if (parameterFullName.Equals(expectedParameterFullName))
                        {
                            string arrayName = parametersIndexUtilities.GetParameterValueStorageName(parameterIndex);

                            int index = parametersIndexUtilities.GetParameterValueStorageLocationIndex(parameterIndex);
                            result = GetValue(obj, arrayName, index);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the value of the parameter from its ParametersValuesStorage field.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="arrayName">Name of the array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the parameter.</returns>
        private static object GetValue(object obj, string arrayName, int index)
        {
            object result = null;
            ArrayStorageUtilities arrayStorageUtilities = new ArrayStorageUtilities();
            FieldInfo field = arrayStorageUtilities.GetArrayStorageFieldInfoByName(obj, arrayName);
            if (field == null)
            {
                return null;
            }

            Array array = field.GetValue(obj) as Array;
            if (array.Length > index)
            {
                result = array.GetValue(index);
            }

            return result;
        }
    }
}
