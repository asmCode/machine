using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    private Dictionary<int, Pin> mPins = new Dictionary<int, Pin>();
    private Dictionary<int, Connection> mConnections = new Dictionary<int, Connection>();
    private Dictionary<int, Element> mElements = new Dictionary<int, Element>();

    public int AddElement()
    {
        var element = new Element();
        element.Id = Id.GetId();
        mElements.Add(element.Id, element);

        return element.Id;
    }

    public int AddPin(int elementId, PinType pinType)
    {
        var pin = new Pin();
        pin.Id = Id.GetId();
        pin.ElementId = elementId;
        pin.PinType = pinType;

        return pin.Id;
    }
}
