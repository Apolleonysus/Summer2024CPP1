using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    //Hisham's method
    //public Transform playerTransform;
    [SerializeField] private float distThreshold;
    [SerializeField] private float projectileFireRate;

    private float timeSinceLastFire = 0;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        if (distThreshold <= 0)
            distThreshold = 2;

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (anim == null)
            Debug.LogError("Animator component not found!");

        if (sr == null)
            Debug.LogError("SpriteRenderer component not found!");
    }



    // Update is called once per frame
    void Update()
    {
        // Debug logs
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null.");
            return;
        }

        PlayerController pc = GameManager.Instance.PlayerInstance;
        if (pc == null)
        {
            Debug.LogError("PlayerController is null.");
            return;
        }

        if (anim == null)
        {
            Debug.LogError("Animator component is null.");
            return;
        }

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curPlayingClips == null || curPlayingClips.Length == 0)
        {
            Debug.LogError("Current animation clips are null or empty.");
            return;
        }

        if (curPlayingClips[0].clip == null)
        {
            Debug.LogError("Current animation clip is null.");
            return;
        }

        if (sr == null)
        {
            Debug.LogError("SpriteRenderer component is null.");
            return;
        }

        sr.flipX = (pc.transform.position.x < transform.position.x) ? true : false;

        float distance = Vector2.Distance(pc.transform.position, transform.position);

        if (distance < distThreshold)
        {
            sr.color = Color.red;

            if (curPlayingClips[0].clip.name.Contains("Idle"))
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }
        else
        {
            sr.color = Color.white;
        }
    }
}