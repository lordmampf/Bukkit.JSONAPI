package com.alecgorge.minecraft.jsonapi.util;

import org.bukkit.Location;
import org.bukkit.entity.Player;

public class PlayerLocation {
	public PlayerLocation(Player player, Location location) {
		this.Player = player;
		this.Location = location;
	}
	
	public Player Player;
	
	public Location Location;
}
