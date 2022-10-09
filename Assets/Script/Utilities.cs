using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool FlipTransform(bool facingRight, Transform targetTransform)
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 scale = targetTransform.localScale;
        scale.x *= -1;
        targetTransform.localScale = scale;

        return facingRight;
    }

    public static void SetGravityScale(Rigidbody2D RB,float scale)
    {
        RB.gravityScale = scale;
    }
}
