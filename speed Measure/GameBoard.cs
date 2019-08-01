using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace speed_Measure
{
    public partial class GameBoard : Form
    {
        string userName = "PLAYER";

        int level = 1;
        int totalLevel = 20;
        int answer = 0;
        int qnsNo = 0;
        int totalQns = 50;
        int qnsRight = 0;
        int qnsWrong = 0;
        int performence = 0;
        int splashWidth = 340;
        int lastLevel = 21;

        bool gameStatus = true;
        bool posOption = true;
        bool mouseClick = true;

        bool backgroundSound = false;
        bool buttonSound = true;
      //  bool gameSound = false;

        bool[] isPlay = { false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        int[] lvlTotalRight = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] lvlTotalWrong = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] lvlPerformence = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        int sign = 0;
        int ansMode = 0;
        int a, b, c, d, p, q, r, s;

        //SoundPlayer bgPlayer;
        SoundPlayer optionPlayer;

        public GameBoard()
        {
            InitializeComponent();
        }
        public GameBoard(string username)
        {
            InitializeComponent();
            userName = "" + username;
            txtUserName.Text = "User : " + username;
            realTime.Start();
            textAnswer.Visible = false;
            optionUpdate();
            updateScore();

            aboutPanel.Hide();
            ClockPanel.Hide();
            musicPanel.Hide();
            calendarPanel.Hide();
            gamePosPanel.Hide();
            upgradeLevel.Hide();
            endGame.Hide();

            this.Width = 520;
            MenubarPanel.BringToFront();


            gamePanel.Left = 40;
            gamePanel.Top = 55;
            gamePanel.Height = 500;
            gamePanel.Width = 520;
            gamePanel.Show();
        }

        //.........ANIMATION CLASS...............................
        public static class Util
        {
            public enum Effect { Roll, Slide, Center, Blend }

            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];

                if (ctl.Visible)
                {
                    flags |= 0x10000;
                    angle += 180;
                }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }

                flags |= dirmap[(angle % 360) / 180];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);

                if (!ok) throw new Exception("Animacion Fallida");
                ctl.Visible = !ctl.Visible;

            }

            private static int[] dirmap = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]

            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

        }
        //............END ANIMATION............................

        // PANEL LOCATION CUSTOMIZE
        void panelLocSize(string panelName)
        {
            if (panelName.Equals("gamePanel"))
            {
                gamePosPanel.Hide();
                upgradeLevel.Hide();

                gamePanel.Left = 40;
                gamePanel.Top = 55;
                gamePanel.Height = 500;
                gamePanel.Width = 520;
                gamePanel.Show();
                optionUpdate();
            } else if (panelName.Equals("gamePosPanel"))
            {
                gamePanel.Hide();
                upgradeLevel.Hide();

                gamePosIImage.BringToFront();
                gamePanel.Left = 40;
                gamePosPanel.Top = 55;
                gamePosPanel.Height = 500;
                gamePosPanel.Width = 520;
                gamePosPanel.Show();
                lvlViewTxt.Text = "LEVEL " + level + " OF " + totalLevel;
                lvlQnsNo.Text = "QUESTION " + qnsNo + " OF 50";
            }
            else if (panelName.Equals("upgradeLevel"))
            {
                gamePanel.Hide();
                gamePosPanel.Hide();
                
                upgradeLevel.Left = 40;
                upgradeLevel.Top = 55;
                upgradeLevel.Height = 500;
                upgradeLevel.Width = 520;
                upgradeLevel.Show();
            }
        }

        //  MENUBAR HANDLING
        private void menuButton_Click(object sender, EventArgs e)
        {
            if (MenubarPanel.Width <= 40)
            {
                if (isPlay[0])
                    step1.Image = Properties.Resources.level_buton1;
                else step1.Image = Properties.Resources.level_button_2;
                if (isPlay[1])
                    step2.Image = Properties.Resources.level_buton1;
                else step2.Image = Properties.Resources.level_button_2;
                if (isPlay[2])
                    step3.Image = Properties.Resources.level_buton1;
                else step3.Image = Properties.Resources.level_button_2;
                if (isPlay[3])
                    step4.Image = Properties.Resources.level_buton1;
                else step4.Image = Properties.Resources.level_button_2;
                if (isPlay[4])
                    step5.Image = Properties.Resources.level_buton1;
                else step5.Image = Properties.Resources.level_button_2;
                if (isPlay[5])
                    step6.Image = Properties.Resources.level_buton1;
                else step6.Image = Properties.Resources.level_button_2;
                if (isPlay[6])
                    step7.Image = Properties.Resources.level_buton1;
                else step7.Image = Properties.Resources.level_button_2;
                if (isPlay[7])
                    step8.Image = Properties.Resources.level_buton1;
                else step8.Image = Properties.Resources.level_button_2;
                if (isPlay[8])
                    step9.Image = Properties.Resources.level_buton1;
                else step9.Image = Properties.Resources.level_button_2;
                if (isPlay[9])
                    step10.Image = Properties.Resources.level_buton1;
                else step10.Image = Properties.Resources.level_button_2;
                if (isPlay[10])
                    step11.Image = Properties.Resources.level_buton1;
                else step11.Image = Properties.Resources.level_button_2;
                if (isPlay[11])
                    step12.Image = Properties.Resources.level_buton1;
                else step12.Image = Properties.Resources.level_button_2;
                if (isPlay[12])
                    step13.Image = Properties.Resources.level_buton1;
                else step13.Image = Properties.Resources.level_button_2;
                if (isPlay[13])
                    step14.Image = Properties.Resources.level_buton1;
                else step14.Image = Properties.Resources.level_button_2;
                if (isPlay[14])
                    step15.Image = Properties.Resources.level_buton1;
                else step15.Image = Properties.Resources.level_button_2;
                if (isPlay[15])
                    step16.Image = Properties.Resources.level_buton1;
                else step16.Image = Properties.Resources.level_button_2;
                if (isPlay[16])
                    step17.Image = Properties.Resources.level_buton1;
                else step17.Image = Properties.Resources.level_button_2;
                if (isPlay[17])
                    step18.Image = Properties.Resources.level_buton1;
                else step18.Image = Properties.Resources.level_button_2;
                if (isPlay[18])
                    step19.Image = Properties.Resources.level_buton1;
                else step19.Image = Properties.Resources.level_button_2;
                if (isPlay[19])
                    step20.Image = Properties.Resources.level_buton1;
                else step20.Image = Properties.Resources.level_button_2;



                menuButton.Image = Properties.Resources.leftArrow;
                while (true)
                {
                    gamePanel.Hide();
                    gameTime.Stop();
                    MenubarPanel.Width += 20;
                    if (MenubarPanel.Width == 300)
                        break;
                    Thread.Sleep(20);
                }
            }
            else
            {
                menubarClose();
            }
        }

        void menubarClose ()
        {
            menuButton.Image = Properties.Resources.rightArrow;
            while (true)
            {
                MenubarPanel.Width -= 20;
                if (MenubarPanel.Width == 40)
                    break;
                Thread.Sleep(20);
                gameTime.Start();
            }
            gamePanel.Show();
        }

        //........................................................................
        //  PROBLEM SET GENERAT
        void setProblemAndAnswer()
        {
            a = b = c = d = p = q = r = s = answer = sign = ansMode = 0;
            if (level > 10)  // 11-20 level
            {
                int low1, low2, high1, high2;
                int val1 = new Random().Next(1, 3);
                int val2 = new Random().Next(1, 3);
                sign += new Random().Next(1, 3);


                if (level == 11)
                {
                    low1 = -5;
                    low2 = -10;
                    high1 = 30;
                    high2 = 40;
                }
                else if (level == 12)
                {
                    low1 = -20;
                    low2 = -30;
                    high1 = 80;
                    high2 = 100;
                }
                else if (level == 13)
                {
                    low1 = -30;
                    low2 = -50;
                    high1 = 200;
                    high2 = 500;
                }
                else if (level == 14)
                {
                    low2 = -5;
                    low1 = -10;
                    high2 = 30;
                    high1 = 40;
                }
                else if (level == 15)
                {
                    low2 = -20;
                    low1 = -30;
                    high2 = 80;
                    high1 = 100;
                }
                else if (level == 16)
                {
                    low2 = -30;
                    low1 = -50;
                    high2 = 200;
                    high1 = 500;
                }
                else if (level == 17)
                {
                    low2 = -5;
                    low1 = -10;
                    high2 = 30;
                    high1 = 40;
                }
                else if (level == 18)
                {
                    low2 = -20;
                    low1 = -30;
                    high2 = 80;
                    high1 = 100;
                }
                else if (level == 19)
                {
                    low2 = -30;
                    low1 = -50;
                    high2 = 200;
                    high1 = 500;
                }
                else
                {
                    low2 = -30;
                    low1 = -50;
                    high2 = 500;
                    high1 = 1200;
                }
                a += new Random().Next(low1, high1);
                b += new Random().Next(low2, high2);
                if (val1 == 1)
                    a *= -1;
                if (val2 == 1)
                    b *= -1;
                if (sign == 1)
                    answer += a + b;
                else
                    answer += a - b;

                if (answer < 0)   // answer to mod answer
                    answer *= -1;

                p += answer;

                q += answer - 10;
                r += answer + 10;
                s += new Random().Next(answer - 10, answer + 10);
                if (s == p || s == q || s == r)
                    s += 3;


                string str = "| ";
                if (a < 0)
                    str += "- " + (-a);
                else str += a;

                if (sign != 1 && b<0) {
                    str += " + " + (-b);
                } else if (sign == 1 && b>=0)
                {
                    str += " + " + b;
                }
                else
                {
                    if (b < 0)
                        str += " - " + (-b);
                    else
                        str += " - " + b;
                }
                problemScreen.Text = str + " | = ?";
            }
            else     // 1-10 level
            {
                sign += new Random().Next(1, 2);
                int low1, low2, high1, high2;
                if (level == 1)
                {
                    low1 = 5;
                    low2 = 10;
                    high1 = 30;
                    high2 = 40;
                    sign = 1;
                } else if (level == 2)
                {
                    low1 = 20;
                    low2 = 30;
                    high1 = 80;
                    high2 = 100;
                    sign = 1;
                } else if (level == 3)
                {
                    low1 = 30;
                    low2 = 50;
                    high1 = 200;
                    high2 = 500;
                    sign = 1;
                }
                else if (level == 4)
                {
                    low2 = 5;
                    low1 = 10;
                    high2 = 30;
                    high1 = 40;
                    sign = 2;
                }
                else if (level == 5)
                {
                    low2 = 20;
                    low1 = 30;
                    high2 = 80;
                    high1 = 100;
                    sign = 2;
                }
                else if (level == 6)
                {
                    low2 = 30;
                    low1 = 50;
                    high2 = 200;
                    high1 = 500;
                    sign = 2;
                }
                else if (level == 7)
                {
                    low2 = 5;
                    low1 = 10;
                    high2 = 30;
                    high1 = 40;
                }
                else if (level == 8)
                {
                    low2 = 20;
                    low1 = 30;
                    high2 = 80;
                    high1 = 100;
                }
                else if (level == 9)
                {
                    low2 = 30;
                    low1 = 50;
                    high2 = 200;
                    high1 = 500;
                } else
                {
                    low2 = 30;
                    low1 = 50;
                    high2 = 500;
                    high1 = 1200;
                }
                a += new Random().Next(low1, high1);
                b += new Random().Next(low2, high2);
                answer += a + b;
                p += answer;

                q += answer - 10;
                r += answer + 10;
                s += new Random().Next(answer - 10, answer + 10);
                if (s == p || s == q || s == r)
                    s += 3;
                problemScreen.Text = a + " + " + b + " = ?";
            }

            ansMode += (int)new Random().Next(1, 5);
            updateAnsMode();


        }
        void updateAnsMode()
        {

            if (ansMode == 1)
            {
                option1.Text = "" + p;
                option2.Text = "" + q;
                option3.Text = "" + r;
                option4.Text = "" + s;
            }
            else if (ansMode == 2)
            {
                option1.Text = "" + q;
                option2.Text = "" + p;
                option3.Text = "" + r;
                option4.Text = "" + s;
            }
            else if (ansMode == 3)
            {
                option1.Text = "" + r;
                option2.Text = "" + q;
                option3.Text = "" + p;
                option4.Text = "" + s;
            }
            else
            {
                option1.Text = "" + s;
                option2.Text = "" + q;
                option3.Text = "" + r;
                option4.Text = "" + p;
            }
        }
        void optionUpdate()
        {
            option1.Image = Properties.Resources.optionBackground;
            option2.Image = Properties.Resources.optionBackground;
            option3.Image = Properties.Resources.optionBackground;
            option4.Image = Properties.Resources.optionBackground;
            levelTxt.Text = "Level : "+level + "/" + totalLevel;
            setProblemAndAnswer();
            textAnswer.Text = "";
            if (!mouseClick)
                textAnswer.Focus();
        }

        void updateScore()
        {
            lvlQnsNo.Text = "Question No : " + (qnsNo + 1);
            lvlRight.Text = "Right : " + qnsRight;
            lvlWrong.Text = "Wrong : " + qnsWrong;
            lPerformrnce.Text = "Performence : " + performence + "%";
        }


        //  MAIN GAME PART
        private void gameTime_Tick(object sender, EventArgs e)
        {
            
            if (qnsNo > totalQns-1) // NUMBER OF QUESTION 
            {
                if (level + 1 == lastLevel)
                {
                    bool play = false;
                    for (int i=0; i<totalLevel; i++)
                    {
                        if (isPlay[i])
                        {
                            play = true;
                            level = i;
                            break;
                        }
                    }

                    if (play)
                    {
                        gameTime.Stop();
                        upgradeLevel.Left = 40;
                        upgradeLevel.Top = 55;
                        upgradeLevel.Height = 500;
                        upgradeLevel.Width = 520;
                        Util.Animate(upgradeLevel, Util.Effect.Center, 100, 0);
                        upgradeLevelNo.Text = "ON LEVEL " + (level);
                    }
                    else
                    {
                        gameTime.Stop();
                        endGame.Left = 40;
                        endGame.Top = 55;
                        endGame.Height = 500;
                        endGame.Width = 520;
                        gamePanel.Hide();
                        Util.Animate(endGame, Util.Effect.Center, 100, 0);
                    }
                }
                else
                {
                    gameTime.Stop();
                    upgradeLevel.Left = 40;
                    upgradeLevel.Top = 55;
                    upgradeLevel.Height = 500;
                    upgradeLevel.Width = 520;
                    Util.Animate(upgradeLevel, Util.Effect.Center, 100, 0);
                    int x = level;
                    for (int i = x; i<totalLevel; i++)
                    {
                        if (isPlay[i])
                        {
                            level = i;
                            break;
                        }
                    }
                    upgradeLevelNo.Text = "ON LEVEL " + (level+1);
                }
            } else
            {
                if (splashWidth > 30)
                {
                    levelSplash.Width = splashWidth - 40;
                    levelBar.Text = "" + (splashWidth - 40) / 10;
                }
                else if (splashWidth >= 0 && posOption)
                {
                    gamePosPanel.Left = 40;
                    gamePosPanel.Top = 55;
                    gamePosPanel.Height = 500;
                    gamePosPanel.Width = 520;
                    gamePanel.Hide();

                    panelLocSize("gamePosPanel");
                }
                else
                {
                    panelLocSize("gamePanel");
                    qnsNo++;
                    optionUpdate();
                    updateScore();
                    splashWidth = 340;
                    levelSplash.Width = 300;
                    levelBar.Text = "" + levelSplash.Width / 10;
                }
                splashWidth -= 10;
            }
            
        }

        //  Right or wrong option click sound
        void optionClickSound(bool ans)
        {
          //  gameTime.Stop();
            levelSplash.Width = 0;
            levelBar.Text = "";

            if (ans)
            {
                optionPlayer = new SoundPlayer(Properties.Resources.CorrectAnswer);
                qnsRight++;
                lvlRight.Text = "Right : " + qnsRight;
                performence += 2;
                optionPlayer.Play();
            }
            else
            {
                optionPlayer = new SoundPlayer(Properties.Resources.wronganswer);
                qnsWrong++;
                lvlRight.Text = "Wrong : " + qnsWrong;
                optionPlayer.Play();
            }
        }

        private void gamePosIImage_Click(object sender, EventArgs e)
        {
            if (gameStatus)
            {
                gameStatus = false;
                gamePosIImage.Image = Properties.Resources.play;
                gamePosTxt.Text = "Continue The Game";
                gameTime.Stop();
            } else
            {
                gameStatus = true;
                gamePosIImage.Image = Properties.Resources.pause;
                gamePosTxt.Text = "Pause The Game";
                splashWidth = 340;
                panelLocSize("gamePanel");
                gameTime.Start();
            }
            optionUpdate();
        }

        private void option1_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                if (option1.Text.Equals("" + answer))
                {
                    option1.Text = "";
                    option1.Image = Properties.Resources.tik;
                    optionClickSound(true);
                }
                else
                {
                    option1.Text = "";
                    option1.Image = Properties.Resources.cross;
                    optionClickSound(false);
                }
                updateScore();
                splashWidth = 40;
            }
        }

        private void option2_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                if (option2.Text.Equals("" + answer))
                {
                    option2.Text = "";
                    option2.Image = Properties.Resources.tik;
                    optionClickSound(true);
                }
                else
                {
                    option2.Text = "";
                    option2.Image = Properties.Resources.cross;
                    optionClickSound(false);
                }
                updateScore();
                splashWidth = 40;
            }
        }

        private void option3_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                if (option3.Text.Equals("" + answer))
                {
                    option3.Text = "";
                    option3.Image = Properties.Resources.tik;
                    optionClickSound(true);
                }
                else
                {
                    option3.Text = "";
                    option3.Image = Properties.Resources.cross;
                    optionClickSound(false);
                }
                updateScore();
                splashWidth = 40;
            }
        }

        private void option4_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                if (option4.Text.Equals("" + answer))
                {
                    option4.Text = "";
                    option4.Image = Properties.Resources.tik;
                    optionClickSound(true);
                }
                else
                {
                    option4.Text = "";
                    option4.Image = Properties.Resources.cross;
                    optionClickSound(false);
                }
                updateScore();
                splashWidth = 40;
            }
        }

        private void hideGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                posOption = true;
            } else
            {
                posOption = false;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Width = 748;
            sidePanel.Visible = true;
            musicPanel.Visible = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Width = 748;
            sidePanel.Visible = true;
            ClockPanel.Visible = true;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Width = 748;
            sidePanel.Visible = true;
            calendarPanel.Visible = true;
        }

        private void TimeClose_Click(object sender, EventArgs e)
        {
            ClockPanel.Visible = false;
            if (!musicPanel.Visible && !calendarPanel.Visible)
            {
                sidePanel.Visible = false;
                this.Width = 519;
            }
            else
            {
                sidePanel.Visible = true;
                this.Width = 748;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            musicPanel.Visible = false;
            if (!ClockPanel.Visible && !calendarPanel.Visible)
            {
                sidePanel.Visible = false;
                this.Width = 519;
            }
            else
            {
                sidePanel.Visible = true;
                this.Width = 748;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            calendarPanel.Visible = false;
            if (!musicPanel.Visible && !ClockPanel.Visible)
            {
                sidePanel.Visible = false;
                this.Width = 519;
            }
            else
            {
                sidePanel.Visible = true;
                this.Width = 748;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.binaryteachzone.com/contact/");
        }

        private void namePanel3_Click(object sender, EventArgs e)
        {
            string name = lblname3.Text;
            if (name.Equals("NASRIN"))
            {
                if (lblname2.Text.Equals("ASM AKASH"))
                {
                    lblname2.Text = "NASRIN NAHAR";
                    lblname3.Text = "AKASH";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.NASRIN;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.AKASH;
                }
                else
                {
                    lblname2.Text = "NASRIN NAHAR";
                    lblname3.Text = "SADIA";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.NASRIN;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.SADIA;
                }
            }
            else if (name.Equals("SADIA"))
            {
                if (lblname2.Text.Equals("ASM AKASH"))
                {
                    lblname2.Text = "SADIA AKTER";
                    lblname3.Text = "AKASH";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.SADIA;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.AKASH;
                }
                else
                {
                    lblname2.Text = "SADIA AKTER";
                    lblname3.Text = "NASRIN";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.SADIA;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.NASRIN;
                }
            }
            else
            {
                if (lblname2.Text.Equals("SADIA AKTER"))
                {
                    lblname2.Text = "ASM AKASH";
                    lblname3.Text = "SADIA";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.AKASH;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.SADIA;
                }
                else
                {
                    lblname2.Text = "ASM AKASH";
                    lblname3.Text = "NASRIN";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.AKASH;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel3.BackgroundImage = Properties.Resources.NASRIN;
                }
            }
        }

        private void namePanel1_Click(object sender, EventArgs e)
        {
            string name = lblname1.Text;
            if (name.Equals("NASRIN"))
            {
                if (lblname2.Text.Equals("ASM AKASH"))
                {
                    lblname2.Text = "NASRIN NAHAR";
                    lblname1.Text = "AKASH";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.NASRIN;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.AKASH;
                }
                else
                {
                    lblname2.Text = "NASRIN NAHAR";
                    lblname1.Text = "SADIA";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.NASRIN;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.SADIA;
                }
            }
            else if (name.Equals("SADIA"))
            {
                if (lblname2.Text.Equals("ASM AKASH"))
                {
                    lblname2.Text = "SADIA AKTER";
                    lblname1.Text = "AKASH";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.SADIA;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.AKASH;
                }
                else
                {
                    lblname2.Text = "SADIA AKTER";
                    lblname1.Text = "NASRIN";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.SADIA;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.NASRIN;
                }
            }
            else
            {
                if (lblname2.Text.Equals("SADIA AKTER"))
                {
                    lblname2.Text = "ASM AKASH";
                    lblname1.Text = "SADIA";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.AKASH;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.SADIA;
                }
                else
                {
                    lblname2.Text = "ASM AKASH";
                    lblname1.Text = "NASRIN";
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel2.BackgroundImage = Properties.Resources.AKASH;
                    Util.Animate(namePanel2, Util.Effect.Center, 100, 0);
                    namePanel1.BackgroundImage = Properties.Resources.NASRIN;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aboutPanel.Hide();
            gamePanel.Show();
            gameTime.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            aboutPanel.Left = 40;
            aboutPanel.Top = 55;
            aboutPanel.Height = 500;
            aboutPanel.Width = 520;
            gameTime.Stop();
            gamePanel.Hide();
            aboutPanel.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.binaryteachzone.com/");
}

        private void realTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "" + DateTime.Now.ToString("hh:mm");
            lblSecond.Text = DateTime.Now.Second.ToString();
            lblAmPm.Text = DateTime.Now.ToString("tt");

            string name = userName;
            int len = name.Length;
            int subLen = DateTime.Now.Second % (len + 2);

            if (subLen >= len)
            {
                nametext.Text = name;
            }
            else
            {
                nametext.Text = name.Substring(0, subLen);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Hide();
            gameTime.Start();
        }

       

        // MENU BUTTON HANDLING
        void playLevel(int x)
        {
            x--;
            if (isPlay[x])
            {
                menubarClose();
                restartLevel(x);
            }
            else
            {
                if (MetroFramework.MetroMessageBox.Show(this, "You Already Played this level\nDo you want to play again?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    menubarClose();
                    restartLevel(x);
                }
                else
                {
                    menubarClose();
                    gameTime.Start();
                }
            }

        }
        private void step1_Click(object sender, EventArgs e)
        {
            playLevel(1);
        }
        private void step2_Click(object sender, EventArgs e)
        {
            playLevel(2);
        }
        private void step3_Click(object sender, EventArgs e)
        {
            playLevel(3);
        }
        private void step4_Click(object sender, EventArgs e)
        {
            playLevel(4);
        }

        private void step5_Click(object sender, EventArgs e)
        {
            playLevel(5);
        }

        private void step6_Click(object sender, EventArgs e)
        {
            playLevel(6);
        }

        private void step7_Click(object sender, EventArgs e)
        {
            playLevel(7);
        }

        private void step8_Click(object sender, EventArgs e)
        {
            playLevel(8);
        }

        private void step9_Click(object sender, EventArgs e)
        {
            playLevel(9);
        }

        private void step10_Click(object sender, EventArgs e)
        {
            playLevel(10);
        }

        private void step11_Click(object sender, EventArgs e)
        {
            playLevel(11);
        }

        private void step12_Click(object sender, EventArgs e)
        {
            playLevel(12);
        }

        private void step13_Click(object sender, EventArgs e)
        {
            playLevel(13);
        }

        private void step14_Click(object sender, EventArgs e)
        {
            playLevel(14);
        }

        private void step15_Click(object sender, EventArgs e)
        {
            playLevel(15);
        }

        private void step16_Click(object sender, EventArgs e)
        {
            playLevel(16);
        }

        private void step17_Click(object sender, EventArgs e)
        {
            playLevel(17);
        }

        private void step18_Click(object sender, EventArgs e)
        {
            playLevel(18);
        }

        private void step19_Click(object sender, EventArgs e)
        {
            playLevel(19);
        }

        private void step20_Click(object sender, EventArgs e)
        {
            playLevel(20);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            for (int i=0; i<20; i++)
            {
                isPlay[i] = true;
            }
            endGame.Hide();
            playLevel(1);
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Button Click Sound"))
            {
                if (buttonSound)
                {
                    soundOnOff.Image = Properties.Resources.pause;
                    pausePlayText.Text = "STOP BUTTON CLICK SOUND";

                } else
                {
                    soundOnOff.Image = Properties.Resources.play;
                    pausePlayText.Text = "START BUTTON CLICK SOUND";
                }
            } else if (comboBox1.Text.Equals("Background Music"))
            {
                if (backgroundSound)
                {
                    soundOnOff.Image = Properties.Resources.pause;
                    pausePlayText.Text = "STOP BACKGROUND MUSIC";
                }
                else
                {
                    soundOnOff.Image = Properties.Resources.play;
                    pausePlayText.Text = "START BACKGROUND MUSIC";
                }
            }
        }

        private void soundOnOff_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text.Equals("Button Click Sound"))
            {
                if (buttonSound)
                {
                   // optionPlayer.Play();
                   // bgPlayer.Stop();
                }
                else
                {
                   // optionPlayer.Stop();
                  //  bgPlayer.Play();
                }
            }
            else if (comboBox1.Text.Equals("Background Music"))
            {
                if (backgroundSound)
                {
                    soundOnOff.Image = Properties.Resources.pause;
                    pausePlayText.Text = "STOP BACKGROUND MUSIC";
                   // bgPlayer.Play();
                }
                else
                {
                    soundOnOff.Image = Properties.Resources.play;
                    pausePlayText.Text = "START BACKGROUND MUSIC";
                }
            }
        }

        private void textAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textAnswer.Text.Equals(""+answer))
                    optionClickSound(true);     
                else
                {
                    optionClickSound(false);
                }  
            }
            updateScore();
            splashWidth = 40;
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void lvlRight_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            posOption = false;
            checkBox1.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textAnswer.Visible = false;
            textAnswer.Focus();
            textHint.Text = "Click on the correct result";
            mouseClick = true;
            option1.Cursor = option2.Cursor = option3.Cursor = option4.Cursor = Cursors.Hand;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textAnswer.Visible = true;
            textHint.Text = "Type correct result and press Enter";
            mouseClick = false;
            textAnswer.Focus();
            option1.Cursor = option2.Cursor = option3.Cursor = option4.Cursor = Cursors.Default;
        }

        private void nextLevel_Click(object sender, EventArgs e)
        {
            bool play = false;
            for (int i=level; i<20; i++)
            {
                if (isPlay[i])
                {
                    restartLevel(i);
                    play = true;
                    break;
                }
            }
            if (!play)
            {
                bool allPlay = true;
                for (int i=1; i<20; i++)
                {
                    if (isPlay[i])
                    {
                        restartLevel(i);
                        allPlay = false;
                        break;
                    }
                }
                if (allPlay)
                {
                    upgradeLevel.Hide();
                    gamePanel.Hide();
                    gameTime.Stop();
                    endGame.Left = 40;
                    endGame.Top = 55;
                    endGame.Height = 500;
                    endGame.Width = 520;
                    gamePanel.Hide();
                    Util.Animate(endGame, Util.Effect.Center, 100, 0);
                }
            }
            
        }

        void restartLevel(int x)
        {
            level = x;
            qnsNo = 0;
            qnsRight = 0;
            qnsWrong = 0;
            performence = 0;
            splashWidth = 340;
            upgradeLevel.Show();
            optionUpdate();
            Util.Animate(upgradeLevel, Util.Effect.Center, 100, 0);
            upgradeLevel.Hide();
            level++;
            updateScore();
            optionUpdate();
            gameTime.Start();
            isPlay[level-1] = false;
        }
        private void repeatLevel_Click(object sender, EventArgs e)
        {
            qnsNo = 0;
            qnsRight = 0;
            qnsWrong = 0;
            performence = 0;
            splashWidth = 340;
            upgradeLevel.Show();
            Util.Animate(upgradeLevel, Util.Effect.Center, 100, 0);
            upgradeLevel.Hide();
            updateScore();
            gameTime.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.binaryteachzone.com/our-books/");
        }
    }
}
