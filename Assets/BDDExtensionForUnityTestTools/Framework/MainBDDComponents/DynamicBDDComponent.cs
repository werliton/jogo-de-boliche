//-----------------------------------------------------------------------
// <copyright file="DynamicBDDComponent.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This is the base class for making a class a Dynamic Component.
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
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[module: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This is the base class for making a class a Dynamic Component.
    /// </summary>
    /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.BaseBDDComponent" />
    public class DynamicBDDComponent : BaseBDDComponent
    {
        /// <summary>
        /// The Parameter Value Storage for the type "bool".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected bool[] boolPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "byte".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected byte[] bytePVS;

        /// <summary>
        /// The Parameter Value Storage for the type "sbyte".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected sbyte[] sbytePVS;

        /// <summary>
        /// The Parameter Value Storage for the type "char".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected char[] charPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "decimal".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected decimal[] decimalPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "double".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected double[] doublePVS;

        /// <summary>
        /// The Parameter Value Storage for the type "float".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected float[] floatPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "int".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected int[] intPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "uint".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected uint[] uintPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "long".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected long[] longPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "ulong".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected ulong[] ulongPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "short".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected short[] shortPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "ushort".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected ushort[] ushortPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "string".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected string[] stringPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Vector2".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Vector2[] vector2PVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Vector3".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Vector3[] vector3PVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Vector4".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Vector4[] vector4PVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Rect".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Rect[] rectPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Quaternion".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Quaternion[] quaternionPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Matrix4x4".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Matrix4x4[] matrix4x4PVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Color".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Color[] colorPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Color32".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Color32[] color32PVS;

        /// <summary>
        /// The Parameter Value Storage for the type "LayerMask".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected LayerMask[] layerMaskPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "AnimationCurve".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected AnimationCurve[] animationCurvePVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Gradient".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Gradient[] gradientPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "RectOffset".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected RectOffset[] rectOffsetPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "GUIStyle".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected GUIStyle[] guiStylePVS;

        /// <summary>
        /// The Parameter Value Storage for the type "GameObject".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected GameObject[] gameObjectPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Transform".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Transform[] transformPVS;

        /// <summary>
        /// The Parameter Value Storage for the type "Component".
        /// </summary>
        [HideInInspector]
        [ParametersValuesStorage]
        [SerializeField]
        protected Component[] componentPVS;

        /// <summary>
        /// This is the Given attribute for a Dynamic Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.GivenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class Given : GivenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Given"/> class.
            /// </summary>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public Given(string text) : base(text)
            {
            }
        }

        /// <summary>
        /// This is the When attribute for a Dynamic Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.WhenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class When : WhenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="When"/> class.
            /// </summary>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public When(string text) : base(text)
            {
            }
        }

        /// <summary>
        /// This is the Then attribute for a Dynamic Component.
        /// </summary>
        /// <seealso cref="HudDimension.BDDExtensionForUnityTestTools.ThenBaseAttribute" />
        [AttributeUsage(System.AttributeTargets.Method)]
        public class Then : ThenBaseAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Then"/> class.
            /// </summary>
            /// <param name="text">The sentence of the scenario represented by the Step Method.</param>
            public Then(string text) : base(text)
            {
            }
        }
    }
}
