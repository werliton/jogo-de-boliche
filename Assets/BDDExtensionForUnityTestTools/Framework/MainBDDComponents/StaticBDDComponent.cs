//-----------------------------------------------------------------------
// <copyright file="StaticBDDComponent.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the base class for making a class a Static Component.
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

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the base class for making a class a Static Component.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BaseBDDComponent" />
    public class StaticBDDComponent : BaseBDDComponent
    {
        /// <summary>
        /// This is the Given attribute for a Static Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.GivenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class Given : GivenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Given"/> class.
            /// </summary>
            /// <param name="executionOrder">The execution order.</param>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public Given(uint executionOrder, string text) : base(text)
            {
                this.ExecutionOrder = executionOrder;
            }

            /// <summary>
            /// Gets or sets the execution order.
            /// </summary>
            /// <value>
            /// The execution order.
            /// </value>
            public uint ExecutionOrder { get; set; }

            /// <summary>
            /// Gets the execution order.
            /// </summary>
            /// <returns>
            /// The execution order.
            /// </returns>
            public override uint GetExecutionOrder()
            {
                return this.ExecutionOrder;
            }
        }

        /// <summary>
        /// This is the When attribute for a Static Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.WhenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class When : WhenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="When"/> class.
            /// </summary>
            /// <param name="executionOrder">The execution order.</param>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public When(uint executionOrder, string text) : base(text)
            {
                this.ExecutionOrder = executionOrder;
            }

            /// <summary>
            /// Gets or sets the execution order.
            /// </summary>
            /// <value>
            /// The execution order.
            /// </value>
            public uint ExecutionOrder { get; set; }

            /// <summary>
            /// Gets the execution order.
            /// </summary>
            /// <returns>
            /// The execution order.
            /// </returns>
            public override uint GetExecutionOrder()
            {
                return this.ExecutionOrder;
            }
        }

        /// <summary>
        /// This is the Then attribute for a Static Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.ThenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class Then : ThenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Then"/> class.
            /// </summary>
            /// <param name="executionOrder">The execution order.</param>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public Then(uint executionOrder, string text) : base(text)
            {
                this.ExecutionOrder = executionOrder;
            }

            /// <summary>
            /// Gets or sets the execution order.
            /// </summary>
            /// <value>
            /// The execution order.
            /// </value>
            public uint ExecutionOrder { get; set; }

            /// <summary>
            /// Gets the execution order.
            /// </summary>
            /// <returns>
            /// The execution order.
            /// </returns>
            public override uint GetExecutionOrder()
            {
                return this.ExecutionOrder;
            }
        }
    }
}
