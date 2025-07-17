using Content.Client.MouseRotator;
using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch(typeof(MouseRotatorSystem), nameof(MouseRotatorSystem.Update))]
internal class MouseRotatorPatch
{
    [HarmonyPrefix]
    private static bool Prefix() => false;
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching MouseRotatorPatch: {__exception}");
    }
}