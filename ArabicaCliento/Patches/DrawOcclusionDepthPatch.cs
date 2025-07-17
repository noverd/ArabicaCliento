using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch("Robust.Client.Graphics.Clyde.Clyde", "DrawOcclusionDepth")]
internal static class DrawOcclusionDepthPatch
{
    [HarmonyPrefix]
    private static bool Prefix()
    {
        return !ArabicaConfig.FOVDisable;
    }
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching DrawOcclusionDepthPatch: {__exception}");
    }
}