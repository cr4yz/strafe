using Strafe;
using Sandbox;
using System;
using System.Linq;
using Strafe.Weapons;

namespace Strafe.Ply
{
	partial class StrafeInventory : BaseInventory
	{

		public StrafeInventory( Player player ) : base( player )
		{

		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as StrafePlayer;
			var weapon = ent as StrafeWeapon;
			var notices = !player.SuppressPickupNotices;
			//
			// We don't want to pick up the same weapon twice
			// But we'll take the ammo from it Winky Face
			//
			if ( weapon != null && IsCarryingType( ent.GetType() ) )
			{
				var ammo = weapon.AmmoClip;
				var ammoType = weapon.AmmoType;

				if ( ammo > 0 )
				{
					//player.GiveAmmo( ammoType, ammo );

					if ( notices )
					{
						Sound.FromWorld( "dm.pickup_ammo", ent.Position );
						//PickupFeed.OnPickup( player, $"+{ammo} {ammoType}" );
					}
				}

				//ItemRespawn.Taken( ent );

				// Despawn it
				ent.Delete();
				return false;
			}

			if ( weapon != null && notices )
			{
				Sound.FromWorld( "dm.pickup_weapon", ent.Position);
				//PickupFeed.OnPickup( player, $"{ent.ClassInfo.Title}" ); 
			}


			//ItemRespawn.Taken( ent );
			return base.Add( ent, makeActive );
		}

		public bool IsCarryingType( Type t )
		{
			return List.Any( x => x.GetType() == t );
		}
	}
}

