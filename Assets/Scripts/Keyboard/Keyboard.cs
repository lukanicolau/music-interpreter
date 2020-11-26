using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Keyboard : Instrument
{
    public SoundPreset soundPreset;
    public List<Key> keysPressed;
    private KeyboardInformation info;

    void Awake()
    {
        StartUp();
        info = GetComponentInChildren<KeyboardInformation>();
        keysPressed = new List<Key>();

        //Assign Key Indexes
        Key[] keys = GetComponentsInChildren<Key>();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].index = i;
        }
    }
    public void OnKeyPressed(Key key)
    {
        if (key.active)
        {
            keysPressed.Add(key);
            keysPressed = keysPressed.OrderBy(k => k.index).ToList();
            info.AddKey(key);
            PlaySound(key.noteSound, false);
        } else
        {
            keysPressed.Remove(key);
            info.RemoveKey(key);
            StopSound(key.noteSound);
        }
    }
}
