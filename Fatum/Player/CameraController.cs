using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera _camera;
    public enum Camera_Type{FREE_LOOK, LOCKED}
    public Camera_Type camType = Camera_Type.FREE_LOOK;
    [Range(0.1f,100.0f)]
    public float _sensitivity;
    public bool _invertXAxis;
    public bool _invertYAxis;
    public Transform lookAt;
    #region CameraTransition
    private bool inTransition;
    private float transitionTime = 0.0f;
    public Transform aimCam;
    #endregion
    // Start is called before the first frame update
    private void Awake(){
        if(camType ==Camera_Type.LOCKED){
            _camera.transform.parent = transform;
        }
    }
    private void FixedUpdate(){
        if(! inTransition){
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            //Settings
            h = (_invertXAxis) ? (-h) :h;
            v = (_invertYAxis) ? (-v) :v;
            
            //rotate the camera around the character
            if(h !=0){
                //Horizontal
                if(camType == Camera_Type.LOCKED) transform.Rotate(Vector3.up, h*90*_sensitivity*Time.deltaTime);
                else if(camType == Camera_Type.FREE_LOOK) _camera.transform.RotateAround(transform.position, transform.up, h*90*_sensitivity*Time.deltaTime);
            }
            if(v !=0){
                _camera.transform.RotateAround(transform.position, transform.right, v*90*_sensitivity*Time.deltaTime);
                
            }
            _camera.transform.LookAt(lookAt);
            //
            Vector3 ea = _camera.transform.rotation.eulerAngles;
            _camera.transform.rotation = Quaternion.Euler(new Vector3(ea.x, ea.y, 0));
        }
    }
    public Camera GetCamera(){return _camera;}

}
