using UnityEngine;
using UnityEngine.VFX;
using Cysharp.Threading.Tasks;

public class FXLifeTime : LifeTime
{
    [SerializeField] float delay;
    protected override void Dispose()
    {
        var vfx = this.GetComponent<VisualEffect>();
        if (vfx != null)
        {
            vfx.Stop();
            Destroy(this.gameObject, delay);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}