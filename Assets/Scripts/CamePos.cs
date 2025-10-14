using UnityEngine;
using VectorExtensions;

public class CamePos : MonoBehaviour
{
    GameObject player;
    Vector3 v;

    void Start()
    {
        player = GameObject.FindObjectsByType<PlayerCore>(FindObjectsSortMode.None)[0].gameObject;
    }

    void Update()
    {
        var d = (player.transform.position - this.transform.position);
        v = new Vector3(d.x, d.y, 0).Pow(2f) * Time.deltaTime;
        this.transform.position += new Vector3(v.x, v.y, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }
}