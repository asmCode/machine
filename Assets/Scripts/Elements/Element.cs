using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element
{
    public int Id { get; set; }
    public abstract ElementType ElementType { get; }
    public abstract int[] InputPinIds { get; }
    public abstract int[] OutputPinIds { get; }
    public Point3 Position { get; set; }

    public Element()
    {
        Id = IdGen.GetId();
    }

    public abstract void Resolve(BoardManager signalManager);

    public virtual void Dump(BoardManager signalManager)
    {
        string dump = "";

        dump += string.Format("Element: {0}\n", GetType().ToString());
        if (OutputPinIds.Length > 0)
            dump += string.Format("\tOutput Pin: {0}\n", signalManager.GetSignalValue(OutputPinIds[0]));
        if (InputPinIds.Length > 0)
            dump += string.Format("\tInput Pin Pin: {0}\n", signalManager.GetSignalValue(InputPinIds[0]));
        dump += "\n";

        Debug.Log(dump);
    }
}
