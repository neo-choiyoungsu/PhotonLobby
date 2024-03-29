﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MessageParser
{
    public static byte[] GetBytes<T>(T str)
    {
        int size = Marshal.SizeOf(str);
        byte[] arr = new byte[size];
        GCHandle h = default(GCHandle);
        try
        {
            h = GCHandle.Alloc(arr, GCHandleType.Pinned);
            Marshal.StructureToPtr<T>(str, h.AddrOfPinnedObject(), false);
        }
        finally
        {
            if (h.IsAllocated)
            {
                h.Free();
            }
        }
        return arr;
    }

    public static T FromBytes<T>(byte[] arr) where T : struct
    {
        T strct = default(T);
        GCHandle h = default(GCHandle);
        try
        {
            h = GCHandle.Alloc(arr, GCHandleType.Pinned);
            strct = Marshal.PtrToStructure<T>(h.AddrOfPinnedObject());

        }
        finally
        {
            if (h.IsAllocated)
            {
                h.Free();
            }
        }
        return strct;
    }
}
