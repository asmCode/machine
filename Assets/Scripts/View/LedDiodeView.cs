using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedDiodeView : ElementView
{
    private Material mBulbMaterial;

    public override ElementType ElementType
    {
        get
        {
            return ElementType.LedDiode;
        }
    }

    public void SetLightPower(float power)
    {
        mBulbMaterial.color = new Color(Mathf.Lerp(0.2f, 1.0f, power), 0, 0);
    }

    public override void Awake()
    {
        base.Awake();

        var meshRenderer = transform.Find("Bulb").GetComponent<MeshRenderer>();
        mBulbMaterial = meshRenderer.material;
    }
}
