using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public PinView mPinView;

    private Game mGame;

    private void Awake()
    {
        mGame = new Game();
        mGame.Init();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            mPinView.SetHoover();
        if (Input.GetKeyDown(KeyCode.W))
            mPinView.SetSelected();
        if (Input.GetKeyDown(KeyCode.E))
            mPinView.SetNormal();

        if (Input.GetKeyDown(KeyCode.R))
            mPinView.SetSignalValue(1.0f);
        if (Input.GetKeyDown(KeyCode.T))
            mPinView.SetSignalValue(0.0f);
    }
}
