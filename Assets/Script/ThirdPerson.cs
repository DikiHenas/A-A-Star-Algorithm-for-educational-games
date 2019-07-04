using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour {

    public TouchField touchField;
    public float cameramoveSpeed=120.0f;
    public GameObject CameraFollowObject;
    Vector3 FollowPos;
    public float Y_ANGLE_MIN= 0.0f;
    public float Y_ANGLE_MAX = 40.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject playerObj;
    public float camDistancexToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    public float rotY = 0.0f;
    public float rotX = 0.0f;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        
	}

    // Update is called once per frame
    void Update() {
        //if (Input.mousePosition.x >= Screen.width/ 2)
        //{

        mouseX = touchField.TouchDist.x/2;//Input.GetAxis("Mouse X");
        mouseY = touchField.TouchDist.y/2;// Input.GetAxis("Mouse Y");           
        //}
        //else {
        //    mouseX = 0;
        //    mouseY = 0;
        //}
            finalInputX = mouseX/2;
            finalInputZ = mouseY/2;        
            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;        
            rotX = Mathf.Clamp(rotX, -Y_ANGLE_MIN, Y_ANGLE_MAX);        
            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        Quaternion heroRotation = Quaternion.Euler(0, rotY, 0);
        transform.rotation = localRotation;
        playerObj.transform.rotation = heroRotation;


    }
    private void LateUpdate()
    {
        CameraUpdater();
    }

   void CameraUpdater()
    {
        //target
        Transform target = CameraFollowObject.transform;
        //gerak target
        float step = cameramoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,target.position,step);


    }
}
