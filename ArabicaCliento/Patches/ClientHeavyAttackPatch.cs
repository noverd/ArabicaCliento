using Content.Shared.Weapons.Melee;
using HarmonyLib;
using Robust.Shared.Map;
using ArabicaCliento.Systems;
using Content.Client.Weapons.Melee;
using Robust.Client.GameObjects;

namespace ArabicaCliento.Patches;

[HarmonyPatch(typeof(MeleeWeaponSystem), "ClientHeavyAttack")]
internal class ClientHeavyAttackPatch
{
    private static IEntityManager? _entMan;
    private static TransformSystem? _transform;
    private static ArabicaAimSystem? _aim;

    
    [HarmonyPrefix]
    private static void Prefix(ref EntityUid user,
        ref EntityCoordinates coordinates,
        ref EntityUid meleeUid,
        ref MeleeWeaponComponent component)
    {
        if (!ArabicaConfig.MeleeAimbotEnabled) return;
        _entMan ??= IoCManager.Resolve<EntityManager>();
        _transform ??= _entMan.System<TransformSystem>();
        _aim ??= _entMan.System<ArabicaAimSystem>();

        var output = _aim.GetClosestToEntInRange(user, component.Range, [user]);
        if (output == null) return;
        coordinates = _transform.ToCoordinates(coordinates.EntityId, output.Value.Position);
    }
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching ClientHeavyAttackPatch: {__exception}");
    }
}