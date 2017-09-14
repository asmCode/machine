using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView
{
    private ElementViewFactory mElementViewFactory;

    public BoardView()
    {
        mElementViewFactory = GameObject.FindObjectOfType<ElementViewFactory>();
    }

    public ElementView AddElement(ElementType elementType, int elementId)
    {
        return mElementViewFactory.Create(elementType);
    }
}
