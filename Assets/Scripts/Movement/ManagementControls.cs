using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementControls : MonoBehaviour
{
    public static ManagementControls Instance;
    [SerializeField] Camera cam;
    [SerializeField] float speed = 8, scrollSpeed = 0.1f;
    [SerializeField] Vector3 mins, maxs;
    public CharMovement selectedChar;

    void Awake()
    {
        Instance = this;
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        //take inputs on the camera movement
        if (Controls.Up)
        {
            move += Vector3.forward * Time.deltaTime * speed;
        }
        if (Controls.Down)
        {
            move -= Vector3.forward * Time.deltaTime * speed;
        }
        if (Controls.Left)
        {
            move -= Vector3.right * Time.deltaTime * speed;
        }
        if (Controls.Right)
        {
            move += Vector3.right * Time.deltaTime * speed;
        }
        //move faster when zoomed out
        move *= cam.transform.position.y / 10;
        //only apply zoom if in y ranges
        Vector3 zoom = Controls.MouseWheel * cam.transform.forward * scrollSpeed * Time.deltaTime;
        if (cam.transform.position.y + zoom.y > mins.y && cam.transform.position.y + zoom.y < maxs.y)
        {
            move += zoom;
        }
        //apply the move to the camera
        cam.transform.position += move;
        //apply mins and maxs
        cam.transform.position = new Vector3(
            Mathf.Clamp(cam.transform.position.x, mins.x, maxs.x),
            Mathf.Clamp(cam.transform.position.y, mins.y, maxs.y),
            Mathf.Clamp(cam.transform.position.z, mins.z, maxs.z)
        );

        //raycast when the mouse is clicked to see what is hit
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 99, 1 << 8))
        {
            Interactible interactible = hit.transform.GetComponent<Interactible>();
            //transform.position = hit.point;
            if (Controls.Click)
            {
                //click
                if (interactible != null)
                {
                    interactible?.Interact();
                }
                if (selectedChar != null)
                {
                    selectedChar.target = new Vector3(hit.point.x, 0, hit.point.z);
                }
            }
            else if (Controls.RightClick)
            {
                //right click
            }
            else
            {
                //hover
            }
        }
        else
        {
            Debug.Log("Didn't hit land with click raycast");
        }
    }

    public void SelectChar(CharMovement selection)
    {
        if (selectedChar != null)
        {
            if(selectedChar == selection)
            {
                cam.transform.position = selection.transform.position - cam.transform.forward * 10;
            }
            selectedChar.Deselect();
        }
        selectedChar = selection;
        selectedChar.Select();
    }
}