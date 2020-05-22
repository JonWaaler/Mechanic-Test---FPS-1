using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform emitter;
    public Transform gun_emitter;
    public float damage = 100f;

    public LineRenderer GunParticles;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            RaycastHit raycastHit = Create_Raycast(emitter.position, emitter.forward);

            //GunParticles.Play();
            //GunParticles.transform.LookAt(raycastHit.point);

            GunParticles.gameObject.SetActive(true);
            GunParticles.SetPosition(1, new Vector3(GunParticles.GetPosition(1).x, GunParticles.GetPosition(1).y, raycastHit.distance -.8f));
            //GunParticles.SetPosition(1, raycastHit.point);
            GunParticles.transform.LookAt(raycastHit.point);
        }

        

        if (Input.GetMouseButtonUp(0))
        {
            GunParticles.gameObject.SetActive(false);

        }
    }

    

    public RaycastHit Create_Raycast(Vector3 startPos, Vector3 direction)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(startPos, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(startPos, direction * hit.distance, Color.yellow);
        }

        return hit;
    }
}