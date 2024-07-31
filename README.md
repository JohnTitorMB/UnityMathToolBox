# UnityMathToolBox

This c# mathematical utility compiles a diverse and varied set of mathematical and physical functions to simplify 3D project development.

# Intersection between Line and various shapes

- [Line - Point](#line---point)
- [Line - Line](#Line---line)
- [Line - Plan](#Line---plan)
- [Line - PlanXZ](#Line---PlanXZ)
- [Line - PlanXY](#Line---PlanXY)
- [Line - PlanYZ](#Line---PlanYZ)
- [Line - Quad](#Line---Quad)
- [Line - Sphere](#Line---Sphere)
- [Line - Sphere Center on zero](#Line---Sphere-Center-on-zero)
- [Line - Infinite Cylinder](#Line---Infinite-Cylinder)
- [Line - Cylinder](#Line---Cylinder)
- [Line - Capsule](#Line---Capsule)
- [Line - Minkowski Box Sphere](#Line---Minkowski-Box-Sphere)

## Line - Point

### Definition :
```c#
    public static bool CheckLinePointIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLinePointIntersection(new Vector3(-5,0,0), new Vector3(3,0,0), new Vector3(1.0f,-0.00001f,0), 0.001f, ref result))
        Debug.Log("Result : " + result);
```

## Line - Line

### Definition :
```c#
    public static bool CheckLineLineIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _lineBPointA, Vector3 _lineBPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineLineIntersection(new Vector3(-5,0,0), new Vector3(3,0,0), new Vector3(0,-4,0), new Vector3(0,7,0), 0.001f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Plan

### Definition :
```c#
    public static bool CheckLinePlanIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLinePlanIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), new Vector3(0,-1,0), new Vector3(0,1,0), ref result))
        Debug.Log("Result : " + result);
```

## Line - PlanXZ

### Definition :
```c#
    public static bool CheckLinePlanXZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLinePlanXZIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), ref result))
        Debug.Log("Result : " + result);
```

## Line - PlanXY

### Definition :
```c#
    public static bool CheckLinePlanXYIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLinePlanXYIntersection(new Vector3(0,0,-3), new Vector3(0,0,5), ref result))
        Debug.Log("Result : " + result);
```

## Line - PlanYZ

### Definition :
```c#
    public static bool CheckLinePlanYZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLinePlanYZIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), ref result))
        Debug.Log("Result : " + result);
```

## Line - Quad

### Definition :
```c#
    public static bool CheckLineQuadIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    if(CollisionUtilily.CheckLineQuadIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f), new Vector3(-0.5f,0,0.5f), ref result))
        Debug.Log("Result : " + result);
```

## Line - Sphere

### Definition :
```c#
    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineSphereIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(0.0f, 2.0f, 0.0f), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Sphere Center on zero

### Definition :
```c#
    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, float _radius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineSphereIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Infinite  Cylinder

### Definition :
```c#
    public static int CheckLineInfiniteCylinderIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineInfiniteCylinderIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Cylinder

### Definition :
```c#
    public static int CheckLineCylinderIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineCylinderIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Capsule

### Definition :
```c#
    public static int CheckLineCapsuleIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineCapsuleIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Minkowski Box Sphere

### Definition :
```c#
      public static int CheckLineMinkowskiBoxSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)

```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckLineMinkowskiBoxSphereIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), Quaternion.identity, 0.2f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

# Parametric Intersection between Line and various shapes

- [Line - Plan](#Line---Plan (P))
- [Line - PlanXZ](#Line---PlanXZ (P))
- [Line - Quad](#Line---Quad-(P))
- [Line - CircularPlan](#Line---CircularPlan-(P))
- [Line - Sphere](#Line---Sphere-(P))
- [Line - Cylinder without caps](#Line---Cylinder-without-caps-(P))
- [Line - Sphere Up Caps](#Line---Sphere-Up-Caps-(P))
- [Line - Capsule](#Line---Capsule-(P))
- [Line - Minkowski Box Sphere](#Line---Minkowski-Box-Sphere-(P))


## Line - Plan (P)

### Definition :
```c#
    public static bool CheckLinePlanIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _planPoint, Vector3 _planNormal, ref float _t)
```

### Example : 
```c#
    float t = 0.0f;
    if(ParametricCollisionUtility.CheckLinePlanIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), new Vector3(0,-1,0), new Vector3(0,1,0), ref t))
        Debug.Log("t : " + t);
```

## Line - PlanXZ (P)

### Definition :
```c#
    public static bool CheckLinePlanXZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref float _t)
```

### Example : 
```c#
    float t = 0.0f;
    if(ParametricCollisionUtility.CheckLinePlanXZIntersection(new Vector3(0,6,0), new Vector3(0,-2,0), ref t))
        Debug.Log("t : " + t);
```

## Line - Quad (P)

### Definition :
```c#
    public static bool CheckLineQuadIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref float _t)
```

### Example : 
```c#
    float t = 0.0f;
    if(ParametricCollisionUtility.CheckLineQuadIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f), new Vector3(-0.5f,0,0.5f), ref t))
        Debug.Log("t : " + t);
```

## Line - CircularPlan (P)

### Definition :
```c#
    public static bool CheckLineCircleIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _circleOrigin, Vector3 _circleNormal, float _radius, ref float _t)
```

### Example : 
```c#
    float t = 0.0f;
    if(ParametricCollisionUtility.CheckLineCircleIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(0.0f, 2.0f, 0.0f), new Vector3(0, 1, 0), ref t))
        Debug.Log("t : " + t);
```

## Line - Sphere (P)

### Definition :
```c#
    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _SphereOrigin, float _radius, ref float _t0, ref float _t1)
```

### Example : 
```c#
    float t0 = 0.0f;
    float t1 = 0.0f;
    if(ParametricCollisionUtility.CheckLineSphereIntersection(new Vector3(0,-6,0), new Vector3(0,5,0), new Vector3(0.0f, 2.0f, 0.0f), 0.5f, ref t0, ref t1))
    {
        Debug.Log("t0 : " + t0);
        Debug.Log("t1 : " + t1);
    }
```

## Line - Cylinder without caps (P)

### Definition :
```c#
    public static int CheckLineCylinderWihtoutCapsIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
```

### Example : 
```c#
    float t0 = 0.0f;
    float t1 = 0.0f;
    if(ParametricCollisionUtility.CheckLineCylinderWihtoutCapsIntersection(new Vector3(-6,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref t0, ref t1))
    {
        Debug.Log("t0 : " + t0);
        Debug.Log("t1 : " + t1);
    }
```

## Line - Sphere Up Caps (P)

### Definition :
```c#
    public static int CheckLineSphereCapsIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
```

### Example : 
```c#
    float t0 = 0.0f;
    float t1 = 0.0f;
    if(ParametricCollisionUtility.CheckLineSphereCapsIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,-0.2,0), new Vector3(0,1,0), 0.5f, ref t0, ref t1))
    {
        Debug.Log("t0 : " + t0);
        Debug.Log("t1 : " + t1);
    }
```

## Line - Capsule (P)

### Definition :
```c#
    public static int CheckLineCapsuleIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
```

### Example : 
```c#
    float t0 = 0.0f;
    float t1 = 0.0f;
    if(ParametricCollisionUtility.CheckLineCapsuleIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref t0, ref t1))
    {
        Debug.Log("t0 : " + t0);
        Debug.Log("t1 : " + t1);
    }
```

## Line - Minkowski Box Sphere (P)

### Definition :
```c#
    public static int CheckLineMinkowskiBoxSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)

```

### Example : 
```c#
    float t0 = 0.0f;
    float t1 = 0.0f;
    if(ParametricCollisionUtility.CheckLineMinkowskiBoxSphereIntersection(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), Quaternion.identity, 0.2f, ref t0, ref t1))
    {
        Debug.Log("t0 : " + t0);
        Debug.Log("t1 : " + t1);
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
