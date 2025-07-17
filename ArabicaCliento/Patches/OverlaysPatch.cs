using System.Reflection;
using Content.Client.Drugs;
using Content.Client.Drunk;
using Content.Client.Eye.Blinding;
using Content.Client.Flash;
using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch]
internal static class OverlaysPatch
{
    [HarmonyTargetMethods]
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(FlashOverlay), "Draw");
        yield return AccessTools.Method(typeof(BlindOverlay), "Draw");
        yield return AccessTools.Method(typeof(BlurryVisionOverlay), "Draw");
        yield return AccessTools.Method(typeof(DrunkOverlay), "Draw");
        yield return AccessTools.Method(typeof(RainbowOverlay), "Draw");
    }

    [HarmonyPrefix]
    private static bool Prefix()
    {
        return !ArabicaConfig.OverlaysDisable;
    }
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching OverlaysPatch: {__exception}");
    }
}