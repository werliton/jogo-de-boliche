//-----------------------------------------------------------------------
// <copyright file="SourcesManagement.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the methods for open the BDD Components sources.
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
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the methods for open the BDD Components sources.
    /// </summary>
    public static class SourcesManagement
    {
        /// <summary>
        /// Gets the absolute DLL path from method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The absolute DLL path from method.</returns>
        public static string GetAbsoluteDLLPathFromMethod(MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType.Assembly.Location;
        }

        /// <summary>
        /// Opens the source code of a BDD Component positioning the cursor to the method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        public static void OpenSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper)
        {
            ReaderParameters readerParameters = new ReaderParameters { ReadSymbols = true };
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(GetAbsoluteDLLPathFromMethod(methodInfo), readerParameters);
            OpenSourceCode(methodInfo, unityInterfaceWrapper, assemblyDefinition);
        }

        /// <summary>
        /// Opens the source code of a BDD Component positioning the cursor to the method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        public static void OpenMethodSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper)
        {
            ReaderParameters readerParameters = new ReaderParameters { ReadSymbols = true };
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(GetAbsoluteDLLPathFromMethod(methodInfo), readerParameters);
            OpenMethodSourceCode(methodInfo, unityInterfaceWrapper, assemblyDefinition);
        }

        /// <summary>
        /// Opens the source code of a BDD Component positioning the cursor to the method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        /// <param name="assemblyDefinition">The assembly definition.</param>
        public static void OpenMethodSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper, AssemblyDefinition assemblyDefinition)
        {
            SequencePoint sp = GetSequencePointForMethod(methodInfo, assemblyDefinition);
            Document document = sp.Document;
            string documentUrl = document.Url;
            unityInterfaceWrapper.UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(documentUrl, sp.StartLine);
        }

        /// <summary>
        /// Opens the source code of a BDD Component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        public static void OpenSourceCode(Component component, IUnityInterfaceWrapper unityInterfaceWrapper)
        {
            MonoScript script = MonoScript.FromMonoBehaviour((MonoBehaviour)component);
            string relativeDocumentUrl = AssetDatabase.GetAssetPath(script);
            string absoluteDocumentUrl = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + relativeDocumentUrl;
            unityInterfaceWrapper.UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(absoluteDocumentUrl, 0);
        }

        /// <summary>
        /// Gets the sequence point for method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="assemblyDefinition">The assembly definition.</param>
        /// <returns>The sequence point for method.</returns>
        private static SequencePoint GetSequencePointForMethod(MethodInfo methodInfo, AssemblyDefinition assemblyDefinition)
        {
            TypeDefinition componentTypeDefinition = null;
            foreach (TypeDefinition typeDefinition in assemblyDefinition.MainModule.Types)
            {
                if (typeDefinition.FullName.Equals(methodInfo.DeclaringType.FullName))
                {
                    componentTypeDefinition = typeDefinition;
                    break;
                }
            }

            string methodFullName = GetMethodFullName(methodInfo);
            MethodDefinition methodDef = null;
            foreach (MethodDefinition methodDefinition in componentTypeDefinition.Methods)
            {
                if (methodDefinition.FullName.Equals(methodFullName))
                {
                    methodDef = methodDefinition;
                    break;
                }
            }

            SequencePoint sp = methodDef.Body.Instructions
        .Where(instruction => instruction.SequencePoint != null)
        .Select(instruction => instruction.SequencePoint)
        .FirstOrDefault();
            return sp;
        }

        /// <summary>
        /// Gets the full name of the method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The full name of the method.</returns>
        private static string GetMethodFullName(MethodInfo methodInfo)
        {
            string parameters = "(";
            foreach (ParameterInfo parameter in methodInfo.GetParameters())
            {
                if (!parameters.Equals("("))
                {
                    parameters += ",";
                }

                parameters += parameter.ParameterType.FullName;
            }

            parameters += ")";
            string methodFullName = methodInfo.ReturnType.FullName + " " + methodInfo.DeclaringType.FullName + "::" + methodInfo.Name + parameters;
            return methodFullName;
        }

        /// <summary>
        /// Opens the source code of the BDD Component.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        /// <param name="assemblyDefinition">The assembly definition.</param>
        private static void OpenSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper, AssemblyDefinition assemblyDefinition)
        {
            SequencePoint sp = GetSequencePointForMethod(methodInfo, assemblyDefinition);
            Document document = sp.Document;
            string documentUrl = document.Url;
            unityInterfaceWrapper.UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(documentUrl, 0);
        }
    }
}
