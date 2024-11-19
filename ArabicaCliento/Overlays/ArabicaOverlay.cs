using System.Numerics;
using Content.Client.Administration.Systems;
using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.Player;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Shared.Enums;
using Robust.Shared.Map;
using Robust.Shared.Player;
using Robust.Shared.Timing;

namespace ArabicaCliento.Overlays;

public class ArabicaOverlay(
    IEntityManager entManager, IEyeManager eye, EntityLookupSystem entityLookup, IResourceCache resourceCache, IUserInterfaceManager userInterfaceManager)
    : Overlay
{
    public override OverlaySpace Space => OverlaySpace.ScreenSpace;
    protected override void Draw(in OverlayDrawArgs args)
    {
        var font = new VectorFont(resourceCache.GetResource<FontResource>("/Fonts/NotoSans/NotoSans-Regular.ttf"), 10);
        
        var viewport = args.WorldAABB;
        
        if(!ArabicaConfig.OverlayEnabled) return;
        
        foreach (var entity in entManager.GetEntities())
        {
            if (!entManager.EntityExists(entity))
            {
                continue;
            }
            if (entManager.GetComponent<TransformComponent>(entity).MapID != args.MapId)
            {
                continue;
            }
            
            if (entManager.GetComponentOrNull<ActorComponent>(entity) != null)
            {
                var aabb = entityLookup.GetWorldAABB(entity);
                var charactername = (entManager.GetComponent<MetaDataComponent>(entity)).EntityName;
                var ckey = (entManager.GetComponent<ActorComponent>(entity)).PlayerSession.Name;
                if (!aabb.Intersects(in viewport))
                {
                    continue;
                }

                var uiScale = userInterfaceManager.RootControl.UIScale;
                var lineoffset = new Vector2(0f, 11f) * uiScale;
                var screenCoordinates = eye.WorldToScreen(aabb.Center +
                                                          new Angle(-eye.CurrentEye.Rotation).RotateVec(
                                                              aabb.TopRight - aabb.Center)) + new Vector2(1f, 7f);

                args.ScreenHandle.DrawString(font, screenCoordinates + lineoffset, ckey, uiScale, Color.Yellow);
                args.ScreenHandle.DrawString(font, screenCoordinates, charactername, uiScale, Color.Aquamarine);
            }
        }
    }
}