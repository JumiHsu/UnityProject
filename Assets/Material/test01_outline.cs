using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test01_outline : MonoBehaviour
{

    public Color color = Color.white;

    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    private void OnDisable()
    {
        UpdateOutline(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);
        block.SetFloat("_Outline", outline ? 1f : 0);
        block.SetColor("_OutlineColor", color);
        spriteRenderer.SetPropertyBlock(block);
    }
}