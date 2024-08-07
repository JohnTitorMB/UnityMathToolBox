# UnityMathToolBox

This c# mathematical utility compiles a diverse and varied set of mathematical and physical functions to simplify 3D project development.

# Intersection :
- [Segment Intersection](#segment-Intersection)


# Segment Intersection

- [Segment - Point](#segment---point)
- [Segment - Segment](#Segment---segment)
- [Segment - Plan](#Segment---plan)
- [Segment - PlanXZ](#Segment---PlanXZ)
- [Segment - PlanXY](#Segment---PlanXY)
- [Segment - PlanYZ](#Segment---PlanYZ)
- [Segment - Quad](#Segment---Quad)
- [Segment - Sphere](#Segment---Sphere)
- [Segment - Sphere Center on zero](#Segment---Sphere-Center-on-zero)
- [Segment - Infinite Cylinder](#Segment---Infinite-Cylinder)
- [Segment - Cylinder](#Segment---Cylinder)
- [Segment - Capsule](#Segment---Capsule)
- [Segment - Minkowski Box Sphere](#Segment---Minkowski-Box-Sphere)

## Segment - Point

### Definition :
```c#
    public static bool CheckSegmentPointIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentPointIntersection(new Vector3(-5,0,0), new Vector3(3,0,0), new Vector3(1.0f,-0.00001f,0), 0.001f, ref result))
        Debug.Log("Result : " + result);
```

## Segment - Segment

### Definition :
```c#
    public static bool CheckSegmentSegmentIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _segmentBPointA, Vector3 _segmentBPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentSegmentIntersection(new Vector3(-5,0,0), new Vector3(3,0,0), new Vector3(0,-4,0), new Vector3(0,7,0), 0.001f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Plan

### Definition :
```c#
    public static bool CheckSegmentPlanIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentPlanIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), new Vector3(0,-1,0), new Vector3(0,1,0), ref result))
        Debug.Log("Result : " + result);
```

## Segment - PlanXZ

### Definition :
```c#
    public static bool CheckSegmentPlanXZIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentPlanXZIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), ref result))
        Debug.Log("Result : " + result);
```

## Segment - PlanXY

### Definition :
```c#
    public static bool CheckSegmentPlanXYIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentPlanXYIntersection(new Vector3(0,0,-3), new Vector3(0,0,5), ref result))
        Debug.Log("Result : " + result);
```

## Segment - PlanYZ

### Definition :
```c#
    public static bool CheckSegmentPlanYZIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentPlanYZIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), ref result))
        Debug.Log("Result : " + result);
```

## Segment - Quad

### Definition :
```c#
    public static bool CheckSegmentQuadIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckSegmentQuadIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f), new Vector3(-0.5f,0,0.5f), ref result))
        Debug.Log("Result : " + result);
```

## Segment - Sphere

### Definition :
```c#
    public static int CheckSegmentSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentSphereIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(0.0f, 2.0f, 0.0f), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Sphere Center on zero

### Definition :
```c#
    public static int CheckSegmentSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentSphereIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Infinite  Cylinder

### Definition :
```c#
    public static int CheckSegmentInfiniteCylinderIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentInfiniteCylinderIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Cylinder

### Definition :
```c#
    public static int CheckSegmentCylinderIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentCylinderIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Capsule

### Definition :
```c#
    public static int CheckSegmentCapsuleIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentCapsuleIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Segment - Minkowski Box Sphere

### Definition :
```c#
      public static int CheckSegmentMinkowskiBoxSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)

```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckSegmentMinkowskiBoxSphereIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), Quaternion.identity, 0.2f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

# Ray Intersection

- [Ray - Point](#ray---point)
- [Ray - Segment](#ray---segment)
- [Ray - Ray](#ray---ray)
- [Ray - Plan](#ray---plan)
- [Ray - PlanXZ](#ray---PlanXZ)
- [Ray - PlanXY](#ray---PlanXY)
- [Ray - PlanYZ](#ray---PlanYZ)
- [Ray - Quad](#ray---Quad)
- [Ray - Sphere](#ray---Sphere)
- [Ray - Sphere Center on zero](#ray---Sphere-Center-on-zero)
- [Ray - Infinite Cylinder](#ray---Infinite-Cylinder)
- [Ray - Cylinder](#ray---Cylinder)
- [Ray - Capsule](#ray---Capsule)
- [Ray - Minkowski Box Sphere](#ray---Minkowski-Box-Sphere)

## Ray - Point

### Definition :
```c#
    public static bool CheckRayPointIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayPointIntersection(new Vector3(-5,0,0), new Vector3(1,0,0), new Vector3(1.0f,-0.00001f,0), 0.001f, ref result))
        Debug.Log("Result : " + result);
```

## Ray - Segment

### Definition :
```c#
    public static bool CheckRaySegmentIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _segmentPointA, Vector3 _segmentPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRaySegmentIntersection(new Vector3(-5,0,0), new Vector3(1,0,0), new Vector3(0,-4,0), new Vector3(0,7,0), 0.001f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Ray

### Definition :
```c#
    public static bool CheckRayRayIntersection(Vector3 _rayAStartPoint, Vector3 _rayADirection, Vector3 _rayBStartPoint, Vector3 _rayBDirection, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRayRayIntersection(new Vector3(-5,0,0), new Vector3(1,0,0), new Vector3(0,-4,0), new Vector3(0,1,0), 0.001f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Plan

### Definition :
```c#
    public static bool CheckRayPlanIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayPlanIntersection(new Vector3(0,6,0), new Vector3(0,-1,0), new Vector3(0,-1,0), new Vector3(0,1,0), ref result))
        Debug.Log("Result : " + result);
```

## Ray - PlanXZ

### Definition :
```c#
    public static bool CheckRayPlanXZIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayPlanXZIntersection(new Vector3(0,6,0), new Vector3(0,-1,0), ref result))
        Debug.Log("Result : " + result);
```

## Ray - PlanXY

### Definition :
```c#
    public static bool CheckRayPlanXYIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayPlanXYIntersection(new Vector3(0,0,-3), new Vector3(0,0,1), ref result))
        Debug.Log("Result : " + result);
```

## Ray - PlanYZ

### Definition :
```c#
    public static bool CheckRayPlanYZIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayPlanYZIntersection(new Vector3(-3,0,0), new Vector3(1,0,0), ref result))
        Debug.Log("Result : " + result);
```

## Ray - Quad

### Definition :
```c#
    public static bool CheckRayQuadIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckRayQuadIntersection(new Vector3(0,-6,0), new Vector3(0,1,0), new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f), new Vector3(-0.5f,0,0.5f), ref result))
        Debug.Log("Result : " + result);
```

## Ray - Sphere

### Definition :
```c#
    public static int CheckRaySphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRaySphereIntersection(new Vector3(0,-6,0), new Vector3(0,1,0), new Vector3(0.0f, 2.0f, 0.0f), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Sphere Center on zero

### Definition :
```c#
    public static int CheckRaySphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRaySphereIntersection(new Vector3(0,-6,0), new Vector3(0,1,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Infinite  Cylinder

### Definition :
```c#
    public static int CheckRayInfiniteCylinderIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRayInfiniteCylinderIntersection(new Vector3(-3,0,0), new Vector3(1,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Cylinder

### Definition :
```c#
    public static int CheckRayCylinderIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRayCylinderIntersection(new Vector3(-3,0,0), new Vector3(1,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Capsule

### Definition :
```c#
    public static int CheckRayCapsuleIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRayCapsuleIntersection(new Vector3(-3,0,0), new Vector3(1,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Ray - Minkowski Box Sphere

### Definition :
```c#
      public static int CheckRayMinkowskiBoxSphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)

```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckRayMinkowskiBoxSphereIntersection(new Vector3(-3,0,0), new Vector3(1,0,0), new Vector3(0,0,0), Quaternion.identity, 0.2f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```


# Math Utility

## BaseChangement

Transforms a local point relative to a base A into a base B

### Definition :
```c#
    public static Vector3 BaseChangement(Vector3 _point, Matrix4x4 baseA, Matrix4x4 baseB)
```

### Example : 
```c#
    Matrix4x4 baseA = Matrix.TRS(new Vector3(10,0,0), Quaternion.identity, new Vector3(1,1,1));
    Matrix4x4 baseB = Matrix.TRS(new Vector3(0,10,0), Quaternion.Euler(0,0,90), new Vector3(1,1,1));
    Vector3 result = MathUtility.BaseChangement(new Vector3(-3,0,0), baseA, baseB);
    Debug.Log("result : " + result);
```

## BaseChangement (Optimised version)

Transforms a local point relative to a base A into a base B

### Definition :
```c#
    public static Vector3 BaseChangementOptimised(Vector3 _point, Vector3 _baseATranslation, Quaternion _baseARotation, Vector3 baseAScale
                                                    , Vector3 _baseBTranslation, Quaternion _baseBRotation, Vector3 baseBScale)
```

### Example : 
```c#
    Vector3 result = MathUtility.BaseChangementOptimised(new Vector3(-3,0,0), new Vector3(10,0,0), Quaternion.identity, new Vector3(1,1,1), new Vector3(0,10,0), Quaternion.Euler(0,0,90), new Vector3(1,1,1));
    Debug.Log("result : " + result);
```

## WorldBaseChangementOptimised
Transforms a world point into a base B


### Definition :
```c#
    public static Vector3 WorldBaseChangementOptimised(Vector3 _point, Vector3 _targetBaseTranslation, Quaternion _targetBaseRotation, Vector3 _targetBaseScale)
```

### Example : 
```c#
    Vector3 result = MathUtility.WorldBaseChangementOptimised(new Vector3(-3,0,0), new Vector3(0,10,0), Quaternion.Euler(0,0,90), new Vector3(1,1,1));
    Debug.Log("result : " + result);
```

## BaseToWorld
Transforms a local point into world point


### Definition :
```c#
    public static Vector3 BaseToWorld(Vector3 _point, Vector3 _baseTranslation, Quaternion _baseRotation, Vector3 _baseScale)
```

### Example : 
```c#
    Vector3 result = MathUtility.BaseToWorld(new Vector3(-3,0,0), new Vector3(0,10,0), Quaternion.Euler(0,0,90), new Vector3(1,1,1));
    Debug.Log("result : " + result);
```
