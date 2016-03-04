package com.alecgorge.minecraft.jsonapi.packets.netty;

import io.netty.channel.Channel;

import org.bukkit.Bukkit;

import com.alecgorge.minecraft.jsonapi.JSONAPI;
import com.alecgorge.minecraft.jsonapi.packets.netty.router.JSONAPIDefaultRoutes;

public class JSONAPINettyInjector {
	private NettyInjector injector = null;

	public JSONAPINettyInjector(final JSONAPI api) {
		if (Bukkit.getServer().getPluginManager().getPlugin("ProtocolLib") == null) {
			Bukkit.getLogger().info("ProtocolLib not found - try to work without.");
			return;
		}

		injector = new NettyInjector() {
			@Override
			protected void injectChannel(Channel channel) {
				channel.pipeline().addFirst(new JSONAPIChannelDecoder(api));
			}
		};
		injector.inject();

		new JSONAPIDefaultRoutes(api);

	}

	public void close() {
		if (injector != null)
			injector.close();
	}
}
