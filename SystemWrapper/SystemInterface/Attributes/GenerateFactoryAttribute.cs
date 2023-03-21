namespace SystemInterface.Attributes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     The attribute used to mark classes which will receive an automatically generated factory implementation, based on a specified contract.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [ExcludeFromCodeCoverage]
    public class GenerateFactoryAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenerateFactoryAttribute" /> class.
        /// </summary>
        /// <param name="factoryContractType">
        ///     The type of the factory interface which will serve as the superclass to the generated factory implementation.
        /// </param>
        public GenerateFactoryAttribute(Type factoryContractType)
        {
            this.FactoryContractType = factoryContractType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the type of the factory interface which will serve as the superclass to the generated factory implementation.
        /// </summary>
        public Type FactoryContractType { get; private set; }

        #endregion
    }
}