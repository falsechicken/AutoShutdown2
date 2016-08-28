/**
 * AutoShutdown2Configuration - The configuration file for the plugin.
 * 
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
using System.Xml.Serialization;

using Rocket.API;

namespace falsechicken.AutoShutdown2
{
	public sealed class ShutdownTime
	{
		[XmlAttribute("Hour")]
		public byte hour;
        	
		[XmlAttribute("Minute")]
		public byte minutes;

		[XmlAttribute("Second")]
		public byte seconds;

		public ShutdownTime(byte _hour, byte _minute, byte _second)
		{
			hour = _hour;
			minutes = _minute;
			seconds = _second;
		}
		
		public ShutdownTime()
		{
			hour = 0;
			minutes = 0;
			seconds = 0;
		}
	}
		
	public sealed class ShutdownWarning
	{
		[XmlAttribute("Hour")]
		public byte hour;
				
		[XmlAttribute("Minute")]
		public byte minutes;

		[XmlAttribute("Second")]
		public byte seconds;
				
		[XmlAttribute("Message")]
		public string message;
		
		[XmlAttribute("Color")]
		public string color;
				
		public ShutdownWarning(byte _hour, byte _minute, byte _second, string _message, string _color)
		{
			hour = _hour;
			minutes = _minute;
			seconds = _second;
			message = _message;
			color = _color;
		}
		
		public ShutdownWarning()
		{
			hour = 0;
			minutes = 0;
			seconds = 0;
			message = "";
			color = "";
		}
	}
		
	public class AutoShutdown2Configuration : IRocketPluginConfiguration
	{
		public string ShutdownMessageColor;

		[XmlArrayItem("Shutdown_Time")]
        [XmlArray(ElementName = "Shutdown_Times")]
        public ShutdownTime[] ShutdownTimes;
        
        [XmlArrayItem("Shutdown_Warning")]
        [XmlArray(ElementName = "Shutdown_Warnings")]
        public ShutdownWarning[] ShutdownWarnings;
		
		public void LoadDefaults()
		{
			ShutdownMessageColor = "Red";

			ShutdownTimes = new ShutdownTime[]
			{
				new ShutdownTime(0, 0, 0),
				new ShutdownTime(2, 0, 0),
				new ShutdownTime(4, 0, 0),
				new ShutdownTime(6, 0, 0),
				new ShutdownTime(8, 0, 0),
				new ShutdownTime(10, 0, 0),
				new ShutdownTime(12, 0, 0),
				new ShutdownTime(14, 0, 0),
				new ShutdownTime(16, 0, 0),
				new ShutdownTime(18, 0, 0),
				new ShutdownTime(20, 0, 0),
				new ShutdownTime(22, 0, 0)			
			};
			
			ShutdownWarnings = new ShutdownWarning[]
			{
				new ShutdownWarning(1, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(3, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(5, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(7, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(9, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(11, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(13, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(15, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(17, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(19, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(21, 55, 0, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(23, 55, 0, "Automatic shutdown in 5 minutes.", "Green")
			};			
		}
	}
}
