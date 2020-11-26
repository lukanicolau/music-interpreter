using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Chord
{
    public List<Music.Note> notes;
    public ChordInversion inversion;
    public ChordType chordType;
    public Music.Note tonic;

    public bool isUnknown;
    public bool isInterval;
    public List<Music.Note> differentNotes;

    private int susN; //Number of sus
    private List<Music.Note> additions;

    public Chord(List<Music.Note> notes)
    {
        additions = new List<Music.Note>();
        this.notes = notes;
        differentNotes = new List<Music.Note>();
        differentNotes.AddRange(notes);
        isInterval = false;
        //Check if there are two equal notes in different octaves
        for (int i = 0; i < notes.Count - 1; i++)
        {
            for (int j = i + 1; j < notes.Count; j++)
            {
                if (Music.GetNote1(notes[i]) == Music.GetNote1(notes[j]))
                {
                    isInterval = true;
                    differentNotes.Remove(notes[j]);
                    return;
                }
            }
        }
        UnderstandMe();
    }

    public void UnderstandMe()
    {
        isUnknown = false;
        isInterval = false;
        Music.Interval[] intervals = new Music.Interval[2];
        for (int i = 0; i < notes.Count - 2; i++) {
            Music.Note[] characteristicNotes = new Music.Note[] { notes[i], notes[i+1], notes[i+2]};
            intervals[0] = Music.GetInterval(characteristicNotes[0], characteristicNotes[1]);
            intervals[1] = Music.GetInterval(characteristicNotes[0], characteristicNotes[2]);
            if (i > 0)
            {
                additions.Add(notes[i - 1]);
                additions.Remove(notes[i + 2]);
            }
            //Set chord characteristics based on three notes (two intervals)
            for (int j = 0; j < 3; j++)
            {
                if (intervals.SequenceEqual(Chords.majorChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], ChordType.MAJOR, (ChordInversion)j);
                    Debug.Log("entre en" + i);
                    return;
                }
                else if (intervals.SequenceEqual(Chords.minorChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], ChordType.MINOR, (ChordInversion)j);
                    Debug.Log("entre en" + i + " y j " + j);
                    return;
                }
                else if (intervals.SequenceEqual(Chords.diminishedChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], ChordType.DIMINISHED, (ChordInversion)j);
                    Debug.Log("entre en" + i);
                    return;
                }
                else if (j < 2 && intervals.SequenceEqual(Chords.susChords[j]))
                {
                    Debug.Log("entre en" + i);
                    SetChord(characteristicNotes[0], ChordType.SUS);
                    susN = (j + 1) * 2;
                    return;
                }
            }
        }
        isUnknown = true;
    }

    public void AddAddition(Music.Note addition)
    {
        additions.Add(addition);
        additions.Sort();
    }

    public void RemoveAddition(Music.Note addition)
    {
        additions.Remove(addition);
    }

    public string GetName()
    {
        string name;
        name = Music.GetNoteName(tonic) + Music.GetChordTypeName(chordType);
        //If it is sus, add it's number
        if (susN != 0) name = name + susN + " ";
        //Add additions...
        for (int i = 0; i < additions.Count; i++)
        {
            if (i < additions.Count - 1)
            {
                //Look if there's implicit additions
                Music.Interval[] intervals = new Music.Interval[2];
                intervals[i] = Music.GetInterval(tonic, additions[i]);
                intervals[i + 1] = Music.GetInterval(tonic, additions[i + 1]);

                if (chordType == ChordType.MAJOR)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (intervals.SequenceEqual(Chords.implicitAdditions[j]))
                        {
                            name = name + Music.GetImplicitAdditionName(j);
                            goto skipAdditionsPair;
                        }
                    }
                }
                else if (chordType == ChordType.MINOR)
                {
                    if (intervals.SequenceEqual(Chords.implicitAdditions[1]))
                    {
                        name = name + Music.GetImplicitAdditionName(1);
                        goto skipAdditionsPair;
                    }
                }
            }
        //If there isn't, add the addition default name
        Music.Interval interval = Music.GetInterval(tonic, additions[i]);
        name = name + Music.GetAdditionName(interval);
        skipAdditionsPair:
            {
                //we go directly to the addition in i+2;
                i++;
                continue; 
            }
        }
        return name;
    }

    private void SetChord(Music.Note tonic, ChordType chordType, ChordInversion inversion)
    {
        this.tonic = tonic;
        this.chordType = chordType;
        this.inversion = inversion;
        additions.Clear();
        if (notes.Count > 3)
        {
            for (int i = 3; i < notes.Count; i++)
            {
                AddAddition(notes[i]);
            }
        }
    }

    private void SetChord(Music.Note tonic, ChordType chordType)
    {
        this.tonic = tonic;
        this.chordType = chordType;
    }

    public enum ChordType
    {
        MAJOR,
        MINOR,
        DIMINISHED,
        SUS,
    }

    public enum ChordInversion
    {
        ROOT,
        FIRST,
        SECOND
    }
}
