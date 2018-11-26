//-----------------------------------------------------------------------
// <copyright file="ArrayStorageUtilities.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// A collection of utilities for manipulating the Parameters Values Storage fields.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// A collection of utilities for manipulating the Parameters Values Storage fields.
    /// </summary>
    public class ArrayStorageUtilities
    {
        /// <summary>
        /// Gets the <see cref="FieldInfo"/> of a Parameters Array Storage field given its type.
        /// </summary>
        /// <param name="dynamicBDDComponent">The dynamic BDD component.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <returns>The <see cref="FieldInfo"/> of the Parameters Array Storage.</returns>
        public FieldInfo GetArrayStorageFieldInfoByType(object dynamicBDDComponent, Type elementType)
        {
            FieldInfo result = null;
            Type type = dynamicBDDComponent.GetType();
            FieldInfo[] fieldsInfo = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldsInfo)
            {
                object[] valuesArrayStorageList = fieldInfo.GetCustomAttributes(typeof(ParametersValuesStorage), true);
                if (valuesArrayStorageList != null && valuesArrayStorageList.Length == 1)
                {
                    if (fieldInfo.FieldType.IsArray && fieldInfo.FieldType.GetElementType().Equals(elementType))
                    {
                        result = fieldInfo;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Resets the array storage.
        /// </summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <param name="dynamicBDDComponent">The dynamic BDD component.</param>
        public void ResetArrayStorage(FieldInfo fieldInfo, object dynamicBDDComponent)
        {
            Type elementType = fieldInfo.FieldType.GetElementType();
            Array array = Array.CreateInstance(elementType, 0);
            fieldInfo.SetValue(dynamicBDDComponent, array);
        }

        /// <summary>
        /// Resets all arrays storage.
        /// </summary>
        /// <param name="components">The components.</param>
        public void ResetAllArrayStorage(Component[] components)
        {
            foreach (object component in components)
            {
                Type type = component.GetType();
                FieldInfo[] fieldsInfo = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (FieldInfo fieldInfo in fieldsInfo)
                {
                    object[] valuesArrayStorageList = fieldInfo.GetCustomAttributes(typeof(ParametersValuesStorage), true);
                    if (valuesArrayStorageList != null && valuesArrayStorageList.Length == 1)
                    {
                        this.ResetArrayStorage(fieldInfo, component);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="FieldInfo"/> of a Parameters Array Storage field given its name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="arrayName">Name of the array.</param>
        /// <returns>The <see cref="FieldInfo"/> of the Parameters Array Storage.</returns>
        public FieldInfo GetArrayStorageFieldInfoByName(object obj, string arrayName)
        {
            FieldInfo result = null;
            Type type = obj.GetType();
            FieldInfo[] fieldsInfo = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldsInfo)
            {
                object[] valuesArrayStorageList = fieldInfo.GetCustomAttributes(typeof(ParametersValuesStorage), true);
                if (valuesArrayStorageList != null && valuesArrayStorageList.Length == 1)
                {
                    if (fieldInfo.FieldType.IsArray && fieldInfo.Name.Equals(arrayName))
                    {
                        result = fieldInfo;
                    }
                }
            }

            return result;
        }
    }
}
