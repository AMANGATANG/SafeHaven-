using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    //Member variable

    [SerializeField]
    private int NumberGen;

    public void Start()
    {
        RandomGenerate();
    }

    // Random Number Generator
    public void RandomGenerate()
    {
        //Randomizing form 1-5
        NumberGen = Random.Range(1, 6);
        Debug.Log("current number is: " + NumberGen);
    }
    public void DiffcultyRandomizer()
    {
        //If statements for specific numbers it will generate
        if (NumberGen == 1)
        {
            Debug.Log("Message 1 is here");
        }
        else if (NumberGen == 2)    
        {
            Debug.Log("Message 2 is here");
        }
        else if (NumberGen == 3)
        {
            Debug.Log("Message 3 is here");
        }
        else if (NumberGen == 4)
        {
            Debug.Log("Message 4 is here");
        }
        else if (NumberGen == 5)
        {
            Debug.Log("Message 5 is here");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
