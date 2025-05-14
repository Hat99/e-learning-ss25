using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Explodable : MonoBehaviour
{
    [Tooltip("How far and in what direction to move the object when exploding")]
    public Vector3 explosionDirection;

    //where to start and end the explosion (calculated in start function)
    private Vector3 explosionStart;
    private Vector3 explosionTarget;

    [Tooltip("How long the explosion takes")]
    public float explosionDuration;

    [Tooltip("Wether to use own explosion duration or parent's")]
    public bool useParentDuration;

    //how far the explosion has progressed
    private float explosionProgress;

    [Tooltip("Is the target object currently exploded?")]
    public bool exploded;

    //is the object currently supposed to be exploding / exploded?
    private bool exploding;

    [Tooltip("Which, if any, parts have to be exploded prior to exploding this")]
    public List<Explodable> explodeAfter = new List<Explodable>();

    //automatically filled, which, if any, parts rely on this being exploded
    //TODO: make this non-public?
    public List<Explodable> explodeBefore = new List<Explodable>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosionStart = transform.localPosition;
        explosionTarget = transform.localPosition + explosionDirection;

        if (useParentDuration)
        {
            //search for explodable scripts in parent objects (not in this object)
            explosionDuration = transform.parent.gameObject.GetComponentInParent<Explodable>().explosionDuration;
        }

        //register this object as dependent on any in explodeAfter
        foreach(Explodable explodable in explodeAfter)
        {
            explodable.explodeBefore.Add(this);
        }

        //TODO ?: add circular dependency check for explodeBefore / explodeAfter
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            Explode();
        }

        if(exploding && explosionProgress < explosionDuration)
        {
            explosionProgress += Time.deltaTime;
            if(explosionProgress > explosionDuration)
            {
                explosionProgress = explosionDuration;
                exploded = true;
            }
            UpdateExplosion();
        }
        else if (!exploding && explosionProgress > 0)
        {
            explosionProgress -= Time.deltaTime;
            if(explosionProgress < 0)
            {
                explosionProgress = 0;
                exploded = false;
            }
            UpdateExplosion();
        }

    }

    public void Explode()
    {
        if(CanExplode())
        {
            exploding = !exploding;
            UpdateExplosion();
        }
    }


    private bool CanExplode()
    {
        //is there an explodeAfter object that's not yet exploded?
        foreach(Explodable explodable in explodeAfter)
        {
            if (!explodable.exploded)
            {
                return false;
            }
        }

        //(when imploding)
        //is there an explodeBefore object that's still exploded?
        foreach(Explodable explodable in explodeBefore)
        {
            if (explodable.exploded)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdateExplosion()
    {
        transform.localPosition = Vector3.Lerp
            (explosionStart, explosionTarget, explosionProgress / explosionDuration);
    }
}
