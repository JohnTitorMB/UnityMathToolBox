using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryUtilities : MonoBehaviour
{
    [Header("Intersection point point")]
    public float T1_threshold = 0.0001f;
    public Transform T1_pointA;
    public Transform T1_pointB;
    public Transform T1_resultPoint;

    [Header("Intersection point line")]
    public float T2_threshold = 0.0001f;
    public Transform T2_point;
    public Transform T2_linePointA;
    public Transform T2_linePointB;
    public Transform T2_resultPoint;

    [Header("Intersection line line")]
    public float T3_threshold = 0.0001f;
    public Transform T3_lineAPointA;
    public Transform T3_lineAPointB;
    public Transform T3_lineBPointA;
    public Transform T3_lineBPointB;
    public Transform T3_resultPoint;
    public Transform T3_resultPoint2;


    [Header("Intersection line plane")]
    public Transform T4_linePointA;
    public Transform T4_linePointB;
    public Transform T4_plane;
    public Transform T4_resultPoint;

    [Header("Intersection line quad")]
    public Transform T5_linePointA;
    public Transform T5_linePointB;
    public Transform T5_localPointA;
    public Transform T5_localPointB;
    public Transform T5_localPointC;
    public Transform T5_quad;
    public Transform T5_resultPoint;

    [Header("Intersection line sphere")]
    public Transform T6_linePointA;
    public Transform T6_linePointB;
    public Transform T6_sphere;
    public Transform T6_resultPoint;
    public Transform T6_resultPoint2;

    [Header("Intersection line InfiniteCylinder")]
    public Transform T7_linePointA;
    public Transform T7_linePointB;
    public Transform T7_infiniteCylinder;
    public Transform T7_resultPoint;
    public Transform T7_resultPoint2;

    [Header("Intersection line Cylinder")]
    public Transform T8_linePointA;
    public Transform T8_linePointB;
    public Transform T8_cylinder;
    public Transform T8_resultPoint;
    public Transform T8_resultPoint2;

    [Header("Intersection line Capsule")]
    public Transform T9_linePointA;
    public Transform T9_linePointB;
    public Transform T9_capsule;
    public Transform T9_resultPoint;
    public Transform T9_resultPoint2;

    [Header("Intersection line MinkowskiBoxSphere")]
    public float T10_radius = 0.5f;
    public Vector3 T10_scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Transform T10_linePointA;
    public Transform T10_linePointB;
    public Transform T10_origin;
    public Transform T10_resultPoint;
    public Transform T10_resultPoint2;
    public Transform[] T10_faces;
    public Transform[] T10_capsules;
    public Transform[] T10_spheres;

    // Update is called once per frame
    void Update()
    {

        
        Vector3 T1_Result = Vector3.zero;
        if (CollisionUtilily.CheckPointPointIntersection(T1_pointA.position, T1_pointB.position, T1_threshold))
        {
            T1_resultPoint.gameObject.SetActive(true);
        }
        else
            T1_resultPoint.gameObject.SetActive(false);

        Vector3 T2_Result = Vector3.zero;

        if (CollisionUtilily.CheckLinePointIntersection(T2_linePointA.position, T2_linePointB.position, T2_point.position, T2_threshold, ref T2_Result))
        {
            T2_resultPoint.gameObject.SetActive(true);
            T2_resultPoint.position = T2_Result;
            Debug.DrawLine(T2_linePointA.position, T2_linePointB.position, Color.green);
        }
        else
        {
            Debug.DrawLine(T2_linePointA.position, T2_linePointB.position, Color.red);
            T2_resultPoint.gameObject.SetActive(false);
        }

        Vector3 T3_Result = Vector3.zero;
        Vector3 T3_Result2 = Vector3.zero;
        if (CollisionUtilily.CheckLineLineIntersection(T3_lineAPointA.position, T3_lineAPointB.position, T3_lineBPointA.position, T3_lineBPointB.position, T3_threshold, ref T3_Result, ref T3_Result2))
        {
            Debug.DrawLine(T3_lineAPointA.position, T3_lineAPointB.position, Color.green);
            Debug.DrawLine(T3_lineBPointA.position, T3_lineBPointB.position, Color.green);

            T3_resultPoint.gameObject.SetActive(true);
            T3_resultPoint.position = T3_Result;

            T3_resultPoint2.gameObject.SetActive(true);
            T3_resultPoint2.position = T3_Result2;
        }
        else
        {
            Debug.DrawLine(T3_lineAPointA.position, T3_lineAPointB.position, Color.red);
            Debug.DrawLine(T3_lineBPointA.position, T3_lineBPointB.position, Color.red);
            T3_resultPoint.gameObject.SetActive(false);
            T3_resultPoint2.gameObject.SetActive(false);
        }

        Vector3 T4_Result = Vector3.zero;
        if (CollisionUtilily.CheckLinePlanIntersection(T4_linePointA.position, T4_linePointB.position, T4_plane.position, T4_plane.up, ref T4_Result))
        {
            T4_resultPoint.transform.position = T4_Result;
            T4_resultPoint.gameObject.SetActive(true);

            Debug.DrawLine(T4_linePointA.position, T4_linePointB.position, Color.green);

            MeshRenderer planRenderer = T4_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0,1,0,0.2f);
        }
        else
        {
            T4_resultPoint.transform.position = T4_Result;
            T4_resultPoint.gameObject.SetActive(true);

            Debug.DrawLine(T4_linePointA.position, T4_linePointB.position, Color.red);
            MeshRenderer planRenderer = T4_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T5_Result = Vector3.zero;


        Vector3 T5_pointA = T5_quad.transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0));
        Vector3 T5_pointB = T5_quad.transform.TransformPoint(new Vector3(0.5f, -0.5f, 0));
        Vector3 T5_pointC = T5_quad.transform.TransformPoint(new Vector3(-0.5f, 0.5f, 0));

        Debug.DrawRay(T5_pointA, Vector3.up * 100, Color.blue);
        Debug.DrawRay(T5_pointB, Vector3.up * 100, Color.black);
        Debug.DrawRay(T5_pointC, Vector3.up * 100);
        if (CollisionUtilily.CheckLineQuadIntersection(T5_linePointA.position, T5_linePointB.position, T5_pointA, T5_pointB, T5_pointC, ref T5_Result))
        {
            T5_resultPoint.transform.position = T5_Result;
            T5_resultPoint.gameObject.SetActive(true);        

            Debug.DrawLine(T5_linePointA.position, T5_linePointB.position, Color.green);

            MeshRenderer planRenderer = T5_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T5_resultPoint.gameObject.SetActive(false);

            Debug.DrawLine(T5_linePointA.position, T5_linePointB.position, Color.red);
            MeshRenderer planRenderer = T5_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }
        Vector3 T6_Result = Vector3.zero;
        Vector3 T6_Result2 = Vector3.zero;

        int resultCount = CollisionUtilily.CheckLineSphereIntersection(T6_linePointA.position, T6_linePointB.position, T6_sphere.position, T6_sphere.localScale.x / 2.0f, ref T6_Result, ref T6_Result2);
        if (resultCount > 0)
        {
            T6_resultPoint.transform.position = T6_Result;
            T6_resultPoint.gameObject.SetActive(true);

            if(resultCount > 1)
            {
                T6_resultPoint2.transform.position = T6_Result2;
                T6_resultPoint2.gameObject.SetActive(true);
            }
            else
                T6_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T6_linePointA.position, T6_linePointB.position, Color.green);

            MeshRenderer planRenderer = T6_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T6_resultPoint.gameObject.SetActive(false);
            T6_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T6_linePointA.position, T6_linePointB.position, Color.red);
            MeshRenderer planRenderer = T6_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T7_Result = Vector3.zero;
        Vector3 T7_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckLineInfiniteCylinderIntersection(T7_linePointA.position, T7_linePointB.position, T7_infiniteCylinder.position, T7_infiniteCylinder.up, T7_infiniteCylinder.localScale.x / 2.0f, ref T7_Result, ref T7_Result2);
        if (resultCount > 0)
        {
            T7_resultPoint.transform.position = T7_Result;
            T7_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T7_resultPoint2.transform.position = T7_Result2;
                T7_resultPoint2.gameObject.SetActive(true);
            }
            else
                T7_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T7_linePointA.position, T7_linePointB.position, Color.green);

            MeshRenderer planRenderer = T7_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T7_resultPoint.gameObject.SetActive(false);
            T7_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T7_linePointA.position, T7_linePointB.position, Color.red);
            MeshRenderer planRenderer = T7_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T8_Result = Vector3.zero;
        Vector3 T8_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckLineCylinderIntersection(T8_linePointA.position, T8_linePointB.position, T8_cylinder.position, T8_cylinder.up, T8_cylinder.localScale.x / 2.0f, T8_cylinder.localScale.y * 2.0f, ref T8_Result, ref T8_Result2);
        if (resultCount > 0)
        {
            T8_resultPoint.transform.position = T8_Result;
            T8_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T8_resultPoint2.transform.position = T8_Result2;
                T8_resultPoint2.gameObject.SetActive(true);
            }
            else
                T8_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T8_linePointA.position, T8_linePointB.position, Color.green);

            MeshRenderer planRenderer = T8_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T8_resultPoint.gameObject.SetActive(false);
            T8_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T8_linePointA.position, T8_linePointB.position, Color.red);
            MeshRenderer planRenderer = T8_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T9_Result = Vector3.zero;
        Vector3 T9_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckLineCapsuleIntersection(T9_linePointA.position, T9_linePointB.position, T9_capsule.position, T9_capsule.up, T9_capsule.localScale.x / 2.0f, T9_capsule.localScale.y * 2.0f - T9_capsule.localScale.x, ref T9_Result, ref T9_Result2);
        if (resultCount > 0)
        {
            T9_resultPoint.transform.position = T9_Result;
            T9_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T9_resultPoint2.transform.position = T9_Result2;
                T9_resultPoint2.gameObject.SetActive(true);
            }
            else
                T9_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T9_linePointA.position, T9_linePointB.position, Color.green);

            MeshRenderer planRenderer = T9_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T9_resultPoint.gameObject.SetActive(false);
            T9_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T9_linePointA.position, T9_linePointB.position, Color.red);
            MeshRenderer planRenderer = T9_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }


        Vector3 halfSize = T10_scale / 2.0f;
        Vector3 size = T10_scale;
        T10_faces[0].localPosition = Vector3.down * (halfSize.y + T10_radius);
        T10_faces[1].localPosition = Vector3.up * (halfSize.y + T10_radius);
        T10_faces[2].localPosition = Vector3.left * (halfSize.x + T10_radius);
        T10_faces[3].localPosition = Vector3.right * (halfSize.x + T10_radius);
        T10_faces[4].localPosition = Vector3.back * (halfSize.z + T10_radius);
        T10_faces[5].localPosition = Vector3.forward * (halfSize.z + T10_radius);


        T10_faces[0].localScale = new Vector3(size.x, size.z, 1);
        T10_faces[1].localScale = new Vector3(size.x, size.z, 1);
        
        T10_faces[1].localScale = new Vector3(1.0f, size.y, size.z);
        T10_faces[2].localScale = new Vector3(1.0f, size.y, size.z);

        T10_faces[3].localScale = new Vector3(size.x, size.y, 1.0f);
        T10_faces[4].localScale = new Vector3(size.x, size.y, 1.0f);




        T10_capsules[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x;
        T10_capsules[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x;
        T10_capsules[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x;
        T10_capsules[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x;

        T10_capsules[4].localPosition = Vector3.back * halfSize.z + Vector3.up * halfSize.y;
        T10_capsules[5].localPosition = Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T10_capsules[6].localPosition = Vector3.forward * halfSize.z + Vector3.up * halfSize.y;
        T10_capsules[7].localPosition = Vector3.left * halfSize.z + Vector3.up * halfSize.y;

        T10_capsules[8].localPosition = Vector3.back * halfSize.z + Vector3.down * halfSize.y;
        T10_capsules[9].localPosition = Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T10_capsules[10].localPosition = Vector3.forward * halfSize.z + Vector3.down * halfSize.y;
        T10_capsules[11].localPosition = Vector3.left * halfSize.z + Vector3.down * halfSize.y;

        float radius2 = T10_radius * 2.0f;
        T10_capsules[0].localScale = new Vector3(radius2, size.y/2.0f, radius2);
        T10_capsules[1].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T10_capsules[2].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T10_capsules[3].localScale = new Vector3(radius2, size.y / 2.0f, radius2);

        T10_capsules[4].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T10_capsules[5].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T10_capsules[6].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T10_capsules[7].localScale = new Vector3(radius2, size.z / 2.0f, radius2);

        T10_capsules[8].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T10_capsules[9].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T10_capsules[10].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T10_capsules[11].localScale = new Vector3(radius2, size.z / 2.0f, radius2);


        T10_spheres[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;
        T10_spheres[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T10_spheres[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T10_spheres[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;

        T10_spheres[4].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;
        T10_spheres[5].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T10_spheres[6].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T10_spheres[7].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;

        T10_spheres[0].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[1].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[2].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[3].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[4].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[5].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[6].localScale = new Vector3(radius2, radius2, radius2);
        T10_spheres[7].localScale = new Vector3(radius2, radius2, radius2);



        Vector3 T10_Result = Vector3.zero;
        Vector3 T10_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckIntersectionLineMinkowskiBoxSphere(T10_linePointA.position, T10_linePointB.position, T10_origin.position, T10_scale, T10_origin.rotation, T10_radius, ref T10_Result, ref T10_Result2);
        if (resultCount > 0)
        {
            T10_resultPoint.transform.position = T10_Result;
            T10_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T10_resultPoint2.transform.position = T10_Result2;
                T10_resultPoint2.gameObject.SetActive(true);
            }
            else
                T10_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T10_linePointA.position, T10_linePointB.position, Color.green);


            foreach(Transform tr in T10_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T10_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T10_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }


        }
        else
        {
            T10_resultPoint.gameObject.SetActive(false);
            T10_resultPoint2.gameObject.SetActive(false);


            foreach (Transform tr in T10_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T10_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T10_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

        }


    }
}


