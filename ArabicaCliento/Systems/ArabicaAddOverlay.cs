using System.Diagnostics;
using ArabicaCliento.Overlays;
using ArabicaCliento.Systems.Abstract;
using Content.Shared.Administration;
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Shared.Console;
using Robust.Shared.Player;

namespace ArabicaCliento.Systems;

public class ArabicaAddOverlay(IEntityManager entManager, IEyeManager eye, EntityLookupSystem entityLookup, 
    IResourceCache resourceCache, IUserInterfaceManager userInterfaceManager, IOverlayManager overlaymanager) : LocalPlayerSystem
{
    private ArabicaOverlay? overlay;

    protected override void OnAttached(LocalPlayerAttachedEvent ev)
    {
        overlay ??= new ArabicaOverlay(entManager, eye, entityLookup, resourceCache, userInterfaceManager);
        overlaymanager.AddOverlay(overlay);
    }
    protected override void OnDetached(LocalPlayerDetachedEvent ev)
    {
        overlaymanager.RemoveOverlay(overlay);
    }
}