using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = null;
        private string strCon = @"Persist Security Info=False;User ID=sa;Password=1234567; Initial Catalog=Luan;Data Source=(local)";
        private string strSql = string.Empty;


       private void tsbSalvar_Click(object sender, EventArgs e)
        {
            strSql = "insert into Funcionarios (Id, Nome, Endereco, CEP, Bairro, Cidade, UF, Telefone) values(@Id, @Nome, @Endereco, @CEP, @Bairro, @Cidade, @UF, @Telefone)";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCep.Text;
            comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUf.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTel.Text;



            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                sqlCon.Close();           
            }
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true;
            txtNome.Enabled = false;
            txtId.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            mskCep.Enabled = false;
            mskTel.Enabled = false;
            txtUf.Enabled = false;
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtBairro.Text = "";
            txtId.Text = "";
            txtUf.Text = "";
            mskCep.Text = "";
            mskTel.Text = "";
        }

        private void tsbPesquisar_Click(object sender, EventArgs e)
        {
            strSql = "select * from Funcionarios where Id = @Id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);


            comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;


            try
            {
                if (tstIdBuscar.Text == string.Empty)
                {
                    throw new Exception("Voce precisa digitar um Id!!");
                }
                sqlCon.Open();

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    throw new Exception("ID nao cadastrado");
                }
                dr.Read();
                txtId.Text = Convert.ToString(dr["Id"]);
                txtNome.Text = Convert.ToString(dr["Nome"]);
                txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                mskCep.Text = Convert.ToString(dr["CEP"]);
                txtBairro.Text = Convert.ToString(dr["Bairro"]);
                txtCidade.Text = Convert.ToString(dr["Cidade"]);
                txtUf.Text = Convert.ToString(dr["UF"]);
                mskTel.Text = Convert.ToString(dr["Telefone"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = true;
            tsbCancelar.Enabled = true; 
            tsbExcluir.Enabled = true;
            tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true;
            txtNome.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            mskCep.Enabled = true;
            mskTel.Enabled = true;
            txtUf.Enabled = true;
            tsbPesquisar.Text = "";
            txtNome.Focus();
            ;        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            strSql = "update Funcionarios set Id=@id, Nome=@Nome, Endereco=@Endereco, CEP=@CEP, Bairro=@Bairro, Cidade=@Cidade, UF=@UF, Telefone=@Telefone where id = @idBuscar";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);


            comando.Parameters.Add("@IdBuscar", SqlDbType.Int).Value = tstIdBuscar.Text;

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCep.Text;
            comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUf.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTel.Text;

            try
            {

                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro atualizado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                sqlCon.Close();
            }
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este funcionario?" , "Cuidado", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)== DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!");            
            }
            else {
                strSql = "delete from Funcionarios where Id=@Id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);


                comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;


                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionario deletado com sucesso!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally {
                    sqlCon.Close();
                }
                tsbNovo.Enabled = true;
                tsbSalvar.Enabled = false;
                tsbAlterar.Enabled = false;
                tsbCancelar.Enabled = false;
                tsbExcluir.Enabled = false;
                tstIdBuscar.Enabled = true;
                tsbPesquisar.Enabled = true;
                txtNome.Enabled = false;
                txtId.Enabled = false;
                txtEndereco.Enabled = false;
                txtCidade.Enabled = false;
                txtBairro.Enabled = false;
                mskCep.Enabled = false;
                mskTel.Enabled = false;
                txtUf.Enabled = false;
                txtNome.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtId.Text = "";
                txtUf.Text = "";
                mskCep.Text = "";
                mskTel.Text = "";
            }

        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = true;
            tstIdBuscar.Enabled = false;
            txtNome.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            mskCep.Enabled = true;
            mskTel.Enabled = true;
            txtUf.Enabled = true;

        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true;
            txtNome.Enabled = false;
            txtId.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            mskCep.Enabled = false;
            mskTel.Enabled = false;
            txtUf.Enabled = false;
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtBairro.Text = "";
            txtId.Text = "";
            txtUf.Text = "";
            mskCep.Text = "";
            mskTel.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'luanDataSet.Funcionarios' table. You can move, or remove it, as needed.
            this.funcionariosTableAdapter.Fill(this.luanDataSet.Funcionarios);
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true;
            txtNome.Enabled = false;
            txtId.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            mskCep.Enabled = false;
            mskTel.Enabled = false;
            txtUf.Enabled = false;
        }

    }
}
