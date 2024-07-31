using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCollisionUtility
{
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
        _lineAPointA = MathUtility.WorldBaseChangementOptimised(_lineAPointA, quadPointA, rotation, Vector3.one);
        _lineAPointB = MathUtility.WorldBaseChangementOptimised(_lineAPointB, quadPointA, rotation, Vector3.one);

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
            if ((point - _circleOrigin).sqrMagnitude <= _radius * _radius)
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

    public static int CheckLineCylinderWihtoutCapsIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
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
            else if (Mathf.Abs(pADotC) > halfHeight)
            {
                _t0 = t1;
                return 1;
            }
            else if (Mathf.Abs(pBDotC) > halfHeight)
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

    public static int CheckLineSphereCapsIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckLineSphereIntersection(_linePointA, _linePointB, _capsOrigin, _capsRadius, ref t0, ref t1);

        if (count == 0)
            return 0;
        else if (count == 1)
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
            else if (pADotC < 0.0f)
            {
                _t0 = t1;
                return 1;
            }
            else if (pBDotC < 0.0f)
            {
                _t0 = t0;
                return 1;
            }

            _t0 = Mathf.Min(t0, t1);
            _t1 = Mathf.Max(t0, t1);
            return 2;
        }
    }

    public static int CheckLineCapsuleIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 lineDir = _linePointB - _linePointA;
        int count = ParametricCollisionUtility.CheckLineCylinderWihtoutCapsIntersection(_linePointA, _linePointB, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _t0 = t0;
            _t1 = t1;
            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f;
        int capsCount = CheckLineSphereCapsIntersection(_linePointA, _linePointB, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2, ref t3);
        int caps2Count = CheckLineSphereCapsIntersection(_linePointA, _linePointB, _cylinderOrigin - cylinderUp * halfHeight, -cylinderUp, _cylinderRadius, ref t4, ref t5);

        if (capsCount == 2)
        {
            _t0 = t2;
            _t1 = t3;
            return 2;
        }
        else if (caps2Count == 2)
        {
            _t0 = t4;
            _t1 = t5;
            return 2;
        }
        else if (capsCount == 1 && caps2Count == 1)
        {
            _t0 = Mathf.Min(t2, t4);
            _t1 = Mathf.Max(t2, t4);

            return 2;
        }
        else if (capsCount == 1 && count == 1)
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
        else if (count == 1)
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

    public static int CheckLineMinkowskiBoxSphereIntersection(Vector3 _linePointA, Vector3 _linePointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)
    {
        Vector3 relatifPA = MathUtility.WorldBaseChangementOptimised(_linePointA, boxCenter, boxRotation, Vector3.one);
        Vector3 relatifPB = MathUtility.WorldBaseChangementOptimised(_linePointB, boxCenter, boxRotation, Vector3.one);
        Vector3 halSize = boxSize / 2.0f;
        Vector3 halSizeR = halSize + Vector3.one * sphereRadius;
        Vector3 lineDir = relatifPB - relatifPA;


        //Left Quad Intersection
        float t = 0.0f;
        Vector3 quadPointA = halSizeR.x * Vector3.left + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t);

        //Right Quad Intersection
        float t1 = 0.0f;
        quadPointA = halSizeR.x * Vector3.right + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection2 = CheckLineQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t1);

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
        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.left, Vector3.back))
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
            int count = CheckLineCapsuleIntersection(relatifPA, relatifPB, capsuleOrigin, capsuleNormal, sphereRadius, capsuleSize, ref t2, ref t3);
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

        if (intersection)
        {
            _t0 = t;
            return 1;
        }
        return 0;
    }


}
