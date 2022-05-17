using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] private GameObject player;
    AnimationToRagdoll ragdollActive;
    private void Awake()
    {
        player = this.gameObject;
        ragdollActive = player.GetComponent<AnimationToRagdoll>();
    }
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(player.gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset =player.gameObject.transform.position - GetMouseAsWorldPoint();
        ragdollActive.OnDown();

    }



    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;
        

        mousePoint.z = mZCoord;


        return Camera.main.ScreenToWorldPoint(mousePoint);

    }
    void OnMouseDrag()
    {
        
        player.transform.position = GetMouseAsWorldPoint() + mOffset;
    }
    private void OnMouseUp()
    {
        ragdollActive.OnUp();
    }
}

