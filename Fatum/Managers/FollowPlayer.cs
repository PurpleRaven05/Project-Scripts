using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform self;
    public float distance = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        self.position = new Vector3(player.position.x, player.position.y,  player.position.z -distance);
        self.rotation = player.rotation;
    }
}
