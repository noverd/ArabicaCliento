using System.Reflection.Emit;
using ArabicaCliento.Systems;
using Content.Client.Weapons.Ranged.Systems;
using Content.Shared.Weapons.Ranged.Events;
using HarmonyLib;
using Robust.Client.GameObjects;
using Robust.Client.Input;
using Robust.Client.Player;
using Robust.Shared.Map;

namespace ArabicaCliento.Patches;

[HarmonyPatch(typeof(GunSystem), nameof(GunSystem.Update))]
internal class GunUpdatePatch
{
    private static EntityManager? _entMan;
    private static IInputManager? _input;
    private static IPlayerManager? _player;
    private static TransformSystem? _transform;
    private static ArabicaAimSystem? _aim;

    NetCoordinates Patch(EntityCoordinates realCoordinates, MetaDataComponent _)
    {
        _entMan ??= IoCManager.Resolve<EntityManager>();
        if (!ArabicaConfig.RangedAimbotEnabled)
            return _entMan.GetNetCoordinates(realCoordinates);
        _input ??= IoCManager.Resolve<IInputManager>();
        _player ??= IoCManager.Resolve<IPlayerManager>();
        _transform ??= _entMan.System<TransformSystem>();
        _aim ??= _entMan.System<ArabicaAimSystem>();
        HashSet<EntityUid>? exclude = null;
        if (_player.LocalEntity != null)
            exclude = [_player.LocalEntity.Value];
        var aimOutput =
            _aim.GetClosestInRange(_input.MouseScreenPosition, ArabicaConfig.RangedAimbotRadius, exclude);
        if (aimOutput == null)
        {
            return _entMan.GetNetCoordinates(realCoordinates);
        }

        var closest = _transform.ToCoordinates(realCoordinates.EntityId, aimOutput.Value.Position);

        return _entMan.GetNetCoordinates(closest);
    }

    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var requestShootCoordinatesField =
            AccessTools.Field(typeof(RequestShootEvent), nameof(RequestShootEvent.Coordinates));
        var methodInfo = AccessTools.Method(typeof(GunUpdatePatch), nameof(Patch));

        var codes = new List<CodeInstruction>(instructions);
        var index = codes.FindIndex(c => c.StoresField(requestShootCoordinatesField));

        codes[index - 1] = new CodeInstruction(OpCodes.Call, methodInfo); // Before stfld goes call

        return codes.AsEnumerable();
    }
    
    [HarmonyFinalizer]
    private static void Finalizer(Exception __exception)
    {
        MarseyLogger.Fatal($"Error while patching GunUpdatePatch: {__exception}");
    }
}