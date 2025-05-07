using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCtrl : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health
    {
        get => health;
        set => health = Math.Clamp(value, 0, 100);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3;

    public float Speed
    {
        get => speed;
        set => speed = Math.Clamp(value, 0, 100);
    }

    [Range(1f, 20f)][SerializeField] private float jumpPower = 3;

    public float JumpPower
    {
        get => jumpPower;
        set => jumpPower = Math.Clamp(value, 0, 100);
    }
}
