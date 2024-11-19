using Content.Client.Administration.Systems;
using Content.Shared.Administration;
using Content.Shared.Body.Components;
using Content.Shared.Damage;
using Content.Shared.Movement.Components;
using Content.Shared.NameIdentifier;
using Robust.Client.Player;
using Robust.Shared.Console;
using Robust.Shared.Network;
using Robust.Shared.Player;

namespace ArabicaCliento.Commands;

[AnyCommand]
public class ArabicaPlayers : IConsoleCommand
{
    public string Command => "arabica.entities";
    public string Description => "Outputs a entity(human-controlable) list with their id, name and coordinates";
    public string Help => "arabica.entities";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var entityManager = IoCManager.Resolve<IEntityManager>();

        foreach (var entity in entityManager.GetEntities())
        {
            var ismob = entityManager.GetComponentOrNull<ActorComponent>(entity);
            if (ismob != null)
            {
                var metadata = entityManager.GetComponentOrNull<MetaDataComponent>(entity);
                var transformcomp = entityManager.GetComponentOrNull<TransformComponent>(entity);
                var namestring = metadata.EntityName;
                var desc = metadata.EntityDescription;
                var username = entityManager.TryGetComponent<ActorComponent>(entity, out var Ckey);
                var damage = entityManager.TryGetComponent<DamageableComponent>(entity, out var Alldamage);
                var WorldCoord = transformcomp.WorldPosition;
                shell.WriteLine($"Entity ID: {entity}");
                shell.WriteLine($"Name: {namestring}");
                shell.WriteLine($"Desc: {desc}");
                shell.WriteLine($"Ckey: {Ckey.PlayerSession}");
                shell.WriteLine($"OverAllDamage: {Alldamage.TotalDamage}");
                shell.WriteLine($"World Cords x,y: {WorldCoord}");
            
                shell.WriteLine(""); 
            }
        }    
        
    }
}