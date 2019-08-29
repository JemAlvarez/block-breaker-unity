using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockDestroyVFX;
    [SerializeField] float destroyDelay = 2f;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    Level level;

    // Vars
    [SerializeField] int registeredHits;            // serialize for debug purpose

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;

        registeredHits++;

        FindObjectOfType<GameSession>().AddScore();

        if (registeredHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = registeredHits - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError($"Block sprite missing from array '{gameObject.name}'");
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        level.DecreaseBreakableBlocks();
        Destroy(gameObject);
        TriggerDestroyVFX();
    }

    private void TriggerDestroyVFX()
    {
        GameObject effect = Instantiate(blockDestroyVFX, transform.position, transform.rotation);
        Destroy(effect, destroyDelay);
    }
}
