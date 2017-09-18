using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Element
{
    public override ElementType ElementType { get { return ElementType.Ball; } }

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
            return new int[] { };
        }
    }

    public override void Resolve(BoardManager signalManager)
    {
        Debug.LogFormat("Resolving: {0}", GetType().ToString());
    }
}
