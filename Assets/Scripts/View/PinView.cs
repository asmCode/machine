using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinView : MonoBehaviour
{
    private Material mMaterial;
    private float mSignalValue;

    public void SetSignalValue(float signalValue)
    {
        mSignalValue = signalValue;
        UpdateSignalColor();
    }

    public void SetHoover()
    {
        mMaterial.color = Color.red;
    }

    public void SetSelected()
    {
        mMaterial.color = Color.blue;
    }

    public void SetNormal()
    {
        UpdateSignalColor();
    }

    void UpdateSignalColor()
    {
        if (mSignalValue == 0.0f)
            mMaterial.color = new Color(0.0f, 0.3f, 0.0f);
        else
            mMaterial.color = new Color(0.0f, 1.0f, 0.0f);
    }

    void Awake()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        mMaterial = meshRenderer.material;
    }

    void Start()
    {
        UpdateSignalColor();
    }
}
