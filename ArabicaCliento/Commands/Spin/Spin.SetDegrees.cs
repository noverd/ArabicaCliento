using Content.Shared.Administration;
using Robust.Shared.Console;

namespace ArabicaCliento.Commands.Spin;

[AnyCommand]
public class ArabicaSpinSetDegrees : IConsoleCommand
{
    public string Command => "arabica.spin.set_degrees";
    public string Description => "This command sets a per second rotate rate in degrees";
    public string Help => "arabica.spin.set_degrees <float>";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length != 1)
        {
            shell.WriteError("Invalid argument count.");
            return;
        }

        if (!float.TryParse(args[0], out var degrees))
        {
            shell.WriteError($"Can't parse {args[0]} as float");
            return;
        }

        ArabicaConfig.SpinBotDegreesPerSecond = degrees;
    }
}