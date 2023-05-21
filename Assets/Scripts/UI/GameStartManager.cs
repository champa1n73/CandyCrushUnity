using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject levelPanel;
    // Start is called before the first frame update
    void Start()
    {
        startPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void PlayGame()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void Home()
    {
        startPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
