﻿using System;
using System.Web.Mvc;
using CMS.Areas.Admin.Controllers;
using CMS.Data;
using CMS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Expectations.Abstraction;

namespace CMS.Tests.Admin.Controllers
{
    [TestClass]
    public class PostControllerTests
    {
        [TestMethod]
        public void Edit_GetRequestSendsPostToView()
        {
            var id = "test-post";
            var repo = Mock.Create <IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns(new Post {Id = id});
            var result = (ViewResult) controller.Edit(id);
            var model = (Post) result.Model;

            Assert.AreEqual(id, model.Id);
        }

        [TestMethod]
        public void Edit_GetRequestNotFoundResult()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns((Post)null);
            var result = controller.Edit(id);

            Assert.IsTrue(result is HttpNotFoundResult);
        }

        [TestMethod]
        public void Edit_PostRequestNotFoundResult()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns((Post)null);
            var result = controller.Edit(id, new Post());

            Assert.IsTrue(result is HttpNotFoundResult);
        }

        [TestMethod]
        public void Edit_PostRequestSendsPostToView()
        {
            var id = "test-post";
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Get(id)).Returns(new Post { Id = id });
            
            controller.ViewData.ModelState.AddModelError("key", "error");

            var result = (ViewResult)controller.Edit(id, new Post(){Id = "test-post-2"});
            var model = (Post)result.Model;

            Assert.AreEqual("test-post-2", model.Id);
        }

        [TestMethod]
        public void Edit_PostRequestCallsEditAndRediretcs()
        {
            var id = "test-post";  
            var repo = Mock.Create<IPostRepository>();
            var controller = new PostController(repo);

            Mock.Arrange(() => repo.Edit(Arg.IsAny<string>(),Arg.IsAny<Post>())).MustBeCalled();
            var result = controller.Edit("foo", new Post() { Id = "test-post-2" });

            Mock.Assert(repo);

            Assert.IsTrue(result is RedirectToRouteResult);
        }
    }
}
