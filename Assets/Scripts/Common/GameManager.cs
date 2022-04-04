using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static int weekNumber;
    public TMP_Text weekNumberDisplay;
    public int strikes;
    public int maxStrikes;
    public int maxAppeal;
    public int passableAppeal;
    public int appeal;
    public Slider appealSlider;
    public Image appealHandleImage;
    public Sprite notPassable;
    public Sprite passable;
    public bool isLastEpisode;
    public ChoiceUIScript choiceUIPanel;
    public GameObject episodeSummaryPanel;
    public GameObject introPanel;
    public TMP_Text reception;
    public TMP_Text comment;
    public GameObject newEpisodeButton;
    public GameObject newSeriesButton;
    public float maxHoursPerWeek;
    public float timePassed;
    public TMP_Text remainingTimeDisplay;
    public bool weekInProgress;

    public GameObject[] fishPrefabs;
    public GameObject creativeBlockPrefab;
    private List<string> fishNameLottery;

    private Dictionary<string, GameObject> fishNameToFishPrefab;
    // Start is called before the first frame update

    public static GameManager GetInstance() {
        return instance;
    }

    public void Restart() {
        weekInProgress = false;
        SceneManager.LoadScene(0);
    }

    public void NextWeek() {
        episodeSummaryPanel.SetActive(false);
        choiceUIPanel.numberUsedThisWeek = new Dictionary<string, int>();
        weekNumber++;
        if (weekNumber % 5 == 0) {
            // Every five weeks, reader expectation rises a little.
            maxAppeal += 100;
            passableAppeal += 80;
        }
        weekNumberDisplay.text = weekNumber.ToString();
        appeal = 0;
        appealSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Min(maxAppeal * 0.25f, 400f));
        appealSlider.maxValue = maxAppeal;
        appealSlider.minValue = 0;
        appealHandleImage.sprite = notPassable;
        timePassed = 0f;
        weekInProgress = true;
    }

    public void CreateFish() {
        int fishIndex = Random.Range(0, fishNameLottery.Count);
        GameObject fishPrefab = fishNameToFishPrefab[fishNameLottery[fishIndex]];
        Instantiate(fishPrefab);
    }

    public void CreativeBlock() {
        Instantiate(creativeBlockPrefab);
    }

    public void FinishWeek() {
        choiceUIPanel.gameObject.SetActive(false);
        weekInProgress = false;
        
        IdeaFishScript[] allFish = FindObjectsOfType<IdeaFishScript>();
        foreach (IdeaFishScript fish in allFish) {
            Destroy(fish.gameObject);
        }
        if (isLastEpisode) {
            newSeriesButton.SetActive(true);
            newEpisodeButton.SetActive(false);
        } else {
            newSeriesButton.SetActive(false);
            newEpisodeButton.SetActive(true);
        }
        comment.text = GetEditorComments();
        reception.text = GetReception();
        episodeSummaryPanel.SetActive(true);
        // TODO: Show ui and editor's words
    }

    public string GetReception() {
        if (appeal >= maxAppeal) {
            return "excellent!";
        }
        if (appeal >= passableAppeal) {
            return "good";
        }
        return "poor";
    }

    public string GetEditorComments() {
        if (isLastEpisode) {
            if (appeal >= maxAppeal) {
                return "That was a wonderful ending. It certainly left a mark in a lot of readers' hearts. It's been an honor working with you, and I'll be glad to see you grow and produce a brand new series.";
            }
            if (appeal >= passableAppeal) {
                return "Glad you got your regular performance back for the last episode. I understand it's hard to maintain the quality for a long-running series. I hope your next series will do better.";
            }
            return "That ending made no sense and was disappointing. I'm afrait this series had lost its charm quite a while ago. Anyway, we're open to sign you up for a new series, provided that you've learned from this experience.";
        }
        string comment = "";
        if (appeal >= passableAppeal) {
            if (appeal >= maxAppeal) {
                strikes = Mathf.Max(0, strikes - 1);
                comment += "What a great episode! It boosted my confidence in your abilities.";
            } else {
                comment += "Good job delivering the promise.";
            }
        } else {
            strikes++;
            switch (strikes) {
                case 1:
                    comment += "Just a warning - you'll need to do better if we want to keep the series going.";
                    break;
                case 2:
                    comment += "This does not look promising at all.";
                    break;
                case 3:
                    comment += "I'm seriously disappointed.";
                    break;
                case 4:
                    comment += "Our ratings are getting too low. Next time you mess up I might have to cancel the series.";
                    break;
                case 5:
                    comment += "Seems like you didn't take my warnigns to heart. The next episode will be your last. Please try to finish on a strong note.";
                    isLastEpisode = true;
                    break;
            }
        }
        if ((weekNumber + 1) % 5 == 0f) {
            comment += " Our readers' expectation has grown higher. Please try to put in more effort next week.";
        }
        return comment;
    }

    public void InitializeGame() {
        weekNumber = 20;
        fishNameToFishPrefab = new Dictionary<string, GameObject>();
        fishNameLottery = new List<string>();
        foreach (GameObject fishPrefab in fishPrefabs) {
            IdeaFishScript fishScript = fishPrefab.GetComponent<IdeaFishScript>();
            fishNameToFishPrefab[fishScript.ideaName] = fishPrefab;
            int numberOfFishInlottery = StaticData.ideaFrequencyWeight[fishScript.ideaName];
            for (int i = 0; i < numberOfFishInlottery; i++) {
                fishNameLottery.Add(fishScript.ideaName);
            }
        }
        introPanel.SetActive(false);
        NextWeek();
    }
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        weekInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weekInProgress) return;
        float possibility = Time.deltaTime * 2f / Mathf.Sqrt(1f * (weekNumber + 4f));
        float rand = Random.Range(0f, 1f);
        if (rand < possibility) {
            CreateFish();
        }
        if (weekNumber > 5) {
            float creativeBlockPossibility = Mathf.Min((weekNumber - 5f) * 0.01f, 0.8f ) * Time.deltaTime;
            float blockRand = Random.Range(0f, 1f);
            if (blockRand < creativeBlockPossibility) {
                CreativeBlock();
            }
        }
        timePassed += Time.deltaTime;
        remainingTimeDisplay.text = Mathf.FloorToInt(Mathf.Max(maxHoursPerWeek - timePassed, 0f)).ToString();
        appealSlider.value = appeal;
        appealHandleImage.sprite = appeal >= passableAppeal ? passable : notPassable;
        if (timePassed >= maxHoursPerWeek) {
            FinishWeek();
        }
    }
}
