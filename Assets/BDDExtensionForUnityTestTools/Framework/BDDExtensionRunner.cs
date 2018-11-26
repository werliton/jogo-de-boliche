//-----------------------------------------------------------------------
// <copyright file="BDDExtensionRunner.cs" company="Hud Dimension">
//     Copyright (c) Hud Dimension. All rights reserved.
//     http://www.HudDimension.co.uk
// </copyright>
//
// <summary>
// This class is the core of the BDD Extension Framework for Unity Test Tools.
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
using UnityEngine;

namespace HudDimension.BDDExtensionForUnityTestTools
{
    /// <summary>
    /// This class is the core of the BDD Extension Framework for Unity Test Tools.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class BDDExtensionRunner : MonoBehaviour
    {
        /// <summary>
        /// Define if the test has to be executed inside the FixedUpdate instead of the Update method.
        /// </summary>
        [SerializeField]
        private bool useFixedUpdate = false;

        /// <summary>
        /// The list of the Step Method of type Given.
        /// </summary>
        [SerializeField]
        private string[] given = new string[] { string.Empty };

        /// <summary>
        /// The list of the Step Method of type When.
        /// </summary>
        [SerializeField]
        private string[] when = new string[] { string.Empty };

        /// <summary>
        /// The list of the Step Method of type Then.
        /// </summary>
        [SerializeField]
        private string[] then = new string[] { string.Empty };

        /// <summary>
        /// The list of the Step Method parameters indexes of type Given.
        /// </summary>
        [SerializeField]
        private string[] givenParametersIndex = new string[] { string.Empty };

        /// <summary>
        ///  The list of the Step Method parameters indexes of type When.
        /// </summary>
        [SerializeField]
        private string[] whenParametersIndex = new string[] { string.Empty };

        /// <summary>
        ///  The list of the Step Method parameters indexes of type Then.
        /// </summary>
        [SerializeField]
        private string[] thenParametersIndex = new string[] { string.Empty };

        /// <summary>
        /// Gets or sets the list of the Step Method of type Given.
        /// </summary>
        /// <value>
        /// The list of the Step Method of type Given.
        /// </value>
        public string[] Given
        {
            get
            {
                return this.given;
            }

            set
            {
                this.given = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of the Step Method of type When.
        /// </summary>
        /// <value>
        /// The list of the Step Method of type When.
        /// </value>
        public string[] When
        {
            get
            {
                return this.when;
            }

            set
            {
                this.when = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of the Step Method of type Then.
        /// </summary>
        /// <value>
        /// The list of the Step Method of type Then.
        /// </value>
        public string[] Then
        {
            get
            {
                return this.then;
            }

            set
            {
                this.then = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of the Step Method parameters indexes of type Given.
        /// </summary>
        /// <value>
        /// The list of the Step Method parameters indexes of type Given.
        /// </value>
        public string[] GivenParametersIndex
        {
            get
            {
                return this.givenParametersIndex;
            }

            set
            {
                this.givenParametersIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of the Step Method parameters indexes of type When.
        /// </summary>
        /// <value>
        /// The list of the Step Method parameters indexes of type When.
        /// </value>
        public string[] WhenParametersIndex
        {
            get
            {
                return this.whenParametersIndex;
            }

            set
            {
                this.whenParametersIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of the Step Method parameters indexes of type Then.
        /// </summary>
        /// <value>
        /// The list of the Step Method parameters indexes of type Then.
        /// </value>
        public string[] ThenParametersIndex
        {
            get
            {
                return this.thenParametersIndex;
            }

            set
            {
                this.thenParametersIndex = value;
            }
        }

        /// <summary>
        /// Gets the object containing the business logic.
        /// </summary>
        /// <value>
        /// The business logic object.
        /// </value>
        public ExtensionRunnerBusinessLogic BusinessLogic { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the test has to be executed inside the FixedUpdate instead of the Update method.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has to use the FixedUpdate; otherwise, <c>false</c>.
        /// </value>
        public bool UseFixedUpdate
        {
            get
            {
                return this.useFixedUpdate;
            }

            set
            {
                this.useFixedUpdate = value;
            }
        }

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            this.BusinessLogic = new ExtensionRunnerBusinessLogic(gameObject);
            this.BusinessLogic.AreThereErrors = this.BusinessLogic.CheckForErrors(gameObject.GetComponents<Component>(), this.Given, this.GivenParametersIndex, this.When, this.WhenParametersIndex, this.Then, this.ThenParametersIndex);
            if (!this.BusinessLogic.AreThereErrors)
            {
                this.BusinessLogic.SetSucceedOnAssertions();
                this.BusinessLogic.MethodsDescription = this.BusinessLogic.GetAllMethodsDescriptions(gameObject.GetComponents<Component>(), this.Given, this.GivenParametersIndex, this.When, this.WhenParametersIndex, this.Then, this.ThenParametersIndex);
            }
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
        {
            if (!this.BusinessLogic.AreThereErrors && !this.useFixedUpdate)
            {
                this.BusinessLogic.IndexToRun = this.BusinessLogic.RunCycle(this.BusinessLogic, this.BusinessLogic.MethodsDescription, this.BusinessLogic.IndexToRun);
            }
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        private void FixedUpdate()
        {
            if (!this.BusinessLogic.AreThereErrors && this.useFixedUpdate)
            {
                this.BusinessLogic.IndexToRun = this.BusinessLogic.RunCycle(this.BusinessLogic, this.BusinessLogic.MethodsDescription, this.BusinessLogic.IndexToRun);
            }
        }
    }
}