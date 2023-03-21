namespace SystemWrapper.Tests.ActiveDirectory
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable SealedMemberInSealedClass
{
    using SystemInterface.ActiveDirectory;

    using SystemWrapper.ActiveDirectory;

    using NUnit.Framework;

    using Testeroids;

    using Assert = Testeroids.Assert;

    public abstract class DirectorySearcherSpecs
    {
        public abstract class given_instantiated_Sut : ContextSpecification<DirectorySearcherWrap>
        {
            #region Context

            private IDirectoryEntry InjectedDirectoryEntry { get; set; }

            protected override void InstantiateMocks()
            {
                base.InstantiateMocks();
                this.InjectedDirectoryEntry = new DirectoryEntryWrap("GC://liebherr.i");
            }

            protected override void EstablishContext()
            {
                base.EstablishContext();
            }

            protected override DirectorySearcherWrap CreateSubjectUnderTest()
            {
                return new DirectorySearcherWrap(this.InjectedDirectoryEntry, 20, null)
                           {
                               SizeLimit = 20,
                               Filter = string.Format("(&(objectcategory=user)(objectclass=user)(|(displayname={0})(cn={0})))", "Br*")
                           };
            }

            #endregion

            public sealed class when_FindAll_is_called: given_instantiated_Sut
            {
                #region Context

                public ISearchResultCollection Result { get; private set; }

                protected override void Because()
                {
                    this.Result = this.Sut.FindAll();
                }

                public override void BaseTestFixtureTearDown()
                {
                    base.BaseTestFixtureTearDown();

                    this.Result.Dispose();
                }

                #endregion

                [Test]
                public void then_Result_contains_20_results()
                {
                    Assert.AreEqual(20, this.Result.Count);
                }

                [Test]
                public void then_each_result_contains_at_least_a_property()
                {
                    foreach (var result in this.Result)
                    {
                        Assert.IsTrue(result.Properties.Count > 0);   
                    }                    
                }
            }

            public sealed class when_FindOne_is_called : given_instantiated_Sut
            {
                #region Context

                public ISearchResult Result { get; private set; }

                protected override void Because()
                {
                    this.Result = this.Sut.FindOne();
                }

                #endregion
                
                [Test]
                public void then_Result_contains_at_least_a_result()
                {
                    Assert.IsTrue(this.Result.Properties.Count > 0);
                }
            }
        }
    }
}