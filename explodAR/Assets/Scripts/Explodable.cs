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
    public float explosionDuration = 1.0f;

    //how far the explosion has progressed
    private float explosionProgress;

    [Tooltip("Is the target object currently exploded?")]
    public bool exploded;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosionStart = transform.localPosition;
        explosionTarget = transform.localPosition + explosionDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            Explode();
        }

        if(exploded && explosionProgress < explosionDuration)
        {
            explosionProgress += Time.deltaTime;
            if(explosionProgress > explosionDuration)
            {
                explosionProgress = explosionDuration;
            }
            UpdateExplosion();
        }
        else if (!exploded && explosionProgress > 0)
        {
            explosionProgress -= Time.deltaTime;
            if(explosionProgress < 0)
            {
                explosionProgress = 0;
            }
            UpdateExplosion();
        }

    }

    public void Explode()
    {
        exploded = !exploded;
        UpdateExplosion();
    }

    private void UpdateExplosion()
    {
        transform.localPosition = Vector3.Lerp
            (explosionStart, explosionTarget, explosionProgress / explosionDuration);
    }
}
