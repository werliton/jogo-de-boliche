//-----------------------------------------------------------------------
// <copyright file="RunnerEditorBusinessLogicParametersRebuild.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class contains the business logic for the rebuild of the parameters index.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class contains the business logic for the rebuild of the parameters index.
    /// </summary>
    public class RunnerEditorBusinessLogicParametersRebuild
    {
        /// <summary>
        /// Determines whether the rebuild of the parameters indexes is needed.
        /// </summary>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        /// <param name="runnerBusinessLogicData">The runner business logic data.</param>
        /// <param name="components">The components.</param>
        /// <param name="bddComponentsFilter">The BDD components filter.</param>
        /// <returns>
        ///   <c>true</c> if the rebuild of the parameters indexes is needed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsParametersRebuildNeeded(IUnityInterfaceWrapper unityInterfaceWrapper, RunnerEditorBusinessLogicData runnerBusinessLogicData, Component[] components, ComponentsFilter bddComponentsFilter)
        {
            bool isBDDObjectsNull = this.IsBDDObjectsNull(runnerBusinessLogicData);
            bool bddObjectsHaveChanged = this.BddObjectsHaveChanged(components, runnerBusinessLogicData, bddComponentsFilter);
            bool isEditorApplicationCompilingJustFinished = this.IsEditorApplicationCompilingJustFinished(unityInterfaceWrapper, runnerBusinessLogicData);
            bool isDynamicScenario = this.IsDynamicScenario(components);
            return this.IsParametersRebuildNeeded(isBDDObjectsNull, bddObjectsHaveChanged, runnerBusinessLogicData.IsCompiling, isEditorApplicationCompilingJustFinished, runnerBusinessLogicData.Rebuild, isDynamicScenario);
        }

        /// <summary>
        /// Determines whether the rebuild of the parameters indexes is needed.
        /// </summary>
        /// <param name="isBDDObjectsNull">If set to <c>true</c> [is BDD objects null].</param>
        /// <param name="bddObjectsHaveChanged">If set to <c>true</c> [BDD objects have changed].</param>
        /// <param name="isEditorApplicationCompiling">If set to <c>true</c> [is editor application compiling].</param>
        /// <param name="isEditorApplicationCompilingJustFinished">If set to <c>true</c> [is editor application compiling just finished].</param>
        /// <param name="rebuild">If set to <c>true</c> [rebuild].</param>
        /// <param name="isDynamicScenario">If set to <c>true</c> [is dynamic scenario].</param>
        /// <returns>
        ///    <c>true</c> if the rebuild of the parameters indexes is needed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsParametersRebuildNeeded(bool isBDDObjectsNull, bool bddObjectsHaveChanged, bool isEditorApplicationCompiling, bool isEditorApplicationCompilingJustFinished, bool rebuild, bool isDynamicScenario)
        {
            if (isEditorApplicationCompiling || !isDynamicScenario)
            {
                // While compiling the state of the BDDComponents could be inconsistent
                return false;
            }
            else
            {
                return isBDDObjectsNull || bddObjectsHaveChanged || isEditorApplicationCompilingJustFinished || rebuild;
            }
        }

        /// <summary>
        /// Determines whether the stored BDDObjects collection is null.
        /// </summary>
        /// <param name="runnerBusinessLogicData">The runner business logic data.</param>
        /// <returns>
        ///   <c>true</c> if  the stored BDDObjects collection is null; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBDDObjectsNull(RunnerEditorBusinessLogicData runnerBusinessLogicData)
        {
            bool result = false;
            if (runnerBusinessLogicData.BDDObjects == null)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the build process is just finished.
        /// </summary>
        /// <param name="unityInterfaceWrapper">The unity interface wrapper.</param>
        /// <param name="runnerBusinessLogicData">The runner business logic data.</param>
        /// <returns>
        ///   <c>true</c> if the build process is just finished; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEditorApplicationCompilingJustFinished(IUnityInterfaceWrapper unityInterfaceWrapper, RunnerEditorBusinessLogicData runnerBusinessLogicData)
        {
            bool result = false;
            if (!unityInterfaceWrapper.EditorApplicationIsCompiling())
            {
                if (runnerBusinessLogicData.IsCompiling)
                {
                    result = true;
                    runnerBusinessLogicData.IsCompiling = false;
                }
            }
            else
            {
                runnerBusinessLogicData.IsCompiling = true;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Determines if the BDD Components are changed inside the Integration Test.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <param name="runnerBusinessLogicData">The runner business logic data.</param>
        /// <param name="bddComponentsFilter">The BDD components filter.</param>
        /// <returns>True if some component is changed.</returns>
        public bool BddObjectsHaveChanged(Component[] components, RunnerEditorBusinessLogicData runnerBusinessLogicData, ComponentsFilter bddComponentsFilter)
        {
            bool result = false;
            object[] newBddObjects = bddComponentsFilter.Filter(components);
            object[] previuousBDDObjects = null;
            if (runnerBusinessLogicData.BDDObjects == null)
            {
                previuousBDDObjects = new object[0];
            }
            else
            {
                previuousBDDObjects = runnerBusinessLogicData.BDDObjects;
            }

            // Checking for some new BDD Object.
            foreach (object mainObj in newBddObjects)
            {
                bool found = false;
                foreach (object obj in previuousBDDObjects)
                {
                    if (obj.Equals(mainObj))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    result = true;
                }
            }

            // Checking for some BDD Object removed
            foreach (object mainObj in previuousBDDObjects)
            {
                bool found = false;
                foreach (object obj in newBddObjects)
                {
                    if (obj.Equals(mainObj))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Rebuilds the serialized objects list.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <param name="serializedObjects">The serialized objects.</param>
        /// <returns>The new serialized objects list.</returns>
        public Dictionary<Type, ISerializedObjectWrapper> RebuildSerializedObjectsList(Component[] components, Dictionary<Type, ISerializedObjectWrapper> serializedObjects)
        {
            Dictionary<Type, ISerializedObjectWrapper> result = new Dictionary<Type, ISerializedObjectWrapper>();
            foreach (Component component in components)
            {
                ISerializedObjectWrapper serializedObjectWrapper = null;
                if (serializedObjects == null || !serializedObjects.ContainsKey(component.GetType()))
                {
                    serializedObjectWrapper = new SerializedObjectWrapper(component);
                }
                else
                {
                    serializedObjects.TryGetValue(component.GetType(), out serializedObjectWrapper);
                }

                result.Add(component.GetType(), serializedObjectWrapper);
            }

            return result;
        }

        /// <summary>
        /// Determines whether the scenario is Dynamic.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns>
        ///   <c>true</c> if the scenario is Dynamic; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDynamicScenario(Component[] components)
        {
            bool result = true;
            foreach (Component component in components)
            {
                if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}