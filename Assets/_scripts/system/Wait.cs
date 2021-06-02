using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait
{
    private static Dictionary<float, WaitForSeconds> dict = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds Second(float time)
    {
        if (dict.ContainsKey(time) == false)
        {
            dict.Add(time, new WaitForSeconds(time));
        }

        return dict[time];
    }
}
