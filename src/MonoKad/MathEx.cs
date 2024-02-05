﻿using System;
using Microsoft.Xna.Framework;

namespace MonoKad
{
    public static class QuaternionEx
    {
        public static Quaternion Zero = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        
        public static void GetAngleAxis(this Quaternion quat, out Vector3 axis, out float angle) {
            Quaternion thisQuat = new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
            if (thisQuat == QuaternionEx.Zero) {
                Console.WriteLine("Can't get the angle-axis of a zero quaternion! Proceeding with an identity quaternion instead");
                thisQuat = Quaternion.Identity;
            }
            thisQuat.Normalize();

            angle = 2.0f * MathF.Acos(thisQuat.W);
            axis = new Vector3(quat.X, quat.Y, quat.Z);
            axis.Normalize();
        }
    }
}