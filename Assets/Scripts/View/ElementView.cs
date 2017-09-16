using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementView : MonoBehaviour
{
    private PinView[] mInputPins;
    private PinView[] mOutputPins;

    public int ElementId { get; set; }
    public abstract ElementType ElementType { get; }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetInputPinsSignalValue(float[] signalValues)
    {
        if (mInputPins == null)
            return;

        if (mInputPins.Length != signalValues.Length)
            return;

        for (int i = 0; i < signalValues.Length; i++)
            mInputPins[i].SetSignalValue(signalValues[i]);
    }

    public void SetOutputPinsSignalValue(float[] signalValues)
    {
        if (mOutputPins  == null)
            return;

        if (mOutputPins.Length != signalValues.Length)
            return;

        for (int i = 0; i < signalValues.Length; i++)
            mOutputPins[i].SetSignalValue(signalValues[i]);
    }

    public virtual void Awake()
    {
        var inputPinsContainer = transform.Find("InputPins");
        mInputPins = inputPinsContainer.GetComponentsInChildren<PinView>();

        var outputPinsContainer = transform.Find("OutputPins");
        mOutputPins = outputPinsContainer.GetComponentsInChildren<PinView>();
    }
}
