using GregsStack.InputSimulatorStandard;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace LyricTyper
{
    static class Program
    {
        static void Main()
        {
            Console.Write("CTRL + C to close program or click on the X to close the window, im too lazy to build in actual closing mechanism via some shortcut lol.\n");
            Console.WriteLine("Name the program in which you want to open to type the lyrics in: ");
            
            string pName = Console.ReadLine().ToLower();
            SwitchDiscord(pName);
            SendLyrics(pName);
        }

        static void SwitchDiscord(string programName)
        {
            [DllImport("user32.dll")]
            static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);

            String ProcWindow = programName;

            
            Process[] procs = Process.GetProcessesByName(ProcWindow);
            foreach (Process proc in procs)
            {
                //switch to process by name
                SwitchToThisWindow(proc.MainWindowHandle, true);
            }
        }

        static void SendLyrics(string programName)
        {
            #region lyrics
            string lyrics = "Somebody once told me the world is gonna roll me " +
                "\nI ain't the sharpest tool in the shed " +
                "\nShe was looking kind of dumb with her finger and her thumb " +
                "\nIn the shape of an \"L\" on her forehead " +
                "\nWell the years start coming and they don't stop coming " +
                "\nFed to the rules and I hit the ground running " +
                "\nDidn't make sense not to live for fun " +
                "\nYour brain gets smart but your head gets dumb " +
                "\nSo much to do, so much to see " +
                "\nSo what's wrong with taking the back streets? " +
                "\nYou'll never know if you don't go " +
                "\nYou'll never shine if you don't glow " +
                "\nHey now, you're an all-star, get your game on, go play " +
                "\nHey now, you're a rock star, get the show on, get paid " +
                "\nAnd all that glitters is gold " +
                "\nOnly shooting stars break the mold " +
                "\nIt's a cool place and they say it gets colder " +
                "\nYou're bundled up now, wait 'til you get older " +
                "\nBut the meteor men beg to differ " +
                "\nJudging by the hole in the satellite picture " +
                "\nThe ice we skate is getting pretty thin " +
                "\nThe water's getting warm so you might as well swim " +
                "\nMy world's on fire, how about yours? " +
                "\nThat's the way I like it and I'll never get bored " +
                "\nHey now, you're an all-star, get your game on, go play " +
                "\nHey now, you're a rock star, get the show on, get paid " +
                "\nAll that glitters is gold " +
                "\nOnly shooting stars break the mold " +
                "\nHey now, you're an all-star, get your game on, go play " +
                "\nHey now, you're a rock star, get the show, on get paid " +
                "\nAnd all that glitters is gold " +
                "\nOnly shooting stars " +
                "\nSomebody once asked could I spare some change for gas? " +
                "\nI need to get myself away from this place " +
                "\nI said, \"Yup\" what a concept " +
                "\nI could use a little fuel myself " +
                "\nAnd we could all use a little change " +
                "\nWell, the years start coming and they don't stop coming " +
                "\nFed to the rules and I hit the ground running " +
                "\nDidn't make sense not to live for fun " +
                "\nYour brain gets smart but your head gets dumb " +
                "\nSo much to do, so much to see " +
                "\nSo what's wrong with taking the back streets? " +
                "\nYou'll never know if you don't go(go!) " +
                "\nYou'll never shine if you don't glow " +
                "\nHey now, you're an all-star, get your game on, go play " +
                "\nHey now, you're a rock star, get the show on, get paid " +
                "\nAnd all that glitters is gold " +
                "\nOnly shooting stars break the mold " +
                "\nAnd all that glitters is gold " +
                "\nOnly shooting stars break the mold";
            #endregion

            int indx = 0;
            while (true)
            {
                if (GetWndwTitle(programName))
                {
                    var simulator = new InputSimulator();
                    simulator.Keyboard.TextEntry(lyrics[indx]);

                    Random rnd = new Random();
                    int rInt = rnd.Next(25, 200);
                    Thread.Sleep(rInt);
                    indx++;
                }
            }
        }


        static bool GetWndwTitle(string programName)
        {
            [DllImport("user32.dll")]
            static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            static extern int GetWindowTextLength(IntPtr hWnd);

            
            var strTitle = string.Empty;
            var handle = GetForegroundWindow(); 
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);

            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString().ToLower();
                if (strTitle.Contains(programName))
                {
                    return true;
                }
            }
            return false;
                
        }
    }
}
