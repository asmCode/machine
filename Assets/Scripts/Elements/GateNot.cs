using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateNot : Element
{
    private int mPinIId;
    private int mPinOId;

    public GateNot()
    {
        mPinIId = IdGen.GetId();
        mPinOId = IdGen.GetId();
    }

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
            return new int[] { mPinOId };
        }
    }

    public override void Resolve(BoardManager signalManager)
    {
        Debug.LogFormat("Resolving: {0}", GetType().ToString());

        if (!signalManager.IsResolved(mPinIId))
            signalManager.Resolve(mPinIId);

        var signalI = signalManager.GetSignalValue(mPinIId);

        float signalO = (signalI > 0.0f) ? 0.0f : 1.0f;
        signalManager.SetSignalValue(mPinOId, signalO);
    }
}

