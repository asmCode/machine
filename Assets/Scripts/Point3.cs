using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point3
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public static bool operator ==(Point3 a, Point3 b)
    {
        return
            a.X == b.X &&
            a.Y == b.Y &&
            a.Z == b.Z;
    }

    public static bool operator !=(Point3 a, Point3 b)
    {
        return !(a == b);
    }

    public static Point3 operator +(Point3 a, Point3 b)
    {
        return new Point3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Point3))
            return false;

        return (Point3)obj == this;
    }

    public override int GetHashCode()
    {
        return X ^ Y ^ Z;
    }

    public Point3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
