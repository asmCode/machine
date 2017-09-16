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

    public void Update()
    {
        UpdatePins();
        UpdateElementViews();
    }

    private void UpdateElementViews()
    {
        for (int i = 0; i < mBoardManager.ElementCount; i++)
        {
            var element = mBoardManager.GetElement(i);
            var elementView = mBoardView.GetElementById(element.Id);
            if (elementView == null)
                continue;

            if (element is LedDiode && elementView is LedDiodeView)
            {
                ((LedDiodeView)elementView).SetLightPower(((LedDiode)element).LightPower);
            }
        }
    }

    private void UpdatePins()
    {
        for (int i = 0; i < mBoardManager.ElementCount; i++)
        {
            var element = mBoardManager.GetElement(i);
            var elementView = mBoardView.GetElementById(element.Id);
            if (elementView == null)
                continue;

            float[] inputPinValues = new float[element.InputPinIds.Length];
            for (int j = 0; j < inputPinValues.Length; j++)
                inputPinValues[j] = mBoardManager.GetSignalValue(element.InputPinIds[j]);
            elementView.SetInputPinsSignalValue(inputPinValues);

            float[] outputPinValues = new float[element.OutputPinIds.Length];
            for (int j = 0; j < outputPinValues.Length; j++)
                outputPinValues[j] = mBoardManager.GetSignalValue(element.OutputPinIds[j]);
            elementView.SetOutputPinsSignalValue(outputPinValues);
        }
    }

    private void HandleElementAdded(int elementId)
    {
        var element = mBoardManager.GetElementById(elementId);
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
