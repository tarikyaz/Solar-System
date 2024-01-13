using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyManager : MonoBehaviour
{
    public static Action OnGlaxyTik { get; internal set; }

    private void FixedUpdate()
    {
        OnGlaxyTik?.Invoke();
    }
}
