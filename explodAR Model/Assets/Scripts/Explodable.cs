using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Explodable : MonoBehaviour, IPointerClickHandler
{
    #region fields

    [Tooltip("How far and in what direction to move the object when exploding")]
    public Vector3 explosionDirection;

    //where to start and end the explosion (calculated in start function)
    private Vector3 _explosionStart;
    private Vector3 _explosionTarget;

    //how far the explosion has progressed
    private float _explosionProgress;

    [Tooltip("Is the target object currently exploded?")]
    public bool exploded;

    //is the object currently supposed to be exploding / exploded?
    private bool _exploding;

    [Tooltip("Which, if any, parts have to be exploded prior to exploding this")]
    public List<Explodable> explodeAfter = new List<Explodable>();

    //automatically filled, which, if any, parts rely on this being exploded
    List<Explodable> explodeBefore = new List<Explodable>();

    #endregion fields



    #region unity methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add listener to global explosion event
        ExplodARController.instance.explodeAllEvent.AddListener(ExplosionOverride);

        //set explosion start and target for lerping
        _explosionStart = transform.localPosition;
        _explosionTarget = transform.localPosition + explosionDirection;

        //register this object as dependent on any in explodeAfter
        foreach (Explodable explodable in explodeAfter)
        {
            explodable.explodeBefore.Add(this);
        }

        //TODO ?: add circular dependency check for explodeBefore / explodeAfter

        //add a mesh collider to make object interactible
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //update explosion progress if the object is currently exploding   
        if(_exploding && _explosionProgress < ExplodARController.instance.explosionDuration)
        {
            _explosionProgress += Time.deltaTime;
            if(_explosionProgress > ExplodARController.instance.explosionDuration)
            {
                //fix the progress value
                _explosionProgress = ExplodARController.instance.explosionDuration;
                exploded = true;
            }
            UpdateExplosion();
        }
        //update explosion progress if the object is currently imploding
        else if (!_exploding && _explosionProgress > 0)
        {
            _explosionProgress -= Time.deltaTime;
            if(_explosionProgress < 0)
            {
                //fix the progress value
                _explosionProgress = 0;
                exploded = false;
            }
            UpdateExplosion();
        }
    }

    #endregion unity methods



    #region methods

    //explodes the object *if* it can explode right now
    public void Explode()
    {
        //if the object can explode (or implode), do it
        if(CanExplode())
        {
            _exploding = !_exploding;
            UpdateExplosion();
        }
    }

    //explodes the object *no matter if* it can explode right now
    public void ExplosionOverride(bool value)
    {
        _exploding = value;
        UpdateExplosion();
    }

    //wether or not the object can currently explode
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

    //updates the object's current position to match its explosion progress
    private void UpdateExplosion()
    {
        transform.localPosition = Vector3.Lerp
            (_explosionStart, _explosionTarget, _explosionProgress / ExplodARController.instance.explosionDuration);
    }

    #endregion methods



    #region OnPointerClick

    //make object clickable to trigger explosions and info pop ups
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Keyboard.current[Key.LeftCtrl].isPressed))
        {
            Info info = gameObject.GetComponent<Info>();
            if (info != null)
            {
                info.ToggleInfo();
            }
        }
        else
        {
            Explode();
        }
        
    }

    #endregion OnPointerClick
}
