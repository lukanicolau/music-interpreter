using UnityEngine;
public static class Music
{
    private static string[] naturalNotes = { "C", "D", "E", "F", "G", "A", "B" };

    //Esto capaz esta al pedo. segun el intervalo, agarrar una natural y agregarle # o b.
    private static string[] noteNamesInSharp = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }; //falta este
    private static string[] noteNamesInFlat = { "C", "Db","D", "Eb","Fb", "F", "Gb", "G", "Ab", "A", "Bb", "Cb" };

    private static string[] intervalNames = {"Perfect Unison", "Minor second", "Major second", "Minor third", "Major third", "Perfect fourth", "Augmented fourth", "Perfect fifth",
                                        "Minor sixth", "Major sixth", "Minor seventh", "Major seventh", "Perfect octave", "Minor ninth", "Major ninth", "Minor tenth", "Major tenth"};

    private static string[] chordTypeNames = { "", "m", "dim", "sus" };

    private static string[] additions = {"", "", "add2", "", "", "add4", "add4#", "", "add5#", "6", "7", "maj7", "", "", "add9", "", "", ""};
    private static string[] implicitChords = { "maj9", "9", "m9" }; //Only the ninth implicits are written because of the keyboard length
    public static Note GetNote(Key key)
    {
        Note note = (Note) key.index;
        return note;
    }

    //Returns note in octave 1
    public static Note GetNote1(Note note)
    {
        return (Note) Mathf.Repeat((float)note, 12);
    }

    //Returns the interval between two notes
    public static Interval GetInterval(Note a, Note b)
    {
        int distance = Mathf.Abs(a - b);
        Interval interval = (Interval) distance;
        return interval;
    }

    //Returns a string of the note
    public static string GetNoteName(Note note)
    {
        return noteNamesInSharp[(int) Mathf.Repeat((float)note, 12)];
    }

    //Returns a string of the interval
    public static string GetIntervalName(Interval interval)
    {
        return intervalNames[(int)interval];
    }

    public static string GetChordTypeName(Chord.ChordType chordType)
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
        MINOR_SIXTH,
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
