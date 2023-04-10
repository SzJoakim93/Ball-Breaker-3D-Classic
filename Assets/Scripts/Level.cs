using System.Globalization;
using UnityEngine;

public class Level : MonoBehaviour
{
    private float size;
    private float borderValue;
    [SerializeField] Transform LeftWall; //-5 - (x - 6) * 1.5
    [SerializeField] Transform RightWall; //5 + (x - 6) * 1.5
    [SerializeField] Transform UpWall;
    [SerializeField] Transform DownWall;
    [SerializeField] Transform Floor; //1 + (x - 6) * 0.3f
    [SerializeField] Transform Camera;
    [SerializeField] Transform Padle;
    [SerializeField] Transform Ball;
    [SerializeField] LevelAssets levelAssets;
    [SerializeField] AdManager adManager;

    public float Size
    {
        get { return size; }
        set { this.size = value; }
    }

    public float BorderValue
    {
        get { return borderValue; }
    }

    private int blocks;
    public int Blocks
    {
        get { return blocks; }
        set { blocks = value; }
    }

    void Start()
    {
        Load();
        adManager.RequestInterstitial();
    }
    

    public void Load()
    {
        Global.BlockList = new System.Collections.Generic.List<Block>();
        TextAsset level_path = Resources.Load<TextAsset>("Levels/L" + Global.level.ToString());
        string[] lines = level_path.text.Split('\n');

        int i = 0;
        foreach(var line in lines)
        {
            if (line != "")
            {
                string [] obj_params = line.Split(' ');
                if (i == 0)
                {
                    size = float.Parse(obj_params[0], CultureInfo.InvariantCulture) * 10.0f;
                    Floor.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(obj_params[1]);
                    UpWall.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(obj_params[1]);
                    DownWall.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(obj_params[1]);
                    LeftWall.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(obj_params[1]);
                    RightWall.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(obj_params[1]);
                }
                else
                {
                    Block newBlock = levelAssets.GetObject(float.Parse(obj_params[0], CultureInfo.InvariantCulture)-0.493f,
                        float.Parse(obj_params[1], CultureInfo.InvariantCulture),
                        float.Parse(obj_params[2], CultureInfo.InvariantCulture)+0.0507f, //translation

                        float.Parse(obj_params[3], CultureInfo.InvariantCulture),
                        float.Parse(obj_params[4], CultureInfo.InvariantCulture),
                        float.Parse(obj_params[5], CultureInfo.InvariantCulture), //scale

                        int.Parse(obj_params[6]), //rotation
                        obj_params[7], //color
                        obj_params[8], //object name
                        int.Parse(obj_params[9])); //life(>0)/ isBonus(-1) /isMove(-2)


                    if (newBlock.IsMovable)
                        newBlock.initMovePositions((float.Parse(obj_params[10], CultureInfo.InvariantCulture)/100-0.493f)*9.4117f,
                            (float.Parse(obj_params[11], CultureInfo.InvariantCulture)/100-0.493f)*9.4117f,
                            float.Parse(obj_params[12], CultureInfo.InvariantCulture)/10);

                    Global.BlockList.Add(newBlock); 
                    for (int j = newBlock.IsMovable ? 13 : 10; j < obj_params.Length && obj_params[j] != "-1"; j++)
                        if (obj_params[j] != "-1")
                            newBlock.AddChild(int.Parse(obj_params[j]));
                    
                    if (!newBlock.IsMovable)
                    {
                        blocks++;
                    }
                }
            }
            

            i++;
        }

        SetLevelProperties();
    }

    void SetLevelProperties()
    {
        borderValue = 5 + (size - 6) * 1.5f;
        float borderSize = 10 + (size - 6) * 3.0f;
        float floorSize = 1 + (size -6) * 0.3f;

        LeftWall.position = new Vector3(-borderValue, 0.31f, 0.0f);
        LeftWall.localScale = new Vector3(0.1f, 0.7f, borderSize);

        RightWall.position = new Vector3(borderValue, 0.31f, 0.0f);
        RightWall.localScale = new Vector3(0.1f, 0.7f, borderSize);

        UpWall.position = new Vector3(0.0f, 0.31f, borderValue);
        UpWall.localScale = new Vector3(borderSize, 0.7f, 0.1f);

        DownWall.position = new Vector3(0.0f, 0.31f, -borderValue);
        DownWall.localScale = new Vector3(borderSize, 0.7f, 0.1f);

        Floor.localScale = new Vector3(floorSize, 1.0f, floorSize);

        Padle.position = new Vector3(0.0f, 0.31f, -borderValue + 0.5f);
        Camera.position = new Vector3(-4.47f * size/5, 8.1f * size/5, -6.64f * size/5);
        Camera.eulerAngles = new Vector3(43.297f + (size-6)*4.276f, 33.586f + (size-6)*0.94f, 7.545f + (size-6)*0.704f);
    }
}