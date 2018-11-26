//-----------------------------------------------------------------------
// <copyright file="MethodsLoader.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class loads the <see cref="BaseMethodDescription"/> objects given a <see cref="IMethodsFilter"/> and a <see cref="BaseMethodDescriptionBuilder"/>.
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
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class loads the <see cref="BaseMethodDescription"/> objects given a <see cref="IMethodsFilter"/> and a <see cref="BaseMethodDescriptionBuilder"/>.
    /// </summary>
    public class MethodsLoader
    {
        /// <summary>
        /// The BaseMethodDescription builder.
        /// </summary>
        private BaseMethodDescriptionBuilder methodBuilder;

        /// <summary>
        /// The method filter.
        /// </summary>
        private IMethodsFilter methodFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsLoader"/> class.
        /// </summary>
        /// <param name="methodBuilder">The BaseMethodDescription builder.</param>
        /// <param name="methodFilter">The method filter.</param>
        public MethodsLoader(BaseMethodDescriptionBuilder methodBuilder, IMethodsFilter methodFilter)
        {
            this.methodBuilder = methodBuilder;
            this.methodFilter = methodFilter;
        }

        /// <summary>
        /// Loads the step methods for a given step type from the components.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method to filter.</typeparam>
        /// <param name="components">The components.</param>
        /// <returns>The list of <see cref="BaseMethodDescription"/> objects. Each element corresponds to a BDD Method of the given Step Type.</returns>
        public virtual List<BaseMethodDescription> LoadStepMethods<T>(Component[] components) where T : IGivenWhenThenDeclaration
        {
            List<BaseMethodDescription> result = new List<BaseMethodDescription>();
            foreach (Component component in components)
            {
                result.AddRange(this.LoadStepMethodsFromSingleComponent<T>(component));
            }

            return result;
        }

        /// <summary>
        /// Loads the step methods filtered and ordered by the list of methods called <paramref name="chosenMethods"/>.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method to filter.</typeparam>
        /// <param name="components">The components.</param>
        /// <param name="chosenMethods">The chosen methods.</param>
        /// <returns>The list of <see cref="BaseMethodDescription"/> objects. Each element corresponds to a BDD Method of the given Step Type.</returns>
        public virtual List<BaseMethodDescription> LoadOrderedStepMethods<T>(Component[] components, string[] chosenMethods) where T : IGivenWhenThenDeclaration
        {
            List<BaseMethodDescription> result = new List<BaseMethodDescription>();
            foreach (string chosenMethod in chosenMethods)
            {
                result.Add(this.LoadStepMethodForASingleChosenMethod<T>(components, chosenMethod));
            }

            return result;
        }

        /// <summary>
        /// Loads the BaseMethodDescription object method for a single chosen method.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method to filter.</typeparam>
        /// <param name="components">The components.</param>
        /// <param name="chosenMethod">The chosen method.</param>
        /// <returns>The requested <see cref="BaseMethodDescription"/> object.</returns>
        private BaseMethodDescription LoadStepMethodForASingleChosenMethod<T>(Component[] components, string chosenMethod) where T : IGivenWhenThenDeclaration
        {
            BaseMethodDescription result = new BaseMethodDescription();

            if (chosenMethod == null || chosenMethod.Equals(string.Empty))
            {
                return null;
            }
            else
            {
                string componentName = chosenMethod.Split('.')[0];
                string methodName = chosenMethod.Split('.')[1];
                Component component = null;
                foreach (Component item in components)
                {
                    if (item.GetType().Name.Equals(componentName))
                    {
                        component = item;
                        break;
                    }
                }

                MethodInfo methodInfo = component.GetType().GetMethod(methodName);
                result = this.methodBuilder.Build<T>(component, methodInfo);
            }

            return result;
        }

        /// <summary>
        /// Loads the step methods from single component.
        /// </summary>
        /// <typeparam name="T">The type of the Step Method to filter.</typeparam>
        /// <param name="component">The component.</param>
        /// <returns>The list of <see cref="BaseMethodDescription"/> objects. Each element corresponds to a BDD Method of the given Step Type.</returns>
        private List<BaseMethodDescription> LoadStepMethodsFromSingleComponent<T>(Component component) where T : IGivenWhenThenDeclaration
        {
            List<BaseMethodDescription> result = new List<BaseMethodDescription>();
            MethodInfo[] methods = component.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (this.methodFilter.Filter<T>(method))
                {
                    result.Add(this.methodBuilder.Build<T>(component, method));
                }
            }

            if (typeof(StaticBDDComponent).IsAssignableFrom(component.GetType()))
            {
                result = this.OrderMethodsByExecutionOrder(result);
            }

            return result;
        }

        /// <summary>
        /// Orders the methods by their execution order.
        /// </summary>
        /// <param name="listToOrder">The list to order.</param>
        /// <returns>The ordered list of <see cref="BaseMethodDescription"/> objects.</returns>
        private List<BaseMethodDescription> OrderMethodsByExecutionOrder(List<BaseMethodDescription> listToOrder)
        {
            List<BaseMethodDescription> result = new List<BaseMethodDescription>();

            uint maxExecutionOrder = 0;
            foreach (BaseMethodDescription method in listToOrder)
            {
                if (method.ExecutionOrder > maxExecutionOrder)
                {
                    maxExecutionOrder = method.ExecutionOrder;
                }
            }

            BaseMethodDescription[] methodsArray = new BaseMethodDescription[maxExecutionOrder];
            foreach (BaseMethodDescription method in listToOrder)
            {
                if (methodsArray[method.ExecutionOrder - 1] != null)
                {
                    return new List<BaseMethodDescription>();
                }

                methodsArray[method.ExecutionOrder - 1] = method;
            }

            for (int index = 0; index < methodsArray.Length; index++)
            {
                if (methodsArray[index] != null)
                {
                    result.Add(methodsArray[index]);
                }
                else
                {
                    return new List<BaseMethodDescription>();
                }
            }

            return result;
        }
    }
}