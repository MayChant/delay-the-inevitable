using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static int weekNumber;
    public static int appeal;

    public GameObject[] fishPrefabs;
    private List<string> fishNameLottery;

    private Dictionary<string, GameObject> fishNameToFishPrefab;
    // Start is called before the first frame update

    public static GameManager GetInstance() {
        return instance;
    }

    public static void NextWeek() {
        weekNumber++;
        appeal = 0;
    }

    public void CreateFish() {
        int fishIndex = Random.Range(0, fishNameLottery.Count);
        GameObject fishPrefab = fishNameToFishPrefab[fishNameLottery[fishIndex]];
        Instantiate(fishPrefab);
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
        weekNumber = 1;
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
    }

    // Update is called once per frame
    void Update()
    {
        float possibility = Time.deltaTime * 5f / (1f * (weekNumber + 4f));
        print(possibility);
        float rand = Random.Range(0f, 1f);
        if (rand < possibility) {
            CreateFish();
        }
        // TODO: Spawn plastic bags
    }
}
