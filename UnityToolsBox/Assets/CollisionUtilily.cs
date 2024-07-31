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

    public static bool CheckLinePointIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _pointA, float distanceThreshold, ref Vector3 _result)
    {
        Vector3 T2_pointDir = _pointA - _linePointA;
        Vector3 T2_lineDir = _linePointB - _linePointA;

        float dot = Vector3.Dot(T2_pointDir, T2_lineDir);
        float squaredMagnitude = T2_lineDir.sqrMagnitude;

        if (dot > 0 && dot < squaredMagnitude)
        {
            Vector3 projectPoint = new Vector3(T2_lineDir.x * dot / squaredMagnitude,
                    T2_lineDir.y * dot / squaredMagnitude,
                    T2_lineDir.z * dot / squaredMagnitude);

            if ((T2_pointDir - projectPoint).sqrMagnitude < distanceThreshold * distanceThreshold)
            {
                _result = (projectPoint + _linePointA);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// The shortest line between two lines in 3D from paulbourke
    /// https://paulbourke.net/geometry/pointlineplane/
    /// </summary>
    /// <param name="_lineAPointA"></param>
    /// <param name="_lineAPointB"></param>
    /// <param name="_lineBPointA"></param>
    /// <param name="_lineBPointB"></param>
    /// <param name="_result"></param>
    /// <param name="_distancethreshold"></param>
    /// <returns></returns>
    public static bool CheckLineLineIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _lineBPointA, Vector3 _lineBPointB, float _distancethreshold, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 lineADir = _lineAPointB - _lineAPointA;
        Vector3 lineBDir = _lineBPointB - _lineBPointA;

        Vector3 p1 = _lineAPointA;
        Vector3 p2 = _lineAPointB;
        Vector3 p3 = _lineBPointA;
        Vector3 p4 = _lineBPointB;

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
        Vector3 pA = _lineAPointA + tA * lineADir;
        Vector3 pB = _lineBPointA + tB * lineBDir;
        _result = pA;
        _result2 = pB;

        if ((pB - pA).sqrMagnitude <= _distancethreshold)
            return true;

        return false;
    }

    public static bool CheckLinePlanIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
    {
        float t = 0;
        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        if (ParametricCollisionUtility.CheckLinePlanIntersection(_lineAPointA, _lineAPointB, _planPoint, _planNormal, ref t))
        {
            _result = _lineAPointA + t * lineDir;
            return true;
        }

        return false;
    }


    public static bool CheckLinePlanXZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
    {
        Debug.DrawLine(_lineAPointA, _lineAPointB, Color.cyan);


        if (_lineAPointA.y > 0 && _lineAPointB.y > 0 || _lineAPointA.y < 0 && _lineAPointB.y < 0)
            return false;

        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        float tDenominator = lineDir.y;
        float epsilon = Mathf.Epsilon;

        //Line is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_lineAPointA.y / tDenominator;
        _result = _lineAPointA + t * lineDir;

        return true;
    }

    public static bool CheckLinePlanXYIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
    {
        if (_lineAPointA.z > 0 && _lineAPointB.z > 0 || _lineAPointA.z < 0 && _lineAPointB.z < 0)
            return false;

        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        float tDenominator = lineDir.z;
        float epsilon = Mathf.Epsilon;

        //Line is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = _lineAPointA.z / tDenominator;
        _result = _lineAPointA + t * lineDir;

        return true;
    }

    public static bool CheckLinePlanYZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref Vector3 _result)
    {
        if (_lineAPointA.x > 0 && _lineAPointB.x > 0 || _lineAPointA.x < 0 && _lineAPointB.x < 0)
            return false;

        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        float tDenominator = lineDir.x;
        float epsilon = Mathf.Epsilon;

        //Line is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = _lineAPointA.x / tDenominator;
        _result = _lineAPointA + t * lineDir;

        return true;
    }

    public static bool CheckLineQuadIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref Vector3 _resultPoint)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _lineAPointA = MathUtility.WorldBaseChangementOptimised(_lineAPointA, quadPointA, rotation, Vector3.one);
        _lineAPointB = MathUtility.WorldBaseChangementOptimised(_lineAPointB, quadPointA, rotation, Vector3.one);

        Vector3 planResultPoint = Vector3.one;
        if (CheckLinePlanXZIntersection(_lineAPointA, _lineAPointB, ref planResultPoint))
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

   

    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _SphereOrigin, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 lineDir = _linePointB - _linePointA;
        Vector3 sphereDir = _linePointA - _SphereOrigin;
        float a = lineDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(lineDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        if (c <= 0)
            return 0;

        float delta = b * b - 4.0f * a * c;
        if (delta <= 0)
            return 0;

        float deltaSqrt = Mathf.Sqrt(delta);
        float a2 = 2 * a;
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
                _result = _linePointA + t0 * lineDir;
                _result2 = _linePointA + t1 * lineDir;
            }
            else
            {
                _result = _linePointA + t1 * lineDir;
                _result2 = _linePointA + t0 * lineDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _linePointA + t0 * lineDir;
        else
            _result = _linePointA + t1 * lineDir;

        return 1;
    }

    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, float _radius, ref Vector3 _result, ref Vector3 _result2)
    {
        Vector3 lineDir = _linePointB - _linePointA;
        Vector3 sphereDir = _linePointA;
        float a = lineDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(lineDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        if (c <= 0)
            return 0;

        float delta = b * b - 4.0f * a * c;
        if (delta <= 0)
            return 0;

        float a2 = 2 * a;
        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(delta) < epsilon)
        {
            float t = -b / a2;
            _result = _linePointA + t * lineDir;
            return 1;
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
                _result = _linePointA + t0 * lineDir;
                _result2 = _linePointA + t1 * lineDir;
            }
            else
            {
                _result = _linePointA + t1 * lineDir;
                _result2 = _linePointA + t0 * lineDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _linePointA + t0 * lineDir;
        else
            _result = _linePointA + t1 * lineDir;

        return 1;
    }

    public static int CheckLineInfiniteCylinderIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        //xA > _linePointA
        //xC > _cylinderOrigin

        Vector3 cLinePointADir = _linePointA - _cylinderOrigin; // X, Y, Z
        Vector3 lineDir = _linePointB - _linePointA; // u

        //cylinderUp // v

        Vector3 w1 = new Vector3(cLinePointADir.x * _cylinderUp.x, cLinePointADir.y * _cylinderUp.y, cLinePointADir.z * _cylinderUp.z);
        Vector3 w2 = new Vector3(cLinePointADir.y * _cylinderUp.y, cLinePointADir.z * _cylinderUp.z, cLinePointADir.x * _cylinderUp.x);
        Vector3 w3 = new Vector3(cLinePointADir.z * _cylinderUp.z, cLinePointADir.x * _cylinderUp.x, cLinePointADir.y * _cylinderUp.y);
        Vector3 w4 = new Vector3(cLinePointADir.x * lineDir.x, cLinePointADir.y * lineDir.y, cLinePointADir.z * lineDir.z);
        Vector3 w5 = new Vector3(lineDir.x * _cylinderUp.x, lineDir.y * _cylinderUp.y, lineDir.z * _cylinderUp.z);
        Vector3 w6 = new Vector3(_cylinderUp.y * _cylinderUp.y + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.z * _cylinderUp.z, _cylinderUp.x * _cylinderUp.x + _cylinderUp.y * _cylinderUp.y);
        Vector3 w7 = new Vector3(cLinePointADir.x * cLinePointADir.x, cLinePointADir.y * cLinePointADir.y, cLinePointADir.z * cLinePointADir.z);


        float _cylinderUpSquareLength = _cylinderUp.magnitude;
        float a = Vector3.Cross(lineDir, _cylinderUp).sqrMagnitude / _cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / _cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / _cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(delta) < epsilon)
            return 0;

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1sCondition = t1 >= 0 && t1 <= 1;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            Vector3 pA = _linePointA + t0 * lineDir;
            Vector3 pB = _linePointB + t0 * lineDir;

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), _cylinderUp);


            if (t0 < t1)
            {
                _result = _linePointA + t0 * lineDir;
                _result2 = _linePointA + t1 * lineDir;
            }
            else
            {
                _result = _linePointA + t1 * lineDir;
                _result2 = _linePointA + t0 * lineDir;
            }
            return 2;
        }
        else if (t0Condition)
            _result = _linePointA + t0 * lineDir;
        else
            _result = _linePointA + t1 * lineDir;

        return 1;

    }


    public static int CheckLineCylinderIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 lineDir = _linePointB - _linePointA; // u
        int count = ParametricCollisionUtility.CheckLineCylinderWihtoutCapsIntersection(_linePointA, _linePointB, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if(count == 2)
        {
            _result = _linePointA + t0 * lineDir;
            _result2 = _linePointA + t1 * lineDir;

            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f;
        bool result1 = ParametricCollisionUtility.CheckLineCircleIntersection(_linePointA, _linePointB, _cylinderOrigin + _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t2);
        bool result2 = ParametricCollisionUtility.CheckLineCircleIntersection(_linePointA, _linePointB, _cylinderOrigin - _cylinderUp * halfHeight, _cylinderUp, _cylinderRadius, ref t3);

        if(result1 && result2)
        {
            t0 = t2;
            t1 = t3;

            count = 2;
        }
        else
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
                _result = _linePointA + t0 * lineDir;
                _result2 = _linePointA + t1 * lineDir;
            }
            else
            {
                _result = _linePointA + t1 * lineDir;
                _result2 = _linePointA + t0 * lineDir;
            }

            return 2;
        }
        else if(count == 1)
        {
            _result = _linePointA + t0 * lineDir;
            return 1;
        }

        return 0;
    }



    public static int CheckLineCapsuleIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 _cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckLineCapsuleIntersection(_linePointA, _linePointB, _cylinderOrigin, _cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 0)
            return 0;

        if(count == 1)
        {
            Vector3 lineDir = _linePointB - _linePointA;
            _result = _linePointA + t0 * lineDir;
            return 1;
        }
        else
        {
            Vector3 lineDir = _linePointB - _linePointA;
            _result = _linePointA + t0 * lineDir;
            _result2 = _linePointA + t1 * lineDir;
            return 2;
        }
    }

    public static int CheckIntersectionLineMinkowskiBoxSphere(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = ParametricCollisionUtility.CheckLineMinkowskiBoxSphereIntersection(_linePointA, _linePointB, boxCenter, boxSize, boxRotation, sphereRadius, ref t0, ref t1);
        if (count == 0)
            return 0;

        if (count == 1)
        {
            Vector3 lineDir = _linePointB - _linePointA;
            _result = _linePointA + t0 * lineDir;
            return 1;
        }
        else
        {
            Vector3 lineDir = _linePointB - _linePointA;
            _result = _linePointA + t0 * lineDir;
            _result2 = _linePointA + t1 * lineDir;
            return 2;
        }
    }

}
