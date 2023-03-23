using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowEntity : MonoBehaviour
{
    public Collider2D[] throwCollider;
    private Rigidbody2D _physics;

    private void Awake()
    {
        _physics = this.GetComponent<Rigidbody2D>();
        if (_physics == null)
        {
            _physics = this.AddComponent<Rigidbody2D>();
            _physics.gravityScale = 0;
            _physics.drag = 0.5f;
            _physics.angularDrag = 0.5f;
            _physics.isKinematic = true;
        }
    }

    public void Throw(Vector2 vector)
    {
        _physics.velocity = vector;
        foreach (var collider in throwCollider)
        {
            collider.enabled = true;
            _physics.isKinematic = false;
        }
    }
}