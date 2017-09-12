using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnd : Element
{
    private ConnectorInput m_inputA;
    private ConnectorInput m_inputB;
    private ConnectorOutput m_output;

    private void Awake()
    {
        m_inputA = transform.Find("SignalInputs/A").GetComponent<ConnectorInput>();
        m_inputB = transform.Find("SignalInputs/B").GetComponent<ConnectorInput>();
        m_output = transform.Find("SignalOutputs/O").GetComponent<ConnectorOutput>();
    }

    public override void Tick()
    {
        throw new NotImplementedException();
    }
}
