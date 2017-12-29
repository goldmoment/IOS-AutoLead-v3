
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOS_AutoLead
{
    public  class ThreadRunLead 
    {
        
        sshNet ssh = new sshNet(iStatic.ipIphone);
        //GETAPPLIST
        public string[] GetAppList()
        {
            string[] arrApplist = ssh.GetAppList();
            return arrApplist;
        }
        //WIPE---------------------------------------------------------------------------------
        public bool Wipe(string App,bool bl)
        {
             return ssh.WipeApp(App, bl);
        }
        //CHANGE-------------------------------------------------------------------------------
        public InfoDevice changeData()
        {
            InfoDevice dvc = new InfoDevice();
            try
            {
               
                string devicejs = ssh.ChangeDevice();
                dynamic data = JObject.Parse(devicejs);
                dvc.Country = data.Device.IOS;
                dvc.NetworkInfo = data.Device.NetworkInfo;
                dvc.Machine = data.Device.HWMachine;
                dvc.Timezone = data.Device.Timezone;
                dvc.ScreenHeight = data.Device.ScreenHeight;
                dvc.ScreenWidth = data.Device.ScreenWidth;
                dvc.OSVersion = data.Device.OSVersion;
                dvc.Language = data.Device.Language;
                dvc.UserAgent = data.Device.UserAgent;
            }
            catch
            {
                return dvc;
            }
            return dvc;
        }
        //OPEN URL-----------------------------------------------------------------------------
        public void fileOpenUrl(string x1,string y1,string x2,string y2)
        {
            string text = "tap("+x1+ ", " + y1 + ")";
                   text += "/r/n";
                   text += "tap(" + x2 + ", " + y2 + ")";
            File.WriteAllText(iStatic.ipIphone + "/openlink.lua", text);
        }
        public bool openUrl(string url)
        {
            if (!ssh.openlink(url)) { return false; }
  
            return true;
        }
        public bool openUrl(string url,int timeoutUrl)
        {
           
            if (!ssh.openlink(url)) { return false; }
            //check appstore open
            DateTime startTime = DateTime.Now;
            while (true)
            {
                ThuVienDll.RequestServer.HTTP_GET("http://" + iStatic.ipIphone + ":8080/control/start_playing?path=openlink.lua", "");
                Thread.Sleep(1000);
                if (int.Parse(DateTime.Now.Subtract(startTime).TotalSeconds.ToString().Split('.')[0]) > timeoutUrl)
                {
                    return false;
                }
                if(ThuVienDll.RequestServer.HTTP_GET("http://" + iStatic.ipIphone + ":6969/checkapps", "").Trim()== "App Store")
                {

                    return true;
                }

            }
          
        }
       
        //OpenApp------------------------------------------------------------------------------
        public void openApp(string App)
        {
            ssh.OpenApp(App);
        }
        //Script-------------------------------------------------------------------------------
        public bool script(string[] arr)
        {
            Thread.Sleep(2000);
            ScriptCode scripCode = new ScriptCode();
            scripCode.runScript(arr);
            return true;
        }
        //Backup--------------------------------------------------------------------------------
        public bool Backup(string app,string country)
        {
            string date = "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + ssh.random(1111, 99999) + "_0";
            if (!ssh.backUpApp(app, country, date)) {  ssh.deleteFile("/var/root/backup/", app + "_"+ country + date); return false; } else { return true; } ;

        }
        //Restore---------------------------------------------------------------------------------
        public bool Restore(string dir,string app)
        {
            if (!ssh.RestoreApp(dir, app))
            {
                ssh.deleteFile("/var/root/backup/", dir.Split('.')[0]);
                return false;
            }
            return true;
        }
        
    }
}
