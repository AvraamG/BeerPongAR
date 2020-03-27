using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{

    public ManoGestureTrigger throwTrigger, pinchTrigger;
    public GestureInfo gesture;
    TrailRenderer trailRenderer;
    MeshRenderer meshRenderer;
    Collider ballcollider;
    float depthStart, depthRelease;
    Text depthValueText;
    GameObject debugGO;

    // Start is called before the first frame update
    void Start()
    {
        //ballRB = GetComponent<Rigidbody>();
        //trailRenderer = GetComponent<TrailRenderer>();
        //meshRenderer = GetComponent<MeshRenderer>();
        //ballcollider = GetComponent<Collider>();
        //debugGO = GameObject.FindGameObjectWithTag("DebugDepth");
        //depthValueText = debugGO.GetComponent<Text>();
    }

    private void OnEnable()
    {
        ballRB = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        ballcollider = GetComponent<Collider>();
        debugGO = GameObject.FindGameObjectWithTag("DebugDepth");
        depthValueText = debugGO.GetComponent<Text>();
        //ballRB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameStatus == GameManager.GameStatus.PLAYING)
        {
            gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
            ThrowBallWithClick(gesture);
            GrabBallWithPinch(gesture);
        }

    }


    private void GrabBallWithPinch(GestureInfo gesture)
    {
        if (gesture.mano_gesture_trigger == pinchTrigger)
        {
            depthStart = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.bounding_box.width;
        }
    }

    float depthModifyer;
    float depthModifyerMultiplyer = 6;
    float minDepthValue = 0.2f, maxDepthValue = 1.5f;

    private void ThrowBallWithClick(GestureInfo gesture)
    {
        if (gesture.mano_gesture_trigger == throwTrigger)
        {
            depthRelease = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.bounding_box.width;
            depthModifyer = Mathf.Clamp(((depthStart - depthRelease) * depthModifyerMultiplyer), minDepthValue, maxDepthValue);
            
            depthValueText.text = "POWER: " + depthModifyer.ToString();
            ThrowBeerPongBall();
        }
    }

    Rigidbody ballRB;
    float timeToSpawnNewBall = 1.5f;
    float timeToDestroyBall = 0.5f;
    float forceModifyer = 0.35f;

    private void ThrowBeerPongBall()
    {
        trailRenderer.emitting = true;
        ballRB.transform.parent = null;
        ballRB.isKinematic = false;
        ballRB.AddForce(Camera.main.transform.forward * forceModifyer * depthModifyer + (Camera.main.transform.up * 0.15f), ForceMode.Impulse);
        StartCoroutine(NewBallFromPool(timeToSpawnNewBall));
        GameManager.Instance.currentGameStatus = GameManager.GameStatus.BETWEENSHOTS;
    }

    Vector3 ballStartPosition = new Vector3(0, 0, 0.6f);
    GameObject ballParent;
    string ballParentTag = "BallParent";

    IEnumerator NewBallFromPool(float delay)
    {
        yield return new WaitForSeconds(timeToSpawnNewBall);
        ballParent = GameObject.FindGameObjectWithTag(ballParentTag);
        BallPooler.Instance.NewBall(ballStartPosition, ballParent.transform, Quaternion.identity);

        yield return new WaitForSeconds(timeToDestroyBall);
        GameManager.Instance.currentGameStatus = GameManager.GameStatus.PLAYING;
        trailRenderer.emitting = false;
        meshRenderer.enabled = true;
        ballcollider.enabled = true;

        gameObject.SetActive(false);
    }

}
