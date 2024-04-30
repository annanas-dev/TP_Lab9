using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace TP_Lab9
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            tabPage4.Name = "1";
            tabPage4.Text = "1";

            // Добавление элементов управления на форму
            CheckBox gridCheckBox = new CheckBox();
            gridCheckBox.Text = "Отображать сетку";
            gridCheckBox.Checked = true; // По умолчанию отображаем сетку
            gridCheckBox.Location = new Point(20, 20);
            gridCheckBox.CheckedChanged += checkBox1_CheckedChanged;
            this.Controls.Add(gridCheckBox);

            NumericUpDown lineThicknessNumericUpDown = new NumericUpDown();
            lineThicknessNumericUpDown.Minimum = 1;
            lineThicknessNumericUpDown.Maximum = 5;
            lineThicknessNumericUpDown.Value = 1; // Толщина линии по умолчанию
            lineThicknessNumericUpDown.Location = new Point(20, 50);
            lineThicknessNumericUpDown.ValueChanged += numericUpDown1_ValueChanged;
            this.Controls.Add(lineThicknessNumericUpDown);

            CheckBox legendCheckBox = new CheckBox();
            legendCheckBox.Text = "Отображать легенду";
            legendCheckBox.Checked = true; // По умолчанию отображаем легенду
            legendCheckBox.Location = new Point(20, 80);
            legendCheckBox.CheckedChanged += checkBox2_CheckedChanged;
            this.Controls.Add(legendCheckBox);

            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart3.Series)
            {
                series.Points.Clear();
            }

            this.chart1.Titles.Clear();
            this.chart2.Titles.Clear();
            this.chart3.Titles.Clear();

            this.chart1.Titles.Add("Вариант 43");
            this.chart2.Titles.Add("Вариант 43");
            this.chart3.Titles.Add("Вариант 43");

            chart1.ChartAreas[0].AxisX.Title = "X";
            chart1.ChartAreas[0].AxisY.Title = "Y";
            chart2.ChartAreas[0].AxisX.Title = "X";
            chart2.ChartAreas[0].AxisY.Title = "Y";
            chart3.ChartAreas[0].AxisX.Title = "X";
            chart3.ChartAreas[0].AxisY.Title = "Y";

            foreach (TabPage page in tabControl2.TabPages)
            {
                foreach (DataGridView data in page.Controls)
                {
                    int k = Convert.ToInt32(page.Text) - 1;
                    for (int i = 0; i < data.Rows.Count - 1; i++)
                    {
                        int x = Convert.ToInt32(data.Rows[i].Cells[0].Value); 
                        int y = Convert.ToInt32(data.Rows[i].Cells[1].Value);
                        chart1.Series[k].Points.AddXY(x, y);
                    }
                    for (int i = 0; i < data.Rows.Count - 1; i++)
                    {
                        int x = Convert.ToInt32(data.Rows[i].Cells[0].Value);
                        int y = Convert.ToInt32(data.Rows[i].Cells[1].Value);
                        chart2.Series[k].Points.AddXY(x, y);
                    }
                    for (int i = 0; i < data.Rows.Count - 1; i++)
                    {
                        int x = Convert.ToInt32(data.Rows[i].Cells[0].Value);
                        int y = Convert.ToInt32(data.Rows[i].Cells[1].Value);
                        chart3.Series[k].Points.AddXY(x, y);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = tabControl2.TabCount;
            chart1.Series.Add(Convert.ToString(count));
            chart2.Series.Add(Convert.ToString(count));
            chart3.Series.Add(Convert.ToString(count));
            chart1.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; 
            chart3.Series[count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            TabPage page = new TabPage((tabControl2.TabCount + 1).ToString());
            tabControl2.TabPages.Add(page);
            DataGridView dgv = new DataGridView();
            dgv.ColumnCount = 2;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns[0].HeaderText = "X";
            dgv.Columns[1].HeaderText = "Y";
            dgv.Dock = DockStyle.Fill;
            page.Controls.Add(dgv);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            foreach (Chart chart in new Chart[] { chart1, chart2, chart3 })
            {
                chart.ChartAreas[0].AxisX.MajorGrid.Enabled = checkBox.Checked;
                chart.ChartAreas[0].AxisY.MajorGrid.Enabled = checkBox.Checked;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            foreach (Chart chart in new Chart[] { chart1, chart2, chart3 })
            {
                foreach (var series in chart.Series)
                {
                    series.BorderWidth = (int)numericUpDown.Value;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            foreach (Chart chart in new Chart[] { chart1, chart2, chart3 })
            {
                chart.Legends[0].Enabled = checkBox.Checked;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;
            string selectedPosition = comboBox.SelectedItem.ToString();

            foreach (Chart chart in new Chart[] { chart1, chart2, chart3 })
            {
                //chart.Legends[0].DockedToChartArea = "ChartArea1"; // Пример установки области для легенды
                //chart.Legends[0].IsDockedInsideChartArea = true;

                // Устанавливаем положение легенды
                switch (selectedPosition)
                {
                    case "Top":
                        chart.Legends[0].Docking = Docking.Top;
                        break;
                    case "Bottom":
                        chart.Legends[0].Docking = Docking.Bottom;
                        break;
                    case "Left":
                        chart.Legends[0].Docking = Docking.Left;
                        break;
                    case "Right":
                        chart.Legends[0].Docking = Docking.Right;
                        break;
                    default:
                        // По умолчанию устанавливаем положение сверху
                        chart.Legends[0].Docking = Docking.Top;
                        break;
                }
            }
        }
    }
}
