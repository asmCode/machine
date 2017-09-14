using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSignalSource : Element
{
    private int mPinOId;

    public ConstantSignalSource()
    {
        mPinOId = IdGen.GetId();
    }

    public override ElementType ElementType { get { return ElementType.ConstantSignalSource; } }

    public override int[] InputPinIds
    {
        get
        {
            return new int[] { };
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

        signalManager.SetSignalValue(mPinOId, 1.0f);
    }
}
