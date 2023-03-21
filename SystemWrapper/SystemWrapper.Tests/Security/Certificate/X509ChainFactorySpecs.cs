namespace SystemWrapper.Tests.Security.Certificate
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable SealedMemberInSealedClass
{
    using SystemInterface.Security.Certificate;

    using SystemWrapper.Security.Certificate;

    using NUnit.Framework;

    using Testeroids;

    public abstract class X509ChainFactorySpecs
    {
        public abstract class given_instantiated_Sut : ContextSpecification<X509ChainFactory>
        {
            #region Context

            protected override X509ChainFactory CreateSubjectUnderTest()
            {
                return new X509ChainFactory();
            }

            #endregion

            public sealed class when_Create_is_called : given_instantiated_Sut
            {
                #region Context

                private IX509Chain Result { get; set; }

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
                    NUnit.Framework.Assert.NotNull(this.Result);
                }
            }
        }
    }
}