using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraFocus : MonoBehaviour
{
    float baseSize;
    float dragSpeed = 100f;
    float zoomSpeed = 10f;
    private Vector3 dragOrigin;
    private Camera camera;
    public static bool toggleCreatureView = false;
    public static Transform targetedCreature;
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(dragOrigin), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.name == "Creature" && !toggleCreatureView)
            {
                toggleCreatureView = true;
                targetedCreature = hit.collider.transform;
            }
            else
            {
                toggleCreatureView = false;
            }
            return;
        }
        if (toggleCreatureView)
        {
            transform.position = targetedCreature.position - new Vector3(0,0,10);
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
            transform.Translate(move * Time.deltaTime);
        }

        if (camera.orthographicSize >= 0)
        {
            camera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        else
        {
            camera.orthographicSize = 0;
        }
    }
}
