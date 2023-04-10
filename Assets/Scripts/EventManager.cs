using UnityEngine;

public class EventManager : MonoBehaviour {

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject paddleSlider;
    [SerializeField] Player player;
    [SerializeField] Sound sounds;
    [SerializeField] Level level;
    [SerializeField] UIManagerInGame UIManager;
    [SerializeField] Star star;
    [SerializeField] Transform [] bonuses;

    bool isLandScape = true;
    void Start()
    {
        if (Settings.Input == Settings.InputType.Slider)
        {
            paddleSlider.SetActive(true);
        }
    }

    void Update()
    {
        if (Screen.width < Screen.height && isLandScape)
        {
            mainCamera.fieldOfView = 100;
            isLandScape = false;
        }
        else if (Screen.width > Screen.height && !isLandScape)
        {
            mainCamera.fieldOfView = 60;
            isLandScape = true;
        }
    }

    public void HitBlock(Block block)
    {
        block.hit();

        if (block.IsMovable)
            sounds.Play(0);
        else if (block.Life > 0)
        {
            player.Score += 2;
            sounds.Play(1);
        }
        else if (block.IsBonus)
        {
            sounds.Play(3);
            instantiateBonus();
        }
        else
        {
            player.Score += 5;
            sounds.Play(2);
        }

        if (block.Life == 0)
        {
            level.Blocks--;
            if (level.Blocks % 10 == 0)
            {
                Star newStar = Instantiate<Star>(star, new Vector3(
                    UnityEngine.Random.Range(-level.BorderValue, level.BorderValue),
                    5.0f,
                    UnityEngine.Random.Range(-level.BorderValue, level.BorderValue)),
                Quaternion.identity);
                newStar.gameObject.SetActive(true);
                newStar.startStar();
            }
                
        }

        if (level.Blocks == 0)
        {
            UIManager.ShowCompletedTitle();
            player.SaveState();
        }
    }

    void instantiateBonus()
    {
        int k = (int)UnityEngine.Random.Range(0.0f, 9.9f) % 10;
        bonuses[k].position = new Vector3(transform.position.x, bonuses[k].position.y, transform.position.z);
        bonuses[k].gameObject.SetActive(true);
    }
}