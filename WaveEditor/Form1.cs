using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TowerDefence;
using Newtonsoft.Json;
using System.IO;
namespace WaveEditor
{
    


    public partial class Form1 : Form
    {

        List<Wave> waves = new List<Wave>();



        public Form1()
        {
            InitializeComponent();
            addComboBox();
            waves.Add(new Wave());
            listBox1.Items.Add("wave1");

        }
        void addComboBox() 
        {
            comboBox1.Items.AddRange(WaveHandler.EnemyConverters().Keys.ToArray<string>());
        }
        
        void loadEnemyList() 
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
            var fileDiag = new OpenFileDialog();
            fileDiag.CheckFileExists = true;
            fileDiag.FileName =  Application.ExecutablePath+"\\maps";
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

            SaveFileDialog saveDiag = new SaveFileDialog();
            saveDiag.DefaultExt = ".jsn";
            if (saveDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
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
            loadEnemyList();
        }
        }

        int selectedWave = 0;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            loadEnemyList();
            
            
  

        }




    }
}
