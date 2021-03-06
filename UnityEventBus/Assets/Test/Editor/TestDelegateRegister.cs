﻿using NUnit.Framework;
using Assets.UnityEventBus.Core;
using System.IO;

namespace Testing.UnityEventBus
{
    public class TestDelegateRegister
    {
        DelegateRegister testObject;

        [SetUp]
        public void InitTestObject()
        {
            testObject = new DelegateRegister();
        }

        [Test]
        public void TestRegister()
        {
            DelegateRegisterTestClass sc = new DelegateRegisterTestClass();
            testObject.Register(sc);
            Assert.IsTrue(testObject.IsRegistered(sc), "The given class isn't registerd");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Started"), "The given class isn't registerd for event Started");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class isn't registerd for event Ended");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class isn't registerd for event GetHit");
        }

        [Test]
        public void TestRegisterForEvent()
        {
            DelegateRegisterTestClass sc = new DelegateRegisterTestClass();
            testObject.RegisterForEvent(sc, "Started");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Started"), "The given class isn't registerd for event Started");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class is registerd for event Ended");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class is registerd for event GetHit");

            testObject.RegisterForEvent(sc, "Ended");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Started"), "The given class isn't registerd for event Started");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class isn't registerd for event Ended");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class is registerd for event GetHit");

            testObject.RegisterForEvent(sc, "GetHit");
            Assert.IsTrue(testObject.IsRegistered(sc), "The given class isn't registerd");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class isn't registerd for event Ended");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class isn't registerd for event GetHit");
        }

        [Test]
        public void TestUnregister()
        {
            DelegateRegisterTestClass sc = new DelegateRegisterTestClass();
            testObject.Register(sc);
            testObject.Unregister(sc);
            Assert.IsFalse(testObject.IsRegistered(sc), "The given class is registerd");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Started"), "The given class is registerd for event Started");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class is registerd for event Ended");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class is registerd for event GetHit");
        }

        [Test]
        public void TestUnregisterForEvent()
        {
            DelegateRegisterTestClass sc = new DelegateRegisterTestClass();
            testObject.Register(sc);

            testObject.UnregisterForEvent(sc, "Started");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Started"), "The given class is registerd for event Started");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class isn't registerd for event Ended");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class isn't registerd for event GetHit");

            testObject.UnregisterForEvent(sc, "Ended");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Started"), "The given class is registerd for event Started");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class is registerd for event Ended");
            Assert.IsTrue(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class isn't registerd for event GetHit");

            testObject.UnregisterForEvent(sc, "GetHit");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Started"), "The given class is registerd for event Started");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "Ended"), "The given class is registerd for event Ended");
            Assert.IsFalse(testObject.IsRegisteredForEvent(sc, "GetHit"), "The given class is registerd for event GetHit");
        }

        [Test]
        public void TestClassWithoutSubscribeAttribute()
        {
            object o = new object();
            testObject.Register(o);
            Assert.IsFalse(testObject.IsRegistered(o), "The given class is registerd.");
        }
    }
}