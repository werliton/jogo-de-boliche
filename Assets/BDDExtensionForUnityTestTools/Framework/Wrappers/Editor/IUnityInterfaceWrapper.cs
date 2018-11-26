//-----------------------------------------------------------------------
// <copyright file="IUnityInterfaceWrapper.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the interface for the unity static methods wrapper.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the interface for the unity static methods wrapper.
    /// </summary>
    public interface IUnityInterfaceWrapper
    {
        /// <summary>
        /// <see cref="EditorApplication.IsCompiling"/>.
        /// </summary>
        /// <returns><see cref="EditorApplication.IsCompiling"/> object.</returns>
        bool EditorApplicationIsCompiling();

        /// <summary>
        /// <see cref="EditorGUILayout.BeginHorizontal"/>
        /// </summary>
        void EditorGUILayoutBeginHorizontal();

        /// <summary>
        /// <see cref="EditorGUILayout.EndHorizontal"/>
        /// </summary>
        void EditorGUILayoutEndHorizontal();

        /// <summary>
        /// <see cref="EditorGUILayout.PropertyField"/>
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="label">The label.</param>
        /// <returns>True if the property has children and is expanded and includeChildren was set to false; otherwise false.</returns>
        bool EditorGUILayoutPropertyField(ISerializedPropertyWrapper property, GUIContent label);

        /// <summary>
        /// <see cref="EditorGUILayout.PropertyField"/>
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="label">The label.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if the property has children and is expanded and includeChildren was set to false; otherwise false.</returns>
        bool EditorGUILayoutPropertyFieldCustomizable(ISerializedPropertyWrapper property, GUIContent label, GUILayoutOption[] options);

        /// <summary>
        /// <see cref="EditorGUILayout.LabelField"/>
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="labelWidth">Width of the label.</param>
        void EditorGUILayoutLabelField(string text, float labelWidth);

        /// <summary>
        /// <see cref="EditorGUILayout.LabelField"/>
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="labelWidth">Width of the label.</param>
        void EditorGUILayoutLabelFieldTruncate(string text, float labelWidth);

        /// <summary>
        /// <see cref="EditorGUILayout.Popup"/>
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="itemsList">The items list.</param>
        /// <returns>The index of the selected item from the itemsList.</returns>
        int EditorGUILayoutPopup(int index, string[] itemsList);

        /// <summary>
        /// <see cref="EditorGUI.Foldout"/>
        /// </summary>
        /// <param name="rect">The <see cref="Rect"/> object.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="content">The content to show.</param>
        /// <returns>True if the fold is open, false otherwise.</returns>
        bool EditorGUIFoldout(Rect rect, bool foldout, string content);

        /// <summary>
        /// <see cref="EditorGUI.Foldout"/>
        /// </summary>
        /// <param name="rect">The <see cref="Rect"/> object.</param>
        /// <param name="foldout">If set to <c>true</c> [foldout].</param>
        /// <param name="content">The content to show.</param>
        /// <param name="style">The style of the foldout.</param>
        /// <returns>True if the fold is open, false otherwise.</returns>
        bool EditorGUIFoldout(Rect rect, bool foldout, string content, GUIStyle style);

        /// <summary>
        /// <see cref="EditorGUIUtility.CurrentViewWidth"/>
        /// </summary>
        /// <returns>The current view width.</returns>
        float EditorGUIUtilityCurrentViewWidth();

        /// <summary>
        /// <see cref="EditorUtility.SetDirty"/>
        /// </summary>
        /// <param name="target">The target.</param>
        void EditorUtilitySetDirty(UnityEngine.Object target);

        /// <summary>
        /// <see cref="GUILayout.Button"/>
        /// </summary>
        /// <param name="v">The text to show.</param>
        /// <param name="style">The style of the button..</param>
        /// <param name="guiLayoutOption">The GUI layout option.</param>
        /// <returns>True if the button is pressed, false otherwise.</returns>
        bool GUILayoutButton(string v, GUIStyle style, GUILayoutOption guiLayoutOption);

        /// <summary>
        /// <see cref="EditorGUILayout.Separator"/>
        /// </summary>
        void EditorGUILayoutSeparator();

        /// <summary>
        /// <see cref="EditorGUILayout.GetControlRect"/>
        /// </summary>
        /// <returns>The <see cref="Rect"/> for the Editor control.</returns>
        Rect EditorGUILayoutGetControlRect();

        /// <summary>
        /// <see cref="GUIContent"/>
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <returns><see cref="GUIContent"/> object.</returns>
        GUIContent GUIContent(string text);

        /// <summary>
        /// <see cref="GUILayout.Width"/>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see cref="GUILayout.Width"/> object.</returns>
        GUILayoutOption GUILayoutWidth(int value);

        /// <summary>
        /// <see cref="UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal"/>
        /// </summary>
        /// <param name="documentUrl">The document URL.</param>
        /// <param name="startLine">The start line.</param>
        void UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(string documentUrl, int startLine);

        /// <summary>
        /// <see cref="EditorGUILayout.LabelField"/>
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="options">The options.</param>
        void EditorGUILayoutLabelField(Texture2D texture, GUILayoutOption[] options);

        /// <summary>
        /// <see cref="GUILayout.Height"/>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The GUILayoutOption object.</returns>
        GUILayoutOption GUILayoutHeight(int value);

        /// <summary>
        /// <see cref="AssetDatabase.LoadAssetAtPath"/>
        /// </summary>
        /// <param name="texture">The texture to find.</param>
        /// <param name="type">The type of the asset.</param>
        /// <returns>The requested texture.</returns>
        Texture2D AssetDatabaseLoadAssetAtPath(string texture, Type type);

        /// <summary>
        /// <see cref="GUILayoutButton"/>
        /// </summary>
        /// <param name="inputTexture">The input texture.</param>
        /// <param name="label">The label.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if the button is pressed, false otherwise.</returns>
        bool GUILayoutButton(Texture2D inputTexture, GUIStyle label, GUILayoutOption[] options);
    }
}