using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceUIScript : MonoBehaviour
{
    public Dictionary<string, int> numberUsedThisWeek = new Dictionary<string, int>();
    public List<IdeaFishData> unusedFish = new List<IdeaFishData>();
    public int currentFishIndex;
    [Header("UI Elements")]
    public Image fishImage;
    public TMP_Text ideaNameText;
    public TMP_Text basePointsText;
    public TMP_Text timeConsumptionText;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject noFishGroup;
    public GameObject yesFishGroup;

    public void Prev() {
        int index = currentFishIndex - 1;
        if (index < 0) {
            currentFishIndex = unusedFish.Count - 1;
        } else {
            currentFishIndex = index;
        }
        ShowFish();
    }

    public void Next() {
        int index = currentFishIndex + 1;
        if (index >= unusedFish.Count) {
            currentFishIndex = 0;
        } else {
            currentFishIndex = index;
        }
        ShowFish();
    }

    public void Eat() {
        IdeaFishData data = unusedFish[currentFishIndex];
        ConsumeFish(data);
        unusedFish.Remove(data);
        Next();
    }

    public void Release() {
        IdeaFishData data = unusedFish[currentFishIndex];
        unusedFish.Remove(data);
        Next();
    }
    
    public void AddFish(IdeaFishScript newFish) {
        unusedFish.Add(new IdeaFishData(newFish));
        currentFishIndex = unusedFish.Count - 1;
        ShowFish();
    }

    public void ConsumeFish(IdeaFishData fish) {
        int value = fish.basePoints - GetMarginalReturnPenalty(fish);
        GameManager.appeal += value;
    }

    public void ShowFish() {
        if (unusedFish.Count == 0) {
            yesFishGroup.SetActive(false);
            noFishGroup.SetActive(true);
        } else {
            yesFishGroup.SetActive(true);
            noFishGroup.SetActive(false);
            if (currentFishIndex >= unusedFish.Count) {
                // in case it happens...
                currentFishIndex = unusedFish.Count - 1;
            }
            if (unusedFish.Count == 1) {
                prevButton.SetActive(false);
                nextButton.SetActive(false);
            } else {
                prevButton.SetActive(true);
                nextButton.SetActive(true);
            }
            IdeaFishData data = unusedFish[currentFishIndex];
            fishImage.sprite = data.image;
            ideaNameText.text = data.ideaName;
            int marginalReturnPenalty = GetMarginalReturnPenalty(data);
            basePointsText.text = marginalReturnPenalty > 0
                ? string.Format("{0} (-{1})", data.basePoints, marginalReturnPenalty)
                : string.Format("{0}", data.basePoints);
            timeConsumptionText.text = string.Format("{0} hrs", data.timeConsumption);
        }
        gameObject.SetActive(true);
    }

    private int GetMarginalReturnPenalty(IdeaFishData data) {
        numberUsedThisWeek.TryGetValue(data.ideaName, out int numberUsed);
        return numberUsed * StaticData.ideaMarginalReturnDecrease[data.ideaName];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && Input.anyKey && !(Input.GetMouseButton(0) 
            || Input.GetMouseButton(1) || Input.GetMouseButton(2))) {
            gameObject.SetActive(false);
        }
    }
}