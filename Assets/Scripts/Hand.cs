using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class Hand : MonoBehaviour
{
    List<EntityMove> entities;
    [HideInInspector] public ReactiveProperty<bool> isHanging = new ReactiveProperty<bool>(false);


    [SerializeField] PlayerModel model;
    [HideInInspector] public float AtPow;
    public ReactiveProperty<float> ratio = new ReactiveProperty<float>(1);

    #region Sprite
    protected SpriteRenderer self;
    [SerializeField] protected Sprite[] sprites;

    #endregion
    protected float elapsedTime;


    protected virtual void Awake()
    {
        self = this.GetComponent<SpriteRenderer>();
        isHanging.Subscribe(v =>
        {
            if (v)
            {
                self.sprite = sprites[1];
            }
            else
            {
                self.sprite = sprites[0];
            }
        });

        model.Attack.Subscribe(a =>
        {
            AtPow = a * 8.0f;
            this.transform.localScale += new Vector3(a * 0.001f, a * 0.001f, 0);
        });
    }

    public void OnHanging(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHanging.Value = true;
        }
        if (context.canceled)
        {
            isHanging.Value = false;
        }
    }

    void ModelAdjuster()
    {
        
    }
}
