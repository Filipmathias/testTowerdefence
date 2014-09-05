using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
namespace WaveEditor
{
    public partial class Form1 : Form
    {

        public class Wave
        {

            public List<EnemyInfo> Enemies { get; set; }
            public Wave() { Enemies = new List<EnemyInfo>(); }
        }

        public class EnemyInfo
        {

            public string Type { get; set; }
            public float level { get; set; }

        }

        List<Wave> waves = new List<Wave>();
        public Form1()
        {
            InitializeComponent();
            AddComboBox();
            waves.Add(new Wave());
            listBox1.Items.Add("wave1");

        }
        void AddComboBox() 
        {
            comboBox1.Items.Add("Normal");
        }
        
        void LoadEnemyList() 
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox2.Items.Clear();
                listBox2.Items.AddRange(waves[listBox1.SelectedIndex].Enemies.ToArray());
            }
        }

        void LoadWaveList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < waves.Count; i++)
            {
                listBox1.Items.Add("wave" + i.ToString());
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileDiag = new OpenFileDialog {CheckFileExists = true, FileName = Application.ExecutablePath + "\\maps"};
            if (fileDiag.ShowDialog() == DialogResult.OK) 
            {
                textBox1.Text = fileDiag.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            waves.Add(new Wave());
            LoadWaveList();
        }

        private void button4_Click(object sender, EventArgs e)
        {


            string data = JsonConvert.SerializeObject(new 
            {
                Map = textBox1.Text,
                Waves = waves.ToArray()
            });

            var saveDiag = new SaveFileDialog {DefaultExt = ".jsn"};
            if (saveDiag.ShowDialog() == DialogResult.OK) 
            {
                File.WriteAllText(saveDiag.FileName, data);    
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1) 
            { 
            var enemy = new EnemyInfo();
            enemy.level = (int)numericUpDown1.Value;
            enemy.Type = comboBox1.Text;

            waves[listBox1.SelectedIndex].Enemies.Add(enemy);
            LoadEnemyList();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnemyList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }
}
