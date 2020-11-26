using UnityEngine;

[CreateAssetMenu(fileName = "SoundPreset", menuName = "Sound Preset", order = 1)]
public class SoundPreset : ScriptableObject 
{
    public AudioClip[] notes;
}
