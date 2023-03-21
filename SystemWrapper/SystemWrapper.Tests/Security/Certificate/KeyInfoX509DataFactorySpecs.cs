namespace SystemWrapper.Tests.Security.Certificate
// ReSharper disable InconsistentNaming
// ReSharper disable AccessToStaticMemberViaDerivedType
// ReSharper disable SealedMemberInSealedClass
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography.Xml;

    using SystemInterface.Security.Certificate;

    using SystemWrapper.Security.Certificate;

    using Moq;

    using NUnit.Framework;

    using Testeroids;
    using Testeroids.Mocking;

    public abstract class KeyInfoX509DataFactorySpecs
    {
        public abstract class given_instantiated_Sut : ContextSpecification<KeyInfoX509DataFactory>
        {
            #region Context

            protected override KeyInfoX509DataFactory CreateSubjectUnderTest()
            {
                return new KeyInfoX509DataFactory();
            }

            #endregion

            public sealed class when_Create_is_called : given_instantiated_Sut
            {
                #region Context

                private string ExpectedSubject { get; set; }

                private KeyInfoX509Data Result { get; set; }

                private X509Certificate2 ReturnedGetCertificate { get; set; }

                private ITesteroidsMock<IX509Certificate> SpecifiedCertificateMock { get; set; }

                protected override void InstantiateMocks()
                {
                    base.InstantiateMocks();

                    this.SpecifiedCertificateMock = this.MockRepository.CreateMock<IX509Certificate>();
                }

                protected override void EstablishContext()
                {
                    base.EstablishContext();

                    var rawData = new byte[] { 48, 130, 3, 242, 48, 130, 2, 222, 160, 3, 2, 1, 2, 2, 1, 13, 48, 9, 6, 5, 43, 14, 3, 2, 29, 5, 0, 48, 76, 49, 11, 48, 9, 6, 3, 85, 4, 6, 19, 2, 68, 69, 49, 17, 48, 15, 6, 3, 85, 4, 10, 19, 8, 76, 105, 101, 98, 104, 101, 114, 114, 49, 17, 48, 15, 6, 3, 85, 4, 11, 19, 8, 90, 69, 68, 86, 45, 79, 82, 71, 49, 23, 48, 21, 6, 3, 85, 4, 3, 19, 14, 76, 105, 101, 98, 104, 101, 114, 114, 82, 111, 111, 116, 67, 65, 48, 30, 23, 13, 49, 49, 48, 54, 48, 57, 50, 51, 48, 48, 48, 48, 90, 23, 13, 49, 54, 48, 54, 48, 55, 50, 51, 48, 48, 48, 48, 90, 48, 129, 157, 49, 17, 48, 15, 6, 10, 9, 146, 38, 137, 147, 242, 44, 100, 1, 25, 22, 1, 105, 49, 24, 48, 22, 6, 10, 9, 146, 38, 137, 147, 242, 44, 100, 1, 25, 22, 8, 108, 105, 101, 98, 104, 101, 114, 114, 49, 19, 48, 17, 6, 10, 9, 146, 38, 137, 147, 242, 44, 100, 1, 25, 22, 3, 108, 109, 98, 49, 12, 48, 10, 6, 3, 85, 4, 11, 19, 3, 76, 77, 66, 49, 11, 48, 9, 6, 3, 85, 4, 11, 19, 2, 77, 68, 49, 12, 48, 10, 6, 3, 85, 4, 11, 19, 3, 77, 68, 52, 49, 19, 48, 17, 6, 3, 85, 4, 11, 19, 10, 68, 101, 118, 101, 108, 111, 112, 101, 114, 115, 49, 27, 48, 25, 6, 3, 85, 4, 3, 19, 18, 84, 117, 114, 104, 97, 108, 32, 72, 97, 115, 97, 110, 32, 40, 76, 77, 66, 41, 48, 130, 1, 34, 48, 13, 6, 9, 42, 134, 72, 134, 247, 13, 1, 1, 1, 5, 0, 3, 130, 1, 15, 0, 48, 130, 1, 10, 2, 130, 1, 1, 0, 200, 13, 23, 165, 138, 234, 204, 241, 109, 213, 46, 126, 93, 107, 132, 146, 240, 51, 30, 178, 44, 80, 7, 180, 182, 106, 73, 110, 104, 147, 14, 241, 190, 151, 53, 80, 106, 78, 146, 228, 44, 166, 33, 236, 126, 11, 64, 27, 125, 192, 102, 125, 138, 196, 160, 255, 1, 174, 254, 190, 47, 135, 123, 132, 77, 213, 159, 183, 91, 206, 249, 60, 49, 177, 83, 218, 136, 118, 45, 238, 111, 138, 137, 202, 231, 151, 236, 243, 70, 166, 33, 199, 189, 154, 122, 120, 117, 53, 217, 201, 193, 84, 91, 26, 34, 62, 141, 61, 57, 21, 172, 250, 113, 128, 52, 148, 161, 172, 115, 177, 84, 205, 26, 139, 210, 242, 36, 19, 193, 184, 246, 179, 73, 199, 16, 235, 2, 86, 198, 236, 250, 93, 128, 99, 116, 149, 173, 64, 72, 20, 11, 46, 207, 239, 224, 29, 248, 210, 96, 204, 140, 116, 84, 1, 143, 192, 211, 255, 52, 174, 163, 178, 217, 64, 68, 90, 130, 8, 179, 216, 44, 111, 60, 33, 173, 196, 104, 57, 190, 78, 87, 243, 248, 30, 250, 248, 142, 223, 172, 65, 43, 33, 8, 120, 69, 48, 84, 224, 88, 93, 59, 229, 133, 40, 226, 199, 148, 175, 71, 161, 129, 36, 199, 245, 91, 56, 203, 198, 211, 197, 210, 3, 30, 224, 32, 67, 13, 16, 209, 30, 61, 226, 187, 66, 146, 180, 191, 78, 48, 202, 154, 112, 149, 236, 227, 29, 2, 3, 1, 0, 1, 163, 129, 148, 48, 129, 145, 48, 31, 6, 3, 85, 29, 37, 4, 24, 48, 22, 6, 10, 43, 6, 1, 4, 1, 130, 55, 10, 3, 12, 6, 8, 43, 6, 1, 5, 5, 7, 3, 2, 48, 110, 6, 3, 85, 29, 1, 4, 103, 48, 101, 128, 16, 123, 194, 115, 9, 48, 51, 13, 162, 36, 250, 205, 128, 106, 118, 52, 17, 161, 78, 48, 76, 49, 11, 48, 9, 6, 3, 85, 4, 6, 19, 2, 68, 69, 49, 17, 48, 15, 6, 3, 85, 4, 10, 19, 8, 76, 105, 101, 98, 104, 101, 114, 114, 49, 17, 48, 15, 6, 3, 85, 4, 11, 19, 8, 90, 69, 68, 86, 45, 79, 82, 71, 49, 23, 48, 21, 6, 3, 85, 4, 3, 19, 14, 76, 105, 101, 98, 104, 101, 114, 114, 82, 111, 111, 116, 67, 65, 130, 1, 5, 48, 9, 6, 5, 43, 14, 3, 2, 29, 5, 0, 3, 130, 1, 1, 0, 165, 132, 73, 202, 63, 104, 101, 39, 172, 118, 22, 208, 110, 179, 147, 26, 200, 215, 59, 182, 200, 152, 102, 175, 105, 196, 183, 136, 103, 191, 83, 223, 197, 131, 137, 48, 215, 213, 31, 27, 254, 215, 137, 48, 135, 20, 92, 246, 198, 104, 161, 4, 71, 5, 78, 203, 161, 116, 247, 217, 11, 119, 22, 169, 232, 238, 15, 254, 122, 17, 10, 161, 246, 223, 116, 152, 81, 10, 6, 190, 162, 63, 41, 240, 5, 184, 135, 37, 32, 141, 116, 249, 134, 113, 192, 151, 9, 245, 169, 15, 89, 210, 70, 235, 185, 180, 200, 146, 122, 94, 158, 202, 189, 29, 45, 198, 65, 173, 99, 112, 80, 155, 210, 46, 234, 175, 178, 212, 234, 119, 126, 83, 113, 57, 109, 155, 206, 127, 229, 119, 235, 224, 114, 51, 64, 38, 45, 194, 77, 193, 240, 81, 88, 80, 68, 175, 165, 71, 34, 91, 189, 242, 16, 104, 108, 13, 104, 8, 104, 201, 189, 235, 195, 106, 165, 58, 102, 185, 75, 34, 198, 112, 233, 129, 18, 119, 87, 72, 148, 137, 137, 137, 240, 157, 200, 6, 54, 167, 19, 224, 127, 254, 230, 220, 242, 242, 57, 87, 9, 204, 115, 32, 171, 90, 136, 144, 30, 56, 196, 27, 46, 194, 68, 22, 10, 216, 139, 10, 139, 12, 7, 204, 34, 240, 144, 23, 47, 32, 210, 27, 254, 123, 179, 2, 233, 230, 74, 103, 141, 11, 227, 4, 25, 249, 241, 129 };

                    this.ReturnedGetCertificate = new X509Certificate2(rawData);

                    this.SpecifiedCertificateMock
                        .Setup(o => o.GetCertificate())
                        .Returns(this.ReturnedGetCertificate)
                        .EnforceUsage();

                    this.ExpectedSubject = "CN=Turhal Hasan (LMB), OU=Developers, OU=MD4, OU=MD, OU=LMB, DC=lmb, DC=liebherr, DC=i";
                }

                protected override sealed void Because()
                {
                    this.Result = this.Sut.Create(this.SpecifiedCertificateMock.Object);
                }

                #endregion

                [Test]
                public void then_GetCertificate_is_called_once_on_SpecifiedCertificateMock()
                {
                    this.SpecifiedCertificateMock.Verify(o => o.GetCertificate(), Times.Once());
                }

                [Test]
                public void then_Result_is_not_null()
                {
                    Testeroids.Assert.IsNotNull(this.Result);
                }

                [Test]
                public void then_Result_is_constructed_from_passed_certificate()
                {
                    Testeroids.Assert.IsTrue(this.Result.Certificates.Cast<X509Certificate>().Any(cer => cer.Subject == this.ExpectedSubject));
                }
            }
        }
    }
}