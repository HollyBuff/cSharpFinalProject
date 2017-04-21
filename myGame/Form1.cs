using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myGame
{
    public partial class Form1 : Form
    {

        bool right, left, jump;
        int G = 25;
        int Force;

        public Form1()
        {
            InitializeComponent();

            player.Top = screen.Height - player.Height; // sets the block start position
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 1. side collision
            if (player.Right > block.Left && player.Left < block.Right - player.Width && player.Bottom < block.Bottom && player.Bottom > block.Top)
            {
                right = false;
            }
            if (player.Left < block.Right && player.Right > block.Right - player.Width && player.Bottom < block.Bottom && player.Bottom > block.Top)
            {
                left = false;
            }

            /////

            // 2. 
            if (right == true) { player.Left += 5; }
            if (left == true) { player.Left -= 5; }

            if (jump == true)
            {
                // falling (if the player has jumped before)
                player.Top -= Force;
                Force -= 1;
            }

            if (player.Top + player.Height >= screen.Height)
            {
                player.Top = screen.Height - player.Height; //stop falling at the bottom
                jump = false;
            }
            else
            {
                player.Top += 5; // Falling
            }

            // 3. top collision
            if (player.Left + player.Width - 1 > block.Left && player.Left + player.Width + 5 < block.Left + block.Width + player.Width && player.Top + player.Height >= block.Top && player.Top < block.Top)
            {
                player.Top = block.Location.Y - player.Height;
                Force = 0;
                jump = false;
            }

            // head collision
            if (player.Left + player.Width - 1 > block.Left && player.Left + player.Width + 5 < block.Left + block.Width + player.Width && player.Top - block.Bottom <= 10 && player.Top - block.Top > -10)
            {
                Force = -1;
            }

            /////
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Escape) { this.Close(); } // Escape -> Exit

            if (jump != true)
            {
                if (e.KeyCode == Keys.Space)
                {
                    jump = true;
                    Force = G;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = false; }
            if (e.KeyCode == Keys.Left) { left = false; }
        }
    }
}
