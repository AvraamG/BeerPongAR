using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitCup : MonoBehaviour
{

    string ballTag = "Ball";
    [SerializeField] ParticleSystem scoreParicle;
    MeshRenderer meshRenderer;
    TrailRenderer trailRenderer;
    Collider ballcollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ballTag)
        {
            meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
            trailRenderer = other.gameObject.GetComponent<TrailRenderer>();
            ballcollider = other.gameObject.GetComponent<Collider>();
            meshRenderer.enabled = false;
            trailRenderer.enabled = false;
            ballcollider.enabled = false;
            Handheld.Vibrate();
            scoreParicle.Play();
        }
    }

}
