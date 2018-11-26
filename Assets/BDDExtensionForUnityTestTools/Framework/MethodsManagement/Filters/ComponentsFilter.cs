//-----------------------------------------------------------------------
// <copyright file="ComponentsFilter.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// Class containing the utility to select from a collection of <see cref="Component"/> objects only the <see cref="StaticBDDComponent"/> and the <see cref="DynamicBDDComponent"/> components.
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
using System.Linq;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// Class containing the utility to select from a collection of <see cref="Component"/> objects only the <see cref="StaticBDDComponent"/> and the <see cref="DynamicBDDComponent"/> components.
    /// </summary>
    public class ComponentsFilter
    {
        /// <summary>
        /// Filters the specified components.
        /// </summary>
        /// <param name="components">The components to be filtered.</param>
        /// <returns>The filtered components.</returns>
        public virtual Component[] Filter(Component[] components)
        {
            List<Component> bddComponentsList = Filter(typeof(StaticBDDComponent), components);

            if (bddComponentsList.Count() == 0)
            {
                bddComponentsList = Filter(typeof(DynamicBDDComponent), components);
            }

            return bddComponentsList.ToArray();
        }

        /// <summary>
        /// Gets a string containing the list of the components.
        /// </summary>
        /// <param name="bddComponentList">The BDD Components list.</param>
        /// <returns>A string containing the list of the components.</returns>
        private static string GetClassList(List<Component> bddComponentList)
        {
            string result = string.Empty;
            foreach (Component component in bddComponentList)
            {
                if (!result.Equals(string.Empty))
                {
                    result += ",\n";
                }

                result += component.GetType().Name;
            }

            return result;
        }

        /// <summary>
        /// Filters the components by the specified BDD Declaration.
        /// </summary>
        /// <param name="bddComponentDeclaration">The type of the BDD Declaration.</param>
        /// <param name="components">The components.</param>
        /// <returns>The  filtered components.</returns>
        private static List<Component> Filter(Type bddComponentDeclaration, Component[] components)
        {
            List<Component> bddComponentsList = new List<Component>();
            foreach (Component component in components)
            {
                if (bddComponentDeclaration.IsAssignableFrom(component.GetType()))
                {
                    bddComponentsList.Add(component);
                }
            }

            return bddComponentsList;
        }
    }
}
