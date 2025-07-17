using System.Reflection;
using HarmonyLib;

namespace ArabicaCliento.Patches;

[HarmonyPatch("Robust.Client.Console.ClientConsoleHost", "CanExecute")]
internal class ConsoleHostPatch
{
    [HarmonyPostfix]
    private static void Postfix(ref bool __result) => __result = true;


    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching ConsoleHostPatch: {__exception}");
    }
}