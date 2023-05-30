using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RorateHammer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool rotate; // do you want it to rotate?
	public float rotationSpeed;
    public Quaternion startQuaternion;
// Start is called before the first frame update
    void Start()
    {
        startQuaternion = transform.localRotation;
    }

    // Update is called once per frame
    void Update () {

		if (rotate)
			transform.localRotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime*1f);
            //Physics.SyncTransforms();

	}

}
