using System;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Drawing;

namespace PingIPong
{
    class Program
    {
        static int xmax = 80;
        static int ymax = 24;

        static int p1loc = 12;
        static int[] p1locs = new int[] { 11, 12, 13 };

        static int p2loc = 12;
        static int[] p2locs = new int[] { 11, 12, 13 };

        static Point ball_loc = new Point(40, 12);

        static int ballspeed = 200;

        static int winner = 0;

        static int ballrout = 3;
        // 012
        // 3*4
        // 567

        static void Main(string[] args)
        {
            
            if (args.Length > 0)
            {
                if (args[0].Substring(0,6) == "speed:")
                {
                    ballspeed = Convert.ToInt16(args[0].Split(':')[1]);
                }
            }
            Console.WriteLine("Defualt speed: " + ballspeed.ToString());
            Console.WriteLine("Speed keys: (L)[-100 speed], (S)[+100 speed]");
            Console.WriteLine("p1 move keys: (W)[Up], (S)[Down]");
            Console.WriteLine("p2 move keys: (UpArrow), (DownArrow)");
            Console.WriteLine("\nType any key for start");
            Console.ReadKey();
            draw();
            readkey(); 
            ballmove();
            while (true)
            {
            }
        }

        static void ballmove()
        {

            Task.Run(() =>
                {
                    while (true)
                    {

                        Point sball_loc = ball_loc;

                        if (ballrout == 0)
                        {
                            sball_loc.X--;
                            sball_loc.Y--;
                        }
                        else if (ballrout == 1)
                        {
                            sball_loc.Y--;
                        }
                        else if (ballrout == 2)
                        {
                            sball_loc.X++;
                            sball_loc.Y--;
                        }
                        else if (ballrout == 3)
                        {
                            sball_loc.X--;
                        }
                        else if (ballrout == 4)
                        {
                            sball_loc.X++;
                        }
                        else if (ballrout == 5)
                        {
                            sball_loc.X--;
                            sball_loc.Y++;
                        }
                        else if (ballrout == 6)
                        {
                            sball_loc.Y++;
                        }
                        else if (ballrout == 7)
                        {
                            sball_loc.X++;
                            sball_loc.Y++;
                        }

                        if (sball_loc.X == 0)
                        {
                            //p1 goback check
                            for (int i = 0; i < p1locs.Length; i++)
                            {
                                if (p1locs[i] == sball_loc.Y)
                                {
                                    if (i == 0)
                                    {
                                        // 012
                                        // 3*4
                                        // 567
                                        ballrout = 2;
                                    }
                                    else if (i == 1)
                                    {
                                        ballrout = 4;
                                    }
                                    else if (i == 2)
                                    {
                                        ballrout = 7;
                                    }
                                    if (ballspeed > 50)
                                    {
                                        ballspeed -= ballspeed / 50;
                                        ballspeed -= 5;
                                    }
                                }
                            }
                        }

                        if (sball_loc.X == 79)
                        {
                            //p2 goback check
                            for (int i = 0; i < p2locs.Length; i++)
                            {
                                if (p2locs[i] == sball_loc.Y)
                                {
                                    if (i == 0)
                                    {
                                        // 012
                                        // 3*4
                                        // 567
                                        ballrout = 0;
                                    }
                                    else if (i == 1)
                                    {
                                        ballrout = 3;
                                    }
                                    else if (i == 2)
                                    {
                                        ballrout = 5;
                                    }
                                    if (ballspeed > 50)
                                    {
                                        ballspeed -= ballspeed / 50;
                                        ballspeed -= 5;
                                    }
                                }
                            }
                        }

                        //sim end

                        if (ballrout == 0)
                        {
                            ball_loc.X--;
                            ball_loc.Y--;
                        }
                        else if (ballrout == 1)
                        {
                            ball_loc.Y--;
                        }
                        else if (ballrout == 2)
                        {
                            ball_loc.X++;
                            ball_loc.Y--;
                        }
                        else if (ballrout == 3)
                        {
                            ball_loc.X--;
                        }
                        else if (ballrout == 4)
                        {
                            ball_loc.X++;
                        }
                        else if (ballrout == 5)
                        {
                            ball_loc.X--;
                            ball_loc.Y++;
                        }
                        else if (ballrout == 6)
                        {
                            ball_loc.Y++;
                        }
                        else if (ballrout == 7)
                        {
                            ball_loc.X++;
                            ball_loc.Y++;
                        }

                        

                        if (ball_loc.X > 80)
                        {
                            ball_loc = new Point(40, 12);
                            winner = 1;
                            ballrout = 3;
                            Task.Delay(1000).Wait();
                        }
                        else if (ball_loc.X < 0)
                        {
                            winner = 2;
                            ball_loc = new Point(40, 12);
                            ballrout = 4;
                            Task.Delay(1000).Wait();
                        }
                        else if (ball_loc.Y < 0)
                        {
                            // 012
                            // 3*4
                            // 567
                            int newr = 0;
                            if (ballrout == 0)
                            {
                                newr = 5;
                            }
                            else if (ballrout == 1)
                            {
                                newr = 7;
                            }
                            else if (ballrout == 2)
                            {
                                newr = 7;
                            }
                            ballrout = newr;
                        }
                        else if (ball_loc.Y > 24)
                        {
                            // 012
                            // 3*4
                            // 567
                            int newr = 0;
                            if (ballrout == 5)
                            {
                                newr = 0;
                            }
                            else if (ballrout == 6)
                            {
                                newr = 1;
                            }
                            else if (ballrout == 7)
                            {
                                newr = 2;
                            }
                            ballrout = newr;
                        }
                        draw();
                        Task.Delay(ballspeed).Wait();
                    }
                });
        }

        static void readkey()
        {
            Task.Run(() =>
                {
                    while (true)
                    {
                        var key = Console.ReadKey();

                        if (key.Key == ConsoleKey.S)
                        {
                            if (p1loc + 1 < 23)
                            {
                                updatep1(p1loc + 1);
                                draw();
                            }
                        }
                        if (key.Key == ConsoleKey.W)
                        {
                            if (p1loc - 1 > 0)
                            {
                                updatep1(p1loc - 1);
                                draw();
                            }
                        }
                        if (key.Key == ConsoleKey.DownArrow)
                        {
                            if (p2loc + 1 < 23)
                            {
                                updatep2(p2loc + 1);
                                draw();
                            }
                        }
                        if (key.Key == ConsoleKey.UpArrow)
                        {
                            if (p2loc - 1 > 0)
                            {
                                updatep2(p2loc - 1);
                                draw();
                            }
                        }
                        if (key.Key == ConsoleKey.P)
                        {
                            ballspeed += 100;
                        }
                        if (key.Key == ConsoleKey.L)
                        {
                            if (ballspeed > 100) ballspeed -= 100;
                        }
                    }
                });
        }

        static void updatep1(int newloc)
        {
            p1loc = newloc;
            p1locs = new int[] { p1loc - 1, p1loc, p1loc + 1 };
        }

        static void updatep2(int newloc)
        {
            p2loc = newloc;
            p2locs = new int[] { p2loc - 1, p2loc, p2loc + 1 };
        }


        static void draw()
        {
            Console.Clear();
            if (winner == 0) Console.Title = "Ball X: " + ball_loc.X.ToString() + ", Y: " + ball_loc.Y.ToString()+", Speed: " + ballspeed.ToString()+" (Lower is faster | L/P Keys)";
            if (winner != 0) Console.Title = "Player " + winner.ToString() + " is wonner, Ball X: " + ball_loc.X.ToString() + ", Y: " + ball_loc.Y.ToString() + ", Speed: " + ballspeed.ToString() + " (Lower is faster | L/P Keys)";
            for (int y = 0;y < ymax;y++)
            {
                StringBuilder line = new StringBuilder();

                foreach (int p in p1locs)
                {
                    if (p == y) { line.Append("|");}
                }

                bool p2sameline = false;
                foreach (int p in p2locs)
                {
                    if (p == y) p2sameline = true;
                }

                if (line.Length == 0)
                {
                    if (y == ball_loc.Y)
                    {
                        if (!p2sameline) { line.Append(getxfory(" ", ball_loc.X)+"*"); line.Append(getxfory(" ", 78-ball_loc.X)); }
                        else { line.Append(getxfory(" ", ball_loc.X) + "*"); line.Append(getxfory(" ", 77 - ball_loc.X)); }
                    }
                    else
                    {
                        if (!p2sameline) { line.Append(getxfory(" ", 79)); }
                        else { line.Append(getxfory(" ", 78)); }
                    }
                }
                else if (line.Length == 1)
                {
                    if (y == ball_loc.Y)
                    {
                        if (!p2sameline) { line.Append(getxfory(" ", ball_loc.X - 1) + "*"); line.Append(getxfory(" ", (77 - ball_loc.X) + 1)); }
                        else { line.Append(getxfory(" ", ball_loc.X-1) + "*"); line.Append(getxfory(" ", (76 - ball_loc.X)+1)); }
                    }
                    else
                    {
                        if (!p2sameline) { line.Append(getxfory(" ", 78)); }
                        else { line.Append(getxfory(" ", 77)); }
                    }
                }

                if (p2sameline) { line.Append("|"); }
                Console.WriteLine(line.ToString());
            }
        }

        static string getxfory(string val, int time)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < time; i++) sb.Append(val);
            return sb.ToString();
        }
    }
}
