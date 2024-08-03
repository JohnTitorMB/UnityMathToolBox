using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionUtilily
{
    public static bool CheckPointPointIntersection(Vector3 pointA, Vector3 pointB, float distanceThrhold)
    {
        if ((pointA - pointB).sqrMagnitude <= distanceThrhold * distanceThrhold)
        {
            return true;
        }
        else
            return false;
    }

    public static bool CheckSegmentPointIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
    {
        Vector3 T2_pointDir = _pointA - _segmentPointA;
        Vector3 T2_segmentDir = _segmentPointB - _segmentPointA;

        float dot = Vector3.Dot(T2_pointDir, T2_segmentDir);
        float squaredMagnitude = T2_segmentDir.sqrMagnitude;

        if (dot > 0 && dot < squaredMagnitude)
        {
            Vector3 projectPoint = new Vector3(T2_segmentDir.x * dot / squaredMagnitude,
                    T2_segmentDir.y * dot / squaredMagnitude,
                    T2_segmentDir.z * dot / squaredMagnitude);

            if ((T2_pointDir - projectPoint).sqrMagnitude < distanceThreshold * distanceThreshold)
            {
                _result = (projectPoint + _segmentPointA);
                return true;
            }
        }

        return false;
    }

    public static bool CheckSegmentSegmentIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _segmentBPointA, Vector3 _segmentBPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _segmentAPointB - _segmentAPointA;
        Vector3 segmentBDir = _segmentBPointB - _segmentBPointA;

        Vector3 p1 = _segmentAPointA;
        Vector3 p2 = _segmentAPointB;
        Vector3 p3 = _segmentBPointA;
        Vector3 p4 = _segmentBPointB;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;


        float tB = tBNumerator / tBDenominator;

        tA = Mathf.Clamp01(tA);
        tB = Mathf.Clamp01(tB);
        Vector3 pA = _segmentAPointA + tA * segmentADir;
        Vector3 pB = _segmentBPointA + tB * segmentBDir;
        _result = pA;
        _result2 = pB;

        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckSegmentPlanIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
    {
        float t = 0;
        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        if (ParametricCollisionUtility.CheckSegmentPlanIntersection(_segmentAPointA, _segmentAPointB, _planPoint, _planNormal, ref t))
        {
            _result = _segmentAPointA + t * segmentDir;
            return true;
        }

        return false;
    }


    public static bool CheckSegmentPlanXZIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
    {
        if (_segmentAPointA.y > 0 && _segmentAPointB.y > 0 || _segmentAPointA.y < 0 && _segmentAPointB.y < 0)
            return false;

        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float tDenominator = segmentDir.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_segmentAPointA.y / tDenominator;
        _result = _segmentAPointA + t * segmentDir;

        return true;
    }

    public static bool CheckSegmentPlanXYIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
    {
        if (_segmentAPointA.z > 0 && _segmentAPointB.z > 0 || _segmentAPointA.z < 0 && _segmentAPointB.z < 0)
            return false;

        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float tDenominator = segmentDir.z;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = _segmentAPointA.z / tDenominator;
        _result = _segmentAPointA + t * segmentDir;

        return true;
    }

    public static bool CheckSegmentPlanYZIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref Vector3 _result)
    {
        if (_segmentAPointA.x > 0 && _segmentAPointB.x > 0 || _segmentAPointA.x < 0 && _segmentAPointB.x < 0)
            return false;

        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float tDenominator = segmentDir.x;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = _segmentAPointA.x / tDenominator;
        _result = _segmentAPointA + t * segmentDir;

        return true;
    }

    public static bool CheckSegmentQuadIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _segmentAPointA = MathUtility.WorldBaseChangementOptimised(_segmentAPointA, quadPointA, rotation, Vector3.one);
        _segmentAPointB = MathUtility.WorldBaseChangementOptimised(_segmentAPointB, quadPointA, rotation, Vector3.one);

        Vector3 planResultPoint = Vector3.one;
        if (CheckSegmentPlanXZIntersection(_segmentAPointA, _segmentAPointB, ref planResultPoint))
        {
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _resultPoint = MathUtility.BaseToWorld(planResultPoint, quadPointA, rotation, Vector3.one);
                return true;
            }
        }

        return false;
    }

   

    public static int CheckSegmentSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentDir = _segmentPointB - _segmentPointA;
        Vector3 sphereDir = _segmentPointA - _SphereOrigin;
        float a = segmentDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(segmentDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        float deltaSqrt = Mathf.Sqrt(delta);
        float a2 = 2 * a;

        if (delta < epsilon)
        {
            float t = -b / a2;
            if(t >= 0 && t <= 1)
            {
                _result = _segmentPointA + t * segmentDir;
                return 1;
            }

            return 0;
        }

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1sCondition = t1 >= 0 && t1 <= 1;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _result = _segmentPointA + t0 * segmentDir;
                _result2 = _segmentPointA + t1 * segmentDir;
            }
            else
            {
                _result = _segmentPointA + t1 * segmentDir;
                _result2 = _segmentPointA + t0 * segmentDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _segmentPointA + t0 * segmentDir;
        else
            _result = _segmentPointA + t1 * segmentDir;

        return 1;
    }

    public static int CheckSegmentSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentDir = _segmentPointB - _segmentPointA;
        Vector3 sphereDir = _segmentPointA;
        float a = segmentDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(segmentDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        float a2 = 2 * a;
        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0 && t <= 1)
            {
                _result = _segmentPointA + t * segmentDir;
                return 1;
            }

            return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1sCondition = t1 >= 0 && t1 <= 1;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _result = _segmentPointA + t0 * segmentDir;
                _result2 = _segmentPointA + t1 * segmentDir;
            }
            else
            {
                _result = _segmentPointA + t1 * segmentDir;
                _result2 = _segmentPointA + t0 * segmentDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _segmentPointA + t0 * segmentDir;
        else
            _result = _segmentPointA + t1 * segmentDir;

        return 1;
    }

    public static int CheckSegmentInfiniteCylinderIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        //xA > _segmentPointA
        //xC > _cylinderOrigin

        Vector3 cSegmentPointADir = _segmentPointA - _cylinderOrigin; // X, Y, Z
        Vector3 segmentDir = _segmentPointB - _segmentPointA; // u

        //cylinderUp // v

        Vector3 w1 = new Vector3(cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * _cylinderUp.x, segmentDir.y * _cylinderUp.y, segmentDir.z * _cylinderUp.z);
        Vector3 w6 = new Vector3(_cylinderUp.y * _cylinderUp.y + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.y * _cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);


        float _cylinderUpSquareLength = _cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, _cylinderUp).sqrMagnitude / _cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / _cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / _cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;


        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0 && t < 1)
            {
                _result = _segmentPointA + t * segmentDir;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1sCondition = t1 >= 0 && t1 <= 1;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {

            if (t0 < t1)
            {
                _result = _segmentPointA + t0 * segmentDir;
                _result2 = _segmentPointA + t1 * segmentDir;
            }
            else
            {
                _result = _segmentPointA + t1 * segmentDir;
                _result2 = _segmentPointA + t0 * segmentDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _segmentPointA + t0 * segmentDir;
        else
            _result = _segmentPointA + t1 * segmentDir;

        return 1;

    }


    public static int CheckSegmentCylinderIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 segmentDir = _segmentPointB - _segmentPointA; // u
        int count = ParametricCollisionUtility.CheckSegmentCylinderWihtoutCapsIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if(count == 2)
        {
            _result = _segmentPointA + t0 * segmentDir;
            _result2 = _segmentPointA + t1 * segmentDir;

            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f;
        bool result1 = ParametricCollisionUtility.CheckSegmentCircleIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin + _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t2);
        bool result2 = ParametricCollisionUtility.CheckSegmentCircleIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin - _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t3);

        if(result1 && result2)
        {
            t0 = t2;
            t1 = t3;

            count = 2;
        }
        else if (result1 == true || result2 == true)
        {
            if (result2)
                t2 = t3;

            if (count == 1)
            {
                t1 = t2;
                count = 2;
            }
            else
            {
                t0 = t2;
                count = 1;
            }
        }

        if(count == 2)
        {
            if (t0 < t1)
            {
                _result = _segmentPointA + t0 * segmentDir;
                _result2 = _segmentPointA + t1 * segmentDir;
            }
            else
            {
                _result = _segmentPointA + t1 * segmentDir;
                _result2 = _segmentPointA + t0 * segmentDir;
            }

            return 2;
        }
        else if(count == 1)
        {
            _result = _segmentPointA + t0 * segmentDir;
            return 1;
        }

        return 0;
    }



    public static int CheckSegmentCapsuleIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckSegmentCapsuleIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 0)
            return 0;

        if(count == 1)
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            _result = _segmentPointA + t0 * segmentDir;
            return 1;
        }
        else
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            _result = _segmentPointA + t0 * segmentDir;
            _result2 = _segmentPointA + t1 * segmentDir;
            return 2;
        }
    }

    public static int CheckSegmentMinkowskiBoxSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckSegmentMinkowskiBoxSphereIntersection(_segmentPointA, _segmentPointB, boxCenter, boxSize, boxRotation, sphereRadius, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            _result = _segmentPointA + t0 * segmentDir;
            return 1;
        }
        else
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            _result = _segmentPointA + t0 * segmentDir;
            _result2 = _segmentPointA + t1 * segmentDir;
            return 2;
        }
    }

    public static bool CheckRayPointIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
    {
        Vector3 T2_pointDir = _pointA - _rayStartPoint;

        float dot = Vector3.Dot(T2_pointDir, _rayDirection);
        float squaredMagnitude = _rayDirection.sqrMagnitude;

        if (dot > 0)
        {
            Vector3 projectPoint = new Vector3(_rayDirection.x * dot / squaredMagnitude,
                    _rayDirection.y * dot / squaredMagnitude,
                    _rayDirection.z * dot / squaredMagnitude);

            if ((T2_pointDir - projectPoint).sqrMagnitude < distanceThreshold * distanceThreshold)
            {
                _result = (projectPoint + _rayStartPoint);
                return true;
            }
        }

        return false;
    }

    public static bool CheckRaySegmentIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _segmentPointA, Vector3 _segmentPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _rayDirection;
        Vector3 segmentBDir = _segmentPointB - _segmentPointA;

        Vector3 p1 = _rayStartPoint;
        Vector3 p2 = _rayStartPoint + _rayDirection;
        Vector3 p3 = _segmentPointA;
        Vector3 p4 = _segmentPointB;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;

        float tB = tBNumerator / tBDenominator;

        tA = tA < 0 ? 0 : tA;
        tB = Mathf.Clamp01(tB);

        Vector3 pA = _rayStartPoint + tA * segmentADir;
        Vector3 pB = _segmentPointA + tB * segmentBDir;
        _result = pA;
        _result2 = pB;

        

        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckRayRayIntersection(Vector3 _rayAStartPoint, Vector3 _rayADirection, Vector3 _rayBStartPoint, Vector3 _rayBDirection, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _rayADirection;
        Vector3 segmentBDir = _rayBDirection;

        Vector3 p1 = _rayAStartPoint;
        Vector3 p2 = _rayAStartPoint + _rayADirection;
        Vector3 p3 = _rayBStartPoint;
        Vector3 p4 = _rayBStartPoint + _rayBDirection;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;

        float tB = tBNumerator / tBDenominator;

        tA = tA < 0 ? 0 : tA;
        tB = tB < 0 ? 0 : tB;

        Vector3 pA = _rayAStartPoint + tA * segmentADir;
        Vector3 pB = _rayBStartPoint + tB * segmentBDir;
        _result = pA;
        _result2 = pB;



        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckRayPlanIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
    {
        float t = 0;
        Vector3 segmentDir = _rayDirection;
        if (ParametricCollisionUtility.CheckRayPlanIntersection(_rayStartPoint, _rayDirection, _planPoint, _planNormal, ref t))
        {
            _result = _rayStartPoint + t * _rayDirection;
            return true;
        }

        return false;
    }

    public static bool CheckRayPlanXZIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
    {
        if (_rayStartPoint.y > 0 && _rayDirection.y > 0 || _rayStartPoint.y < 0 && _rayDirection.y < 0)
            return false;

        float tDenominator = _rayDirection.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_rayStartPoint.y / tDenominator;
        _result = _rayStartPoint + t * _rayDirection;

        return true;
    }

    public static bool CheckRayPlanXYIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
    {
        if (_rayStartPoint.z > 0 && _rayDirection.z > 0 || _rayStartPoint.z < 0 && _rayDirection.z < 0)
            return false;

        float tDenominator = _rayDirection.z;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_rayStartPoint.z / tDenominator;
        _result = _rayStartPoint + t * _rayDirection;

        return true;
    }

    public static bool CheckRayPlanYZIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref Vector3 _result)
    {
        if (_rayStartPoint.x > 0 && _rayDirection.x > 0 || _rayStartPoint.x < 0 && _rayDirection.x < 0)
            return false;

        float tDenominator = _rayDirection.x;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_rayStartPoint.x / tDenominator;
        _result = _rayStartPoint + t * _rayDirection;

        return true;
    }

    public static bool CheckRayQuadIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _rayStartPoint = MathUtility.WorldBaseChangementOptimised(_rayStartPoint, quadPointA, rotation, Vector3.one);
        _rayDirection = MathUtility.WorldBaseChangementDirection(_rayDirection, rotation);

        Vector3 planResultPoint = Vector3.one;
        if (CheckRayPlanXZIntersection(_rayStartPoint, _rayDirection, ref planResultPoint))
        {
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _resultPoint = MathUtility.BaseToWorld(planResultPoint, quadPointA, rotation, Vector3.one);
                return true;
            }
        }

        return false;
    }

    public static int CheckRaySphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 sphereDir = _rayStartPoint - _SphereOrigin;
        float a = _rayDirection.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(_rayDirection, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        float deltaSqrt = Mathf.Sqrt(delta);
        float a2 = 2 * a;

        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0)
            {
                _result = _rayStartPoint + t * _rayDirection;
                return 1;
            }
            else
                return 0;
        }


        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0;
        bool t1sCondition = t1 >= 0;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _result = _rayStartPoint + t0 * _rayDirection;
                _result2 = _rayStartPoint + t1 * _rayDirection;
            }
            else
            {
                _result = _rayStartPoint + t1 * _rayDirection;
                _result2 = _rayStartPoint + t0 * _rayDirection;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _rayStartPoint + t0 * _rayDirection;
        else
            _result = _rayStartPoint + t1 * _rayDirection;

        return 1;
    }

    public static int CheckRaySphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 sphereDir = _rayStartPoint;
        float a = _rayDirection.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(_rayDirection, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        float a2 = 2 * a;
        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0)
            {
                _result = _rayStartPoint + t * _rayDirection;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0;
        bool t1sCondition = t1 >= 0;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _result = _rayStartPoint + t0 * _rayDirection;
                _result2 = _rayStartPoint + t1 * _rayDirection;
            }
            else
            {
                _result = _rayStartPoint + t1 * _rayDirection;
                _result2 = _rayStartPoint + t0 * _rayDirection;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _rayStartPoint + t0 * _rayDirection;
        else
            _result = _rayStartPoint + t1 * _rayDirection;

        return 1;
    }

    public static int CheckRayInfiniteCylinderIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 cSegmentPointADir = _rayStartPoint - _cylinderOrigin; 
        Vector3 segmentDir = _rayDirection;

        Vector3 w1 = new Vector3(cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * _cylinderUp.x, segmentDir.y * _cylinderUp.y, segmentDir.z * _cylinderUp.z);
        Vector3 w6 = new Vector3(_cylinderUp.y * _cylinderUp.y + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.y * _cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);


        float _cylinderUpSquareLength = _cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, _cylinderUp).sqrMagnitude / _cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / _cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / _cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;


        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0)
            {
                _result = _rayStartPoint + t * segmentDir;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0;
        bool t1sCondition = t1 >= 0;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            Vector3 pA = _rayStartPoint + t0 * segmentDir;
            float pADotC = Vector3.Dot((pA - _cylinderOrigin), _cylinderUp);


            if (t0 < t1)
            {
                _result = _rayStartPoint + t0 * segmentDir;
                _result2 = _rayStartPoint + t1 * segmentDir;
            }
            else
            {
                _result = _rayStartPoint + t1 * segmentDir;
                _result2 = _rayStartPoint + t0 * segmentDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _rayStartPoint + t0 * segmentDir;
        else
            _result = _rayStartPoint + t1 * segmentDir;

        return 1;

    }

    public static int CheckRayCylinderIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 segmentDir = _rayDirection; // u
        int count = ParametricCollisionUtility.CheckRayCylinderWihtoutCapsIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _result = _rayStartPoint + t0 * segmentDir;
            _result2 = _rayStartPoint + t1 * segmentDir;

            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f;
        bool result1 = ParametricCollisionUtility.CheckRayCircleIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin + _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t2);
        bool result2 = ParametricCollisionUtility.CheckRayCircleIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin - _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t3);

        if (result1 && result2)
        {
            t0 = t2;
            t1 = t3;

            count = 2;

            return 2;
        }
        else if(result1 == true || result2 == true)
        {
            if (result2)
                t2 = t3;

            if (count == 1)
            {
                t1 = t2;
                count = 2;
            }
            else
            {
                t0 = t2;
                count = 1;
            }
        }

        if (count == 2)
        {
            if (t0 < t1)
            {
                _result = _rayStartPoint + t0 * segmentDir;
                _result2 = _rayStartPoint + t1 * segmentDir;
            }
            else
            {
                _result = _rayStartPoint + t1 * segmentDir;
                _result2 = _rayStartPoint + t0 * segmentDir;
            }

            return 2;
        }
        else if (count == 1)
        {
            _result = _rayStartPoint + t0 * segmentDir;
            return 1;
        }

        return 0;
    }

    public static int CheckRayCapsuleIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckRayCapsuleIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            _result = _rayStartPoint + t0 * _rayDirection;
            return 1;
        }
        else
        {
            _result = _rayStartPoint + t0 * _rayDirection;
            _result2 = _rayStartPoint + t1 * _rayDirection;
            return 2;
        }
    }

    public static int CheckRayMinkowskiBoxSphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckRayMinkowskiBoxSphereIntersection(_rayStartPoint, _rayDirection, boxCenter, boxSize, boxRotation, sphereRadius, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            _result = _rayStartPoint + t0 * _rayDirection;
            return 1;
        }
        else
        {
            _result = _rayStartPoint + t0 * _rayDirection;
            _result2 = _rayStartPoint + t1 * _rayDirection;
            return 2;
        }
    }

    public static bool CheckLinePointIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _pointA, float distanceThreshold)
    {
        Vector3 T2_pointDir = _pointA - _linePoint;

        float dot = Vector3.Dot(T2_pointDir, _lineDirection);
        float squaredMagnitude = _lineDirection.sqrMagnitude;

        Vector3 projectPoint = new Vector3(_lineDirection.x * dot / squaredMagnitude,
                    _lineDirection.y * dot / squaredMagnitude,
                    _lineDirection.z * dot / squaredMagnitude);

        if ((T2_pointDir - projectPoint).sqrMagnitude < distanceThreshold * distanceThreshold)
        {
            return true;
        }

        return false;
    }

    public static bool CheckLineSegmentIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _segmentPointA, Vector3 _segmentPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _lineDirection;
        Vector3 segmentBDir = _segmentPointB - _segmentPointA;

        Vector3 p1 = _linePoint;
        Vector3 p2 = _linePoint + _lineDirection;
        Vector3 p3 = _segmentPointA;
        Vector3 p4 = _segmentPointB;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;

        float tB = tBNumerator / tBDenominator;

        tB = Mathf.Clamp01(tB);

        Vector3 pA = _linePoint + tA * segmentADir;
        Vector3 pB = _segmentPointA + tB * segmentBDir;
        _result = pA;
        _result2 = pB;



        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckLineRayIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _rayBStartPoint, Vector3 _rayBDirection, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _lineDirection;
        Vector3 segmentBDir = _rayBDirection;

        Vector3 p1 = _linePoint;
        Vector3 p2 = _linePoint + _lineDirection;
        Vector3 p3 = _rayBStartPoint;
        Vector3 p4 = _rayBStartPoint + _rayBDirection;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;

        float tB = tBNumerator / tBDenominator;

        tB = tB < 0 ? 0 : tB;

        Vector3 pA = _linePoint + tA * segmentADir;
        Vector3 pB = _rayBStartPoint + tB * segmentBDir;
        _result = pA;
        _result2 = pB;


        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckLineLineIntersection(Vector3 _lineAPoint, Vector3 _lineADirection, Vector3 _lineBPoint, Vector3 _lineBDirection, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 segmentADir = _lineADirection;
        Vector3 segmentBDir = _lineBDirection;

        Vector3 p1 = _lineAPoint;
        Vector3 p2 = _lineAPoint + _lineADirection;
        Vector3 p3 = _lineBPoint;
        Vector3 p4 = _lineBPoint + _lineBDirection;

        float d1343 = (p1.x - p3.x) * (p4.x - p3.x) + (p1.y - p3.y) * (p4.y - p3.y) + (p1.z - p3.z) * (p4.z - p3.z);
        float d4321 = (p4.x - p3.x) * (p2.x - p1.x) + (p4.y - p3.y) * (p2.y - p1.y) + (p4.z - p3.z) * (p2.z - p1.z);
        float d1321 = (p1.x - p3.x) * (p2.x - p1.x) + (p1.y - p3.y) * (p2.y - p1.y) + (p1.z - p3.z) * (p2.z - p1.z);
        float d4343 = (p4.x - p3.x) * (p4.x - p3.x) + (p4.y - p3.y) * (p4.y - p3.y) + (p4.z - p3.z) * (p4.z - p3.z);
        float d2121 = (p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y) + (p2.z - p1.z) * (p2.z - p1.z);

        float tANumerator = d1343 * d4321 - d1321 * d4343;
        float tADenominator = d2121 * d4343 - d4321 * d4321;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(tADenominator) < epsilon)
            return false;

        float tA = tANumerator / tADenominator;

        float tBNumerator = d1343 + tA * d4321;
        float tBDenominator = d4343;

        if (Mathf.Abs(tBDenominator) < epsilon)
            return false;

        float tB = tBNumerator / tBDenominator;

        Vector3 pA = _lineAPoint + tA * segmentADir;
        Vector3 pB = _lineBPoint + tB * segmentBDir;
        _result = pA;
        _result2 = pB;


        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckLinePlanIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
    {
        float t = 0;
        if (ParametricCollisionUtility.CheckLinePlanIntersection(_linePoint, _lineDirection, _planPoint, _planNormal, ref t))
        {
            _result = _linePoint + t * _lineDirection;
            return true;
        }

        return false;
    }

    public static bool CheckLinePlanXZIntersection(Vector3 _linePoint, Vector3 _lineDirection, ref Vector3 _result)
    {
        float tDenominator = _lineDirection.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_linePoint.y / tDenominator;
        _result = _linePoint + t * _lineDirection;

        return true;
    }

    public static bool CheckLinePlanXYIntersection(Vector3 _linePoint, Vector3 _lineDirection, ref Vector3 _result)
    {
        float tDenominator = _lineDirection.z;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_linePoint.z / tDenominator;
        _result = _linePoint + t * _lineDirection;

        return true;
    }

    public static bool CheckLinePlanYZIntersection(Vector3 _linePoint, Vector3 _lineDirection, ref Vector3 _result)
    {
        float tDenominator = _lineDirection.x;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_linePoint.x / tDenominator;
        _result = _linePoint + t * _lineDirection;

        return true;
    }

    public static bool CheckLineQuadIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _linePoint = MathUtility.WorldBaseChangementOptimised(_linePoint, quadPointA, rotation, Vector3.one);
        _lineDirection = MathUtility.WorldBaseChangementDirection(_lineDirection, rotation);

        Vector3 planResultPoint = Vector3.one;
        if (CheckLinePlanXZIntersection(_linePoint, _lineDirection, ref planResultPoint))
        {
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _resultPoint = MathUtility.BaseToWorld(planResultPoint, quadPointA, rotation, Vector3.one);
                return true;
            }
        }

        return false;
    }

    public static int CheckLineSphereIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 sphereDir = _linePoint - _SphereOrigin;
        float a = _lineDirection.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(_lineDirection, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;


        float a2 = 2 * a;

        if (delta < epsilon)
        {
            float t = -b / a2;
            _result = _linePoint + t * _lineDirection;
            return 1;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        if (t0 < t1)
        {
            _result = _linePoint + t0 * _lineDirection;
            _result2 = _linePoint + t1 * _lineDirection;
        }
        else
        {
            _result = _linePoint + t1 * _lineDirection;
            _result2 = _linePoint + t0 * _lineDirection;
        }
        return 2;
    }

    public static int CheckLineSphereIntersection(Vector3 _linePoint, Vector3 _lineDirection, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 sphereDir = _linePoint;
        float a = _lineDirection.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(_lineDirection, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

   

        float delta = b * b - 4.0f * a * c;
        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        float a2 = 2 * a;
        if (delta < epsilon)
        {
            float t = -b / a2;
            _result = _linePoint + t * _lineDirection;
            return 1;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        if (t0 < t1)
        {
            _result = _linePoint + t0 * _lineDirection;
            _result2 = _linePoint + t1 * _lineDirection;
        }
        else
        {
            _result = _linePoint + t1 * _lineDirection;
            _result2 = _linePoint + t0 * _lineDirection;
        }
        return 2;
    }

    public static int CheckLineInfiniteCylinderIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 cSegmentPointADir = _linePoint - _cylinderOrigin;
        Vector3 segmentDir = _lineDirection;

        Vector3 w1 = new Vector3(cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * _cylinderUp.y, cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * _cylinderUp.z, cSegmentPointADir.x * _cylinderUp.x, cSegmentPointADir.y * _cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * _cylinderUp.x, segmentDir.y * _cylinderUp.y, segmentDir.z * _cylinderUp.z);
        Vector3 w6 = new Vector3(_cylinderUp.y * _cylinderUp.y + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.y * _cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);


        float _cylinderUpSquareLength = _cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, _cylinderUp).sqrMagnitude / _cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / _cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / _cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = 1e-7f;
        if (delta < -epsilon)
            return 0;

        if (delta < epsilon)
        {
            float t = -b / a2;
            _result = _linePoint + t * _lineDirection;
            return 1;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        if (t0 < t1)
        {
            _result = _linePoint + t0 * segmentDir;
            _result2 = _linePoint + t1 * segmentDir;
        }
        else
        {
            _result = _linePoint + t1 * segmentDir;
            _result2 = _linePoint + t0 * segmentDir;
        }
        return 2;
    }

    public static int CheckLineCylinderIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 segmentDir = _lineDirection; // u
        int count = ParametricCollisionUtility.CheckLineCylinderWihtoutCapsIntersection(_linePoint, _lineDirection, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _result = _linePoint + t0 * segmentDir;
            _result2 = _linePoint + t1 * segmentDir;

            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f;
        bool result1 = ParametricCollisionUtility.CheckLineCircleIntersection(_linePoint, _lineDirection, _cylinderOrigin + _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t2);
        bool result2 = ParametricCollisionUtility.CheckLineCircleIntersection(_linePoint, _lineDirection, _cylinderOrigin - _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t3);

        if (result1 && result2)
        {
            t0 = t2;
            t1 = t3;

            count = 2;

            return 2;
        }
        else if (result1 == true || result2 == true)
        {
            if (result2)
                t2 = t3;

            if (count == 1)
            {
                t1 = t2;
                count = 2;
            }
            else
            {
                t0 = t2;
                count = 1;
            }
        }

        if (count == 2)
        {
            if (t0 < t1)
            {
                _result = _linePoint + t0 * segmentDir;
                _result2 = _linePoint + t1 * segmentDir;
            }
            else
            {
                _result = _linePoint + t1 * segmentDir;
                _result2 = _linePoint + t0 * segmentDir;
            }

            return 2;
        }
        else if (count == 1)
        {
            _result = _linePoint + t0 * segmentDir;
            return 1;
        }

        return 0;
    }

    public static int CheckLineCapsuleIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckLineCapsuleIntersection(_linePoint, _lineDirection, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            _result = _linePoint + t0 * _lineDirection;
            return 1;
        }
        else
        {
            _result = _linePoint + t0 * _lineDirection;
            _result2 = _linePoint + t1 * _lineDirection;
            return 2;
        }
    }


    public static int CheckLineMinkowskiBoxSphereIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckRayMinkowskiBoxSphereIntersection(_linePoint, _lineDirection, boxCenter, boxSize, boxRotation, sphereRadius, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            _result = _linePoint + t0 * _lineDirection;
            return 1;
        }
        else
        {
            _result = _linePoint + t0 * _lineDirection;
            _result2 = _linePoint + t1 * _lineDirection;
            return 2;
        }
    }

}
