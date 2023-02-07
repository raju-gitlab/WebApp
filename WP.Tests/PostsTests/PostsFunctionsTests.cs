using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.API.Controllers;
using WP.Model.Models;

namespace WP.Tests.PostsTests
{
    [TestClass]
    public class PostsFunctionsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            PostsController controller = new PostsController();
            object result = controller.AddPost(new PostsViewModel { });
            //Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.ToString()));
        }
    }
}
