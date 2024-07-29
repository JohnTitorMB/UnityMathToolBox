using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionUtilily
{
    public static bool CheckPointPointIntersection(Vector3 pointA, Vector3 pointB, float distanceThrhold, ref Vector3 result)
    {
        if ((pointA - pointB).sqrMagnitude <= distanceThrhold * distanceThrhold)
        {
            result = (pointA + pointB) / 2.0f;
            return true;
        }
        else
            return false;
    }

    public static bool CheckPointLineIntersection(Vector3 _pointA, Vector3 _linePointA, Vector3 _linePointB, float distanceThreshold, ref Vector3 _result)
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
    public static bool CheckLineLineIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _lineBPointA, Vector3 _lineBPointB, ref Vector3 _result, ref Vector3 _result2, float _distancethreshold)
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

    public static bool CheckLinePlanIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _planPoint, Vector3 _planNormal, ref float _t)
    {
        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        float tNumerator = Vector3.Dot(_planNormal, _planPoint - _lineAPointA);
        float tDenominator = Vector3.Dot(_planNormal, lineDir);
        float epsilon = Mathf.Epsilon;

        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = tNumerator / tDenominator;
        float lineDirMagitude = lineDir.magnitude;
        if (t <= 0 || t >= lineDirMagitude)
            return false;

        _t = t;
        return true;
    }

    public static bool CheckLinePlanIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _planPoint, Vector3 _planNormal, ref Vector3 _result)
    {
        float t = 0;
        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        if (CheckLinePlanIntersection(_lineAPointA, _lineAPointB, _planPoint, _planNormal, ref t))
        {
            _result = _lineAPointA + t * lineDir;
            return true;
        }

        return false;
    }

    public static bool CheckLinePlanXZIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, ref float _t0)
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

        _t0 = -_lineAPointA.y / tDenominator;
        
        return true;
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

    public static Vector3 BaseChangement(Vector3 _point, Matrix4x4 baseA, Matrix4x4 baseB)
    {
        Vector3 worldPoint = baseA * _point;
        Vector3 baseChangement = baseB.inverse * worldPoint;

        return baseChangement;
    }

    public static Vector3 BaseChangementOptimised(Vector3 _point, Vector3 _baseATranslation, Quaternion _baseARotation, Vector3 baseAScale
                                                  , Vector3 _baseBTranslation, Quaternion _baseBRotation, Vector3 baseBScale)
    {
        Vector3 _pointScale = _point;
        _pointScale.Scale(baseAScale);
        Vector3 worldPoint = _baseARotation * _pointScale + _baseATranslation;

        Vector3 baseBInverteScale = new Vector3(1.0f / baseBScale.x, 1.0f / baseBScale.y, 1.0f / baseBScale.z);
        Vector3 newPoint = Quaternion.Inverse(_baseBRotation) * (worldPoint - _baseBTranslation);
        newPoint.Scale(baseBInverteScale);

        return newPoint;
    }

    public static Vector3 WorldBaseChangementOptimised(Vector3 _point, Vector3 _targetBaseTranslation, Quaternion _targetBaseRotation, Vector3 _targetBaseScale)
    {
        Vector3 baseBInverteScale = new Vector3(1.0f / _targetBaseScale.x, 1.0f / _targetBaseScale.y, 1.0f / _targetBaseScale.z);
        Vector3 newPoint = Quaternion.Inverse(_targetBaseRotation) * (_point - _targetBaseTranslation);
        newPoint.Scale(baseBInverteScale);

        return newPoint;
    }

    public static Vector3 BaseToWorld(Vector3 _point, Vector3 _baseTranslation, Quaternion _baseRotation, Vector3 _baseScale)
    {
        Vector3 _pointScale = _point;
        _pointScale.Scale(_baseScale);
        Vector3 worldPoint = _baseRotation * _pointScale + _baseTranslation;
        return worldPoint;
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
        _lineAPointA = WorldBaseChangementOptimised(_lineAPointA, quadPointA, rotation, Vector3.one);
        _lineAPointB = WorldBaseChangementOptimised(_lineAPointB, quadPointA, rotation, Vector3.one);

        Vector3 planResultPoint = Vector3.one;
        if (CheckLinePlanXZIntersection(_lineAPointA, _lineAPointB, ref planResultPoint))
        {
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _resultPoint = BaseToWorld(planResultPoint, quadPointA, rotation, Vector3.one);
                return true;
            }
        }

        return false;
    }

    public static bool CheckLineQuadIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref float _t0)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _lineAPointA = WorldBaseChangementOptimised(_lineAPointA, quadPointA, rotation, Vector3.one);
        _lineAPointB = WorldBaseChangementOptimised(_lineAPointB, quadPointA, rotation, Vector3.one);

        float t = 0.0f;
        if (CheckLinePlanXZIntersection(_lineAPointA, _lineAPointB, ref t))
        {
            Vector3 lineDir = (_lineAPointB - _lineAPointA);
            Vector3 planResultPoint = _lineAPointA + t * lineDir;
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _t0 = t;
                return true;
            }
        }

        return false;
    }

    public static bool CheckLineCircleIntersection(Vector3 _lineAPointA, Vector3 _lineAPointB, Vector3 _circleOrigin, Vector3 _circleNormal, float _radius, ref float _t)
    {
        Vector3 lineDir = (_lineAPointB - _lineAPointA);
        float t = 0;
        if (CheckLinePlanIntersection(_lineAPointA, _lineAPointB, _circleOrigin, _circleNormal, ref t))
        {
            Vector3 point = _lineAPointA + t * lineDir;
            if((point - _circleOrigin).sqrMagnitude <= _radius * _radius)
            {
                _t = t;
                return true;
            }
        }

        return false;
    }

    public static int CheckLineSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _SphereOrigin, float _radius, ref float _t0, ref float _t1)
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
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }
            return 2;
        }
        else if (t0Condition)
            _t0 = t0;
        else
            _t0 = t1;

        return 1;
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

    public static int CheckIntersectionLineInfiniteCylindreMethod1(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, ref Vector3 _result, ref Vector3 _result2)
    {
        //xA > _linePointA
        //xC > _cylinderOrigin

        Vector3 cLinePointADir = _linePointA - _cylinderOrigin; // X, Y, Z
        Vector3 lineDir = _linePointB - _linePointA; // u

        //cylinderUp // v

        Vector3 w1 = new Vector3(cLinePointADir.x * cylinderUp.x, cLinePointADir.y * cylinderUp.y, cLinePointADir.z * cylinderUp.z);
        Vector3 w2 = new Vector3(cLinePointADir.y * cylinderUp.y, cLinePointADir.z * cylinderUp.z, cLinePointADir.x * cylinderUp.x);
        Vector3 w3 = new Vector3(cLinePointADir.z * cylinderUp.z, cLinePointADir.x * cylinderUp.x, cLinePointADir.y * cylinderUp.y);
        Vector3 w4 = new Vector3(cLinePointADir.x * lineDir.x, cLinePointADir.y * lineDir.y, cLinePointADir.z * lineDir.z);
        Vector3 w5 = new Vector3(lineDir.x * cylinderUp.x, lineDir.y * cylinderUp.y, lineDir.z * cylinderUp.z);
        Vector3 w6 = new Vector3(cylinderUp.y * cylinderUp.y + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.y * cylinderUp.y);
        Vector3 w7 = new Vector3(cLinePointADir.x * cLinePointADir.x, cLinePointADir.y * cLinePointADir.y, cLinePointADir.z * cLinePointADir.z);


        float cylinderUpSquareLength = cylinderUp.magnitude;
        float a = Vector3.Cross(lineDir, cylinderUp).sqrMagnitude / cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

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

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);


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

    public static int CheckIntersectionLineCylinderWihtoutCaps(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {
        Vector3 cLinePointADir = _linePointA - _cylinderOrigin; // X, Y, Z
        Vector3 lineDir = _linePointB - _linePointA; // u

        //cylinderUp // v

        Vector3 w1 = new Vector3(cLinePointADir.x * cylinderUp.x, cLinePointADir.y * cylinderUp.y, cLinePointADir.z * cylinderUp.z);
        Vector3 w2 = new Vector3(cLinePointADir.y * cylinderUp.y, cLinePointADir.z * cylinderUp.z, cLinePointADir.x * cylinderUp.x);
        Vector3 w3 = new Vector3(cLinePointADir.z * cylinderUp.z, cLinePointADir.x * cylinderUp.x, cLinePointADir.y * cylinderUp.y);
        Vector3 w4 = new Vector3(cLinePointADir.x * lineDir.x, cLinePointADir.y * lineDir.y, cLinePointADir.z * lineDir.z);
        Vector3 w5 = new Vector3(lineDir.x * cylinderUp.x, lineDir.y * cylinderUp.y, lineDir.z * cylinderUp.z);
        Vector3 w6 = new Vector3(cylinderUp.y * cylinderUp.y + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.y * cylinderUp.y);
        Vector3 w7 = new Vector3(cLinePointADir.x * cLinePointADir.x, cLinePointADir.y * cLinePointADir.y, cLinePointADir.z * cLinePointADir.z);

        float cylinderUpSquareLength = cylinderUp.magnitude;
        float a = Vector3.Cross(lineDir, cylinderUp).sqrMagnitude / cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = Mathf.Epsilon;
        if (Mathf.Abs(delta) < epsilon)
            return 0;

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1Condition = t1 >= 0 && t1 <= 1;
        float halfHeight = _cylinderHeight / 2.0f;

        if (!t0Condition && !t1Condition)
            return 0;
        else if (t0Condition && t1Condition)
        {
            Vector3 pA = _linePointA + t0 * lineDir;
            Vector3 pB = _linePointA + t1 * lineDir;

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);
            float pBDotC = Vector3.Dot((pB - _cylinderOrigin), cylinderUp);
            if (Mathf.Abs(pADotC) > halfHeight && Mathf.Abs(pBDotC) > halfHeight)
                return 0;
            else if(Mathf.Abs(pADotC) > halfHeight)
            {
                _t0 = t1;
                return 1;
            }
            else if(Mathf.Abs(pBDotC) > halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }

            return 2;
        }
        else
        {
            if (t1Condition)
                t0 = t1;

            Vector3 pA = _linePointA + t0 * lineDir;
            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);

            if (Mathf.Abs(pADotC) <= halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            return 0;
        }
    }

    public static int CheckIntersectionLineCylindreMethod1(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 lineDir = _linePointB - _linePointA; // u
        int count = CheckIntersectionLineCylinderWihtoutCaps(_linePointA, _linePointB, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if(count == 2)
        {
            _result = _linePointA + t0 * lineDir;
            _result2 = _linePointA + t1 * lineDir;

            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f;
        bool result1 = CheckLineCircleIntersection(_linePointA, _linePointB, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2);
        bool result2 = CheckLineCircleIntersection(_linePointA, _linePointB, _cylinderOrigin - cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t3);

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


    public static int CheckIntersectionLineSphereCaps(Vector3 _linePointA, Vector3 _linePointB, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckLineSphereIntersection(_linePointA, _linePointB, _capsOrigin, _capsRadius, ref t0, ref t1);

        if (count == 0)
            return 0;
        else if(count == 1)
        {
            Vector3 lineDir = _linePointB - _linePointA;
            Vector3 pA = _linePointA + t0 * lineDir;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);

            if (pADotC < 0.0f)
                return 0;
            else
                _t0 = t0;

            return 1;
        }
        else
        {
            Vector3 lineDir = _linePointB - _linePointA;
            Vector3 pA = _linePointA + t0 * lineDir;
            Vector3 pB = _linePointA + t1 * lineDir;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);
            float pBDotC = Vector3.Dot((pB - _capsOrigin), _capsNormal);
            if (pADotC < 0.0f && pBDotC < 0.0f)
                return 0;
            else if(pADotC < 0.0f)
            {   
                _t0 = t1;
                return 1;
            }
            else if(pBDotC < 0.0f)
            {
                _t0 = t0;
                return 1;
            }

            _t0 = Mathf.Min(t0, t1);
            _t1 = Mathf.Max(t0, t1);
            return 2;
        }
    }

    public static int CheckIntersectionLineCapsule(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 lineDir = _linePointB - _linePointA; 
        int count = CheckIntersectionLineCylinderWihtoutCaps(_linePointA, _linePointB, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _t0 = t0;
            _t1 = t1;
            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f;
        int capsCount = CheckIntersectionLineSphereCaps(_linePointA, _linePointB, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2, ref t3);
        int caps2Count = CheckIntersectionLineSphereCaps(_linePointA, _linePointB, _cylinderOrigin - cylinderUp * halfHeight, -cylinderUp, _cylinderRadius, ref t4, ref t5);

        if(capsCount == 2)
        {
            _t0 = t2;
            _t1 = t3;
            return 2;
        }
        else if(caps2Count == 2)
        {
            _t0 = t4;
            _t1 = t5;
            return 2;
        }
        else if(capsCount == 1 && caps2Count == 1)
        {
            _t0 = Mathf.Min(t2, t4);
            _t1 = Mathf.Max(t2, t4);

            return 2;
        }
        else if(capsCount == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t2);
            _t1 = Mathf.Max(t0, t2);
            return 2;
        }
        else if (caps2Count == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t4);
            _t1 = Mathf.Max(t0, t4);
            return 2;
        }
        else if(count == 1)
        {
            _t0 = t0;
            return 1;
        }
        else if (capsCount == 1)
        {
            _t0 = t2;
            return 1;
        }
        else if (caps2Count == 1)
        {
            _t0 = t4;
            return 1;
        }

        return 0;
    }

    public static int CheckIntersectionLineCapsule(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref Vector3 _result, ref Vector3 _result2)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckIntersectionLineCapsule(_linePointA, _linePointB, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
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
        int count = CheckIntersectionLineMinkowskiBoxSphere(_linePointA, _linePointB, boxCenter, boxSize, boxRotation, sphereRadius, ref t0, ref t1);
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

    public static int CheckIntersectionLineMinkowskiBoxSphere(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)
    {
        Vector3 relatifPA = WorldBaseChangementOptimised(_linePointA, boxCenter, boxRotation, Vector3.one);
        Vector3 relatifPB = WorldBaseChangementOptimised(_linePointB, boxCenter, boxRotation, Vector3.one);
        Vector3 halSize = boxSize / 2.0f;
        Vector3 halSizeR = halSize + Vector3.one*sphereRadius;
        Vector3 lineDir = relatifPB - relatifPA;


        //Left Quad Intersection
        float t = 0.0f;
        Vector3 quadPointA = halSizeR.x * Vector3.left + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t);

        //Right Quad Intersection
        float t1 = 0.0f;
        quadPointA = halSizeR.x * Vector3.right + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection2 = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t1);

        if(intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if(intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Bottom Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.down + halSize.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Top Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.up + halSize.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Back Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }


        float t0Tmp = 0.0f, t1Tmp = 0.0f;
        if(CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.left, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.right, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.right, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.left, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.back, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.forward, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.right, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.left, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.back, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.forward, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.right, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.left, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }


        bool CheckCapsule(Vector3 capsuleOrigin, Vector3 capsuleNormal, float capsuleSize, Vector3 dir1, Vector3 dir2)
        {
            float t2 = 0.0f, t3 = 0.0f;
            int count = CheckIntersectionLineCapsule(relatifPA, relatifPB, capsuleOrigin, capsuleNormal, sphereRadius, capsuleSize, ref t2, ref t3);
            if (count > 0)
            {
                int countTMP = count;
                Vector3 resultPoint = relatifPA + t2 * lineDir;
                if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                    Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                    count--;

                if (countTMP == 2)
                {
                    resultPoint = relatifPA + t3 * lineDir;
                    if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                        Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                        count--;
                    else if (count == 1)
                        t2 = t3;
                }
            }

            if (count == 2)
            {
                t0Tmp = Mathf.Min(t2, t3);
                t1Tmp = Mathf.Max(t2, t3);
                return true;
            }
            else if (count == 1 && intersection)
            {
                t0Tmp = Mathf.Min(t, t2);
                t1Tmp = Mathf.Max(t, t2);
                return true;
            }
            else if (count == 1)
            {
                intersection = true;
                t = t2;
            }

            return false;
        }

        if(intersection)
        {
            _t0 = t;
            return 1;
        }
        return 0;
    }
}
