using System.Reflection;
using Content.Client.ContextMenu.UI;
using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch(typeof(EntityMenuElement))]
[HarmonyPatch("GetEntityDescription")]
internal class EntityMenuElementPatch
{
    private static MethodInfo? _methodCache;
    
    [HarmonyPrefix]
    private static bool Prefix(EntityUid entity, EntityMenuElement __instance, ref string __result)
    {
        _methodCache ??= AccessTools.Method(typeof(EntityMenuElement), "GetEntityDescriptionAdmin");
        __result = _methodCache.Invoke(__instance, [entity]) as string ?? throw new InvalidOperationException();
        return false;
    }
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching EntityMenuElementPatch: {__exception}");
    }
}