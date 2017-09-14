using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementView : MonoBehaviour
{
    public abstract ElementType ElementType { get; }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetInputPinsSignalValue(float[] signalValues)
    {

    }

    public void SetOutputPinsSignalValue(float[] signalValues)
    {

    }

    void Awake()
    {
        var inputPinsContainer = transform.Find("InputPins");
        dajesz
    }
}
