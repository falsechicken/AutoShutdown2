/*
 */
using System;
using Rocket.API;
using System.Xml.Serialization;

namespace falsechicken.AutoShutdown2
{
	public sealed class ShutdownTime
	{
		[XmlAttribute("Hour")]
		public byte hour;
        	
		[XmlAttribute("Minute")]
		public byte minutes;

		public ShutdownTime(byte _hour, byte _minute)
		{
			hour = _hour;
			minutes = _minute;
		}
		
		public ShutdownTime()
		{
			hour = 0;
			minutes = 0;
		}
	}
		
	public sealed class ShutdownWarning
	{
		[XmlAttribute("Hour")]
		public byte hour;
				
		[XmlAttribute("Minute")]
		public byte minute;
				
		[XmlAttribute("Message")]
		public string message;
		
		[XmlAttribute("Color")]
		public string color;
				
		public ShutdownWarning(byte _hour, byte _minute, string _message, string _color)
		{
			hour = _hour;
			minute = _minute;
			message = _message;
			color = _color;
		}
		
		public ShutdownWarning()
		{
			hour = 0;
			minute = 0;
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
			ShutdownMessageColor = "red";

			ShutdownTimes = new ShutdownTime[]
			{
				new ShutdownTime(0, 0),
				new ShutdownTime(2, 0),
				new ShutdownTime(4, 0),
				new ShutdownTime(6, 0),
				new ShutdownTime(8, 0),
				new ShutdownTime(10, 0),
				new ShutdownTime(12, 0),
				new ShutdownTime(14, 0),
				new ShutdownTime(16, 0),
				new ShutdownTime(18, 0),
				new ShutdownTime(20, 0),
				new ShutdownTime(22, 0)			
			};
			
			ShutdownWarnings = new ShutdownWarning[]
			{
				new ShutdownWarning(1, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(3, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(5, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(7, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(9, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(11, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(13, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(15, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(17, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(19, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(21, 55, "Automatic shutdown in 5 minutes.", "Green"),
				new ShutdownWarning(23, 55, "Automatic shutdown in 5 minutes.", "Green")
			};			
		}
		
	}
}
