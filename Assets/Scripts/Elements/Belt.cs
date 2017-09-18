using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Element
{
    private int mPinIId;

    public bool Running { get; private set; }
    public override ElementType ElementType { get { return ElementType.Belt; } }

    public Belt()
    {
        mPinIId = IdGen.GetId();
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
            return new int[] { };
        }
    }

    public override void Resolve(BoardManager signalManager)
    {
        Debug.LogFormat("Resolving: {0}", GetType().ToString());

        if (!signalManager.IsResolved(mPinIId))
            signalManager.Resolve(mPinIId);

        var signalI = signalManager.GetSignalValue(mPinIId);

        Running = signalI > 0.0f;

        var spaceAbove = Position + new Point3(0, 1, 0);
        var movementDirection = new Point3(-1, 0, 0);

        if (Running)
            signalManager.RequestMovement(spaceAbove, movementDirection);
    }
}

