using UnityEngine;
using R3;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class Chain : MonoBehaviour
{
    GameObject From;
    GameObject To;
    GameObject chain;
    float w;
    float h;
    float distance;
    List<GameObject> Chains = new List<GameObject>();

    public void Create(GameObject from, GameObject to, float max, ChainSO so)
    {
        distance = max;
        From = from;
        chain = so.chain;
        w = so.w;
        h = so.h;
        To = to;
        var dir = (To.transform.position - from.transform.position).normalized;
        var n = CurrentNum(max, h);
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                var obj = Instantiate(chain, from.transform.position + 0.5f * h * dir, Quaternion.FromToRotation(Vector2.right, dir));
                obj.transform.SetParent(from.transform);
                Chains.Add(obj);
                var chai = obj.GetComponents<HingeJoint2D>();
                var clist = chai.OrderByDescending(c => -c.anchor.x).ToArray();
                clist[0].connectedBody = From.GetComponent<Rigidbody2D>();
                continue;
            }
            var ob = Instantiate(chain, from.transform.position + (i + 0.5f) * h * dir, Quaternion.FromToRotation(Vector2.right, dir));
            Chains.Add(ob);
            var cha = ob.GetComponents<HingeJoint2D>();
            var clis = cha.OrderByDescending(c => -c.anchor.x).ToArray();
            clis[0].connectedBody = Chains[i - 1].GetComponent<Rigidbody2D>();
            if (i == n - 1)
            {
                var cend = clis[0].AddComponent<HingeJoint2D>();
                cend.anchor = new Vector2(0.2f, 0);
                cend.connectedAnchor = new Vector2(-0.3242f, -0.00109f);
                cend.connectedBody = to.GetComponent<Rigidbody2D>();
            }
        }


    }
    void MakeChain()
    {

    }

    /*void Update()
    {
        Distance.Value = CurrentNum((From.transform.position - To.transform.position).magnitude, h);
    }*/
    int CurrentNum(float r, float h)
    {
        if (r < 2 * h)
        {
            return 2;
        }
        else
        {
            return Mathf.FloorToInt(r / h) + 1;
        }
    }
}
