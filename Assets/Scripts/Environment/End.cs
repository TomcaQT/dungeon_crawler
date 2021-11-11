using System;
using UnityEngine;

public class End : MonoBehaviour
{
    public event EventHandler OnRoomEnd;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<PlayerController>();

        if (target != null)
            OnRoomEnd?.Invoke(this, new EventArgs());
    }
}
