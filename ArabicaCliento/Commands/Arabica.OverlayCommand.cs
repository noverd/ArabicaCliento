using Content.Shared.Administration;
using Content.Shared.Damage;
using Robust.Shared.Console;
using Robust.Shared.Player;

namespace ArabicaCliento.Commands;

[AnyCommand]
public class ArabicaOverlay : IConsoleCommand
{
    public string Command => "arabica.overlay";
    public string Description => "Shows Ckeys and CharNames of players";
    public string Help => "arabica.overlay";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (ArabicaConfig.OverlayEnabled == false)
        {
            ArabicaConfig.OverlayEnabled = true;
            shell.WriteLine("Overlay enabled");
        }
        else
        {
            ArabicaConfig.OverlayEnabled = false;
            shell.WriteLine("Overlay disabled");
        }
    }
}