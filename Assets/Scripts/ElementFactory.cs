using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFactory
{
    private static Dictionary<int, System.Func<Element>> mCreators = new Dictionary<int, System.Func<Element>>();

    public static void RegisterElementCreator(int type, System.Func<Element> creator)
    {
        mCreators.Add(type, creator);
    }

    public static Element Create(int type, int x, int y)
    {
        var creator = mCreators[type];
        return creator();
    }
}
