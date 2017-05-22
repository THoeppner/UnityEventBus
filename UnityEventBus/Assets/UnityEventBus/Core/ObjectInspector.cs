using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEventBus.Attributes;

namespace Assets.UnityEventBus.Core
{
    public class ObjectInspectionHelper
    {
        public SubscribeAttribute subscribeAttribute;
        public MethodInfo methodInfo;
    }

    public static class ObjectInspector
    {
        public static Dictionary<string, ObjectInspectionHelper> GetMethodInfosWithValidSubscribeAttribute(object listener)
        {
            Dictionary<string, ObjectInspectionHelper> infos = new Dictionary<string, ObjectInspectionHelper>();

            // Find all methods which have the attribute Subscribe
            MethodInfo[] methods = listener.GetType().GetMethods()
                                    .Where(m => m.GetCustomAttributes(typeof(SubscribeAttribute), false).Length > 0)
                                    .ToArray();

            foreach (MethodInfo mi in methods)
            {
                // We only support one SubscribeAttribute per method
                SubscribeAttribute[] attrs = (SubscribeAttribute[])mi.GetCustomAttributes(typeof(SubscribeAttribute), false);
                if (string.IsNullOrEmpty(attrs[0].EventName))
                    continue;
                infos[attrs[0].EventName] = new ObjectInspectionHelper() { subscribeAttribute = attrs[0], methodInfo = mi };
            }
            return infos;
        }

        public static ObjectInspectionHelper GetMethodInfoForSubsribedEventEvent(object listener, string eventName)
        {
            ObjectInspectionHelper helper;
            Dictionary<string, ObjectInspectionHelper> infos = GetMethodInfosWithValidSubscribeAttribute(listener);
            infos.TryGetValue(eventName, out helper);
            return helper;
        }
    }
}