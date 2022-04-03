using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaFishScript : MonoBehaviour
{
    public string ideaName;
    public float baseSpeed;
    public float oscillationAmount;
    public float oscillationFrequency;
    public float direction;
    public int minPoints;
    public int maxPoints;
    public int minTimeConsumption;
    public int maxTimeConsumption;
    public Sprite image;
    public bool isCaught;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 spawnPoint = Vector2.zero;
        float rand = Random.Range(0f, 1f);
        if (rand > 0.5f) {
            direction = 1f;
            transform.localScale = new Vector3(-1, 1, 1);
            spawnPoint.x = -9f;
        } else {
            direction = -1f;
            spawnPoint.x = 9f;
        }
        spawnPoint.y = Random.Range(-4f, 0f);
        transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCaught) {
            float newXPos = transform.position.x + Time.deltaTime * baseSpeed * direction;
            float newYPos = transform.position.y + Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmount * Time.deltaTime;
            transform.position = new Vector2(newXPos, newYPos);
        }
    }
}
