//-----------------------------------------------------------------------
// <copyright file="SerializedObjectWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the wrapper for the SerializedObject.
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
using UnityEditor;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the wrapper for the SerializedObject.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.ISerializedObjectWrapper" />
    public class SerializedObjectWrapper : ISerializedObjectWrapper
    {
        /// <summary>
        /// The serialized object.
        /// </summary>
        private SerializedObject serializedObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedObjectWrapper"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public SerializedObjectWrapper(UnityEngine.Object component)
        {
            this.serializedObject = new SerializedObject(component);
        }

        /// <summary>
        /// Applies the modified properties to the SerializedObject.
        /// </summary>
        public void ApplyModifiedProperties()
        {
            this.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Finds the SerializedProperty.
        /// </summary>
        /// <param name="parameterLocationString">The parameter location string.</param>
        /// <returns>
        /// The SerializedProperty.
        /// </returns>
        public ISerializedPropertyWrapper FindProperty(string parameterLocationString)
        {
            SerializedProperty property = this.serializedObject.FindProperty(parameterLocationString);
            ISerializedPropertyWrapper propertyWrapper = new SerializedPropertyWrapper(property);
            return propertyWrapper;
        }

        /// <summary>
        /// Gets the serializedObject.
        /// </summary>
        /// <returns>
        /// The serializedObject.
        /// </returns>
        public SerializedObject GetSerializedObject()
        {
            return this.serializedObject;
        }

        /// <summary>
        /// Updates the data from the SerializedObject.
        /// </summary>
        public void Update()
        {
            this.serializedObject.Update();
        }
    }
}
