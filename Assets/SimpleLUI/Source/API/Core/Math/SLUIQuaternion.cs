﻿//
// SimpleLUI Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

using System;
using UnityEngine;

namespace SimpleLUI.API.Core.Math
{
    [Serializable]
    public class SLUIQuaternion
    {
        public float x, y, z, w;

        public SLUIQuaternion() { }
        public SLUIQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /*
        public SLUIQuaternion(double x, double y, double z, double w)
        {
            this.x = (float) x;
            this.y = (float) y;
            this.z = (float) z;
            this.w = (float) w;
        }
        */
    }

    public static class SLUIQuaternionUtil
    {
        public static Quaternion ToRealQuaternion(this SLUIQuaternion q)
        {
            return new Quaternion(q.x, q.y, q.z, q.w);
        }

        public static SLUIQuaternion ToSLUIQuaternion(this Quaternion q)
        {
            return new SLUIQuaternion(q.x, q.y, q.z, q.w);
        }
    }
}