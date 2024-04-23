using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyescript : MonoBehaviour
{
    List<Vector2> rays = new List<Vector2>();
    RaycastHit2D hit;
    public static float sight = 2;
    // Start is called before the first frame update
    void Start()
    {
        addRay(0, 1);
        addRay(.25f, 1f);
        addRay(-.25f, 1f);
    }
    // Update is called once per frame
    public List<float> see()
    {
        List<float> vals = new List<float>();
        foreach(Vector2 ray in rays)
        {
            hit = Physics2D.Raycast(transform.position + transform.TransformDirection(0,1,0), transform.TransformDirection(ray), sight);
            if (hit.collider != null)
            {
                Color hitColor = hit.collider.transform.gameObject.GetComponent<SpriteRenderer>().color;
                vals.Add(hitColor.r * 255);
                vals.Add(hitColor.g * 255);
                vals.Add(hitColor.b * 255);
                vals.Add(Vector2.Distance(hit.point, transform.position));
                Debug.DrawRay(transform.position, transform.TransformDirection(ray) * sight, hitColor);
            }
            else
            {
                vals.Add(0);
                vals.Add(0);
                vals.Add(0);
                vals.Add(0);
            }
        }
        return vals;
    }
    void addRay(float x, float y)
    {
        rays.Add(new Vector2(x, y));
    }
}
