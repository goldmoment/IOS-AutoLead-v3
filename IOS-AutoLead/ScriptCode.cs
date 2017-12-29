using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOS_AutoLead
{
    public class ScriptCode
    {

        sshNet ssh = new sshNet(iStatic.ipIphone);

        public void runScript(string[] arrayCode)
        {
            string password = "koiboy1993";
            if (File.Exists("./quy/FirstName.txt"))
            {
                string[] arr = File.ReadAllLines("./quy/FirstName.txt");
                password= arr[new Random().Next(0, arr.Length - 2)] + new Random().Next(100, 9999);
            }
            
            foreach (string code in arrayCode)
            {
                Console.WriteLine(code);
                if (code.Trim().ToLower().Contains("touch("))
                {
                    
                    string[] arrayxy= code.Split(new string[] { "Touch(", ")", "," }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("arrayxy ok"+ arrayxy.Length);
                    if (arrayxy.Length - 1 == 1)
                    {
                        Touch(arrayxy[0], arrayxy[1]);
                    }
                }
                if (code.Trim().ToLower().Contains("swipe("))
                {
                    string[] arrayxy = code.Split(new string[] { "Swipe(", ")", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrayxy.Length - 1 == 4)
                    {
                        Swipe(arrayxy[0], arrayxy[1], arrayxy[2], arrayxy[3], arrayxy[4]);
                    }
                }
                if (code.Trim().ToLower().Contains("send("))
                {
                    string[] arraysend = code.Split(new string[] { "Send(", ")"}, StringSplitOptions.RemoveEmptyEntries);
                    if (arraysend.Length - 1 == 0)
                    {
                        sendKey(arraysend[0]);
                    }
                }
                if (code.Trim().ToLower().Contains("wait("))
                {
                    string[] arraywait = code.Split(new string[] { "Wait(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arraywait.Length - 1 == 0)
                    {
                        int sleep = (int)(double.Parse(arraywait[0]) * 1000);
                        Thread.Sleep(sleep);
                    }
                }
                if (code.Trim().ToLower().Contains("randomfirstname()"))
                {
                    if (File.Exists("./quy/FirstName.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/FirstName.txt");
                        sendKey(arr[new Random().Next(0, arr.Length - 2)]);
                    }
                }
                if (code.Trim().ToLower().Contains("randomlastname()"))
                {
                    if (File.Exists("./quy/LastName.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/LastName.txt");
                        sendKey(arr[new Random().Next(0, arr.Length - 2)]);
                    }
                }
                if (code.Trim().ToLower().Contains("randomnumber("))
                {
                    string[] arrayxy = code.Split(new string[] { "RandomNumber(", ")", "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrayxy.Length - 1 == 2)
                    {
                        string rd = new Random().Next(int.Parse(arrayxy[0]),int.Parse(arrayxy[1])).ToString();
                        sendKey(rd);
                    }
                }
                if (code.Trim().ToLower().Contains("randomuser"))
                {
                    if (File.Exists("./quy/LastName.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/LastName.txt");
                        sendKey(arr[new Random().Next(0, arr.Length - 2)] + new Random().Next(100, 9999));
                    }
                }
                if (code.Trim().ToLower().Contains("randomuser"))
                {
                    if (File.Exists("./quy/LastName.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/LastName.txt");
                        sendKey(arr[new Random().Next(0, arr.Length - 2)] + new Random().Next(100, 9999));
                    }
                }
                if (code.Trim().ToLower().Contains("randompassword"))
                {
                    sendKey(password);
                }
                if (code.Trim().ToLower().Contains("textrandom"))
                {
                    if (File.Exists("./quy/TextRandom.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/TextRandom.txt");
                        sendKey(arr[new Random().Next(0, arr.Length - 2)]);
                    }
                }
                if (code.Trim().ToLower().Contains("textrandomdelte"))
                {
                    if (File.Exists("./quy/TextRandomDelete.txt"))
                    {
                        string[] arr = File.ReadAllLines("./quy/TextRandomDelete.txt");
                        string strrd = arr[new Random().Next(0, arr.Length - 2)];
                        sendKey(strrd);
                        arr = arr.Where(val => val != strrd).ToArray();
                        File.WriteAllLines("./quy/TextRandomDelete.txt", arr);
                    }
                }
            }
        }

        private void Touch(string x, string y)
        {
            
            ssh.runcmd("stouch touch " + x + " " + y );
            
        }
        private void Swipe(string x, string y, string x1, string y1, string time)
        {
     
            ssh.runcmd("stouch swipe " + x + " " + y + " " + x1 + " " + y1 + " " + time);
            
        }
        private void sendKey(string key)
        {
            for(int i=0; i<= key.Length - 1; i++)
            {
                ssh.runcmd("skeyboard 7 " + checkkeyboad(key[i].ToString()));
            }
        }
        private string checkkeyboad(string str )
        {
            if(str.Length == 1)
            {
                switch (str.ToLower())
                {
                    case "a": return "4";
                    case "b": return "5";
                    case "c": return "6";
                    case "d": return "7";
                    case "e": return "8";
                    case "f": return "9";
                    case "g": return "10";
                    case "h": return "11";
                    case "i": return "12";
                    case "j": return "13";
                    case "k": return "14";
                    case "l": return "15";
                    case "m": return "16";
                    case "n": return "17";
                    case "o": return "18";
                    case "p": return "19";
                    case "q": return "20";
                    case "r": return "21";
                    case "s": return "22";
                    case "t": return "23";
                    case "u": return "24";
                    case "v": return "25";
                    case "w": return "26";
                    case "x": return "27";
                    case "y": return "28";
                    case "z": return "29";
                    case "1": return "30";
                    case "2": return "31";
                    case "3": return "32";
                    case "4": return "33";
                    case "5": return "34";
                    case "6": return "35";
                    case "7": return "36";
                    case "8": return "37";
                    case "9": return "38";
                    case "0": return "39";
                }
            }
            return "error";
        }
    }
}
