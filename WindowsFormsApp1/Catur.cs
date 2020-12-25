using System;
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

        public static int WHITE = 1;
        public static int BLACK = 0;
        public static int SPACE = -1;

        public Catur()
        {
            isi = "";
            isi += "rqkr";
            isi += "hb*h";
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

        public List<Point> hint(int x, int y, bool turn )
        {
            //turn true = player 1 white
            //turn false = player 2 black
            List<Point> gerakan = new List<Point>(); //hasil akhir
            List<Point> gerakbidak = new List<Point>(); //untuk gerakan tiap bidak
            int move = 0;
            //hint
            int warna = turn ? WHITE : BLACK; 
            char pion = isi[y * 4 + x];

            if(cekwarna(pion) != warna)
            {

            }
            else if ( pion == 'k' || pion == 'K')
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
                gerakbidak.Add(new Point(0,-1));  //atas
                gerakbidak.Add(new Point(1,-1));  //kanan atas
                gerakbidak.Add(new Point(1, 0));  // kanan
                gerakbidak.Add(new Point(1, 1));  //kanan bawah
                gerakbidak.Add(new Point(0, 1));  //bawah
                gerakbidak.Add(new Point(-1, 1)); //kiri bawah
                gerakbidak.Add(new Point(-1, 0)); //kiri
                gerakbidak.Add(new Point(-1,-1)); //kiri atas
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
                    if (cekposisivalid(nextX, nextY, warna))
                    {
                        gerakan.Add(new Point(nextX, nextY));
                        int w = cekwarna(isi[nextX + nextY * 4]);

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

        public bool cekposisivalid(int x, int y, bool iswhite)
        {
            bool isvalid = true;
            int warna = iswhite ? WHITE : BLACK;
            if (x < 0 || x > 3 || y < 0 || y > 7)
            {
                //pengecekan posisi
                isvalid = false;
            }
            else if (cekwarna(isi[y * 4 + x]) == warna)
            {
                //pengecekan warna
                isvalid = false;
            }
            
            return isvalid;
        }

        public bool cekposisivalid(int x, int y, int warna)
        {
            return cekposisivalid(x,y,warna==WHITE);
        }

        public Point konversi(Point mouse)
        {
            Point point = new Point {
                X = (mouse.X - 10) / 82,
                Y = (mouse.Y - 60) / 82
            };
            return point;
        }

        public void move(Point source, Point destination)
        {
            //someString = someString.Remove(index, 1).Insert(index, "g");
            int idxdes = destination.Y * 4 + destination.X;
            int idxsrc = source.Y * 4 + source.X;
            Isi = isi.Remove(idxdes, 1).Insert(idxdes, isi[idxsrc] + "");
            Isi = isi.Remove(idxsrc, 1).Insert(idxsrc, "*");
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

        public void gerakAI(bool turn)
        {
            List<KeyValuePair<Point,Point>> possiblemove = new List<KeyValuePair<Point, Point>>();

            bool isWhite = turn;
            string bidakwhite = "KQHRB";
            string bidakblack = "kqhrb";

            string bidakaktif = isWhite ? bidakwhite : bidakblack;

            for (int i = 0; i < bidakaktif.Length; i++)
            {
                int idx = isi.IndexOf(bidakaktif[i], 0 );
                
                //while bidak masi ditemukan
                while (idx != -1)
                {
                    //System.Windows.Forms.MessageBox.Show(idx +" " + bidakaktif[i]);
                    Point source = new Point(idx % 4, idx / 4);
                    List<Point> destinations = hint(source.X, source.Y, isWhite);

                    foreach (Point item in destinations)
                    {
                        possiblemove.Add(new KeyValuePair<Point, Point>(source, item));
                    }
                    //possiblemove.Add(new Point(idx % 4, idx / 4));
                    idx = isi.IndexOf(bidakaktif[i], idx + 1);
                }
            }

            //random gerakan
            int ctr = new Random().Next(possiblemove.Count);
            move(possiblemove[ctr].Key, possiblemove[ctr].Value);
        }

        public void draw(Graphics g, List<Point> hint)
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
        }
    }
}
