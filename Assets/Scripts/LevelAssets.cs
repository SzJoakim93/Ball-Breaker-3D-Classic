using UnityEngine;

public class LevelAssets : MonoBehaviour
{
    Transform [] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = gameObject.GetComponentsInChildren<Transform>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Block GetObject(float tx, float ty, float tz,
                            float sx, float sy, float sz,
                            int rotation, string color, string name, int life)
    {
        string [] splitedPath = name.Split('/');
        foreach (var obj in objects)
        {
            if (obj.name == splitedPath[1])
            {
                //*9,4117
                //+2,867068
                Transform newObj =
                    Instantiate<Transform>(obj, new Vector3(tx*9.4117f, ty*9.4117f+2.867f, tz*9.4117f*(-1)-1.5f), Quaternion.identity);
                newObj.localScale = new Vector3(sx*1000, sy*1000, sz*1000);
                newObj.eulerAngles = new Vector3(0.0f, -rotation+180.0f, 0.0f);
                newObj.gameObject.SetActive(true);
                newObj.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>(color);

                Block objBlock = newObj.GetComponent<Block>();
                objBlock.initProp(life);

                return objBlock;
            }
        }

        Debug.Log("Gameobject not found: " + name);
        return null;
    }
}
