
public static class Chords
{
    public static Music.Interval[][] implicitAdditions = { 
        new Music.Interval[] { Music.Interval.MAJOR_SEVENTH, Music.Interval.MAJOR_NINTH }, //maj9
        new Music.Interval[] { Music.Interval.MINOR_SEVENTH, Music.Interval.MAJOR_NINTH }, //9 and m9
    }; 

    //All possible combinatios of three notes...
    public static Music.Interval[][] majorChordInvertions = {
         CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.PERFECT_FIFTH) ,
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.MINOR_SIXTH),
         CreateChord(Music.Interval.PERFECT_FOURTH, Music.Interval.MAJOR_SIXTH)
    };

    public static Music.Interval[][] minorChordInvertions = {
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.PERFECT_FIFTH),
         CreateChord(Music.Interval.MAJOR_THIRD, Music.Interval.MAJOR_SIXTH),
         CreateChord(Music.Interval.PERFECT_FOURTH, Music.Interval.MINOR_SIXTH)
    };

    public static Music.Interval[][] diminishedChordInvertions = {
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.AUGMENTED_FOURTH),
         CreateChord(Music.Interval.MINOR_THIRD, Music.Interval.MAJOR_SIXTH),
         CreateChord(Music.Interval.AUGMENTED_FOURTH, Music.Interval.MAJOR_SIXTH)
    };

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
    
}
