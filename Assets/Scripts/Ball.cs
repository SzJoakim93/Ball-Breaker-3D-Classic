using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    Transform padle;
    [SerializeField]
    Sound sounds;
    [SerializeField]
    Player player;
    [SerializeField]
    Star star;
    [SerializeField]
    EventManager eventManager;


    [SerializeField]
    UIManagerInGame UIManager;

    float ballspeed_x;
    public float Ball_X
    {
        get { return ballspeed_x; }
    }

    float ballspeed_y;
    public float Ball_Y
    {
        get { return ballspeed_y; }
    }

    float ballspeed;
    public float BallSpeed
    {
        set { ballspeed = value; }
        get { return ballspeed; }
    }

    bool isLaunched;
    public bool IsLaunched
    {
        get { return isLaunched; }
    }

    void Start()
    {
        isLaunched = false;
        ballspeed_x = 0.5f;
        ballspeed_y = 0.5f;
    }

    void Update()
    {
        if (UIManager.Pause)
        {
            return;
        }

        hitObjects();
        transform.Translate(ballspeed_x*ballspeed*Time.deltaTime, 0.0f, ballspeed_y*ballspeed*Time.deltaTime);

        if (!isLaunched)
        {
            transform.position = new Vector3(padle.position.x, transform.position.y, padle.position.z + padle.localScale.z/40.0f);
        }
            

        if (transform.position.z < padle.position.z - 10.0f)
        {
            player.Ballcount--;
            if (player.Ballcount < 1)
            {
                player.Life--;

                if (player.Life > 0)
                {
                    transform.position = new Vector3(padle.position.x, transform.position.y, padle.position.z + padle.localScale.z/40.0f);
                    isLaunched = false;
                }
                else
                {
                    gameObject.SetActive(false);
                    UIManager.ShowGameOver();
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        if (Input.GetButton("Fire1"))
        {

        }
    }

    public void ActivateBall(float ballspeed, float ballspeed_x, float ballspeed_y)
    {
        if (!isLaunched)
        {
            isLaunched = true;
            this.ballspeed = ballspeed;
            this.ballspeed_x = ballspeed_x;
            this.ballspeed_y = ballspeed_y;
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "wall_left")
        {
            ballspeed_x *= -1.0f;
            sounds.Play(0);
        }
        else if (obj.tag == "wall_right")
        {
            ballspeed_x *= -1.0f;
            sounds.Play(0);
        }
        else if (obj.tag == "wall_up")
        {
            ballspeed_y *= -1.0f;
            sounds.Play(0);
        }
        else if (obj.tag == "wall_down")
        {
            ballspeed_y *= -1.0f;
            sounds.Play(0);
        }
        else if (obj.tag == "star")
        {
            Destroy(obj.gameObject);
            player.Star++;
            player.Score += 15;
            sounds.Play(8);
        }
        else if (obj.tag == "padle")
        {
            sounds.Play(0);
            if (Global.Grip.IsActive())
            {
                isLaunched = false;
                ballspeed = 0.0f;
                transform.position = new Vector3(padle.position.x, transform.position.y, padle.position.z + padle.localScale.z/40.0f);
            }

            pongFromPaddle(obj.transform);
        }
    }

    void pongFromPaddle(Transform padle)
    {
        ballspeed_x = (transform.position.x - padle.position.x) / player.Size * 0.6f;
        ballspeed_y = 1-Mathf.Abs(ballspeed_x);
    }

    void hitObjects()
    {
        if (!isLaunched)
        {
            return;
        }

        foreach (var block in Global.BlockList)
        {
            if (block.isActiveAndEnabled && block.transform.position.y - block.transform.localScale.y/100 < 0.1f)
            {
                bool coll = false;
                if (coll = collision_front(block.transform))
                    setNegativeDir_Y();
                else if (coll = collision_left(block.transform))
                    setPositiveDir_X();
                else if (coll = collision_back(block.transform))
                    setPositiveDir_Y();
                else if (coll = collision_right(block.transform))
                    setNegativeDir_X();

                if (coll)
                {
                    eventManager.HitBlock(block);
                }
            }
        }
    }

    bool collision_front(Transform block)
    {
        return transform.position.x + 0.4f > block.position.x - block.localScale.x/100 && transform.position.x - 0.4f < block.position.x + block.localScale.x/100 &&
        transform.position.z + 0.4f > block.position.z - block.localScale.z/100 && transform.position.z + 0.4f < block.position.z - block.localScale.z/100 + 0.1;
    }

    bool collision_back(Transform block)
    {
        return transform.position.x + 0.4f > block.position.x - block.localScale.z/100 && transform.position.x - 0.4f < block.position.x + block.localScale.x/100 &&
        transform.position.z - 0.4f < block.position.z + block.localScale.z/100 && transform.position.z - 0.4f > block.position.z + block.localScale.z/100 - 0.1;
    }

    bool collision_left(Transform block)
    {
        return transform.position.z + 0.4f > block.position.z - block.localScale.z/100 && transform.position.z - 0.4f < block.position.z + block.localScale.z/100 &&
        transform.position.x - 0.4f < block.position.x + block.localScale.z/100 && transform.position.x - 0.4f > block.position.x + block.localScale.x/100 - 0.1;
    }

    bool collision_right(Transform block)
    {
        return transform.position.z + 0.4f > block.position.z - block.localScale.z/100 && transform.position.z - 0.4f < block.position.z + block.localScale.z/100 &&
        transform.position.x + 0.4f > block.position.x - block.localScale.x/100 && transform.position.x + 0.4f < block.position.x - block.localScale.x/100 + 0.1;
    }

    void setPositiveDir_X()
    {
        if (ballspeed_x < 0.0f && !Global.MegaBall.IsActive())
        {
            ballspeed_x *=-1;
        }
    }

    void setNegativeDir_X()
    {
        if (ballspeed_x > 0.0f && !Global.MegaBall.IsActive())
        {
            ballspeed_x *=-1;
        }
    }

    void setPositiveDir_Y()
    {
        if (ballspeed_y < 0.0f && !Global.MegaBall.IsActive())
        {
            ballspeed_y *=-1;
        }
    }

    void setNegativeDir_Y()
    {
        if (ballspeed_y > 0.0f && !Global.MegaBall.IsActive())
        {
            ballspeed_y *=-1;
        }
    }
}
