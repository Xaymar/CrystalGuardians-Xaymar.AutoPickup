#if !UNITY_EDITOR && !UNITY_STANDALONE
using System;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace Xaymar.AutoPickup
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic

            // Start of: AssetBundle loader.
            string whoAmI = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string whereAmI = System.IO.Path.GetDirectoryName(whoAmI);
            if (string.IsNullOrEmpty(whereAmI))
            {
                throw new System.IO.FileNotFoundException("Failed to find myself.");
            }

            // - Load all Asset Bundles in our directory.
            foreach (var file in System.IO.Directory.EnumerateFiles(whereAmI, "*.assetbundle"))
            {
                // Can be optimized to load asynchronously, which speeds up loading with many asset bundles.
                Logger.LogInfo($"Loading bundle '{file}'...");
                var bundle = AssetBundle.LoadFromFile(file);
            }
            // End of: AssetBundle loader.

            // Apply patches.
            try
            {
                var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
                Logger.LogInfo("Applying patch to DroppedItem...");
                harmony.PatchAll(typeof(Xaymar.Example.DroppedItemInjector));
            }
            catch (Exception e)
            {
                Logger.LogFatal($"Failed to apply patch(es): {e}");
                UnityEngine.Application.Quit(1);
            }

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

#endif