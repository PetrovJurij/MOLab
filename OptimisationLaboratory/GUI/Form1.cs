using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Core;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        Matrix Matrix1=new Matrix(1,1);
        Matrix Matrix2=new Matrix(1,1);

        public Form1()
        {
            InitializeComponent();

            Matr1.Columns.Add("Column0", "0");
            Matr1.Rows.Add();
            Matr2.Columns.Add("Column0", "0");
            Matr2.Rows.Add();

            FillDataGrids();
        }

        private void FillDataGrids()
        {
            for (int i = 0; i < Matrix1.N; i++)
            {
                for (int j = 0; j < Matrix1.M; j++)
                {
                    Matr1.Rows[i].Cells[j].Value = Matrix1[i][j];
                }
            }

            for (int i = 0; i < Matrix2.N; i++)
            {
                for (int j = 0; j < Matrix2.M; j++)
                {
                    Matr2.Rows[i].Cells[j].Value = Matrix2[i][j];
                }
            }
        }

        private void Matr1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Matrix1[e.RowIndex][e.ColumnIndex] =Convert.ToDouble(Matr1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void Matr2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Matrix2[e.RowIndex][e.ColumnIndex] = Convert.ToDouble(Matr2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void Matr1N_TextChanged(object sender, EventArgs e)
        {
            int N;
            try
            {
                N = Int32.Parse(Matr1N.Text);
            }
            catch (FormatException ex)
            {
                N = 1;
            }


            Matrix temp = (Matrix)Matrix1.Clone();
            Matrix1 = new Matrix(N,Matrix1.M);

            int MinN = Matrix1.N > temp.N ? temp.N : Matrix1.N;

            for (int i=0;i<MinN;i++)
                for(int j=0;j<Matrix1.M;j++)
                {
                    Matrix1[i][j] = temp[i][j];
                }

            while (Matr1.Rows.Count > N)
            {
                Matr1.Rows.RemoveAt(Matr1.Rows.Count - 1);
            }

            while (Matr1.Rows.Count < N)
            {
                Matr1.Rows.Add();
            }

            FillDataGrids();
        }

        private void Matr1M_TextChanged(object sender, EventArgs e)
        {
            int M;
            try
            {
                M = Int32.Parse(Matr1M.Text);
                if (M < 0)
                    throw new FormatException(); 
            }
            catch (FormatException ex)
            {
                M = 1;
            }

            Matrix temp = (Matrix)Matrix1.Clone();
            Matrix1 = new Matrix(Matrix1.N, M);

            int MinM = Matrix1.M > temp.M ? temp.M : Matrix1.M;

            for (int i = 0; i < Matrix1.N; i++)
                for (int j = 0; j < MinM; j++)
                {
                    Matrix1[i][j] = temp[i][j];
                }


            while (Matr1.Columns.Count > M)
            {
                Matr1.Columns.RemoveAt(Matr1.Columns.Count - 1);
            }

            while (Matr1.Columns.Count < M)
            {
                Matr1.Columns.Add("Column"+Matr1.Columns.Count,(Matr1.Columns.Count).ToString());
            }

            FillDataGrids();
        }

        private void Matr2N_TextChanged(object sender, EventArgs e)
        {
            int N;
            try
            {
                N = Int32.Parse(Matr2N.Text);
            }
            catch (FormatException ex)
            {
                N = 1;
            }

            Matrix temp = (Matrix)Matrix2.Clone();
            Matrix2 = new Matrix(N, Matrix2.M);

            int MinN = Matrix2.N > temp.N ? temp.N : Matrix2.N;

            for (int i = 0; i < MinN; i++)
                for (int j = 0; j < Matrix2.M; j++)
                {
                    Matrix2[i][j] = temp[i][j];
                }

            while (Matr2.Rows.Count > N)
            {
                Matr2.Rows.RemoveAt(Matr2.Rows.Count - 1);
            }

            while (Matr2.Rows.Count < N)
            {
                Matr2.Rows.Add();
            }

            FillDataGrids();
        }

        private void Matr2M_TextChanged(object sender, EventArgs e)
        {
            int M;
            try
            {
                M = Int32.Parse(Matr2M.Text);
            }
            catch (FormatException ex)
            {
                M = 1;
            }

            Matrix temp = (Matrix)Matrix2.Clone();
            Matrix2 = new Matrix(Matrix2.N, M);

            int MinM = Matrix2.M > temp.M ? temp.M : Matrix2.M;

            for (int i = 0; i < Matrix2.N; i++)
                for (int j = 0; j < MinM; j++)
                {
                    Matrix2[i][j] = temp[i][j];
                }


            while (Matr2.Columns.Count > M)
            {
                Matr2.Columns.RemoveAt(Matr2.Columns.Count - 1);
            }

            while (Matr2.Columns.Count < M)
            {
                Matr2.Columns.Add("Column" + Matr2.Columns.Count, (Matr2.Columns.Count).ToString());
            }

            FillDataGrids();
        }

        private void MulButton_Click(object sender, EventArgs e)
        {
            AnswerMatr.Rows.Clear();
            AnswerMatr.Columns.Clear();

            Matrix Answ=new Matrix(1,1);

            try
            {
                Answ = Matrix1 * Matrix2;
            }catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            while(AnswerMatr.Columns.Count<Answ.M)
            {
                AnswerMatr.Columns.Add("Column" + AnswerMatr.Columns.Count, (AnswerMatr.Columns.Count).ToString());
            }
            while(AnswerMatr.Rows.Count<Answ.N)
            {
                AnswerMatr.Rows.Add();
            }

            for(int i=0;i<Answ.N;i++)
            {
                for(int j=0;j<Answ.M;j++)
                {
                    AnswerMatr.Rows[i].Cells[j].Value = Answ[i][j];
                }
            }
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            AnswerMatr.Rows.Clear();
            AnswerMatr.Columns.Clear();
            Matrix Answ = new Matrix(1, 1);

            try
            {
                Answ = Matrix1 - Matrix2;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            while (AnswerMatr.Columns.Count < Answ.M)
            {
                AnswerMatr.Columns.Add("Column" + AnswerMatr.Columns.Count, (AnswerMatr.Columns.Count).ToString());
            }
            while (AnswerMatr.Rows.Count < Answ.N)
            {
                AnswerMatr.Rows.Add();
            }

            for (int i = 0; i < Answ.N; i++)
            {
                for (int j = 0; j < Answ.M; j++)
                {
                    AnswerMatr.Rows[i].Cells[j].Value = Answ[i][j];
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AnswerMatr.Rows.Clear();
            AnswerMatr.Columns.Clear();

            Matrix Answ = new Matrix(1, 1);

            try
            {
                Answ = Matrix1 + Matrix2;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            while (AnswerMatr.Columns.Count < Answ.M)
            {
                AnswerMatr.Columns.Add("Column" + AnswerMatr.Columns.Count, (AnswerMatr.Columns.Count).ToString());
            }
            while (AnswerMatr.Rows.Count < Answ.N)
            {
                AnswerMatr.Rows.Add();
            }

            for (int i = 0; i < Answ.N; i++)
            {
                for (int j = 0; j < Answ.M; j++)
                {
                    AnswerMatr.Rows[i].Cells[j].Value = Answ[i][j];
                }
            }
        }

        private void DivButton_Click(object sender, EventArgs e)
        {
            AnswerMatr.Rows.Clear();
            AnswerMatr.Columns.Clear();

            Matrix Answ = new Matrix(1, 1);

            try
            {
                Answ = Matrix1 / Matrix2;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            while (AnswerMatr.Columns.Count < Answ.M)
            {
                AnswerMatr.Columns.Add("Column" + AnswerMatr.Columns.Count, (AnswerMatr.Columns.Count).ToString());
            }
            while (AnswerMatr.Rows.Count < Answ.N)
            {
                AnswerMatr.Rows.Add();
            }

            for (int i = 0; i < Answ.N; i++)
            {
                for (int j = 0; j < Answ.M; j++)
                {
                    AnswerMatr.Rows[i].Cells[j].Value = Answ[i][j];
                }
            }
        }

        
    }
}
