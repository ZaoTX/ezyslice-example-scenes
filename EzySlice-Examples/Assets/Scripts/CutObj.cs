using UnityEngine;
using EzySlice;

// Attach to an object to cut 
public class CutObj : MonoBehaviour
{
    public Transform slicePlane;         
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cut();
        }
    }

    void Cut()
    { 
        SlicedHull hull = gameObject.Slice(slicePlane.position, slicePlane.up);
        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(gameObject);
            GameObject lowerHull = hull.CreateLowerHull(gameObject);

            gameObject.SetActive(false);
        }
    }
     
}
