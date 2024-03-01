using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPS_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       enum enplayer { player1,player2};
       enum enplayer2 { person,computer};
       enum enwinner { player1, player2 ,Draw};
        enplayer playerturn = enplayer.player1;
        enplayer2 player2turn = enplayer2.person;
        
        struct stgameresult
        {
          public  enwinner winner;
            public short player1win;
            public short player2win;
            public short nrounds;
        }
        stgameresult gameresult;
        void roundresult(PictureBox p1, PictureBox p2)
        {
            {
                if (p1.Tag.ToString() == p2.Tag.ToString())
                {
                    gameresult.winner = enwinner.Draw;
                    return;
                }
                if (p1.Tag.ToString() == "r"&& p2.Tag.ToString() == "p")
                {
                     gameresult.winner = enwinner.player2;
                    
                }
                else if (p1.Tag.ToString() == "p" && p2.Tag.ToString() == "s")
                {
                    gameresult.winner = enwinner.player2;
                   
                }
                else if (p1.Tag.ToString() == "s"&& p2.Tag.ToString() == "r")
                {
                    gameresult.winner = enwinner.player2;
                }
                else { 
                    gameresult.winner = enwinner.player1;
                }
            }
        }
        void picturechoice(PictureBox pb)
        {
            if (pb.Tag.ToString() == "r")
                pb.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\rock.jpg");
            else if (pb.Tag.ToString() == "p")
            {
                pb.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\paper.jpg");
            }
            else{
                pb.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\scissors.jpg");
            }
        }
        void result()
        {
            switch (gameresult.winner)
            {
                case enwinner.player1:
                    pbp1photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\win.jpg");
                    pbp2photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\fail.jpg");
                    gameresult.player1win++;
                    gameresult.nrounds++;
                    break;
                case enwinner.player2:
                    pbp1photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\fail.jpg");
                    pbp2photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\win.jpg");
                    gameresult.player2win++;
                    gameresult.nrounds++;
                    break;
                default:
                    gameresult.nrounds++;
                    pbp1photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\normal.jpg");
                    pbp2photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\normal.jpg");
                    break;
            }
            lbp1result.Text = gameresult.player1win.ToString();
            lbp2result.Text = gameresult.player2win.ToString();
            lbround.Text = gameresult.nrounds.ToString();
        }
        void changepicture(Button bt)
        {
             switch (playerturn)
             {
                case enplayer.player1:
                    pbp1choice.Tag = bt.Tag;
                    picturechoice(pbp1choice);
                    if (player2turn == enplayer2.person)
                    {
                        playerturn = enplayer.player2;
                        lbturn.Text = "Player2";
                        btcomputer.Visible = false;
                    }

                    pbwhosturnphoto.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\p2turn.jpg");
                    break;
                case enplayer.player2:
                    pbp2choice.Tag = bt.Tag;
                    picturechoice(pbp2choice);
                    playerturn = enplayer.player1;
                    lbturn.Text = "Player1";
                    pbwhosturnphoto.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\p1turn.jpg");
                    roundresult(pbp1choice, pbp2choice);
                     result();
                        break;

                }
            if (player2turn == enplayer2.computer)
            {
                computerchoice();
                picturechoice(pbp2choice);
                playerturn = enplayer.player1;
                lbturn.Text = "Player1";
                pbp1photo.Image = Image.FromFile(@"C:\Users\hp\Downloads\Telegram Desktop\p1turn.jpg");
                roundresult(pbp1choice, pbp2choice);
                result();
            }
        }
        void computerchoice()
        {
            Random random = new Random();
            byte rand = (byte)random.Next(1, 3);
            switch (rand)
            {
                case 1:
                    pbp2choice.Tag = "r";
                    break;
                case 2:
                    pbp2choice.Tag = "p";
                    break;
                case 3:
                    pbp2choice.Tag = "s";
                    break;
            }
        }
        private void btclick(object sender, EventArgs e)
        {
            changepicture((Button )sender);
        }
        private void btcomputer_Click(object sender, EventArgs e)
        {
            lbwhoplayer2.Text = "Computer: ";
            lbplayer2.Text = lbwhoplayer2.Text;
            player2turn = enplayer2.computer;
        }

        private void btstop_Click(object sender, EventArgs e)
        {
            if(gameresult.player1win> gameresult.player2win)
            {
                MessageBox.Show("Player 1 is the winner :)", "Final Result",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }else if (gameresult.player1win < gameresult.player2win)
            {
                if (player2turn == enplayer2.computer)
                {
                    MessageBox.Show("Computer  is the winner :)", "Final Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Player 2 is the winner :)", "Final Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Draw :)", "Final Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
