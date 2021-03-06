﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeSprite {

    // Inspired by https://gamedev.stackexchange.com/questions/142791/how-can-i-fade-a-game-object-in-and-out-over-a-specified-duration
    // Used by objects with Rigidbody2D's that you don't want to move while fading
    public static IEnumerator Fade(Rigidbody2D constr, SpriteRenderer sprite, float start, float target, float duration)
    {
        float time = 0;
        Color col = sprite.color;
        //float start = col.a;

        RigidbodyConstraints2D original = constr.constraints;
        constr.constraints |= RigidbodyConstraints2D.FreezePositionX;
        //constr.constraints |= RigidbodyConstraints2D.FreezePositionY;

        while (time < duration)
        {
            time += Time.deltaTime;

            float blend = Mathf.Clamp01(time / duration);

            col.a = Mathf.Lerp(start, target, blend);

            sprite.color = col;

            yield return null;
        }

        constr.constraints = original;
    }


    // Inspired by https://gamedev.stackexchange.com/questions/142791/how-can-i-fade-a-game-object-in-and-out-over-a-specified-duration
    // Used by objects that don't need to be still or don't have a Rigidbody2D
    public static IEnumerator FadeImage(SpriteRenderer sprite, float start, float target, float duration)
    {
        float time = 0;
        Color col = sprite.color;
        //float start = col.a;

        while (time < duration)
        {
            time += Time.deltaTime;

            float blend = Mathf.Clamp01(time / duration);

            col.a = Mathf.Lerp(start, target, blend);

            sprite.color = col;

            yield return null;
        }
    }
}
