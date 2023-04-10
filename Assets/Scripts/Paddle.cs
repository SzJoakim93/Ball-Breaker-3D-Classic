using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    Level level;

    [SerializeField]
    Player player;

    [SerializeField]
    Sound sounds;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetAxis ("Horizontal") < 0.0f && transform.position.x - player.Size > -level.Size)
            transform.Translate(-4.0f*Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetAxis ("Horizontal") > 0.0f && transform.position.x + player.Size < level.Size)
            transform.Translate(4.0f*Time.deltaTime, 0.0f, 0.0f);

        if (Settings.Input == Settings.InputType.Swap && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x/50.0f, 0.0f, 0.0f);
        }

        if (transform.position.x - player.Size < -level.Size)
        {
            transform.position = new Vector3(-level.Size + player.Size, transform.position.y, transform.position.z);
        }

        if (transform.position.x + player.Size > level.Size)
        {
            transform.position = new Vector3(level.Size - player.Size, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.StartsWith("bonus"))
        {
            coll.gameObject.SetActive(false);
            sounds.Play(4);
        }

        if (coll.CompareTag("bonus_expand"))
            player.ActivateIncreaseSize();
        else if (coll.CompareTag("bonus_shrink"))
            player.ActivateDecreaseSize();
        else  if (coll.CompareTag("bonus_life"))
            player.Life++;
        else  if (coll.CompareTag("bonus_ammo"))
            player.Ammo += 5;
        else  if (coll.CompareTag("bonus_grip"))
            player.ActivateGrip();
        else  if (coll.CompareTag("bonus_wall"))
            player.ActivateWall();
        else if (coll.CompareTag("bonus_triple"))
            player.TripleBall();
        else if (coll.CompareTag("bonus_fast"))
            player.SetBallSpeed(0.2f);
        else if (coll.CompareTag("bonus_slow"))
            player.SetBallSpeed(0.08f);
        else if (coll.CompareTag("bonus_mega"))
            player.ActivateMegaBall();
    }

    public void OnSlideValueChanged(float x)
    {
        transform.position = new Vector3((x * level.Size*2) - level.Size, transform.position.y, transform.position.z);
    }
}