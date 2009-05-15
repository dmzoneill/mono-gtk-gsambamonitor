using System;
using System.Timers;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Gtk;


public partial class MainWindow: Gtk.Window
{	
	private System.Timers.Timer myTimer;
	private System.Timers.Timer textTimer;
	private delegate void updateTextBox(object sender, EventArgs e);
	private static TextTagTable table = new TextTagTable();
	private static Gtk.TextBuffer buffer = new Gtk.TextBuffer(table);
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		
		this.myTimer = new System.Timers.Timer();
	   	this.myTimer.Elapsed += new ElapsedEventHandler( checksmb );
		this.myTimer.Interval = 5000;
		this.myTimer.Start();
		
		this.textTimer = new System.Timers.Timer();
	   	this.textTimer.Elapsed += new ElapsedEventHandler( update );
		this.textTimer.Interval = 5000;
		this.textTimer.Start();
				
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		this.myTimer.Stop();
		a.RetVal = true;
	}
	
	
	protected void update( object source, ElapsedEventArgs e )
	{
		String part1 = GSambaMonitor.ProcessController.getSmbFiles();
		String part2 = GSambaMonitor.ProcessController.getSmbShares();
		
		this.textview2.Buffer.Clear();
		this.textview2.Buffer.Text = "  fail " + part1 + " " + part2;
		
	}
	
	
	protected void checksmb( object source, ElapsedEventArgs e )
	{	
		GSambaMonitor.ProcessController.stop();
		GSambaMonitor.ProcessController.start();
		Console.WriteLine("------------------------------------------");
		Console.WriteLine(GSambaMonitor.ProcessController.getSmbFiles());
		Console.WriteLine(GSambaMonitor.ProcessController.getSmbShares());
		Console.WriteLine("------------------------------------------");
		
	}
	
}