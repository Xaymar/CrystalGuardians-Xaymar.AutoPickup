using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#if !UNITY_EDITOR && !UNITY_STANDALONE
using HarmonyLib;
using UnityEngine;

namespace Xaymar.Example
{
    [HarmonyPatch(typeof(DroppedItem))]
    internal class DroppedItemInjector
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void Update_Postfix(DroppedItem __instance)
        {
            __instance.GetType().GetMethod("OnMouseDown", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, []);
        }
    }
}
#endif
