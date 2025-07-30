using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject slicePlane;
    public GameObject testObj;
    Mesh testObj_shardmesh;
    List<Vector3> convexHull = new List<Vector3>();
    private void Start()
    {
        testObj_shardmesh = testObj.GetComponent<MeshFilter>().sharedMesh;
    }
    List<GameObject> getChildren() {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parentObj.transform)
        {
            if (child.gameObject.activeSelf)
            {
                children.Add(child.gameObject);
            }
        }

        return children;
    }

    void CutObject(GameObject victim) {
        if (victim != null) {
            SlicedHull hull = victim.Slice(slicePlane.transform.position, slicePlane.transform.up);
            //SlicedHull hull = victim.Slice(slicePlane.transform.position, slicePlane.transform.up);
            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(victim);
                GameObject lowerHull = hull.CreateLowerHull(victim);

                victim.SetActive(false);
            }
        }
    }
    // private void OnDrawGizmos()
    // {
    //     EzySlice.Plane plane = new EzySlice.Plane(slicePlane.transform.position,slicePlane.transform.up);
    //     convexHull = Slicer.GetConvexHull(testObj_shardmesh,plane);
    //     if (convexHull != null)
    //     {
    //         foreach (Vector3 point in convexHull)
    //         {
    //             Gizmos.DrawSphere(point, 0.1f);
    //         }
    //     }
    //     else {
    //         testObj_shardmesh = testObj.GetComponent<MeshFilter>().sharedMesh; 
    //     }
    // }
    void CutAllChildren() { 
        List<GameObject> children = getChildren();
        foreach (GameObject victim in children)
        {
            CutObject(victim);
        }
        slicePlane.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CutAllChildren();
        }
    }
}
