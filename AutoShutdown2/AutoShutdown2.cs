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
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Core.Plugins;
using UnityEngine;

namespace AutoShutdown2
{
	
	public class AutoShutdown2 : RocketPlugin<AutoShutdown2Configuration>
	{
		
		private Boolean shutdownPostponed;
		
		private byte currentHour;
		private byte currentMinute;
		private byte currentSecond;
		private byte shutdownPostponedMinute;
		
		private Dictionary<byte, List<ShutdownWarning>> warningHourTable; //Cache the warnings for each hour for faster lookups.
		private Dictionary<byte, List<ShutdownTime>> shutdownHourTable; //Cache the shutdown times for each hour for faster lookups.
		
		private DateTime lastCalled;

		protected override void Load()
		{
			shutdownPostponed = false;
			
			warningHourTable = new Dictionary<byte, List<ShutdownWarning>>();
			shutdownHourTable = new Dictionary<byte, List<ShutdownTime>>();
			
			PopulateCacheTables();
			
			UpdateLastCalledTime();
			
		}
		
		void FixedUpdate()
		{
			currentHour = (byte) DateTime.Now.Hour;
			currentMinute = (byte) DateTime.Now.Minute;
			currentSecond = (byte) DateTime.Now.Second;
			
			if ((DateTime.Now - lastCalled).TotalSeconds > 1) //Check once per second.
			{
				CheckWarnings();
				CheckShutdowns();
				UpdateLastCalledTime();
			}
		}
		
		public void PostponeShutdown()
		{
			shutdownPostponed = true;
		}

		private void CheckShutdowns()
		{
			if (shutdownHourTable[currentHour].Count < 1)
				return; //If there are no shutdowns for this hour return.

			foreach (ShutdownTime sT in shutdownHourTable[currentHour]) {  
				if (sT.minutes == currentMinute && currentSecond == 0) {
					UnturnedChat.Say("Automatic server shutdown in progress...", Color.green);
					Steam.shutdown ();
				}
			}
		}
		
		private void CheckWarnings()
		{
			if (warningHourTable [currentHour].Count < 1)
				return; //If there are no warnings for this hour return.

			foreach (ShutdownWarning sW in warningHourTable[currentHour]) {
				if (sW.minute == currentMinute && currentSecond == 0) {
					UnturnedChat.Say (sW.message, UnturnedChat.GetColorFromName(sW.color, Color.green));
				}
			}
		}

		
		private void PopulateCacheTables()
		{
			
			warningHourTable.Clear();
			shutdownHourTable.Clear();
			
			for (byte hour = 0; hour < 24; hour++) //Populate the shutdown and warning cache tables.
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
	
		private void UpdateLastCalledTime()
		{
			lastCalled = DateTime.Now;
		}
	
	}

}