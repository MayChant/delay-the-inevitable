using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public IdeaFishScript caughtFish;
    private AudioSource audioSource;

    private void Catch(IdeaFishScript ideaFishScript) {
        audioSource.Play();
        ideaFishScript.isCaught = true;
        ideaFishScript.transform.parent = transform;
        ideaFishScript.transform.localPosition = Vector2.zero;
        ideaFishScript.transform.rotation = Quaternion.Euler(0f, 0f, 90f * ideaFishScript.direction);
        caughtFish = ideaFishScript;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (caughtFish != null) return; // don't catch more than one
        if (other.tag == "fish") {
            Catch(other.GetComponent<IdeaFishScript>());
        }
    }
}
