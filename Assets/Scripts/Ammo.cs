using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] Level level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 5.0f * Time.deltaTime, 0.0f);

        foreach (var block in Global.BlockList)
        {
            if (collision(block.transform))
            {
                eventManager.HitBlock(block);
                gameObject.SetActive(false);
            }
        }

        if (transform.position.z > level.Size)
        {
            gameObject.SetActive(false);
        }
    }

    bool collision(Transform block)
    {
        return block.gameObject.activeInHierarchy && block.transform.position.y - block.transform.localScale.y/100 < 0.1f &&
            transform.position.x + 0.4f > block.position.x - block.localScale.x/100 && transform.position.x - 0.4f < block.position.x + block.localScale.x/100 &&
            transform.position.z + 0.4f > block.position.z - block.localScale.z/100 && transform.position.z + 0.4f < block.position.z - block.localScale.z/100 + 0.1;
    }
}
