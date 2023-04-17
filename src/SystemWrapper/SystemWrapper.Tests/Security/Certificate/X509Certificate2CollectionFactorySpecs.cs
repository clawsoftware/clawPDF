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

    public abstract class X509Certificate2CollectionFactorySpecs
    {
        public abstract class given_instantiated_Sut : ContextSpecification<X509Certificate2CollectionFactory>
        {
            #region Context

            protected override X509Certificate2CollectionFactory CreateSubjectUnderTest()
            {
                return new X509Certificate2CollectionFactory();
            }

            #endregion

            public sealed class when_Create_is_called_without_parameter : given_instantiated_Sut
            {
                #region Context

                private IX509Certificate2Collection Result { get; set; }

                protected override void EstablishContext()
                {
                    base.EstablishContext();
                }

                protected override sealed void Because()
                {
                    this.Result = this.Sut.Create();
                }

                #endregion

                [Test]
                public void then_Result_is_not_null()
                {
                    Testeroids.Assert.NotNull(this.Result);
                }
            }

            public sealed class when_Create_is_called_with_collection_parameter : given_instantiated_Sut
            {
                #region Context

                private IX509Certificate2Collection Result { get; set; }

                private X509Certificate2Collection SpecifiedCollection { get; set; }

                protected override void EstablishContext()
                {
                    base.EstablishContext();

                    this.SpecifiedCollection = new X509Certificate2Collection(new X509Certificate2());
                }

                protected override sealed void Because()
                {
                    this.Result = this.Sut.Create(this.SpecifiedCollection);
                }

                #endregion

                [Test]
                public void then_Result_is_not_null()
                {
                    Testeroids.Assert.NotNull(this.Result);
                }
            }
        }
    }
}