using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewExample.services;
using InterviewExample.models;

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
                Console.WriteLine(user);
                Assert.IsTrue(postCount > 0, user.ToString());
            }
        }
    }
}
