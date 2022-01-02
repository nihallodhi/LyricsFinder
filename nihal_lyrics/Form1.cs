//using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Xml;

namespace nihal_lyrics
{
    public partial class Form1 : Form
    {
        String song;
        String url;
        int index = 0;
        String lyrics = String.Empty;
        String lyrics1 = String.Empty;
        String[] song1;

        WebRequest myWebRequest;
        WebResponse myWebResponse;
        Stream streamResponse;
        StreamReader sreader;

        String Rstring = String.Empty;
        String Rstring1 = String.Empty;
        String Links = String.Empty;


        char s = '\n';


        String[] links1;
        String lyrics2;



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = String.Empty;
            song = String.Empty;
            song = textBox1.Text; //need to enhance
            song1 = song.Split(' ');
            
            for(int i=0;i<song1.Length;i++)
            {
                if(song.Length==1)
                {
                    url = "http://search.azlyrics.com/search.php?q=" + song;
                }
                else if(i+1==song1.Length)
                {
                    url = "http://search.azlyrics.com/search.php?q=" + song;
                }
                else
                {
                    url = "http://search.azlyrics.com/search.php?q=" + song +"+";
                }

            }
           
            
            myWebRequest = WebRequest.Create(url);
            myWebResponse = myWebRequest.GetResponse();

            streamResponse = myWebResponse.GetResponseStream();

            sreader = new StreamReader(streamResponse);

                while(sreader.Read()!= 0)
            {
                Rstring = sreader.ReadLine();

                if (Rstring.Contains("Song results:"))
                    {
                    break;
                    }
            }

            Rstring1 = sreader.ReadToEnd();
            Links = GetContent(Rstring1);

            links1 = Links.Split(s);

            url = String.Empty;

            getinfo(links1[0]);

            //myWebRequest = WebRequest.Create(links1[0]);
            //myWebResponse = myWebRequest.GetResponse();

            //streamResponse = myWebResponse.GetResponseStream();

            //sreader = new StreamReader(streamResponse);
            //Rstring = String.Empty;

            //while (sreader.Read() != 0)
            //{
            //    Rstring = sreader.ReadLine();

            //    if (Rstring.Contains("Usage of azlyrics.com content"))
            //    {
            //        break;
            //    }
            //}


            //while (!(lyrics = sreader.ReadLine()).Contains("MxM banner"))
            //{

            //    lyrics1 += lyrics;
            //    lyrics1 += System.Environment.NewLine;

            //}
            //lyrics2 = Regex.Replace(lyrics1,@"\<\bbr\b\>", " ");

            //Rstring1 = String.Empty;

            //for (int i=0;i<3;i++)
            //{
            //    richTextBox1.Text += links1[i];
            //    richTextBox1.Text += System.Environment.NewLine;
            //}

            //richTextBox2.Text = lyrics2;
            //for (int i = 0; i < 3; i++)
            //{
            //    richTextBox1.Text += links1[i];
            //    richTextBox1.Text += System.Environment.NewLine;
            //}

            streamResponse.Close();
            sreader.Close();
            myWebResponse.Close();
            index++;
        }


        public void getinfo(String url)
        {
            //richTextBox1.Text = String.Empty;
            richTextBox2.Text = String.Empty;
            lyrics1 = String.Empty;
            lyrics2 = String.Empty;

            myWebRequest = WebRequest.Create(url);
            myWebResponse = myWebRequest.GetResponse();

            streamResponse = myWebResponse.GetResponseStream();

            sreader = new StreamReader(streamResponse);
            Rstring = String.Empty;

            while (sreader.Read() != 0)
            {
                Rstring = sreader.ReadLine();

                if (Rstring.Contains("Usage of azlyrics.com content"))
                {
                    break;
                }
            }


            while (!(lyrics = sreader.ReadLine()).Contains("MxM banner"))
            {

                lyrics1 += lyrics;
                lyrics1 += System.Environment.NewLine;

            }
            lyrics2 = Regex.Replace(lyrics1, @"\<\bbr\b\>", " ");

            richTextBox2.Text = Regex.Replace(lyrics2, @"\<\bdiv\b\>", "");
            Rstring1 = String.Empty;

            //richTextBox2.Text = lyrics2;

        }


        private String GetContent(String Rstring)
        {
            String sString = "";
            HTMLDocument d = new HTMLDocument();
            IHTMLDocument2 doc = (IHTMLDocument2)d;
            doc.write(Rstring);
            String sString1 = "";
            String sString2 = "";
            //sString2 = 

            IHTMLElementCollection L = doc.links;

            foreach (IHTMLElement links in L)
            {
                sString += links.getAttribute("href", 0);
                sString += "\n";

                //if (sString.Contains(song))
                //{
                //    sString1 += sString;
                //    sString1 += System.Environment.NewLine;

                //}
                //sString = String.Empty;
            }
            return sString;
        }

        private void ChangeSong(object sender, EventArgs e)
        {

            if (index == 3)
                MessageBox.Show("Go fuck yourself! :)");
            else
            {                
                getinfo(links1[index]);
                index++;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13)
            {

                button1_Click(sender,e);
            }
        }
    }
}
