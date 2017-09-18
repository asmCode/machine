using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    public System.Action<int> ElementAdded;
    public System.Action<int, int> ConnectionCreated;
    public System.Action<Element> ElementLocationChanged;

    private Dictionary<int, InputPin> mInputPins = new Dictionary<int, InputPin>();
    private Dictionary<int, OutputPin> mOutputPins = new Dictionary<int, OutputPin>();
    private List<Connection> mConnections = new List<Connection>();
    private List<Element> mElements = new List<Element>();

    public int ElementCount
    {
        get { return mElements.Count; }
    }

    public Element GetElement(int index)
    {
        if (index >= mElements.Count)
            return null;

        return mElements[index];
    }

    public int AddElement(int type, Point3 position)
    {
        var element = ElementFactory.Create(type, 0, 0);
        element.Position = position;
        mElements.Add(element);

        foreach (var pinId in element.InputPinIds)
            AddInputPin(pinId, element.Id);

        foreach (var pinId in element.OutputPinIds)
            AddOutputPin(pinId, element.Id);

        if (ElementAdded != null)
            ElementAdded(element.Id);

        return element.Id;
    }

    public ElementType GetElementType(int elementId)
    {
        var element = GetElementById(elementId);
        if (element == null)
            return ElementType.None;

        return element.ElementType;
    }

    public int AddInputPin(int pinId, int elementId)
    {
        var pin = new InputPin();
        pin.Id = pinId;
        pin.ElementId = elementId;

        mInputPins.Add(pinId, pin);

        return pin.Id;
    }

    public int AddOutputPin(int pinId, int elementId)
    {
        var pin = new OutputPin();
        pin.Id = pinId;
        pin.ElementId = elementId;
        pin.Resolved = false;
        pin.SignalValue = 0.0f;

        mOutputPins.Add(pinId, pin);

        return pin.Id;
    }

    public void SetConnection(int pinInId, int pinOutId)
    {
        var connection = new Connection();
        connection.PinInId = pinInId;
        connection.PinOutId = pinOutId;

        mConnections.Add(connection);

        if (ConnectionCreated != null)
            ConnectionCreated(pinInId, pinOutId);
    }

    public void RequestMovement(Point3 elementLocation, Point3 offset)
    {
        var element = GetElementAtLocation(elementLocation);
        if (element == null)
            return;

        SetElementLocation(element, element.Position + offset);
    }

    public void SetElementLocation(Element element, Point3 elementLocation)
    {
        element.Position = elementLocation;

        if (ElementLocationChanged != null)
            ElementLocationChanged(element);
    }

    public Element GetElementAtLocation(Point3 elementLocation)
    {
        var element = mElements.Find((e) =>
        {
            return e.Position == elementLocation;
        });

        return element;
    }

    public void ResolveAll()
    {
        ClearResolvedFlag();

        //foreach (var outputPin in mOutputPins)
        //{
        //    if (IsResolved(outputPin.Key))
        //        continue;

        //    Resolve(outputPin.Key);
        //}

        foreach (var element in mElements)
        {
            element.Resolve(this);
        }
    }

    public void Dump()
    {
        foreach (var element in mElements)
        {
            element.Dump(this);
        }
    }

    public void SetSignalValue(int outputPinId, float signalValue)
    {
        var outputPin = GetOutputPin(outputPinId);
        if (outputPin == null)
            return;

        outputPin.SignalValue = signalValue;
        outputPin.Resolved = true;
    }

    public float GetSignalValue(int pinId)
    {
        var outputPin = GetOutputPin(pinId);
        if (outputPin != null)
            return outputPin.SignalValue;

        var connection = GetConnectionByPinId(pinId);
        if (connection == null)
            return 0.0f;

        return GetSignalValue(connection.PinOutId);
    }

    public bool IsResolved(int pinId)
    {
        var outputPin = GetOutputPin(pinId);
        if (outputPin != null)
            return outputPin.Resolved;

        var connection = GetConnectionByPinId(pinId);
        if (connection == null)
            return true;

        return IsResolved(connection.PinOutId);
    }

    public void Resolve(int pinId)
    {
        Element element = null;

        if (IsOutputPin(pinId))
            element = GetElementByPinId(pinId);
        else
        {
            var connection = GetConnectionByPinId(pinId);
            element = GetElementByPinId(connection.PinOutId);
        }

        element.Resolve(this);
    }

    public void ClearResolvedFlag()
    {
        foreach (var outputPin in mOutputPins)
            outputPin.Value.Resolved = false;
    }

    private Connection GetConnectionByPinId(int pinId)
    {
        foreach (var connection in mConnections)
        {
            if (connection.PinInId == pinId ||
                connection.PinOutId == pinId)
                return connection;
        }

        return null;
    }

    private Element GetElementByPinId(int pinId)
    {
        var inputPin = GetInputPin(pinId);
        if (inputPin != null)
            return GetElementById(inputPin.ElementId);

        var outputPin = GetOutputPin(pinId);
        if (outputPin != null)
            return GetElementById(outputPin.ElementId);

        return null;
    }

    public Element GetElementById(int elementId)
    {
        return mElements.Find((t) => { return t.Id == elementId; });
    }

    private InputPin GetInputPin(int inputPinId)
    {
        InputPin pin;
        if (!mInputPins.TryGetValue(inputPinId, out pin))
            return null;

        return pin;
    }

    private OutputPin GetOutputPin(int outputPinId)
    {
        OutputPin pin;
        if (!mOutputPins.TryGetValue(outputPinId, out pin))
            return null;

        return pin;
    }

    public bool IsInputPin(int pinId)
    {
        return mInputPins.ContainsKey(pinId);
    }

    public bool IsOutputPin(int pinId)
    {
        return mOutputPins.ContainsKey(pinId);
    }
}
