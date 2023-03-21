namespace SystemWrapper.Tests.ActiveDirectory
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable SealedMemberInSealedClass
{
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory;
    using SystemWrapper.ActiveDirectory.Contracts;

    using NUnit.Framework;

    using Testeroids;

    using Assert = Testeroids.Assert;

    public abstract class PrincipalContextFactorySpecs
    {
        public sealed class after_instantiating_Sut : SubjectInstantiationContextSpecification<PrincipalContextFactory>
        {
            #region Context

            protected override sealed PrincipalContextFactory BecauseSutIsCreated()
            {
                return new PrincipalContextFactory();
            }

            #endregion

            [Test]
            public void then_Sut_is_instance_of_IPrincipalContextFactory()
            {
                Assert.IsInstanceOf<IPrincipalContextFactory>(this.Sut);
            }
        }

        public abstract class given_instantiated_Sut : ContextSpecification<PrincipalContextFactory>
        {
            #region Context

            protected override PrincipalContextFactory CreateSubjectUnderTest()
            {
                return new PrincipalContextFactory();
            }

            #endregion

            [Category("Integration tests")]
            [Ignore("Manual tests")]
            public sealed class when_PrincipalContextFactory_is_used : given_instantiated_Sut
            {
                #region Context

                protected override sealed void Because()
                {
                }

                #endregion

                [Test]
                public void then_Result_matches_ExpectedResult()
                {
                    try
                    {
                        var pcf = this.Sut;
                        var gpf = new GroupPrincipalFactory();

                        using (var pc = pcf.Create(ContextType.Domain, "lmbsvdc01.lmb.liebherr.i"))
                        {
                            var group = gpf.Create(pc, "LI-Lidia-Release");
                            group.Members.Add(pc, IdentityType.SamAccountName, "lgbbua0");
                            group.Save();
                        }
                    }
                    catch (System.DirectoryServices.DirectoryServicesCOMException)
                    {
                        //doSomething with E.Message.ToString(); 
                    }
                }
            }
        }
    }
}