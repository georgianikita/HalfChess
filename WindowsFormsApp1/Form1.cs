using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static int MODE_PvP = 0;
        public static int MODE_ABvP = 1;
        public static int MODE_AWvP = 2;
        public static int MODE_AvA = 3;
        Catur papan = new Catur();
        bool fin = false;
        List<Point> posisi = new List<Point>();
        Point pionselected ;
        bool turn = true;
        char bidak;
        int mode;

        public Form1(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             triggerGerakAi();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            papan.draw(e.Graphics, posisi);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //game berenti
            if (fin)
            {
                return;
            }

            //game berjalan
            if(posisi.Count > 0)
            {
                //posisi selanjutnya yg ditekan
                Point p = papan.konversi(e.Location);

                //jika yg diteken ada di list hint
                if (posisi.Contains(p))
                {
                    //taruh pion ke posisi baru
                    papan.move(pionselected,p);
                    turn = !turn;
                    posisi.Clear();
                    checkGameEnded();
                }
                else
                {
                    //posisi bidak
                    pionselected = papan.konversi(e.Location);

                    //dapetin hint
                    posisi = papan.hint(pionselected.X, pionselected.Y, turn);
                }
            }
            else
            {
                //posisi bidak
                pionselected = papan.konversi(e.Location);

                //dapetin hint
                posisi = papan.hint(pionselected.X, pionselected.Y, turn);
                
            }
            //gambar ulang setelah dpt hint
            Invalidate();
            
        }
        
        private void triggerGerakAi()
        {
            if (turn)
            {
                if (mode == MODE_AWvP || mode == MODE_AvA)
                {
                    papan.gerakAI(turn);
                    turn = !turn;
                    checkGameEnded();

                }

            }
            else
            {
                if (mode == MODE_ABvP || mode == MODE_AvA)
                {
                    papan.gerakAI(turn);
                    turn = !turn;
                    checkGameEnded();
                }
            }
            
        }
        public void checkGameEnded()
        {
            int winner = papan.getWinner();
            if (winner != Catur.SPACE)
            {
                MessageBox.Show("WINNER " + (winner == Catur.WHITE ? "WHITE !" : "BLACK !"));
                fin = true;
            }
            else
            {
                triggerGerakAi();
            }
        }
    }
}
