using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparlesVFX;
    [SerializeField] Sprite[] hitSprites;

    //cached reference
    Level level;

    //state variables
    [SerializeField] int timesHit; //TODO only serialized for debug purposes

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
        timesHit++;
        int MaxHits = hitSprites.Length + 1;

        if (timesHit >= MaxHits)
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
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];

        } 
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }

    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();

        gameObject.SetActive(false);
        Destroy(gameObject);

        level.BlockDestroyed();

        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();

        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparlesVFX, transform.position, transform.rotation);

        Destroy(sparkles, 1f);
    }

}
