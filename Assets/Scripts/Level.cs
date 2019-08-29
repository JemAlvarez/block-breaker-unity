using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;               // Serialize for debug purposes

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void DecreaseBreakableBlocks()
    {
        breakableBlocks--;
    }
}
