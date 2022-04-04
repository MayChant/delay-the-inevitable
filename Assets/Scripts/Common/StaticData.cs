using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static Dictionary<string, int> ideaMarginalReturnDecrease = new Dictionary<string, int> {
        {"Romantic Subplot", 5},
        {"Action Sequence", 3},
        {"Funny Moment", 2},
        {"Overarching Plot", 1},
        {"Character Death", 10},
        {"Heroic Moment", 5},
        {"Villainous Moment", 5},
        {"Creative Block", 0},
    };

     public static Dictionary<string, int> ideaFrequencyWeight = new Dictionary<string, int> {
        {"Romantic Subplot", 5},
        {"Action Sequence", 5},
        {"Funny Moment", 2},
        {"Overarching Plot", 2},
        {"Character Death", 1},
        {"Heroic Moment", 3},
        {"Villainous Moment", 3},
    };
}
