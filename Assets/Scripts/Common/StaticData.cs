using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static Dictionary<string, int> ideaMarginalReturnDecrease = new Dictionary<string, int> {
        {"Romantic Subplot", 3},
        {"Action Sequence", 2},
        {"Funny Moment", 1},
        {"Overarching Plot", 1},
        {"Character Death", 10},
        {"Heroic Moment", 2},
        {"Villainous Moment", 2},
        {"Procrastination", 0},
    };

     public static Dictionary<string, int> ideaFrequencyWeight = new Dictionary<string, int> {
        {"Romantic Subplot", 3},
        {"Action Sequence", 5},
        {"Funny Moment", 2},
        {"Overarching Plot", 2},
        {"Character Death", 1},
        {"Heroic Moment", 3},
        {"Villainous Moment", 3},
    };
}
