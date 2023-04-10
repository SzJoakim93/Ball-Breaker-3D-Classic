using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] UIManagerInGame UIManager;
    [SerializeField] GameObject downWall;
    [SerializeField] Ball[] balls;
    [SerializeField] Paddle paddle;
    [SerializeField] Ammo ammoObj;

    private float size = 1.0f;
    public float Size
    {
        get { return size; }
        set { this.size = value; }
    }

    private int life = 2;
    public int Life
    {
        get { return life; }
        set { life = value; UIManager.LifeChanged(life); }
    }

    private int ammo = 5;
    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; UIManager.AmmoChanged(ammo); }
    }

    private int star;
    public int Star
    {
        get { return star; }
        set { star = value; UIManager.StarChanged(star); }
    }

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; UIManager.ScoreChanged(score); }
    }

    private int ballcount = 1;
    public int Ballcount
    {
        get { return ballcount; }
        set { ballcount = value; }
    }

    public bool IsAllBallLaunched
    {
        get { return balls.Where(x => x.gameObject.activeInHierarchy && !x.IsLaunched).Count() == 0; }
    }

    public void TripleBall()
    {
        Ball activeBall = null;
        ballcount = 3;
        for (int i = 0; i < balls.Length; i++)
            if (balls[i].gameObject.activeInHierarchy)
            {
                activeBall = balls[i];
                break;
            }

        bool leftSide = true;
        for (int i = 0; i < balls.Length; i++)
            if (!balls[i].gameObject.activeInHierarchy && activeBall != null && balls[i] != activeBall)
            {
                balls[i].gameObject.SetActive(true);
                balls[i].transform.position = activeBall.transform.position;

                if (leftSide)
                {
                    balls[i].ActivateBall(activeBall.BallSpeed, activeBall.Ball_X + 0.1f, activeBall.Ball_Y - 0.1f);
                    leftSide = false;
                }
                else
                {
                    balls[i].ActivateBall(activeBall.BallSpeed, activeBall.Ball_X - 0.1f, activeBall.Ball_Y + 0.1f);
                }
            }
    }

    public void SetBallSpeed(float speed)
    {
        for (int i = 0; i < balls.Length; i++)
            if (balls[i].gameObject.activeInHierarchy)
                balls[i].BallSpeed = speed;
    }

    public void ActivateWall()
    {
        Global.Wall.Activate();
        downWall.SetActive(true);
    }

    public void ActivateGrip()
    {
        Global.Grip.Activate();
    }

    public void ActivateMegaBall()
    {
        Global.MegaBall.Activate();
    }

    public void ActivateIncreaseSize()
    {
        paddle.transform.localScale = new Vector3(150.0f, paddle.transform.localScale.y, paddle.transform.localScale.z);
        Global.IncreaseSize.Activate();
    }

    public void ActivateDecreaseSize()
    {
        paddle.transform.localScale = new Vector3(50.0f, paddle.transform.localScale.y, paddle.transform.localScale.z);
        Global.DescreaseSize.Activate();
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star", 0) + star);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + score);
        
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);

        if (Global.level == unlockedLevels && Global.level < Global.max_levels)
        {
            unlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        }

    }

    public void FireOrLaunch()
    {
        if (IsAllBallLaunched)
        {
            Fire();
        }
        else
        {
            Launch();
        }
    }

    void Fire()
    {
        if (ammo > 0 && !ammoObj.gameObject.activeInHierarchy)
        {
            ammoObj.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y, paddle.transform.position.z);
            ammoObj.gameObject.SetActive(true);
            Ammo--;
        }
    }

    void Launch()
    {
        for (int i = 0; i < balls.Length;i++)
        {
            if (balls[i].gameObject.activeInHierarchy && !balls[i].IsLaunched)
            {
                balls[i].ActivateBall(5.0f, 0.5f, 0.5f);
            }
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            
        }

        if (Global.Wall.IsEnd())
        {
            downWall.SetActive(false);
        }

        if (Global.IncreaseSize.IsEnd())
        {
            paddle.transform.localScale = new Vector3(100.0f, paddle.transform.localScale.y, paddle.transform.localScale.z);
        }

        if (Global.DescreaseSize.IsEnd())
        {
            paddle.transform.localScale = new Vector3(100.0f, paddle.transform.localScale.y, paddle.transform.localScale.z);
        }

    }
}