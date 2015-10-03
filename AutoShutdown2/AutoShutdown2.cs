/*
 * AutoShutdown2 - Unturned Rocket plugin to shut down the server at speciffic times.
 * Copyright (C) 2015 False_Chicken
 * Contact: jmdevsupport@gmail.com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, Get it here: https://www.gnu.org/licenses/gpl-2.0.html
 */
 
using System;
using System.Collections.Generic;
using SDG.Unturned;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using UnityEngine;

namespace falsechicken.AutoShutdown2
{
	
	public class AutoShutdown2 : RocketPlugin<AutoShutdown2Configuration>
	{
		#region CONSTANTS

		private const string C_VERSION = "0.2";

		#endregion

		#region STORAGE VARS

		private byte currentHour, currentMinutes, currentSeconds; //The current hour, minutes, and seconds for fast lookup later.
		
		private Dictionary<byte, List<ShutdownWarning>> warningHourTable; //Cache the warnings for each hour for faster lookups.
		private Dictionary<byte, List<ShutdownTime>> shutdownHourTable; //Cache the shutdown times for each hour for faster lookups.
		
		private DateTime lastCalled; //Used to store when the last checks where performed. We only want to update once per second.

		#endregion

		#region ROCKET FUNCTIONS

		protected override void Load()
		{
			warningHourTable = new Dictionary<byte, List<ShutdownWarning>>();
			shutdownHourTable = new Dictionary<byte, List<ShutdownTime>>();
			
			PopulateCacheTables();
			
			UpdateLastCalledTime();

			ShowLoadedMessage();
		}

		void FixedUpdate()
		{
			currentHour = (byte) DateTime.Now.Hour;
			currentMinutes = (byte) DateTime.Now.Minute;
			currentSeconds = (byte) DateTime.Now.Second;
			
			if ((DateTime.Now - lastCalled).TotalSeconds > 1) //Check once per second.
			{
				CheckWarnings();
				CheckShutdowns();
				UpdateLastCalledTime();
			}
		}

		#endregion

		#region CORE PLUGIN FUNCTIONS

		/**
		 * Check the shutdown cache table to see if we have any shutdowns set for this hour. Then check the minute is correct before shutting down.
		 */
		private void CheckShutdowns()
		{
			if (shutdownHourTable[currentHour].Count < 1)
				return; //If there are no shutdowns for this hour return.

			foreach (ShutdownTime sT in shutdownHourTable[currentHour]) {  
				if (sT.minutes == currentMinutes && currentSeconds == 0) {
					UnturnedChat.Say("Automatic server shut down in progress...", 
					                 UnturnedChat.GetColorFromName(this.Configuration.Instance.ShutdownMessageColor, Color.green));
				
					Steam.shutdown ();
				}
			}
		}
		
		/**
		 * Check the warning cache table to see if we have any warnings set for this hour. Then check if the minute is correct before printing.
		 */
		private void CheckWarnings()
		{
			if (warningHourTable [currentHour].Count < 1)
				return; //If there are no warnings for this hour return.

			foreach (ShutdownWarning sW in warningHourTable[currentHour]) {
				if (sW.minute == currentMinutes && currentSeconds == 0) {
					UnturnedChat.Say (sW.message, UnturnedChat.GetColorFromName(sW.color, Color.green));
				}
			}
		}

		/**
		 * Initialize and populate the cache tables for use.
		 */
		private void PopulateCacheTables()
		{
			warningHourTable.Clear();
			shutdownHourTable.Clear();
			
			for (byte hour = 0; hour < 24; hour++) //Populate the shutdown and warning cache tables with keys 0 - 23 to represent the hours of the day.
			{
				warningHourTable.Add(hour, new List<ShutdownWarning>());
				shutdownHourTable.Add(hour, new List<ShutdownTime>());
			}

			foreach (ShutdownWarning sW in this.Configuration.Instance.ShutdownWarnings)
			{
					warningHourTable[sW.hour].Add(sW);
			}

			foreach (ShutdownTime sT in this.Configuration.Instance.ShutdownTimes)
			{
					shutdownHourTable[sT.hour].Add(sT);
			}
		}
	
		/**
		 * Update the last called variable. Used to make sure we only check the time once per second.
		 */
		private void UpdateLastCalledTime()
		{
			lastCalled = DateTime.Now;
		}

		/**
		 * Print a message to the console informing the user that the plugin has loaded.
		 */
		private void ShowLoadedMessage()
		{
			Logger.Log(" Version " + C_VERSION + " loaded.");
		}

		#endregion
	}
}