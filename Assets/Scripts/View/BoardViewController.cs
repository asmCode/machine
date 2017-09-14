using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewController
{
    private BoardManager mBoardManager;
    private BoardView mBoardView;

    public BoardViewController(BoardManager boardManager, BoardView boardView)
    {
        mBoardManager = boardManager;
        mBoardView = boardView;

        boardManager.ElementAdded += HandleElementAdded;
        boardManager.ConnectionCreated += HandleConnectionCreated;
    }

    private void HandleElementAdded(int elementId)
    {
        var element = mBoardManager.GetElement(elementId);
        if (element == null)
            return;

        var elementView = mBoardView.AddElement(element.ElementType, element.Id);
        if (elementView == null)
            return;

        Vector3 position = new Vector3(element.Position.X, element.Position.Y, element.Position.Z);
        elementView.SetPosition(position);
    }

    private void HandleConnectionCreated(int inputPinId, int outputPinId)
    {
        Debug.Log("Connection created");
    }
}
