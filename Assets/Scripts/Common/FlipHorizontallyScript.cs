using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipHorizontallyScript : MonoBehaviour
{
    public float timeInterval;
    private float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timeInterval <= timePassed) {
            timePassed = 0f;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
