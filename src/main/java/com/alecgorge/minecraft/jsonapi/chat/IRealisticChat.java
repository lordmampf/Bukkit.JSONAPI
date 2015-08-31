package com.alecgorge.minecraft.jsonapi.chat;

public interface IRealisticChat {
	public abstract boolean chatWithName(String message, String uuid);
	public abstract void pluginDisable();
	public abstract boolean canHandleChats();
}
