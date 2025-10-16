using UnityEngine;

public class EntityHanged : MonoBehaviour
{
    [SerializeField] GameObject chainPrefab;
    GameObject chainObj;
    EntityModel model;
    EntityMove move;

    void Awake()
    {
        model = GetComponent<EntityModel>();
        move = GetComponent<EntityMove>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.TryGetComponent<Hand>(out var hand))
        {
            if (hand.isHanging.Value && chainObj == null)
            {
                chainObj = Instantiate(chainPrefab, hand.transform.position, Quaternion.identity);
                var chain = chainObj.GetComponent<EntityHandChain>();
                chain.Set(hand, model);
            }
        }
    }
}
