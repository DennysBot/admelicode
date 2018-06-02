using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli
{
    public partial class Form1 : Form
    {


        private Bunifu.Framework.UI.BunifuMetroTextbox txtprueba;
        public Form1()
        {
            InitializeComponent();

            // 
            // bunifuMetroTextbox1
            // 
          



        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //textBox1.Focus();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem dd = sender as ToolStripMenuItem;

            ContextMenuStrip  contextMenuStrip  =(ContextMenuStrip)   dd.Owner;
            Control control = contextMenuStrip.SourceControl;
            this.Controls.Remove(control);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = true;
            fontDialog.ShowEffects = true;
            fontDialog.ShowHelp = true;
            fontDialog.MinSize = 7;
            fontDialog.MaxSize = 40;


            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ToolStripMenuItem dd = sender as ToolStripMenuItem;

                ContextMenuStrip contextMenuStrip = (ContextMenuStrip)dd.Owner;
                Control control = contextMenuStrip.SourceControl;
                control.Font = fontDialog.Font;
                control.ForeColor = fontDialog.Color;

            }


            Color myColor = Color.FromArgb(255, 181, 178);

            string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = dataGridView1.Columns;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            
            

            //e.Location
            propertyGrid1.SelectedObject = dataGridView1;
        }

        private void dataGridView1_AllowUserToOrderColumnsChanged(object sender, EventArgs e)
        {
            int j = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView1.CurrentCell.ColumnIndex; 
         }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            int j = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView1.CurrentCell.ColumnIndex;
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_Move(object sender, EventArgs e)
        {
            int inf = 0;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            int j = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView1.CurrentCell.ColumnIndex;
        }

        private void dataGridView1_DragLeave(object sender, EventArgs e)
        {
            int j = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView1.CurrentCell.ColumnIndex;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            int j = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView1.CurrentCell.ColumnIndex;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
