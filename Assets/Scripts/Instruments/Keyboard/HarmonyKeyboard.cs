using System.Collections.Generic;
using System.Linq;
public class HarmonyKeyboard : Instrument
{
    public SoundPreset soundPreset;
    public List<Key> keysPressed;

    private Knob volumeKnob;
    void Awake()
    {
        StartUp();
        keysPressed = new List<Key>();

        //Assign Key Indexes
        Key[] keys = GetComponentsInChildren<Key>();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].index = i;
        }
        volumeKnob = GetComponentInChildren<Knob>();
        volumeKnob.value = volume;
    }
    public void OnKeyPressed(Key key)
    {
        if (key.active)
        {
            keysPressed.Add(key);
            keysPressed = keysPressed.OrderBy(k => k.index).ToList();
            myInfo.AddKey(key);
            PlaySound(key.noteSound, false);
        } else
        {
            keysPressed.Remove(key);
            myInfo.RemoveKey(key);
            StopSound(key.noteSound);
        }
    }
}
