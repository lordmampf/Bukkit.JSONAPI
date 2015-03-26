package com.alecgorge.minecraft.jsonapi.util;

import org.bukkit.Location;
import org.bukkit.OfflinePlayer;

public class PlayerLocation {
	public PlayerLocation(OfflinePlayer player, Location location) {
		this.Player = player;
		this.Location = location;
	}
	
	public OfflinePlayer Player;
	
	public Location Location;
}
