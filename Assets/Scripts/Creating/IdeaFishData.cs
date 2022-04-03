using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IdeaFishData {
    public string ideaName;
    public Sprite image;
    public int basePoints;
    public int timeConsumption;
    public IdeaFishData(IdeaFishScript fish) {
        ideaName = fish.ideaName;
        image = fish.image;
        basePoints = UnityEngine.Random.Range(fish.minPoints, fish.maxPoints + 1);
        timeConsumption = UnityEngine.Random.Range(fish.minTimeConsumption, fish.maxTimeConsumption + 1);
    }
}