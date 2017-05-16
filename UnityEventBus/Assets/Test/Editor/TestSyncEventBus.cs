
using NUnit.Framework;
using UnityEventBus.Core;
using UnityEventBus.API;

namespace Testing.UnityEventBus
{

    public class TestSyncEventBus
    {
        EventBus testObject;

        [SetUp]
        public void InitTestObject()
        {
            testObject = new SyncEventBus();
        }

        [Test]
        public void TestPostOneRegisterdClass()
        {
            PostTestClass c = new PostTestClass();
            Assert.AreEqual(c.StartedCounter, 0, "The method counter isn't correct");

            testObject.Register(c);
            testObject.Post("Started");
            Assert.AreEqual(c.StartedCounter, 1, "The method counter isn't correct");
        }

        [Test]
        public void TestPostTwoRegisterdClass()
        {
            PostTestClass c0 = new PostTestClass();
            PostTestClass c1 = new PostTestClass();
            Assert.AreEqual(c0.StartedCounter, 0, "The method counter isn't correct");
            Assert.AreEqual(c1.StartedCounter, 0, "The method counter isn't correct");

            testObject.Register(c0);
            testObject.Register(c1);
            testObject.Post("Started");
            Assert.AreEqual(c0.StartedCounter, 1, "The method counter isn't correct");
            Assert.AreEqual(c1.StartedCounter, 1, "The method counter isn't correct");
        }

        [Test]
        public void TestUnregisterDuringPost()
        {
            PostTestClass c0 = new PostTestClass();
            UnregisterDuringPostTestClass u1 = new UnregisterDuringPostTestClass();
            u1.EventBus = testObject;
            PostTestClass c1 = new PostTestClass();
            Assert.AreEqual(c0.UnregisterEventCounter, 0, "The method counter isn't correct");
            Assert.AreEqual(c1.UnregisterEventCounter, 0, "The method counter isn't correct");

            testObject.Register(c0);
            testObject.Register(u1);
            Assert.IsTrue(testObject.IsRegistered(u1), "The given class isn't registerd");
            testObject.Register(c1);
            testObject.Post("UnregisterEvent");
            Assert.AreEqual(c0.UnregisterEventCounter, 1, "The method counter isn't correct");
            Assert.IsFalse(testObject.IsRegistered(u1), "The given class is registerd");
            Assert.AreEqual(c1.UnregisterEventCounter, 1, "The method counter isn't correct");
        }


        //// A UnityTest behaves like a coroutine in PlayMode
        //// and allows you to yield null to skip a frame in EditMode
        //[UnityTest]
        //public IEnumerator NewEditModeTestWithEnumeratorPasses() {
        //	// Use the Assert class to test conditions.
        //	// yield to skip a frame
        //	yield return null;
        //}
    }
}