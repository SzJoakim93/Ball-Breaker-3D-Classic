using UnityEngine;

public class Star : MonoBehaviour
{
    private float startTime;

    public void startStar()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 30.0f * Time.deltaTime, 0.0f);
        if (transform.position.y > 0.5f)
            transform.Translate(0.0f, -1.0f*Time.deltaTime, 0.0f);

        if (Time.timeSinceLevelLoad - startTime > 30.0f)
            Destroy(gameObject);
    }
}
