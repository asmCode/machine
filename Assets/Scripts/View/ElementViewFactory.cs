using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementViewFactory : MonoBehaviour
{
    public ElementView[] Elements;

    public ElementView Create(ElementType elementType)
    {
        ElementView elementViewPrefab = System.Array.Find(Elements, (t) => { return t.ElementType == elementType; });
        if (elementViewPrefab == null)
            return null;

        return Instantiate(elementViewPrefab);
    }

    void Awake()
    {
    }
}
