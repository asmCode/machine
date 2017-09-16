using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedDiode : Element
{
    private int mPinIId;

    public float LightPower { get; private set; }

    public LedDiode()
    {
        mPinIId = IdGen.GetId();
    }

    public override ElementType ElementType { get { return ElementType.LedDiode; } }

    public override int[] InputPinIds
    {
        get
        {
            return new int[] { mPinIId };
        }
    }

    public override int[] OutputPinIds
    {
        get
        {
            return new int[] { };
        }
    }

    public override void Resolve(BoardManager signalManager)
    {
        Debug.LogFormat("Resolving: {0}", GetType().ToString());

        if (!signalManager.IsResolved(mPinIId))
            signalManager.Resolve(mPinIId);

        var signalI = signalManager.GetSignalValue(mPinIId);

        LightPower = signalI;
    }
}

