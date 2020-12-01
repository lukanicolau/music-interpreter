using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Chord
{
    public List<Music.Note> notes;
    public Chords.ChordInversion inversion;
    public Chords.ChordType chordType;
    public Music.Note tonic;

    public bool isUnknown;
    public bool isInterval;
    public List<Music.Note> differentNotes;

    public bool inSharp;

    private string plusInfo;
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
        //Set chord characteristics based on three notes (two intervals).
        //First priority
        for (int i = 0; i < notes.Count - 2; i++) {
            Music.Note[] characteristicNotes = new Music.Note[] { notes[i], notes[i+1], notes[i+2]};
            intervals[0] = Music.GetInterval(characteristicNotes[0], characteristicNotes[1]);
            intervals[1] = Music.GetInterval(characteristicNotes[0], characteristicNotes[2]);
            if (i > 0)
            {
                Debug.Log("Additions");
                AddAddition(notes[i - 1]);
                Debug.Log("Added" + Music.GetNoteName(notes[i-1],true));
                RemoveAddition(notes[i + 2]);
                Debug.Log("Removed" + Music.GetNoteName(notes[i + 2], true));
            }
            for (int j = 0; j < 3; j++)
            {
                if (intervals.SequenceEqual(Chords.majorChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], Chords.ChordType.MAJOR, (Chords.ChordInversion)j);
                    return;
                }
                else if (intervals.SequenceEqual(Chords.minorChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], Chords.ChordType.MINOR, (Chords.ChordInversion)j);
                    return;
                }
                else if (intervals.SequenceEqual(Chords.diminishedChordInvertions[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], Chords.ChordType.DIMINISHED, (Chords.ChordInversion)j);
                    return;
                } 
                else if (intervals.SequenceEqual(Chords.pseudoMaj7[j]))
                {
                    SetChord(characteristicNotes[(int)Mathf.Repeat(-j, 3)], Chords.ChordType.MAJOR, (Chords.ChordInversion)j);
                    plusInfo = "maj7";
                    return;
                } 
                else if (j < 2) {
                    if (j < 1)
                    {
                        if (intervals.SequenceEqual(Chords.augmentedTriad))
                        {
                            SetChord(characteristicNotes[0], Chords.ChordType.MAJOR);
                            plusInfo = "5#";
                            return;
                        }
                        else if (intervals.SequenceEqual(Chords.add4Chord))
                        {
                            SetChord(characteristicNotes[0], Chords.ChordType.MINOR);
                            if (additions.Count > 0)
                            {
                                if (Music.GetInterval(tonic, additions[0]) != Music.Interval.MINOR_SEVENTH) plusInfo = "4";
                            } else
                            {
                                plusInfo = "4";
                            }
                            return;
                        }
                    }
                    if (intervals.SequenceEqual(Chords.add2Chords[j]))
                    {
                        SetChord(characteristicNotes[0], (Chords.ChordType)j);
                        if (additions.Count > 0)
                        {
                            if (Music.GetInterval(tonic, additions[0]) != Music.Interval.MINOR_SEVENTH) plusInfo = "2";
                        }
                        else
                        {
                            plusInfo = "2";
                        }
                        return;
                    }
                }  
            }
        }
        //Second priority
        for (int i = 0; i < notes.Count - 2; i++)
        {
            Music.Note[] characteristicNotes = new Music.Note[] { notes[i], notes[i + 1], notes[i + 2] };
            intervals[0] = Music.GetInterval(characteristicNotes[0], characteristicNotes[1]);
            intervals[1] = Music.GetInterval(characteristicNotes[0], characteristicNotes[2]);
            if (i > 0)
            {
                AddAddition(notes[i - 1]);
                RemoveAddition(notes[i + 2]);
            }
            for (int j = 0; j < 2; j++)
            {
                if (intervals.SequenceEqual(Chords.susChords[j]))
                {
                    SetChord(characteristicNotes[0], Chords.ChordType.SUS);
                    plusInfo = "" + (j + 1) * 2;
                    return;
                }
            }
        }
        isUnknown = true;
    }

    public string GetName()
    {
        string name;
        name = Music.GetNoteName(tonic, inSharp) + Music.GetChordTypeName(chordType);
        name = name + plusInfo;
        
        //Add additions...
        for (int i = 0; i < additions.Count; i++)
        {
            if (chordType == Chords.ChordType.MAJOR || chordType == Chords.ChordType.MINOR)
            {
                //Look if there's implicit additions
                if (i < additions.Count - 1)
                {
                    Music.Interval[] intervals = new Music.Interval[2];
                    intervals[i] = Music.GetInterval(tonic, additions[i]);
                    intervals[i + 1] = Music.GetInterval(tonic, additions[i + 1]);

                    if (chordType == Chords.ChordType.MAJOR)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (intervals.SequenceEqual(Chords.implicitAdditions[j]))
                            {
                                if (!name.Contains(Music.GetImplicitAdditionName(j)))
                                {
                                    name = name + Music.GetImplicitAdditionName(j);
                                }
                                goto skipAdditionsPair;
                            }
                        }
                    }
                    else if (chordType == Chords.ChordType.MINOR)
                    {
                        if (intervals.SequenceEqual(Chords.implicitAdditions[1]))
                        {
                            if (!name.Contains(Music.GetImplicitAdditionName(1)))
                            {
                                name = name + Music.GetImplicitAdditionName(1);
                            }
                            goto skipAdditionsPair;
                        }
                    }
                }
            }
            //If there isn't, add the addition default name
            Music.Interval interval = Music.GetInterval(tonic, additions[i]);
            if (!name.Contains(Music.GetAdditionName(interval)))
            {
                name = name + Music.GetAdditionName(interval);
            }
            skipAdditionsPair:
            {
                //we go directly to the addition in i+2;
                i++;
                continue; 
            }
        }
        return name;
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

    private void SetChord(Music.Note tonic, Chords.ChordType chordType, Chords.ChordInversion inversion)
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

    private void SetChord(Music.Note tonic, Chords.ChordType chordType)
    {
        this.tonic = tonic;
        this.chordType = chordType;
    }

}
