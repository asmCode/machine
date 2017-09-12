using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorOutput : Connector
{
    public delegate float GetValueDelegate();

    public override float Value
    {
        get;
        protected set;
    }
}
