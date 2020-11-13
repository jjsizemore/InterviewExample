using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewExample.services;
using InterviewExample.models;
using FluentAssertions;

namespace InterviewExample
{
    [TestClass]
    public class BaseTest
    {
        private static int LOOKUP_SAMPLES = 150;
        private static int USER_COUNT = 100;
        private static int POST_PER_USER_COUNT = 5;

        private PostService postService = new PostService();
        private UserService userService = new UserService();

        private Random r = new Random();

        [TestInitialize]
        public void Setup()
        {
            userService.Setup(USER_COUNT);
            postService.Setup(USER_COUNT, POST_PER_USER_COUNT);
        }

        [TestMethod]
        public void TestGet()
        {
            for (int i = 0; i < LOOKUP_SAMPLES; i++)
            {
                Post lookup = postService.Get("" + r.Next(USER_COUNT * POST_PER_USER_COUNT));
                Assert.IsTrue(lookup != null);
                User u = userService.Get("" + lookup.userId);
                Assert.IsTrue(u != null);
            }
        }

        [TestMethod]
        public void TestCount()
        {
            for (int i = 0; i < LOOKUP_SAMPLES; i++)
            {
                int user = r.Next(USER_COUNT);
                int postCount = postService.getPostCount(user.ToString());
                Console.WriteLine($"User {user}: {postCount} posts");
                // Assert.IsTrue(postCount > 0, user.ToString());
                postCount.Should().BeGreaterThan(0, $"User {user} is expected to have at least 1 post");
            }
        }
    }
}
