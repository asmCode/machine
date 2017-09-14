using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnd : Element
{
    private int mPinAId;
    private int mPinBId;
    private int mPinOId;

    public GateAnd()
    {
        mPinAId = IdGen.GetId();
        mPinBId = IdGen.GetId();
        mPinOId = IdGen.GetId();
    }

    public override ElementType ElementType { get { return ElementType.GateAnd; } }

    public override int[] InputPinIds
    {
        get
        {
            return new int[] { mPinAId, mPinBId };
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

        if (!signalManager.IsResolved(mPinAId))
            signalManager.Resolve(mPinAId);

        if (!signalManager.IsResolved(mPinBId))
            signalManager.Resolve(mPinBId);

        var signalA = signalManager.GetSignalValue(mPinAId);
        var signalB = signalManager.GetSignalValue(mPinBId);

        float signalO = (signalA > 0.0f && signalB > 0.0f) ? 1.0f : 0.0f;
        signalManager.SetSignalValue(mPinOId, signalO);
    }
}

