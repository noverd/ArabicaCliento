using System.Reflection;
using Content.Client.Administration.Managers;
using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch(typeof(ClientAdminManager))]
internal static class ClientAdminManagerPatch
{
    [HarmonyTargetMethods]
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(ClientAdminManager), nameof(ClientAdminManager.CanAdminMenu));
        yield return AccessTools.Method(typeof(ClientAdminManager), nameof(ClientAdminManager.CanAdminPlace));
        yield return AccessTools.Method(typeof(ClientAdminManager), nameof(ClientAdminManager.CanScript));
        yield return AccessTools.Method(typeof(ClientAdminManager), nameof(ClientAdminManager.CanCommand));
        yield return AccessTools.Method(typeof(ClientAdminManager), nameof(ClientAdminManager.IsActive));
    }

    [HarmonyPrefix]
    private static bool Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
    
        
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching ClientAdminManagerPatch: {__exception}");
    }
}