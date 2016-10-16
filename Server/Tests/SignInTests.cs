using System.Collections.Generic;
using System.Linq;
using Andead.Chat.Resources.Logging;
using Andead.Chat.Resources.Resources.Strings;
using Andead.Chat.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChatServiceTests
{
    [TestClass]
    public class SignInTests
    {
        [TestMethod]
        public void SignIn_WithEmptyName_ReturnsFailure()
        {
            IChatService service = CreateService();

            IEnumerable<SignInRequest> requests = new[]
            {
                new SignInRequest(),
                new SignInRequest {Name = null},
                new SignInRequest {Name = string.Empty}
            }.Concat(Enumerable.Range(1, 10)
                .Select(i => new SignInRequest
                {
                    Name = Enumerable.Repeat(" ", i).Aggregate((s, c) => s + c)
                }));

            foreach (SignInRequest request in requests)
            {
                SignInResponse response = service.SignIn(request);

                Assert.IsNotNull(response);

                Assert.IsFalse(response.Success);
                Assert.AreEqual(response.Message, Errors.EmptyNameNotAllowed);
            }
        }

        private static IChatService CreateService()
        {
            var clientProvider = new Mock<IChatClientProvider>();
            clientProvider.Setup(p => p.GetCurrent()).Returns(Mock.Of<IChatClient>());

            IChatService service = new ChatService(clientProvider.Object, Mock.Of<ILogger>());
            return service;
        }
    }
}