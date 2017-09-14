using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point3
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
