using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSlot : MonoBehaviour
{
    public GameObject instrumentPrefab;
    public float zoomValue;
    public BoxCollider2D myCollider;
    public Instrument instrument;

    private SpriteRenderer spriteRenderer;
    private SlotsManager slotsManager;
    private GameObject instrumentGameObject;
    private Color hoverColor;
    private void Start()
    {
        slotsManager = transform.parent.GetComponent<SlotsManager>();
        myCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (instrumentPrefab)
        {
            instrumentGameObject = Instantiate(instrumentPrefab);
            instrumentGameObject.transform.position = transform.position;
            instrumentGameObject.transform.parent = transform;
            instrument = instrumentGameObject.GetComponent<Instrument>();
        }
        hoverColor = spriteRenderer.color;
    }

    private void OnMouseDown()
    {
        slotsManager.EnterInstrument(this);
    }

    private void OnMouseOver()
    {
        hoverColor.a = 0.3f;
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        hoverColor.a = 0f;
        spriteRenderer.color = hoverColor;
    }


}
