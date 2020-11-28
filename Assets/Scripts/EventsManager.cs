using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private SlotsManager slotsManager;
    private void Start()
    {
        slotsManager = FindObjectOfType<SlotsManager>();
    }

    private void Update()
    {
        if (slotsManager.inInstrument && Input.GetKeyDown(KeyCode.Escape))
        {
            slotsManager.ExitInstrument();
        }
    }
}
