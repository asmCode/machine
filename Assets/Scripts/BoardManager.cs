using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    private Dictionary<int, InputPin> mInputPins = new Dictionary<int, InputPin>();
    private Dictionary<int, OutputPin> mOutputPins = new Dictionary<int, OutputPin>();
    private List<Connection> mConnections = new List<Connection>();
    private Dictionary<int, Element> mElements = new Dictionary<int, Element>();

    public int AddElement(int type)
    {
        var element = ElementFactory.Create(type, 0, 0);
        mElements.Add(element.Id, element);

        foreach (var pinId in element.InputPinIds)
            AddInputPin(pinId, element.Id);

        foreach (var pinId in element.OutputPinIds)
            AddOutputPin(pinId, element.Id);

        return element.Id;
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
    }

    public void ResolveAll()
    {
        ClearResolvedFlag();

        foreach (var outputPin in mOutputPins)
        {
            if (IsResolved(outputPin.Key))
                continue;

            Resolve(outputPin.Key);
        }
    }

    public void Dump()
    {
        foreach (var element in mElements)
        {
            element.Value.Dump(this);
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
            return GetElement(inputPin.ElementId);

        var outputPin = GetOutputPin(pinId);
        if (outputPin != null)
            return GetElement(outputPin.ElementId);

        return null;
    }

    public Element GetElement(int elementId)
    {
        return mElements[elementId];
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
