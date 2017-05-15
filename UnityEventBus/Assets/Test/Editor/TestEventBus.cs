
using NUnit.Framework;
using UnityEventBus.Core;
using UnityEventBus.API;

namespace Testing.UnityEventBus
{

    public class TestEventBus
    {
        EventBus testObject;

        [SetUp]
        public void InitTestObject()
        {
            testObject = new SyncEventBus();
        }

        [Test]
        public void TestRegister()
        {
            // Use the Assert class to test conditions.
            SubscribeClass sc = new SubscribeClass();
            testObject.Register(sc);
            Assert.IsTrue(testObject.IsRegistered(sc), "The given class isn't registerd");
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