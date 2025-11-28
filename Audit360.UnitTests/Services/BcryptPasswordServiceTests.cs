using Xunit;
using Audit360.Infrastructure.Services;

namespace Audit360.UnitTests.Services
{
    public class BcryptPasswordServiceTests
    {
        [Fact]
        public void HashPassword_ReturnsNonEmptyHash()
        {
            var svc = new BcryptPasswordService();
            var hash = svc.HashPassword("myP@ssw0rd");
            Assert.False(string.IsNullOrWhiteSpace(hash));
            Assert.NotEqual("myP@ssw0rd", hash);
        }

        [Fact]
        public void Verify_ReturnsTrueForCorrectPassword_AndFalseForWrong()
        {
            var svc = new BcryptPasswordService();
            var password = "anotherP@ss";
            var hash = svc.HashPassword(password);

            var ok = svc.Verify(hash, password);
            Assert.True(ok);

            var nok = svc.Verify(hash, "wrongpass");
            Assert.False(nok);
        }
    }
}
