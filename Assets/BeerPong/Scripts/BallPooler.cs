using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{
    #region Singelton
    private static BallPooler _instance;
    public static BallPooler Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        InitializeSingleton();
    }

    /// <summary>
    /// if the private static variable of the instance is not assigned, assign it to this script. Otherwise take care of it, destroy/deactivate etc.
    /// </summary>
    private void InitializeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            //This is optional. You can Destroy it, you can Set it active false, or you can remove the script.
            Destroy(this.gameObject);
        }
    }
    #endregion

    // the prefab of the ball
    public GameObject ballPrefab;
    // the list where the balls are stored
    List<GameObject> ballList;

    /// <summary>
    /// Create the object pooling list and adds 10 balls to that list and rename them to Ball"
    /// </summary>
    void Start()
    {
        ballList = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject objectToSpawn = (GameObject)Instantiate(ballPrefab);

            objectToSpawn.name = "Ball";
            objectToSpawn.SetActive(false);
            ballList.Add(objectToSpawn);
        }
    }

    /// <summary>
    /// when this method are called a deactivated ball vill be set to shoot position and set active.
    /// </summary>
    /// <param name="position">the position where the object will spawn</param>
    /// <param name="parent">the parent transform where the object will be child of</param>
    /// <param name="rotation"> the rotation of the spawned object</param>
    public void NewBall(Vector3 position, Transform parent, Quaternion rotation)
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            if (!ballList[i].activeInHierarchy)
            {
                ballList[i].transform.SetParent(parent);
                ballList[i].transform.localPosition = position;
                ballList[i].transform.rotation = rotation;
                ballList[i].SetActive(true);
                Rigidbody rb = ballList[i].GetComponent<Rigidbody>();
                rb.isKinematic = true;
                break;
            }
        }
    }
}
