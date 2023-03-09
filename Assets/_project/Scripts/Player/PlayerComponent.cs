using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public PlayerController Controller { get; internal set; }

    public virtual void Init(PlayerController controller)
    {
        this.Controller = controller;
    }
}
