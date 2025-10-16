using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] float time = 1.0f;
    [HideInInspector] public float timer = 0.0f;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= time)
        {
            Destroy(this.gameObject);
        }
    }
}
