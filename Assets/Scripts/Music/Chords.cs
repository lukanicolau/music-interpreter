
public static class Chords
{
    public static Music.Interval[][] implicitAdditions = { //Additions sequences where the last is the only one that is noted. (Only until ninth because of the keyboard length)
        new Music.Interval[] { Music.Interval.MAJOR_SEVENTH, Music.Interval.MAJOR_NINTH }, //maj9
        new Music.Interval[] { Music.Interval.MINOR_SEVENTH, Music.Interval.MAJOR_NINTH }, //9 and m9
    }; 

    //All possible combinatios of three notes...
    public static Music.Interval[][] majorChordInvertions = {
         CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.PERFECT_FIFTH) ,
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.AUGMENTED_FIFTH),
         CreateChord(Music.Interval.PERFECT_FOURTH, Music.Interval.MAJOR_SIXTH)
    };

    public static Music.Interval[][] minorChordInvertions = {
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.PERFECT_FIFTH),
         CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.MAJOR_SIXTH),
         CreateChord(Music.Interval.PERFECT_FOURTH, Music.Interval.AUGMENTED_FIFTH)
    };

    public static Music.Interval[][] diminishedChordInvertions = {
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.AUGMENTED_FOURTH),
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.MAJOR_SIXTH),
         CreateChord(Music.Interval.AUGMENTED_FOURTH, Music.Interval.MAJOR_SIXTH)
    };

    //Theoretically, from now on, these are not chords. 
    //By the way, they sound as if they would (for me). 
    //Nevertheless, when the 5th or 3th is added (depending on what's missing), they become real chords and that's why they are so important.  

    public static Music.Interval[] augmentedTriad = CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.AUGMENTED_FIFTH);

    public static Music.Interval[][] pseudoMaj7 =
    {
        CreateChord(Music.Interval.PERFECT_FIFTH, Music.Interval.MAJOR_SEVENTH),
        CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.PERFECT_FOURTH),
        CreateChord(Music.Interval.MINOR_SECOND, Music.Interval.AUGMENTED_FIFTH)
    };

    public static Music.Interval[][] add2Chords =
    {
        CreateChord(Music.Interval.MAJOR_SECOND, Music.Interval.MAJOR_THIRD), //major sus2
        CreateChord(Music.Interval.MAJOR_SECOND, Music.Interval.MINOR_THIRD), //minor sus2
    };

    public static Music.Interval[] add4Chord = CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.PERFECT_FOURTH);
    
    public static Music.Interval[][] susChords =
    {
        CreateChord(Music.Interval.MAJOR_SECOND, Music.Interval.PERFECT_FIFTH), //sus2
        CreateChord(Music.Interval.PERFECT_FOURTH, Music.Interval.PERFECT_FIFTH) //sus4
    };

    private static Music.Interval[] CreateChord(Music.Interval first, Music.Interval second)
    {
        Music.Interval[] chord = new Music.Interval[] { first, second };
        return chord;
    }

    public enum ChordType
    {
        MAJOR,
        MINOR,
        DIMINISHED,
        SUS
    }

    public enum ChordInversion
    {
        ROOT,
        FIRST,
        SECOND
    }

}
