using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    public bool inInstrument;
    public float fadeSpeed;
    public float transitionSpeed;

    private InstrumentSlot[] slots;
    private CameraController cameraController;
    private InfoManager info;
    private void Start()
    {
        slots = GetComponentsInChildren<InstrumentSlot>();
        cameraController = FindObjectOfType<CameraController>();
        info = FindObjectOfType<InfoManager>();
    }

    public void EnterInstrument(InstrumentSlot luckySlot)
    {
        inInstrument = true;
        cameraController.ZoomIn(luckySlot.transform.position + new Vector3(luckySlot.myCollider.bounds.extents.x, 0, 0), luckySlot.zoomValue, transitionSpeed);
        if (luckySlot.instrument)
        {
            luckySlot.instrument.myInfo.UpdateInfo();
        }
        info.DisplayInfo();
        foreach (InstrumentSlot slot in slots)
        {
            if (slot != luckySlot)
            {
                foreach (SpriteRenderer renderer in slot.GetComponentsInChildren<SpriteRenderer>())
                {
                    if (renderer != slot.GetComponent<SpriteRenderer>())
                    {
                        StartCoroutine(FadeOutRenderer(renderer));
                    }
                }
            }
            slot.myCollider.enabled = false;
        }
    }
    public void ExitInstrument()
    {
        inInstrument = false;
        cameraController.ZoomOut();
        info.HideInfo();
        foreach (InstrumentSlot slot in slots)
        {
            foreach (SpriteRenderer renderer in slot.GetComponentsInChildren<SpriteRenderer>())
            {
                if (renderer != slot.GetComponent<SpriteRenderer>())
                {
                    StartCoroutine(FadeInRenderer(renderer));
                }
            }
            slot.myCollider.enabled = true;
        }
    }

    IEnumerator FadeOutRenderer(SpriteRenderer renderer)
    {
        Color color = renderer.color;
        while (renderer.color.a > 0)
        {
            color.a -= fadeSpeed;
            renderer.color = color; 
            yield return null;
        }
        renderer.color = color;
    }

    IEnumerator FadeInRenderer(SpriteRenderer renderer)
    {
        Color color = renderer.color;
        while (renderer.color.a < 1)
        {
            color.a += fadeSpeed;
            renderer.color = color;
            yield return null;
        }
        renderer.color = color;
    }

}
