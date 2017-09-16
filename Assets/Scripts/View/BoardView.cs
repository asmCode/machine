using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView
{
    private ElementViewFactory mElementViewFactory;
    private Transform mElementsContainer;
    private List<ElementView> mElementViews = new List<ElementView>();

    public BoardView()
    {
        mElementViewFactory = GameObject.FindObjectOfType<ElementViewFactory>();
        mElementsContainer = GameObject.Find("ElementsContainer").transform;
    }

    public ElementView AddElement(ElementType elementType, int elementId)
    {
        var element = mElementViewFactory.Create(elementType);
        if (element != null)
        {
            element.ElementId = elementId;
            element.transform.SetParent(mElementsContainer);
            mElementViews.Add(element);
        }

        return element;
    }

    public ElementView GetElementById(int elementId)
    {
        return mElementViews.Find((t) => { return t.ElementId == elementId; });
    }
}
