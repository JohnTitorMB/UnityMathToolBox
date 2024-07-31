# UnityMathToolBox

This c# mathematical utility compiles a diverse and varied set of mathematical and physical functions to simplify 3D project development.

# Intersection between Line and various shapes

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
    public static int CheckIntersectionLineInfiniteCylinder(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckIntersectionLineInfiniteCylinder(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Cylinder

### Definition :
```c#
    public static int CheckIntersectionLineCylinder(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckIntersectionLineCylinder(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Capsule

### Definition :
```c#
    public static int CheckIntersectionLineCapsule(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckIntersectionLineCapsule(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), new Vector3(0,1,0), 0.5f, 2.0f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```

## Line - Minkowski Box Sphere

### Definition :
```c#
      public static int CheckIntersectionLineMinkowskiBoxSphere(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)

```

### Example : 
```c#
    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;
    if(CollisionUtilily.CheckIntersectionLineMinkowskiBoxSphere(new Vector3(-3,0,0), new Vector3(5,0,0), new Vector3(0,0,0), Quaternion.identity, 0.2f, ref result, ref result2))
    {
        Debug.Log("Result : " + result);
        Debug.Log("Result : " + result2);
    }
```
