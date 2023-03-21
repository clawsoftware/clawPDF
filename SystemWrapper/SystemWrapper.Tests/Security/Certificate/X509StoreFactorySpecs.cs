namespace SystemWrapper.Tests.Security.Certificate
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable SealedMemberInSealedClass
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    using SystemWrapper.Security.Certificate;

    using NUnit.Framework;

    using Testeroids;

    public abstract class X509StoreFactorySpecs
    {
        public abstract class given_instantiated_Sut : ContextSpecification<X509StoreFactory>
        {
            #region Context

            protected override X509StoreFactory CreateSubjectUnderTest()
            {
                return new X509StoreFactory();
            }

            #endregion

            public sealed class when_Create_is_called : given_instantiated_Sut
            {
                #region Context

                private IX509Store Result { get; set; }

                private StoreLocation SpecifiedStoreLocation { get; set; }

                protected override void EstablishContext()
                {
                    base.EstablishContext();

                    this.SpecifiedStoreLocation = StoreLocation.LocalMachine;
                }

                protected override sealed void Because()
                {
                    this.Result = this.Sut.Create(this.SpecifiedStoreLocation);
                }

                #endregion

                [Test]
                public void then_Result_is_not_null()
                {
                    NUnit.Framework.Assert.NotNull(this.Result);
                }
            }
        }
    }
}