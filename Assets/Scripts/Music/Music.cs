using UnityEngine;
using System;
public static class Music
{
    public static string[] naturalNotesNames = { "C", "D", "E", "F", "G", "A", "B" };

    private static string[] noteNamesInSharp = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    
    private static string[] noteNamesInFlat = { "C", "Db","D", "Eb","E", "F", "Gb", "G", "Ab", "A", "Bb", "B" };

    private static string[] intervalNames = {"Perfect Unison", "Minor second", "Major second", "Minor third", "Major third", "Perfect fourth", "Augmented fourth", "Perfect fifth",
                                        "Minor sixth", "Major sixth", "Minor seventh", "Major seventh", "Perfect octave", "Minor ninth", "Major ninth", "Minor tenth", "Major tenth"};

    private static string[] chordTypeNames = { "", "m", "dim", "sus"};

    private static string[] additions = {"", "", "add2", "", "", "add4", "add4#", "", "add5#", "add6", "7", "maj7", "", "", "add9", "", "", ""};
    private static string[] implicitChords = { "maj9", "9", "m9" }; //Only the ninth implicits are written because of the keyboard length
    public static Note GetNote(Key key)
    {
        Note note = (Note) key.index;
        return note;
    }

    ///<summary>Returns note in octave 1</summary>
    public static Note GetNote1(Note note)
    {
        return (Note) Mathf.Repeat((float)note, 12);
    }


    ///<summary>Returns the interval between two notes</summary>
    public static Interval GetInterval(Note a, Note b)
    {
        int distance = Mathf.Abs(a - b);
        Interval interval = (Interval) distance;
        return interval;
    }
    public static string GetNoteName(Note note, bool inSharp)
    {
        if (inSharp)
        {
            return noteNamesInSharp[(int)Mathf.Repeat((float)note, 12)];
        } else
        {
            return noteNamesInFlat[(int)Mathf.Repeat((float)note, 12)];
        }
    }

    ///<summary>Returns a string of the note</summary>
    public static string GetNaturalNoteName(NaturalNote naturalNote)
    {
        return naturalNotesNames[(int)Mathf.Repeat((int)naturalNote, 7)];
    }
    public static Note NaturalToNote(NaturalNote naturalNote)
    {
        int naturalNoteIndex = (int)naturalNote;
        if (naturalNoteIndex <= 2)
        {
            return (Note) (naturalNoteIndex*2);
        }
        else if (naturalNoteIndex <= 6)
        {
            return (Note)(naturalNoteIndex * 2 - 1);
        } else
        {
            return (Note)(naturalNoteIndex * 2 - 2);
        }
    }
    ///<summary>Dir = -1 turns C# in C; Dir = 1 turns C# in D</summary>
    public static NaturalNote NoteToNatural(Note note, int dir)
    {
        int noteIndex = (int)note;
        float result = (float)note / 2f;
        if (noteIndex <= 4 || noteIndex > 11)
        {
            if (dir == -1)
            {
                return (NaturalNote)Mathf.FloorToInt(result);
            } else
            {
                return (NaturalNote)Mathf.CeilToInt(result);
            }
        } else
        {
            if (dir == -1)
            {
                return (NaturalNote)Mathf.CeilToInt(result);
            } else
            {
                return (NaturalNote)Mathf.FloorToInt(result);
            }
        }
    }

    ///<summary>Returns a string of the interval</summary>
    public static int GetIntervalNumber(Interval interval) {
        int intervalIndex = (int)interval;
        if (intervalIndex <= 7)
        {
            return Mathf.CeilToInt(intervalIndex / 2f);
        } else if (intervalIndex > 7 && intervalIndex <= 12)
        {
            return Mathf.CeilToInt((intervalIndex+1) / 2f);
        } else
        {
            return Mathf.CeilToInt((intervalIndex + 2) / 2f);
        }
    }
    public static string GetIntervalName(Interval interval)
    {
        return intervalNames[(int)interval];
    }

    public static string GetChordTypeName(Chords.ChordType chordType)
    {
        return chordTypeNames[(int) chordType];
    }

    public static string GetAdditionName(Interval interval)
    {
        return additions[(int)interval];
    }

    public static string GetImplicitAdditionName(int index)
    {
        return implicitChords[index];
    }

    public enum NaturalNote
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B,
        C2,
        D2,
        E2,
    }

    public enum Note
    {
        C,
        CC,
        D,
        DD,
        E,
        F,
        FF,
        G,
        GG,
        A,
        AA,
        B,
        C2,
        CC2,
        D2,
        DD2,
        E2,
    }

    public enum Interval
    {
        PERFECT_UNISON,
        MINOR_SECOND,
        MAJOR_SECOND,
        MINOR_THIRD,
        MAJOR_THIRD,
        PERFECT_FOURTH,
        AUGMENTED_FOURTH,
        PERFECT_FIFTH,
        AUGMENTED_FIFTH,
        MAJOR_SIXTH,
        MINOR_SEVENTH,
        MAJOR_SEVENTH,
        PERFECT_OCTAVE,
        MINOR_NINTH,
        MAJOR_NINTH,
        MINOR_TENTH,
        MAJOR_TENTH
    }

}
