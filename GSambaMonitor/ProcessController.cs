using System;
using System.Diagnostics;
using System.Threading;
using System.Collections; 


namespace GSambaMonitor
{
		
	public class ProcessController
	{		
		private static ThreadStart job = null;
		private static Thread jobRunner = null;
		private static String filesString;
		private static String sharesString;
					
		public static String getSmbFiles()
		{
			return filesString;
		}	
		
		
		public static String getSmbShares()
		{
			return sharesString;	
		}
		
		public static void start()
		{			
			try
			{
				job = new ThreadStart(process);
        		jobRunner = new Thread(job);
        		jobRunner.Start();						
			}
			catch(Exception)
			{}
		}		
		
		public static void stop()
		{
			try{
				jobRunner.Abort();
			}
			catch(Exception)
			{}
		}
		
		
				
		private static void process()
		{						
			sharesString = "";
			filesString = "";
			
			Process shares = new Process();
			shares.StartInfo.FileName = "smbstatus";
			shares.StartInfo.Arguments = "-S";
			shares.StartInfo.UseShellExecute = false;
			shares.StartInfo.RedirectStandardOutput = true;
			shares.Start();
			
			String sharestream = shares.StandardOutput.ReadLine();
			Boolean openshares = false;
			
			String sharesResult = "";
			
			while(sharestream!=null)
			{
				if(sharestream.Contains("--------------------"))
					openshares = true;
				
				if(openshares==true)
				{
					sharesResult += sharestream;					
				}
								
				sharestream = shares.StandardOutput.ReadLine();
			}
			
			sharesString = sharesResult;
			
			Process filelocks = new Process();
			filelocks.StartInfo.FileName = "smbstatus";
			filelocks.StartInfo.Arguments = "-L";
			filelocks.StartInfo.UseShellExecute = false;
			filelocks.StartInfo.RedirectStandardOutput = true;
			filelocks.Start();
			
			String filestream = filelocks.StandardOutput.ReadLine();
			Boolean openfiles = false;
			
			String result = "";
			
			while(filestream!=null)
			{
				if(filestream.Contains("------------------"))
				{
					openfiles = true;
				}
					
				if(openfiles==true)
				{
					result += filestream;
				}
								
				filestream = filelocks.StandardOutput.ReadLine();
			}
			
			filesString = result;
											
		}	
		
	}
}
