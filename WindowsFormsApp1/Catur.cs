﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Catur
    {
        String isi;
        Random random = new Random();

        public static int WHITE = 1;
        public static int BLACK = 0;
        public static int SPACE = -1;

        public Catur()
        {
            isi = "";
            isi += "rqkr";
            isi += "hbbh";
            isi += "****";
            isi += "****";
            isi += "****";
            isi += "****";
            isi += "HBBH";
            isi += "RQKR";
        }

        public Catur(string isi)
        {
            Isi = isi;
        }

        public string Isi { get => isi; set => isi = value; }

        public List<Point> hint(int x, int y, bool turn) => hint(x, y, turn, isi);

        public List<Point> hint(int x,int y, bool turn, string papan)
        {
            //turn true = player 1 white
            //turn false = player 2 black
            List<Point> gerakan = new List<Point>(); //hasil akhir
            List<Point> gerakbidak = new List<Point>(); //untuk gerakan tiap bidak
            int move = 0;
            //hint
            int warna = turn ? WHITE : BLACK;
            char pion = papan[y * 4 + x];

            if (cekwarna(pion) != warna)
            {

            }
            else if (pion == 'k' || pion == 'K')
            {
                gerakbidak.Add(new Point(0, -1));  //atas
                gerakbidak.Add(new Point(1, -1));  //kanan atas
                gerakbidak.Add(new Point(1, 0));  // kanan
                gerakbidak.Add(new Point(1, 1));  //kanan bawah
                gerakbidak.Add(new Point(0, 1));  //bawah
                gerakbidak.Add(new Point(-1, 1)); //kiri bawah
                gerakbidak.Add(new Point(-1, 0)); //kiri
                gerakbidak.Add(new Point(-1, -1)); //kiri atas
                move = 1;
            }
            else if (pion == 'q' || pion == 'Q')
            {
                //arah pergerakan queen
                gerakbidak.Add(new Point(0, -1));  //atas
                gerakbidak.Add(new Point(1, -1));  //kanan atas
                gerakbidak.Add(new Point(1, 0));  // kanan
                gerakbidak.Add(new Point(1, 1));  //kanan bawah
                gerakbidak.Add(new Point(0, 1));  //bawah
                gerakbidak.Add(new Point(-1, 1)); //kiri bawah
                gerakbidak.Add(new Point(-1, 0)); //kiri
                gerakbidak.Add(new Point(-1, -1)); //kiri atas
                move = 7;
            }
            else if (pion == 'b' || pion == 'B')
            {
                gerakbidak.Add(new Point(-1, -1));  //atas
                gerakbidak.Add(new Point(1, -1));  //kanan atas
                gerakbidak.Add(new Point(-1, 1));  // kanan
                gerakbidak.Add(new Point(1, 1));  //kanan bawah
                move = 3;

            }
            else if (pion == 'h' || pion == 'H')
            {
                gerakbidak.Add(new Point(-1, -2));  //kiri deket naik
                gerakbidak.Add(new Point(-2, -1));  //kiri jauh naik
                gerakbidak.Add(new Point(1, -2));   //kanan deket naik
                gerakbidak.Add(new Point(2, -1));  //kanan jauh naik
                gerakbidak.Add(new Point(-1, 2));  //kiri deket turun
                gerakbidak.Add(new Point(-2, 1));  //kiri jauh turun
                gerakbidak.Add(new Point(1, 2)); //kanan deket turun
                gerakbidak.Add(new Point(2, 1)); //kanan jauh turun
                move = 1;
            }
            else if (pion == 'r' || pion == 'R')
            {
                gerakbidak.Add(new Point(0, -1)); //atas
                gerakbidak.Add(new Point(0, 1));  //bawah
                gerakbidak.Add(new Point(1, 0));  //kanan
                gerakbidak.Add(new Point(-1, 0)); //kiri
                move = 7;

            }

            // tambah ke hint
            for (int i = 0; i < gerakbidak.Count; i++)
            {
                Point gerak = gerakbidak[i];
                for (int j = 1; j <= move; j++)
                {
                    //arah kiri 
                    //jika sebelumnya false maka tidak perlu cek posisi berikutnya
                    int nextX = x + gerak.X * j;
                    int nextY = y + gerak.Y * j;
                    if (cekposisivalid(nextX, nextY, warna, papan))
                    {
                        gerakan.Add(new Point(nextX, nextY));
                        int w = cekwarna(papan[nextX + nextY * 4]);

                        //masi bisa gerak ketika posisi selanjutnya spasi
                        if (w != SPACE)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return gerakan;
        }

        public int cekwarna (char c)
        {
            int cek;
            if ("KQHRB".Contains(c))
            {
                //white
                cek = WHITE;
            }
            else if ("kqhrb".Contains(c))
            {
                //black
                cek = BLACK;
            }
            else
            {
                //spasi
                cek = SPACE;
            }
            return cek;
        }

        public bool cekposisivalid(int x, int y, int warna) => cekposisivalid(x, y, warna == WHITE);
        public bool cekposisivalid(int x, int y, bool iswhite) => cekposisivalid(x, y, iswhite, isi);
        public bool cekposisivalid(int x, int y, int warna, string papan) => cekposisivalid(x, y, warna == WHITE, papan);
        public bool cekposisivalid(int x, int y, bool iswhite, string papan) {
            bool isvalid = true;
            int warna = iswhite ? WHITE : BLACK;
            if (x < 0 || x > 3 || y < 0 || y > 7) {
                //pengecekan posisi
                isvalid = false;
            } else if (cekwarna(papan[y * 4 + x]) == warna) {
                //pengecekan warna
                isvalid = false;
            }

            return isvalid;
        }

        public Point konversi(Point mouse)
        {
            Point point = new Point {
                X = (mouse.X - 10) / 82,
                Y = (mouse.Y - 60) / 82
            };
            return point;
        }

        public void move(Point source, Point destination) => isi = getNewPapan(source, destination);

        public string getNewPapan(Point source, Point destination) => getNewPapan(isi, source, destination);

        public string getNewPapan(string papan,Point source,Point destination)
        {
            int idxdes = destination.Y * 4 + destination.X;
            int idxsrc = source.Y * 4 + source.X;
            papan = papan.Remove(idxdes, 1).Insert(idxdes, papan[idxsrc] + "");
            papan = papan.Remove(idxsrc, 1).Insert(idxsrc, "*");
            return papan;
        }

        public int getWinner()
        {
            if (!isi.Contains('k'))
            {
                return WHITE;
            }
            else if(!isi.Contains('K'))
            {
                return BLACK;
            }
            return SPACE;
        }

        public List<KeyValuePair<Point, Point>> getAllPossibleMoves(bool isWhite) => getAllPossibleMoves(isWhite, isi);

        public List<KeyValuePair<Point, Point>> getAllPossibleMoves(bool isWhite,string papan)
        {
            List<KeyValuePair<Point, Point>> possiblemove = new List<KeyValuePair<Point, Point>>();

            string bidakwhite = "KQHRB";
            string bidakblack = "kqhrb";

            string bidakaktif = isWhite ? bidakwhite : bidakblack;

            for (int i = 0; i < bidakaktif.Length; i++)
            {
                int idx = papan.IndexOf(bidakaktif[i], 0);

                //while bidak masi ditemukan
                while (idx != -1)
                {
                    //System.Windows.Forms.MessageBox.Show(idx +" " + bidakaktif[i]);
                    Point source = new Point(idx % 4, idx / 4);
                    List<Point> destinations = hint(source.X, source.Y, isWhite, papan);

                    foreach (Point item in destinations)
                    {
                        possiblemove.Add(new KeyValuePair<Point, Point>(source, item));
                    }
                    //possiblemove.Add(new Point(idx % 4, idx / 4));
                    idx = papan.IndexOf(bidakaktif[i], idx + 1);
                }
            }
            return possiblemove;
        }

        public Point gerakAI(bool isWhite)
        {
            Point source = new Point(-1, -1);
            List<KeyValuePair<Point, Point>> possiblemove = getAllPossibleMoves(isWhite);
            if (possiblemove.Count > 0 )
            {
                //get gerakan AI dari minimax
                var keyValueSbe = minimax(isi,isWhite, 4);
                int idx = keyValueSbe.Key;
                move(possiblemove[idx].Key, possiblemove[idx].Value);
                 source = new Point(possiblemove[idx].Key.X, possiblemove[idx].Key.Y);
                return source;
            }
            return source;
        }

        public void draw(Graphics g, List<Point> hint, Point source)
        {
            //dictionary pion 
            // huruf besar = white (gerak pertama)
            // huruf kecil black
            // h : horse
            // b : bishop
            // r : rook
            // q : queen
            // k : king
            Dictionary<char, Image> dictionarypion = new Dictionary<char, Image>();
            //hitam
            dictionarypion.Add('k', Image.FromFile("h1.png"));
            dictionarypion.Add('q', Image.FromFile("h2.png"));
            dictionarypion.Add('b', Image.FromFile("h3.png"));
            dictionarypion.Add('h', Image.FromFile("h4.png"));
            dictionarypion.Add('r', Image.FromFile("h5.png"));

            //putih
            dictionarypion.Add('K', Image.FromFile("p1.png"));
            dictionarypion.Add('Q', Image.FromFile("p2.png"));
            dictionarypion.Add('B', Image.FromFile("p3.png"));
            dictionarypion.Add('H', Image.FromFile("p4.png"));
            dictionarypion.Add('R', Image.FromFile("p5.png"));

            //tampilan map atas 
            g.DrawString("A", new Font("Arial", 20), new SolidBrush(Color.Black), 37, 30);
            g.DrawString("B", new Font("Arial", 20), new SolidBrush(Color.Black), 117, 30);
            g.DrawString("C", new Font("Arial", 20), new SolidBrush(Color.Black), 197, 30);
            g.DrawString("D", new Font("Arial", 20), new SolidBrush(Color.Black), 277, 30);

            //tampilan map kanan 
            for (int i = 1; i <= 8; i++)
            {
                g.DrawString(i + "", new Font("Arial", 20), new SolidBrush(Color.Black), 340, 82 * i);
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Rectangle rect = new Rectangle((j * 82) + 10, (i * 82) + 60, 80, 80);
                    //cetak tile
                    if ((i + j) % 2 == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.SandyBrown), rect);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.White), rect);
                    }

                    //cetak pion
                    char c = isi[i * 4 + j];
                    if (dictionarypion.ContainsKey(c))
                    {
                        g.DrawImage(dictionarypion[c], rect);
                    }
                }
            }

            //gambar hint
            for (int i = 0; i < hint.Count; i++)
            {
                Rectangle rect = new Rectangle((hint[i].X * 82) + 10, (hint[i].Y * 82) + 60, 80, 80);
                g.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.Yellow)), rect);
            }

            //gambar source
            Rectangle rect1 = new Rectangle((source.X * 82) + 10, (source.Y * 82) + 60, 80, 80);
            g.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.Red)), rect1);
        }

        public float sbe(string papan)
        {
            float acc = 0;
            Dictionary<char, int> valuePion = new Dictionary<char, int>();

            //hitam
            valuePion.Add('k', -100000);
            valuePion.Add('q', -50000);
            valuePion.Add('b', -1000);
            valuePion.Add('h', -1000);
            valuePion.Add('r', -3000);

            //putih
            valuePion.Add('K', 100000);
            valuePion.Add('Q', 50000);
            valuePion.Add('B', 1000);
            valuePion.Add('H', 1000);
            valuePion.Add('R', 3000);

            for (int i = 0; i < papan.Length; i++)
            {
                if (valuePion.ContainsKey(papan[i]))
                {
                    acc += valuePion[papan[i]];
                }
            }

            return acc;
        }

        public KeyValuePair<int, float> minimax(string papan, bool activePlayer, int ply)
        {
            
            if (ply == 0 || !papan.Contains('k') || !papan.Contains('K'))
            {
                float temp_sbe = sbe(papan);
                KeyValuePair<int, float> hasil = new KeyValuePair<int, float>(-1, temp_sbe);
                //System.Windows.Forms.MessageBox.Show(cetakPapan(papan) + "\n SBE: " + temp_sbe);
                return hasil;
            }
            else
            {
                float ret_sbe = activePlayer ? float.MinValue : float.MaxValue;
                int idx_gerak = 0;
                List<KeyValuePair<Point, Point>> allPossibleMove = getAllPossibleMoves(activePlayer, papan);
                for (int i = 0; i < allPossibleMove.Count; i++)
                {
                    var move = allPossibleMove[i];
                    Point pSource = move.Key;
                    Point pDest= move.Value;
                    string newPapan = getNewPapan(papan, pSource, pDest);
                    //System.Windows.Forms.MessageBox.Show(cetakPapan(papan) + "\n\n" + cetakPapan(newPapan) + "\n\n" + pSource + " " + pDest + "\n" + ( activePlayer ? "White" : "Black" ) + "\n" + ply);
                    var keyvaluesbe = minimax(newPapan, !activePlayer, ply - 1);
                    bool randomgeraksama = ret_sbe == keyvaluesbe.Value && random.Next(2) == 0;
                    if (activePlayer && (keyvaluesbe.Value > ret_sbe || randomgeraksama))
                    {
                        // Ambil MAX jika dia active player
                        ret_sbe = keyvaluesbe.Value;
                        idx_gerak = i;
                    }
                    else if (!activePlayer && (keyvaluesbe.Value < ret_sbe || randomgeraksama))
                    {
                        // Ambil MIN jika dia !active player
                        ret_sbe = keyvaluesbe.Value;
                        idx_gerak = i;
                    }
                }
                return new KeyValuePair<int, float>(idx_gerak, ret_sbe);
            }
        }

        string cetakPapan(string papan) {
            var temp = "";
            temp += papan.Substring(0, 4) + "\n";
            temp += papan.Substring(4, 4) + "\n";
            temp += papan.Substring(8, 4) + "\n";
            temp += papan.Substring(12, 4) + "\n";
            temp += papan.Substring(16, 4) + "\n";
            temp += papan.Substring(20, 4) + "\n";
            temp += papan.Substring(24, 4) + "\n";
            temp += papan.Substring(28, 4) + "\n";
            return temp;
        }
    }
}
