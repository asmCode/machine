﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdGen
{
    private static int m_id = 0;

    public static int GetId()
    {
        m_id++;
        return m_id;
    }
}
