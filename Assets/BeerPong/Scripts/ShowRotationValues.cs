using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRotationValues : MonoBehaviour
{
    Text mytext;

    [SerializeField]
    GameObject arcamera;

    [SerializeField]
    GameObject nestedCamera;
    // Start is called before the first frame update
    void Start()
    {
        mytext = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //the ARCamera does not move
        //  mytext.text = "ARCamera " + arcamera.transform.rotation.ToString() + " Nested Camera " + nestedCamera.transform.rotation.ToString();
    }
}
