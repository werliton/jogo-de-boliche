//-----------------------------------------------------------------------
// <copyright file="UnityInterfaceWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the wrapper for the unity static methods.
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
using UnityEditor;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the wrapper for the unity static methods.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.IUnityInterfaceWrapper" />
    public class UnityInterfaceWrapper : IUnityInterfaceWrapper
    {
        /// <summary>
        ///   <see cref="AssetDatabase.LoadAssetAtPath" />
        /// </summary>
        /// <param name="texture">The texture to find.</param>
        /// <param name="type">The type of the asset.</param>
        /// <returns>
        /// The requested texture.
        /// </returns>
        public Texture2D AssetDatabaseLoadAssetAtPath(string texture, Type type)
        {
            return AssetDatabase.LoadAssetAtPath(texture, type) as Texture2D;
        }

        /// <summary>
        ///   <see cref="EditorApplication.IsCompiling" />.
        /// </summary>
        /// <returns>
        ///   <see cref="EditorApplication.IsCompiling" /> object.
        /// </returns>
        public bool EditorApplicationIsCompiling()
        {
            return EditorApplication.isCompiling;
        }

        /// <summary>
        ///   <see cref="EditorGUI.Foldout" />
        /// </summary>
        /// <param name="rect">The <see cref="Rect" /> object.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="content">The content to show.</param>
        /// <returns>
        /// True if the fold is open, false otherwise.
        /// </returns>
        public bool EditorGUIFoldout(Rect rect, bool foldout, string content)
        {
            return EditorGUI.Foldout(rect, foldout, content);
        }

        /// <summary>
        ///   <see cref="EditorGUI.Foldout" />
        /// </summary>
        /// <param name="rect">The <see cref="Rect" /> object.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="content">The content to show.</param>
        /// <param name="style">The style of the foldout.</param>
        /// <returns>
        /// True if the fold is open, false otherwise.
        /// </returns>
        public bool EditorGUIFoldout(Rect rect, bool foldout, string content, GUIStyle style)
        {
            return EditorGUI.Foldout(rect, foldout, content, style);
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.BeginHorizontal" />
        /// </summary>
        public void EditorGUILayoutBeginHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.EndHorizontal" />
        /// </summary>
        public void EditorGUILayoutEndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.GetControlRect" />
        /// </summary>
        /// <returns>
        /// The <see cref="Rect" /> for the Editor control.
        /// </returns>
        public Rect EditorGUILayoutGetControlRect()
        {
            return EditorGUILayout.GetControlRect(false, GUILayout.Width(20));
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.LabelField" />
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="options">The options.</param>
        public void EditorGUILayoutLabelField(Texture2D texture, GUILayoutOption[] options)
        {
            GUIContent label = new GUIContent(texture);
            EditorGUILayout.LabelField(label, options);
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.LabelField" />
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="labelWidth">Width of the label.</param>
        public void EditorGUILayoutLabelField(string text, float labelWidth)
        {
            EditorGUILayout.LabelField(text, EditorStyles.wordWrappedLabel, GUILayout.Width(labelWidth));
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.LabelField" />
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="labelWidth">Width of the label.</param>
        public void EditorGUILayoutLabelFieldTruncate(string text, float labelWidth)
        {
            EditorGUILayout.LabelField(text, EditorStyles.boldLabel, GUILayout.Width(labelWidth));
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.Popup" />
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="itemsList">The items list.</param>
        /// <returns>
        /// The index of the selected item from the itemsList.
        /// </returns>
        public int EditorGUILayoutPopup(int index, string[] itemsList)
        {
            return EditorGUILayout.Popup(index, itemsList);
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.PropertyField" />
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="label">The label.</param>
        /// <returns>
        /// True if the property has children and is expanded and includeChildren was set to false; otherwise false.
        /// </returns>
        public bool EditorGUILayoutPropertyField(ISerializedPropertyWrapper property, GUIContent label)
        {
            return EditorGUILayout.PropertyField(property.GetProperty(), label, GUILayout.ExpandWidth(true));
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.PropertyField" />
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="label">The label.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// True if the property has children and is expanded and includeChildren was set to false; otherwise false.
        /// </returns>
        public bool EditorGUILayoutPropertyFieldCustomizable(ISerializedPropertyWrapper property, GUIContent label, GUILayoutOption[] options)
        {
            return EditorGUILayout.PropertyField(property.GetProperty(), label, options);
        }

        /// <summary>
        ///   <see cref="EditorGUILayout.Separator" />
        /// </summary>
        public void EditorGUILayoutSeparator()
        {
            EditorGUILayout.Separator();
        }

        /// <summary>
        ///   <see cref="EditorGUIUtility.CurrentViewWidth" />
        /// </summary>
        /// <returns>
        /// The current view width.
        /// </returns>
        public float EditorGUIUtilityCurrentViewWidth()
        {
            return EditorGUIUtility.currentViewWidth;
        }

        /// <summary>
        ///   <see cref="EditorUtility.SetDirty" />
        /// </summary>
        /// <param name="target">The target.</param>
        public void EditorUtilitySetDirty(UnityEngine.Object target)
        {
            EditorUtility.SetDirty(target);
        }

        /// <summary>
        ///   <see cref="GUIContent" />
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <returns>
        ///   <see cref="GUIContent" /> object.
        /// </returns>
        public GUIContent GUIContent(string text)
        {
            return new GUIContent(text);
        }

        /// <summary>
        /// GUIs the layout button.
        /// </summary>
        /// <param name="inputTexture">The input texture.</param>
        /// <param name="style">The style.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if the button is pressed, false otherwise.</returns>
        public bool GUILayoutButton(Texture2D inputTexture, GUIStyle style, GUILayoutOption[] options)
        {
            return GUILayout.Button(inputTexture, style, options);
        }

        /// <summary>
        /// GUIs the layout button.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="style">The style of the button.</param>
        /// <param name="guiLayoutOption">The GUI layout option.</param>
        /// <returns>True if the button is pressed, false otherwise.</returns>
        public bool GUILayoutButton(string text, GUIStyle style, GUILayoutOption guiLayoutOption)
        {
            return GUILayout.Button(text, style, guiLayoutOption);
        }

        /// <summary>
        ///   <see cref="GUILayout.Height" />
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The GUILayoutOption object.
        /// </returns>
        public GUILayoutOption GUILayoutHeight(int value)
        {
            return GUILayout.Height(value);
        }

        /// <summary>
        ///   <see cref="GUILayout.Width" />
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <see cref="GUILayout.Width" /> object.
        /// </returns>
        public GUILayoutOption GUILayoutWidth(int value)
        {
            return GUILayout.Width(value);
        }

        /// <summary>
        ///   <see cref="UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal" />
        /// </summary>
        /// <param name="documentUrl">The document URL.</param>
        /// <param name="startLine">The start line.</param>
        public void UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(string documentUrl, int startLine)
        {
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(documentUrl, startLine);
        }
    }
}
