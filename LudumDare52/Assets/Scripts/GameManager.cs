using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(float size)
    {
        if (size < 0.4f)
        {
            Debug.Log("MISS");
        }
        else if (size < 0.6f)
        {
            Debug.Log("Bad");
        }
        else if (size < 0.9f)
        {
            Debug.Log("Good");
        }
        else
        {
            Debug.Log("Perfect");
        }
    }
}
