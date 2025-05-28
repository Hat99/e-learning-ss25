using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Explodable : MonoBehaviour
{
    [Tooltip("How far and in what direction to move the object when exploding")]
    public Vector3 explosionDirection;

    //where to start and end the explosion (calculated in start function)
    private Vector3 explosionStart;
    private Vector3 explosionTarget;

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

    private GameObject _button;
    private LineRenderer _lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosionStart = transform.localPosition;
        explosionTarget = transform.localPosition + explosionDirection;

        //register this object as dependent on any in explodeAfter
        foreach (Explodable explodable in explodeAfter)
        {
            explodable.explodeBefore.Add(this);
        }

        //TODO ?: add circular dependency check for explodeBefore / explodeAfter

        _button = Instantiate(ExplodARController.instance.explodeButtonTemplate);
        _button.transform.SetParent(this.transform);
        _button.transform.position = transform.position 
            + explosionDirection.normalized * ExplodARController.instance.buttonDistanceFromObject;
        
        _lineRenderer = _button.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.startWidth = .1f;
        _lineRenderer.endWidth = .1f;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _button.transform.position); 

        SetButtonActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            Explode();
        }

        if(exploding && explosionProgress < ExplodARController.instance.explosionDuration)
        {
            explosionProgress += Time.deltaTime;
            if(explosionProgress > ExplodARController.instance.explosionDuration)
            {
                explosionProgress = ExplodARController.instance.explosionDuration;
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
            (explosionStart, explosionTarget, explosionProgress / ExplodARController.instance.explosionDuration);
        _lineRenderer.SetPosition(0, this.transform.position);
        _lineRenderer.SetPosition(1, _button.transform.position);
    }

    public void SetButtonActive(bool active)
    {
        _button.SetActive(active);
    }
}
