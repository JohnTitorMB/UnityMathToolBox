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

    [Header("Intersection point segment")]
    public float T2_threshold = 0.0001f;
    public Transform T2_point;
    public Transform T2_segmentPointA;
    public Transform T2_segmentPointB;
    public Transform T2_resultPoint;

    [Header("Intersection segment segment")]
    public float T3_threshold = 0.0001f;
    public Transform T3_segmentAPointA;
    public Transform T3_segmentAPointB;
    public Transform T3_segmentBPointA;
    public Transform T3_segmentBPointB;
    public Transform T3_resultPoint;
    public Transform T3_resultPoint2;


    [Header("Intersection segment plane")]
    public Transform T4_segmentPointA;
    public Transform T4_segmentPointB;
    public Transform T4_plane;
    public Transform T4_resultPoint;

    [Header("Intersection segment quad")]
    public Transform T5_segmentPointA;
    public Transform T5_segmentPointB;
    public Transform T5_quad;
    public Transform T5_resultPoint;

    [Header("Intersection segment sphere")]
    public Transform T6_segmentPointA;
    public Transform T6_segmentPointB;
    public Transform T6_sphere;
    public Transform T6_resultPoint;
    public Transform T6_resultPoint2;

    [Header("Intersection segment InfiniteCylinder")]
    public Transform T7_segmentPointA;
    public Transform T7_segmentPointB;
    public Transform T7_infiniteCylinder;
    public Transform T7_resultPoint;
    public Transform T7_resultPoint2;

    [Header("Intersection segment Cylinder")]
    public Transform T8_segmentPointA;
    public Transform T8_segmentPointB;
    public Transform T8_cylinder;
    public Transform T8_resultPoint;
    public Transform T8_resultPoint2;

    [Header("Intersection segment Capsule")]
    public Transform T9_segmentPointA;
    public Transform T9_segmentPointB;
    public Transform T9_capsule;
    public Transform T9_resultPoint;
    public Transform T9_resultPoint2;

    [Header("Intersection segment MinkowskiBoxSphere")]
    public float T10_radius = 0.5f;
    public Vector3 T10_scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Transform T10_segmentPointA;
    public Transform T10_segmentPointB;
    public Transform T10_origin;
    public Transform T10_resultPoint;
    public Transform T10_resultPoint2;
    public Transform[] T10_faces;
    public Transform[] T10_capsules;
    public Transform[] T10_spheres;

    [Header("Intersection ray segment")]
    public float T11_threshold = 0.0001f;
    public Transform T11_rayPointA;
    public Transform T11_rayPointB;
    public Transform T11_segmentPointA;
    public Transform T11_segmentPointB;
    public Transform T11_resultPoint;
    public Transform T11_resultPoint2;

    [Header("Intersection ray ray")]
    public float T12_threshold = 0.0001f;
    public Transform T12_rayAPointA;
    public Transform T12_rayAPointB;
    public Transform T12_rayBPointA;
    public Transform T12_rayBPointB;
    public Transform T12_resultPoint;
    public Transform T12_resultPoint2;

    [Header("Intersection ray plan")]
    public Transform T13_rayPointA;
    public Transform T13_rayPointB;
    public Transform T13_plane;
    public Transform T13_resultPoint;

    [Header("Intersection ray quad")]
    public Transform T14_rayPointA;
    public Transform T14_rayPointB;
    public Transform T14_quad;
    public Transform T14_resultPoint;

    [Header("Intersection ray sphere")]
    public Transform T15_rayPointA;
    public Transform T15_rayPointB;
    public Transform T15_sphere;
    public Transform T15_resultPoint;
    public Transform T15_resultPoint2;

    [Header("Intersection ray InfiniteCylinder")]
    public Transform T16_rayPointA;
    public Transform T16_rayPointB;
    public Transform T16_infiniteCylinder;
    public Transform T16_resultPoint;
    public Transform T16_resultPoint2;

    [Header("Intersection ray Cylinder")]
    public Transform T17_rayPointA;
    public Transform T17_rayPointB;
    public Transform T17_cylinder;
    public Transform T17_resultPoint;
    public Transform T17_resultPoint2;

    [Header("Intersection ray Capsule")]
    public Transform T18_rayPointA;
    public Transform T18_rayPointB;
    public Transform T18_capsule;
    public Transform T18_resultPoint;
    public Transform T18_resultPoint2;

    [Header("Intersection ray MinkowskiBoxSphere")]
    public float T19_radius = 0.5f;
    public Vector3 T19_scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Transform T19_rayPointA;
    public Transform T19_rayPointB;
    public Transform T19_origin;
    public Transform T19_resultPoint;
    public Transform T19_resultPoint2;
    public Transform[] T19_faces;
    public Transform[] T19_capsules;
    public Transform[] T19_spheres;

    [Header("Intersection line segment")]
    public float T20_threshold = 0.0001f;
    public Transform T20_linePointA;
    public Transform T20_linePointB;
    public Transform T20_segmentPointA;
    public Transform T20_segmentPointB;
    public Transform T20_resultPoint;
    public Transform T20_resultPoint2;

    [Header("Intersection line ray")]
    public float T21_threshold = 0.0001f;
    public Transform T21_linePointA;
    public Transform T21_linePointB;
    public Transform T21_rayPointA;
    public Transform T21_rayPointB;
    public Transform T21_resultPoint;
    public Transform T21_resultPoint2;

    [Header("Intersection line line")]
    public float T22_threshold = 0.0001f;
    public Transform T22_lineAPointA;
    public Transform T22_lineAPointB;
    public Transform T22_lineBPointA;
    public Transform T22_lineBPointB;
    public Transform T22_resultPoint;
    public Transform T22_resultPoint2;

    [Header("Intersection line plan")]
    public Transform T23_linePointA;
    public Transform T23_linePointB;
    public Transform T23_plane;
    public Transform T23_resultPoint;

    [Header("Intersection line quad")]
    public Transform T24_linePointA;
    public Transform T24_linePointB;
    public Transform T24_quad;
    public Transform T24_resultPoint;

    [Header("Intersection line sphere")]
    public Transform T25_linePointA;
    public Transform T25_linePointB;
    public Transform T25_sphere;
    public Transform T25_resultPoint;
    public Transform T25_resultPoint2;

    [Header("Intersection line InfiniteCylinder")]
    public Transform T26_linePointA;
    public Transform T26_linePointB;
    public Transform T26_infiniteCylinder;
    public Transform T26_resultPoint;
    public Transform T26_resultPoint2;

    [Header("Intersection line Cylinder")]
    public Transform T27_linePointA;
    public Transform T27_linePointB;
    public Transform T27_cylinder;
    public Transform T27_resultPoint;
    public Transform T27_resultPoint2;

    [Header("Intersection line Capsule")]
    public Transform T28_linePointA;
    public Transform T28_linePointB;
    public Transform T28_capsule;
    public Transform T28_resultPoint;
    public Transform T28_resultPoint2;

    [Header("Intersection line MinkowskiBoxSphere")]
    public float T29_radius = 0.5f;
    public Vector3 T29_scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Transform T29_linePointA;
    public Transform T29_linePointB;
    public Transform T29_origin;
    public Transform T29_resultPoint;
    public Transform T29_resultPoint2;
    public Transform[] T29_faces;
    public Transform[] T29_capsules;
    public Transform[] T29_spheres;





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

        if (CollisionUtilily.CheckSegmentPointIntersection(T2_segmentPointA.position, T2_segmentPointB.position, T2_point.position, T2_threshold, ref T2_Result))
        {
            T2_resultPoint.gameObject.SetActive(true);
            T2_resultPoint.position = T2_Result;
            Debug.DrawLine(T2_segmentPointA.position, T2_segmentPointB.position, Color.green);
        }
        else
        {
            Debug.DrawLine(T2_segmentPointA.position, T2_segmentPointB.position, Color.red);
            T2_resultPoint.gameObject.SetActive(false);
        }

        Vector3 T3_Result = Vector3.zero;
        Vector3 T3_Result2 = Vector3.zero;
        if (CollisionUtilily.CheckSegmentSegmentIntersection(T3_segmentAPointA.position, T3_segmentAPointB.position, T3_segmentBPointA.position, T3_segmentBPointB.position, T3_threshold, ref T3_Result, ref T3_Result2))
        {
            Debug.DrawLine(T3_segmentAPointA.position, T3_segmentAPointB.position, Color.green);
            Debug.DrawLine(T3_segmentBPointA.position, T3_segmentBPointB.position, Color.green);

            T3_resultPoint.gameObject.SetActive(true);
            T3_resultPoint.position = T3_Result;

            T3_resultPoint2.gameObject.SetActive(true);
            T3_resultPoint2.position = T3_Result2;
        }
        else
        {
            Debug.DrawLine(T3_segmentAPointA.position, T3_segmentAPointB.position, Color.red);
            Debug.DrawLine(T3_segmentBPointA.position, T3_segmentBPointB.position, Color.red);
            T3_resultPoint.gameObject.SetActive(false);
            T3_resultPoint2.gameObject.SetActive(false);
        }

        Vector3 T4_Result = Vector3.zero;
        if (CollisionUtilily.CheckSegmentPlanIntersection(T4_segmentPointA.position, T4_segmentPointB.position, T4_plane.position, T4_plane.up, ref T4_Result))
        {
            T4_resultPoint.transform.position = T4_Result;
            T4_resultPoint.gameObject.SetActive(true);

            Debug.DrawLine(T4_segmentPointA.position, T4_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T4_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0,1,0,0.2f);
        }
        else
        {
            T4_resultPoint.transform.position = T4_Result;
            T4_resultPoint.gameObject.SetActive(true);

            Debug.DrawLine(T4_segmentPointA.position, T4_segmentPointB.position, Color.red);
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
        if (CollisionUtilily.CheckSegmentQuadIntersection(T5_segmentPointA.position, T5_segmentPointB.position, T5_pointA, T5_pointB, T5_pointC, ref T5_Result))
        {
            T5_resultPoint.transform.position = T5_Result;
            T5_resultPoint.gameObject.SetActive(true);        

            Debug.DrawLine(T5_segmentPointA.position, T5_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T5_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T5_resultPoint.gameObject.SetActive(false);

            Debug.DrawLine(T5_segmentPointA.position, T5_segmentPointB.position, Color.red);
            MeshRenderer planRenderer = T5_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }
        Vector3 T6_Result = Vector3.zero;
        Vector3 T6_Result2 = Vector3.zero;

        int resultCount = CollisionUtilily.CheckSegmentSphereIntersection(T6_segmentPointA.position, T6_segmentPointB.position, T6_sphere.position, T6_sphere.localScale.x / 2.0f, ref T6_Result, ref T6_Result2);
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

            Debug.DrawLine(T6_segmentPointA.position, T6_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T6_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T6_resultPoint.gameObject.SetActive(false);
            T6_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T6_segmentPointA.position, T6_segmentPointB.position, Color.red);
            MeshRenderer planRenderer = T6_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T7_Result = Vector3.zero;
        Vector3 T7_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckSegmentInfiniteCylinderIntersection(T7_segmentPointA.position, T7_segmentPointB.position, T7_infiniteCylinder.position, T7_infiniteCylinder.up, T7_infiniteCylinder.localScale.x / 2.0f, ref T7_Result, ref T7_Result2);
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

            Debug.DrawLine(T7_segmentPointA.position, T7_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T7_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T7_resultPoint.gameObject.SetActive(false);
            T7_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T7_segmentPointA.position, T7_segmentPointB.position, Color.red);
            MeshRenderer planRenderer = T7_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T8_Result = Vector3.zero;
        Vector3 T8_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckSegmentCylinderIntersection(T8_segmentPointA.position, T8_segmentPointB.position, T8_cylinder.position, T8_cylinder.up, T8_cylinder.localScale.x / 2.0f, T8_cylinder.localScale.y * 2.0f, ref T8_Result, ref T8_Result2);
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

            Debug.DrawLine(T8_segmentPointA.position, T8_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T8_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T8_resultPoint.gameObject.SetActive(false);
            T8_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T8_segmentPointA.position, T8_segmentPointB.position, Color.red);
            MeshRenderer planRenderer = T8_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T9_Result = Vector3.zero;
        Vector3 T9_Result2 = Vector3.zero;

        resultCount = CollisionUtilily.CheckSegmentCapsuleIntersection(T9_segmentPointA.position, T9_segmentPointB.position, T9_capsule.position, T9_capsule.up, T9_capsule.localScale.x / 2.0f, T9_capsule.localScale.y * 2.0f - T9_capsule.localScale.x, ref T9_Result, ref T9_Result2);
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

            Debug.DrawLine(T9_segmentPointA.position, T9_segmentPointB.position, Color.green);

            MeshRenderer planRenderer = T9_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T9_resultPoint.gameObject.SetActive(false);
            T9_resultPoint2.gameObject.SetActive(false);

            Debug.DrawLine(T9_segmentPointA.position, T9_segmentPointB.position, Color.red);
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

        resultCount = CollisionUtilily.CheckSegmentMinkowskiBoxSphereIntersection(T10_segmentPointA.position, T10_segmentPointB.position, T10_origin.position, T10_scale, T10_origin.rotation, T10_radius, ref T10_Result, ref T10_Result2);
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

            Debug.DrawLine(T10_segmentPointA.position, T10_segmentPointB.position, Color.green);


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

            Debug.DrawLine(T10_segmentPointA.position, T10_segmentPointB.position, Color.red);


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

        Vector3 T11_Result = Vector3.zero;
        Vector3 T11_Result2 = Vector3.zero;
        Vector3 T11_Direction = (T11_rayPointB.position - T11_rayPointA.position).normalized;
        if (CollisionUtilily.CheckRaySegmentIntersection(T11_rayPointA.position, T11_Direction, T11_segmentPointA.position, T11_segmentPointB.position, T11_threshold, ref T11_Result, ref T11_Result2))
        {
            Debug.DrawRay(T11_rayPointA.position, T11_Direction * 1000000, Color.green);
            Debug.DrawLine(T11_segmentPointA.position, T11_segmentPointB.position, Color.green);

            T11_resultPoint.gameObject.SetActive(true);
            T11_resultPoint.position = T11_Result;

            T11_resultPoint2.gameObject.SetActive(true);
            T11_resultPoint2.position = T11_Result2;
        }
        else
        {
            Debug.DrawRay(T11_rayPointA.position, T11_Direction * 1000000, Color.red);
            Debug.DrawLine(T11_segmentPointA.position, T11_segmentPointB.position, Color.red);
            T11_resultPoint.gameObject.SetActive(false);
            T11_resultPoint2.gameObject.SetActive(false);
        }


        Vector3 T12_Result = Vector3.zero;
        Vector3 T12_Result2 = Vector3.zero;
        Vector3 T12_DirectionA = (T12_rayAPointB.position - T12_rayAPointA.position).normalized;
        Vector3 T12_DirectionB = (T12_rayBPointB.position - T12_rayBPointA.position).normalized;
        if (CollisionUtilily.CheckRayRayIntersection(T12_rayAPointA.position, T12_DirectionA, T12_rayBPointA.position, T12_DirectionB, T12_threshold, ref T12_Result, ref T12_Result2))
        {
            Debug.DrawRay(T12_rayAPointA.position, T12_DirectionA * 1000000, Color.green);
            Debug.DrawRay(T12_rayBPointA.position, T12_DirectionB * 1000000, Color.green);

            T12_resultPoint.gameObject.SetActive(true);
            T12_resultPoint.position = T12_Result;

            T12_resultPoint2.gameObject.SetActive(true);
            T12_resultPoint2.position = T12_Result2;
        }
        else
        {
            Debug.DrawRay(T12_rayAPointA.position, T12_DirectionA * 1000000, Color.red);
            Debug.DrawRay(T12_rayBPointA.position, T12_DirectionB * 1000000, Color.red);
            T12_resultPoint.gameObject.SetActive(false);
            T12_resultPoint2.gameObject.SetActive(false);
        }

        Vector3 T13_Result = Vector3.zero;
        Vector3 T13_Direction = (T13_rayPointB.position - T13_rayPointA.position).normalized;
        if (CollisionUtilily.CheckRayPlanIntersection(T13_rayPointA.position, T13_Direction, T13_plane.position, T13_plane.up, ref T13_Result))
        {
            T13_resultPoint.transform.position = T13_Result;
            T13_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T13_rayPointA.position, T13_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T13_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T13_resultPoint.transform.position = T13_Result;
            T13_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T13_rayPointA.position, T13_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T13_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T14_Result = Vector3.zero;
        Vector3 T14_pointA = T14_quad.transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0));
        Vector3 T14_pointB = T14_quad.transform.TransformPoint(new Vector3(0.5f, -0.5f, 0));
        Vector3 T14_pointC = T14_quad.transform.TransformPoint(new Vector3(-0.5f, 0.5f, 0));

        Debug.DrawRay(T14_pointA, Vector3.up * 100, Color.blue);
        Debug.DrawRay(T14_pointB, Vector3.up * 100, Color.black);
        Debug.DrawRay(T14_pointC, Vector3.up * 100);
        Vector3 T14_Direction = (T14_rayPointB.position - T14_rayPointA.position).normalized;
        if (CollisionUtilily.CheckRayQuadIntersection(T14_rayPointA.position, T14_Direction, T14_pointA, T14_pointB, T14_pointC, ref T14_Result))
        {
            T14_resultPoint.transform.position = T14_Result;
            T14_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T14_rayPointA.position, T14_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T14_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T14_resultPoint.gameObject.SetActive(false);

            Debug.DrawRay(T14_rayPointA.position, T14_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T14_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }


        Vector3 T15_Result = Vector3.zero;
        Vector3 T15_Result2 = Vector3.zero;
        Vector3 T15_Direction = (T15_rayPointB.position - T15_rayPointA.position).normalized;

        int T15_resultCount = CollisionUtilily.CheckRaySphereIntersection(T15_rayPointA.position, T15_Direction, T15_sphere.position, T15_sphere.localScale.x / 2.0f, ref T15_Result, ref T15_Result2);
        if (T15_resultCount > 0)
        {
            T15_resultPoint.transform.position = T15_Result;
            T15_resultPoint.gameObject.SetActive(true);

            if (T15_resultCount > 1)
            {
                T15_resultPoint2.transform.position = T15_Result2;
                T15_resultPoint2.gameObject.SetActive(true);
            }
            else
                T15_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T15_rayPointA.position, T15_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T15_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T15_resultPoint.gameObject.SetActive(false);
            T15_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T15_rayPointA.position, T15_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T15_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T16_Result = Vector3.zero;
        Vector3 T16_Result2 = Vector3.zero;
        Vector3 T16_Direction = (T16_rayPointB.position - T16_rayPointA.position).normalized;

        resultCount = CollisionUtilily.CheckRayInfiniteCylinderIntersection(T16_rayPointA.position, T16_Direction, T16_infiniteCylinder.position, T16_infiniteCylinder.up, T16_infiniteCylinder.localScale.x / 2.0f, ref T16_Result, ref T16_Result2);
        if (resultCount > 0)
        {
            T16_resultPoint.transform.position = T16_Result;
            T16_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T16_resultPoint2.transform.position = T16_Result2;
                T16_resultPoint2.gameObject.SetActive(true);
            }
            else
                T16_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T16_rayPointA.position, T16_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T16_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T16_resultPoint.gameObject.SetActive(false);
            T16_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T16_rayPointA.position, T16_Direction * 1000000, Color.red);

            MeshRenderer planRenderer = T16_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T17_Result = Vector3.zero;
        Vector3 T17_Result2 = Vector3.zero;
        Vector3 T17_Direction = (T17_rayPointB.position - T17_rayPointA.position).normalized;

        resultCount = CollisionUtilily.CheckRayCylinderIntersection(T17_rayPointA.position, T17_Direction, T17_cylinder.position, T17_cylinder.up, T17_cylinder.localScale.x / 2.0f, T17_cylinder.localScale.y * 2.0f, ref T17_Result, ref T17_Result2);
        if (resultCount > 0)
        {
            T17_resultPoint.transform.position = T17_Result;
            T17_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T17_resultPoint2.transform.position = T17_Result2;
                T17_resultPoint2.gameObject.SetActive(true);
            }
            else
                T17_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T17_rayPointA.position, T17_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T17_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T17_resultPoint.gameObject.SetActive(false);
            T17_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T17_rayPointA.position, T17_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T17_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T18_Result = Vector3.zero;
        Vector3 T18_Result2 = Vector3.zero;
        Vector3 T18_Direction = (T18_rayPointB.position - T18_rayPointA.position).normalized;

        resultCount = CollisionUtilily.CheckRayCapsuleIntersection(T18_rayPointA.position, T18_Direction, T18_capsule.position, T18_capsule.up, T18_capsule.localScale.x / 2.0f, T18_capsule.localScale.y * 2.0f - T18_capsule.localScale.x, ref T18_Result, ref T18_Result2);
        if (resultCount > 0)
        {
            T18_resultPoint.transform.position = T18_Result;
            T18_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T18_resultPoint2.transform.position = T18_Result2;
                T18_resultPoint2.gameObject.SetActive(true);
            }
            else
                T18_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T18_rayPointA.position, T18_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T18_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T18_resultPoint.gameObject.SetActive(false);
            T18_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T18_rayPointA.position, T18_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T18_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }




        halfSize = T19_scale / 2.0f;
        size = T19_scale;
        T19_faces[0].localPosition = Vector3.down * (halfSize.y + T19_radius);
        T19_faces[1].localPosition = Vector3.up * (halfSize.y + T19_radius);
        T19_faces[2].localPosition = Vector3.left * (halfSize.x + T19_radius);
        T19_faces[3].localPosition = Vector3.right * (halfSize.x + T19_radius);
        T19_faces[4].localPosition = Vector3.back * (halfSize.z + T19_radius);
        T19_faces[5].localPosition = Vector3.forward * (halfSize.z + T19_radius);


        T19_faces[0].localScale = new Vector3(size.x, size.z, 1);
        T19_faces[1].localScale = new Vector3(size.x, size.z, 1);

        T19_faces[1].localScale = new Vector3(1.0f, size.y, size.z);
        T19_faces[2].localScale = new Vector3(1.0f, size.y, size.z);

        T19_faces[3].localScale = new Vector3(size.x, size.y, 1.0f);
        T19_faces[4].localScale = new Vector3(size.x, size.y, 1.0f);




        T19_capsules[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x;
        T19_capsules[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x;
        T19_capsules[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x;
        T19_capsules[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x;

        T19_capsules[4].localPosition = Vector3.back * halfSize.z + Vector3.up * halfSize.y;
        T19_capsules[5].localPosition = Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T19_capsules[6].localPosition = Vector3.forward * halfSize.z + Vector3.up * halfSize.y;
        T19_capsules[7].localPosition = Vector3.left * halfSize.z + Vector3.up * halfSize.y;

        T19_capsules[8].localPosition = Vector3.back * halfSize.z + Vector3.down * halfSize.y;
        T19_capsules[9].localPosition = Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T19_capsules[10].localPosition = Vector3.forward * halfSize.z + Vector3.down * halfSize.y;
        T19_capsules[11].localPosition = Vector3.left * halfSize.z + Vector3.down * halfSize.y;

        radius2 = T19_radius * 2.0f;
        T19_capsules[0].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T19_capsules[1].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T19_capsules[2].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T19_capsules[3].localScale = new Vector3(radius2, size.y / 2.0f, radius2);

        T19_capsules[4].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T19_capsules[5].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T19_capsules[6].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T19_capsules[7].localScale = new Vector3(radius2, size.z / 2.0f, radius2);

        T19_capsules[8].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T19_capsules[9].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T19_capsules[10].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T19_capsules[11].localScale = new Vector3(radius2, size.z / 2.0f, radius2);


        T19_spheres[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;
        T19_spheres[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T19_spheres[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T19_spheres[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;

        T19_spheres[4].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;
        T19_spheres[5].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T19_spheres[6].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T19_spheres[7].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;

        T19_spheres[0].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[1].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[2].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[3].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[4].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[5].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[6].localScale = new Vector3(radius2, radius2, radius2);
        T19_spheres[7].localScale = new Vector3(radius2, radius2, radius2);



        Vector3 T19_Result = Vector3.zero;
        Vector3 T19_Result2 = Vector3.zero;
        Vector3 T19_Direction = (T19_rayPointB.position - T19_rayPointA.position).normalized;

        resultCount = CollisionUtilily.CheckRayMinkowskiBoxSphereIntersection(T19_rayPointA.position, T19_Direction, T19_origin.position, T19_scale, T19_origin.rotation, T19_radius, ref T19_Result, ref T19_Result2);
        if (resultCount > 0)
        {
            T19_resultPoint.transform.position = T19_Result;
            T19_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T19_resultPoint2.transform.position = T19_Result2;
                T19_resultPoint2.gameObject.SetActive(true);
            }
            else
                T19_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T19_rayPointA.position, T19_Direction * 1000000, Color.green);


            foreach (Transform tr in T19_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T19_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T19_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }


        }
        else
        {
            T19_resultPoint.gameObject.SetActive(false);
            T19_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T19_rayPointA.position, T19_Direction * 1000000, Color.red);
            foreach (Transform tr in T19_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T19_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T19_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

        }











        Vector3 T20_Result = Vector3.zero;
        Vector3 T20_Result2 = Vector3.zero;
        Vector3 T20_Direction = (T20_linePointB.position - T20_linePointA.position).normalized;
        if (CollisionUtilily.CheckLineSegmentIntersection(T20_linePointA.position, T20_Direction, T20_segmentPointA.position, T20_segmentPointB.position, T20_threshold, ref T20_Result, ref T20_Result2))
        {
            Debug.DrawRay(T20_linePointA.position, T20_Direction * 1000000, Color.green);
            Debug.DrawRay(T20_linePointA.position, -T20_Direction * 1000000, Color.green);
            Debug.DrawLine(T20_segmentPointA.position, T20_segmentPointB.position, Color.green);

            T20_resultPoint.gameObject.SetActive(true);
            T20_resultPoint.position = T20_Result;

            T20_resultPoint2.gameObject.SetActive(true);
            T20_resultPoint2.position = T20_Result2;
        }
        else
        {
            Debug.DrawRay(T20_linePointA.position, T20_Direction * 1000000, Color.red);
            Debug.DrawRay(T20_linePointA.position, -T20_Direction * 1000000, Color.red);
            Debug.DrawLine(T20_segmentPointA.position, T20_segmentPointB.position, Color.red);
            T20_resultPoint.gameObject.SetActive(false);
            T20_resultPoint2.gameObject.SetActive(false);
        }


        Vector3 T21_Result = Vector3.zero;
        Vector3 T21_Result2 = Vector3.zero;
        Vector3 T21_DirectionA = (T21_linePointB.position - T21_linePointA.position).normalized;
        Vector3 T21_DirectionB = (T21_rayPointB.position - T21_rayPointA.position).normalized;
        if (CollisionUtilily.CheckLineRayIntersection(T21_linePointA.position, T21_DirectionA, T21_rayPointA.position, T21_DirectionB, T21_threshold, ref T21_Result, ref T21_Result2))
        {
            Debug.DrawRay(T21_linePointA.position, T21_DirectionA * 1000000, Color.green);
            Debug.DrawRay(T21_linePointA.position, -T21_DirectionA * 1000000, Color.green);
           
            Debug.DrawRay(T21_rayPointA.position, T21_DirectionB * 1000000, Color.green);

            T21_resultPoint.gameObject.SetActive(true);
            T21_resultPoint.position = T21_Result;

            T21_resultPoint2.gameObject.SetActive(true);
            T21_resultPoint2.position = T21_Result2;
        }
        else
        {
            Debug.DrawRay(T21_linePointA.position, T21_DirectionA * 1000000, Color.red);
            Debug.DrawRay(T21_linePointA.position, -T21_DirectionA * 1000000, Color.red);
            
            Debug.DrawRay(T21_rayPointA.position, T21_DirectionB * 1000000, Color.red);
            T12_resultPoint.gameObject.SetActive(false);
            T12_resultPoint2.gameObject.SetActive(false);
        }


        Vector3 T22_Result = Vector3.zero;
        Vector3 T22_Result2 = Vector3.zero;
        Vector3 T22_DirectionA = (T22_lineAPointB.position - T22_lineAPointA.position).normalized;
        Vector3 T22_DirectionB = (T22_lineBPointB.position - T22_lineBPointA.position).normalized;
        if (CollisionUtilily.CheckLineLineIntersection(T22_lineAPointA.position, T22_DirectionA, T22_lineBPointA.position, T22_DirectionB, T22_threshold, ref T22_Result, ref T22_Result2))
        {
            Debug.DrawRay(T22_lineAPointA.position, T22_DirectionA * 1000000, Color.green);
            Debug.DrawRay(T22_lineAPointA.position, -T22_DirectionA * 1000000, Color.green);
            Debug.DrawRay(T22_lineBPointA.position, T22_DirectionB * 1000000, Color.green);
            Debug.DrawRay(T22_lineBPointA.position, -T22_DirectionB * 1000000, Color.green);

            T22_resultPoint.gameObject.SetActive(true);
            T22_resultPoint.position = T22_Result;

            T22_resultPoint2.gameObject.SetActive(true);
            T22_resultPoint2.position = T22_Result2;
        }
        else
        {
            Debug.DrawRay(T22_lineAPointA.position, T22_DirectionA * 1000000, Color.red);
            Debug.DrawRay(T22_lineAPointA.position, -T22_DirectionA * 1000000, Color.red);
            Debug.DrawRay(T22_lineBPointA.position, T22_DirectionB * 1000000, Color.red);
            Debug.DrawRay(T22_lineBPointA.position, -T22_DirectionB * 1000000, Color.red);

            T22_resultPoint.gameObject.SetActive(false);
            T22_resultPoint2.gameObject.SetActive(false);
        }



        Vector3 T23_Result = Vector3.zero;
        Vector3 T23_Direction = (T23_linePointB.position - T23_linePointA.position).normalized;
        if (CollisionUtilily.CheckLinePlanIntersection(T23_linePointA.position, T23_Direction, T23_plane.position, T23_plane.up, ref T23_Result))
        {
            T23_resultPoint.transform.position = T23_Result;
            T23_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T23_linePointA.position, T23_Direction * 1000000, Color.green);
            Debug.DrawRay(T23_linePointA.position, -T23_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T23_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T23_resultPoint.transform.position = T23_Result;
            T23_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T23_linePointA.position, T23_Direction * 1000000, Color.red);
            Debug.DrawRay(T23_linePointA.position, -T23_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T23_plane.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T24_Result = Vector3.zero;
        Vector3 T24_pointA = T24_quad.transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0));
        Vector3 T24_pointB = T24_quad.transform.TransformPoint(new Vector3(0.5f, -0.5f, 0));
        Vector3 T24_pointC = T24_quad.transform.TransformPoint(new Vector3(-0.5f, 0.5f, 0));

        Debug.DrawRay(T24_pointA, Vector3.up * 100, Color.blue);
        Debug.DrawRay(T24_pointB, Vector3.up * 100, Color.black);
        Debug.DrawRay(T24_pointC, Vector3.up * 100);
        Vector3 T24_Direction = (T24_linePointB.position - T24_linePointA.position).normalized;
        if (CollisionUtilily.CheckLineQuadIntersection(T24_linePointA.position, T24_Direction, T24_pointA, T24_pointB, T24_pointC, ref T24_Result))
        {
            T24_resultPoint.transform.position = T24_Result;
            T24_resultPoint.gameObject.SetActive(true);

            Debug.DrawRay(T24_linePointA.position, T24_Direction * 1000000, Color.green);
            Debug.DrawRay(T24_linePointA.position, -T24_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T24_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T24_resultPoint.gameObject.SetActive(false);

            Debug.DrawRay(T24_linePointA.position, T24_Direction * 1000000, Color.red);
            Debug.DrawRay(T24_linePointA.position, -T24_Direction * 1000000, Color.red);

            MeshRenderer planRenderer = T24_quad.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }


        Vector3 T25_Result = Vector3.zero;
        Vector3 T25_Result2 = Vector3.zero;
        Vector3 T25_Direction = (T25_linePointB.position - T25_linePointA.position).normalized;

        int T25_resultCount = CollisionUtilily.CheckLineSphereIntersection(T25_linePointA.position, T25_Direction, T25_sphere.position, T25_sphere.localScale.x / 2.0f, ref T25_Result, ref T25_Result2);
        if (T25_resultCount > 0)
        {
            T25_resultPoint.transform.position = T25_Result;
            T25_resultPoint.gameObject.SetActive(true);

            if (T25_resultCount > 1)
            {
                T25_resultPoint2.transform.position = T25_Result2;
                T25_resultPoint2.gameObject.SetActive(true);
            }
            else
                T25_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T25_linePointA.position, T25_Direction * 1000000, Color.green);
            Debug.DrawRay(T25_linePointA.position, -T25_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T25_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T25_resultPoint.gameObject.SetActive(false);
            T25_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T25_linePointA.position, T25_Direction * 1000000, Color.red);
            Debug.DrawRay(T25_linePointA.position, -T25_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T25_sphere.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T26_Result = Vector3.zero;
        Vector3 T26_Result2 = Vector3.zero;
        Vector3 T26_Direction = (T26_linePointB.position - T26_linePointA.position).normalized;

        resultCount = CollisionUtilily.CheckLineInfiniteCylinderIntersection(T26_linePointA.position, T26_Direction, T26_infiniteCylinder.position, T26_infiniteCylinder.up, T26_infiniteCylinder.localScale.x / 2.0f, ref T26_Result, ref T26_Result2);
        if (resultCount > 0)
        {
            T26_resultPoint.transform.position = T26_Result;
            T26_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T26_resultPoint2.transform.position = T26_Result2;
                T26_resultPoint2.gameObject.SetActive(true);
            }
            else
                T26_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T26_linePointA.position, T26_Direction * 1000000, Color.green);
            Debug.DrawRay(T26_linePointA.position, -T26_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T26_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T26_resultPoint.gameObject.SetActive(false);
            T26_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T26_linePointA.position, T26_Direction * 1000000, Color.red);
            Debug.DrawRay(T26_linePointA.position, -T26_Direction * 1000000, Color.red);

            MeshRenderer planRenderer = T16_infiniteCylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T27_Result = Vector3.zero;
        Vector3 T27_Result2 = Vector3.zero;
        Vector3 T27_Direction = (T27_linePointB.position - T27_linePointA.position).normalized;

        resultCount = CollisionUtilily.CheckLineCylinderIntersection(T27_linePointA.position, T27_Direction, T27_cylinder.position, T27_cylinder.up, T27_cylinder.localScale.x / 2.0f, T27_cylinder.localScale.y * 2.0f, ref T27_Result, ref T27_Result2);
        if (resultCount > 0)
        {
            T27_resultPoint.transform.position = T27_Result;
            T27_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T27_resultPoint2.transform.position = T27_Result2;
                T27_resultPoint2.gameObject.SetActive(true);
            }
            else
                T27_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T27_linePointA.position, T27_Direction * 1000000, Color.green);
            Debug.DrawRay(T27_linePointA.position, -T27_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T27_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T27_resultPoint.gameObject.SetActive(false);
            T27_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T27_linePointA.position, T27_Direction * 1000000, Color.red);
            Debug.DrawRay(T27_linePointA.position, -T27_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T27_cylinder.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }

        Vector3 T28_Result = Vector3.zero;
        Vector3 T28_Result2 = Vector3.zero;
        Vector3 T28_Direction = (T28_linePointB.position - T28_linePointA.position).normalized;

        resultCount = CollisionUtilily.CheckLineCapsuleIntersection(T28_linePointA.position, T28_Direction, T28_capsule.position, T28_capsule.up, T28_capsule.localScale.x / 2.0f, T28_capsule.localScale.y * 2.0f - T28_capsule.localScale.x, ref T28_Result, ref T28_Result2);
        if (resultCount > 0)
        {
            T28_resultPoint.transform.position = T28_Result;
            T28_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T28_resultPoint2.transform.position = T28_Result2;
                T28_resultPoint2.gameObject.SetActive(true);
            }
            else
                T28_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T28_linePointA.position, T28_Direction * 1000000, Color.green);
            Debug.DrawRay(T28_linePointA.position, -T28_Direction * 1000000, Color.green);

            MeshRenderer planRenderer = T28_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            T28_resultPoint.gameObject.SetActive(false);
            T28_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T28_linePointA.position, T28_Direction * 1000000, Color.red);
            Debug.DrawRay(T28_linePointA.position, -T28_Direction * 1000000, Color.red);
            MeshRenderer planRenderer = T28_capsule.GetComponent<MeshRenderer>();
            planRenderer.material.color = new Color(1, 0, 0, 0.2f);
        }




        halfSize = T19_scale / 2.0f;
        size = T19_scale;
        T29_faces[0].localPosition = Vector3.down * (halfSize.y + T29_radius);
        T29_faces[1].localPosition = Vector3.up * (halfSize.y + T29_radius);
        T29_faces[2].localPosition = Vector3.left * (halfSize.x + T29_radius);
        T29_faces[3].localPosition = Vector3.right * (halfSize.x + T29_radius);
        T29_faces[4].localPosition = Vector3.back * (halfSize.z + T29_radius);
        T29_faces[5].localPosition = Vector3.forward * (halfSize.z + T29_radius);


        T29_faces[0].localScale = new Vector3(size.x, size.z, 1);
        T29_faces[1].localScale = new Vector3(size.x, size.z, 1);

        T29_faces[1].localScale = new Vector3(1.0f, size.y, size.z);
        T29_faces[2].localScale = new Vector3(1.0f, size.y, size.z);

        T29_faces[3].localScale = new Vector3(size.x, size.y, 1.0f);
        T29_faces[4].localScale = new Vector3(size.x, size.y, 1.0f);




        T29_capsules[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x;
        T29_capsules[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x;
        T29_capsules[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x;
        T29_capsules[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x;

        T29_capsules[4].localPosition = Vector3.back * halfSize.z + Vector3.up * halfSize.y;
        T29_capsules[5].localPosition = Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T29_capsules[6].localPosition = Vector3.forward * halfSize.z + Vector3.up * halfSize.y;
        T29_capsules[7].localPosition = Vector3.left * halfSize.z + Vector3.up * halfSize.y;

        T29_capsules[8].localPosition = Vector3.back * halfSize.z + Vector3.down * halfSize.y;
        T29_capsules[9].localPosition = Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T29_capsules[10].localPosition = Vector3.forward * halfSize.z + Vector3.down * halfSize.y;
        T29_capsules[11].localPosition = Vector3.left * halfSize.z + Vector3.down * halfSize.y;

        radius2 = T29_radius * 2.0f;
        T29_capsules[0].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T29_capsules[1].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T29_capsules[2].localScale = new Vector3(radius2, size.y / 2.0f, radius2);
        T29_capsules[3].localScale = new Vector3(radius2, size.y / 2.0f, radius2);

        T29_capsules[4].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T29_capsules[5].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T29_capsules[6].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T29_capsules[7].localScale = new Vector3(radius2, size.z / 2.0f, radius2);

        T29_capsules[8].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T29_capsules[9].localScale = new Vector3(radius2, size.z / 2.0f, radius2);
        T29_capsules[10].localScale = new Vector3(radius2, size.x / 2.0f, radius2);
        T29_capsules[11].localScale = new Vector3(radius2, size.z / 2.0f, radius2);


        T29_spheres[0].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;
        T29_spheres[1].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T29_spheres[2].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.down * halfSize.y;
        T29_spheres[3].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.down * halfSize.y;

        T29_spheres[4].localPosition = Vector3.back * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;
        T29_spheres[5].localPosition = Vector3.back * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T29_spheres[6].localPosition = Vector3.forward * halfSize.z + Vector3.right * halfSize.x + Vector3.up * halfSize.y;
        T29_spheres[7].localPosition = Vector3.forward * halfSize.z + Vector3.left * halfSize.x + Vector3.up * halfSize.y;

        T29_spheres[0].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[1].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[2].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[3].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[4].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[5].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[6].localScale = new Vector3(radius2, radius2, radius2);
        T29_spheres[7].localScale = new Vector3(radius2, radius2, radius2);



        Vector3 T29_Result = Vector3.zero;
        Vector3 T29_Result2 = Vector3.zero;
        Vector3 T29_Direction = (T29_linePointB.position - T29_linePointA.position).normalized;

        resultCount = CollisionUtilily.CheckLineMinkowskiBoxSphereIntersection(T29_linePointA.position, T29_Direction, T29_origin.position, T29_scale, T29_origin.rotation, T29_radius, ref T29_Result, ref T29_Result2);
        if (resultCount > 0)
        {
            T29_resultPoint.transform.position = T29_Result;
            T29_resultPoint.gameObject.SetActive(true);

            if (resultCount > 1)
            {
                T29_resultPoint2.transform.position = T29_Result2;
                T29_resultPoint2.gameObject.SetActive(true);
            }
            else
                T29_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T29_linePointA.position, T29_Direction * 1000000, Color.green);
            Debug.DrawRay(T29_linePointA.position, -T29_Direction * 1000000, Color.green);


            foreach (Transform tr in T29_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T29_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }

            foreach (Transform tr in T29_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(0, 1, 0, 0.2f);
            }


        }
        else
        {
            T29_resultPoint.gameObject.SetActive(false);
            T29_resultPoint2.gameObject.SetActive(false);

            Debug.DrawRay(T29_linePointA.position, T29_Direction * 1000000, Color.red);
            Debug.DrawRay(T29_linePointA.position, -T29_Direction * 1000000, Color.red);

            foreach (Transform tr in T29_faces)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T29_capsules)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

            foreach (Transform tr in T29_spheres)
            {
                MeshRenderer Renderer = tr.GetComponent<MeshRenderer>();
                Renderer.material.color = new Color(1, 0, 0, 0.2f);
            }

        }







    }
}


