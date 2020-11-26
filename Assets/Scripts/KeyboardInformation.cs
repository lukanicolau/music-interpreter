using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KeyboardInformation : MonoBehaviour
{
    public TextMeshProUGUI notesGUI;
    public TextMeshProUGUI detailsGUI;


    private List<Music.Note> notesPlaying;
    private Chord chord;

    private readonly string[] randomText = { "Interesing...", "Umm... I don't know what you just played", "Wow that's Spinetta as fuck", "That's not even a chord" };

    private void Awake()
    {
        notesPlaying = new List<Music.Note>();
    }

    public void AddKey(Key key)
    {
        Music.Note note = Music.GetNote(key);
        notesPlaying.Add(note);
        notesPlaying.Sort();
        if (notesPlaying.Count == 3)
        {
            chord = new Chord(notesPlaying);
        }
        else if (notesPlaying.Count > 3)
        {
            chord.AddAddition(note);
            chord.UnderstandMe();
        }
        UpdateInfo();
    }

    public void RemoveKey(Key key)
    {
        Music.Note note = Music.GetNote(key);
        if (notesPlaying.Contains(note))
        {
            notesPlaying.Remove(note);
        }
        if (notesPlaying.Count == 3)
        {
            chord = new Chord(notesPlaying);
        } else if (notesPlaying.Count > 3)
        {
            chord.RemoveAddition(note);
            chord.UnderstandMe();
        }
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        //Notes ordered from left to right as played in keyboard
        for (int i = 0; i < notesPlaying.Count; i++)
        {
            notesGUI.text = notesGUI.text + ", " + Music.GetNoteName(notesPlaying[i]);
        }

        //More detailed info about the whole music phenomenon
        if (notesPlaying.Count == 0)
        {
            notesGUI.text = "...";
            detailsGUI.text = "";
        } else if (notesPlaying.Count == 1)
        {
            detailsGUI.text = "A single note";
        } else if (notesPlaying.Count == 2)
        { //Intervals
            Music.Interval interval = Music.GetInterval(notesPlaying[notesPlaying.Count - 2], notesPlaying[notesPlaying.Count - 1]);
            detailsGUI.text = Music.GetIntervalName(interval);
        } else if (notesPlaying.Count >= 3)
        {
            if (chord.isInterval)
            {
                Music.Interval interval = Music.GetInterval(chord.differentNotes[0], chord.differentNotes[1]);
                detailsGUI.text = Music.GetIntervalName(interval);
            }
            else
            {
                if (!chord.isUnknown)
                {
                    detailsGUI.text = chord.GetName();
                }
                else
                {
                    int random = Random.Range(0, randomText.Length);
                    detailsGUI.text = randomText[random];
                }
            }
        } else
        {
            int random = Random.Range(0, randomText.Length);
            detailsGUI.text = randomText[random];
        }
    }
}
