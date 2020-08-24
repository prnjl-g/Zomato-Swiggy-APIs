using NUnit.Framework;
using Service.Database;
using Moq;
using Microsoft.EntityFrameworkCore;
namespace graphql_create.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mockSet = new Mock<DbSet<Comment >>();
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(m => m.CommentsOnIssues).Returns(mockSet.Object);

            CommentRepository obj = new CommentRepository(mockContext.Object);
            var result = obj.DeleteComment(90, "xyz");
            Assert.AreEqual("Invalid Comment Id", result);
        }
    }
}