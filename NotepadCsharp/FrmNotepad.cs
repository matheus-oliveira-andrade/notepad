using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadCsharp
{
    public partial class FrmNotepad : Form
    {
        private bool _saved;
        private bool fileCreate;
        private bool _fileCreate;
        private string _pathFile = string.Empty;

        public FrmNotepad()
        {
            InitializeComponent();
        }

        private void richTxtBText_TextChanged(object sender, EventArgs e)
        {
            statusStrip1.Items["toolStripStatusLabel1"].Text = "Não salvo";
            statusStrip1.Items["toolStripSttLblQntdCaracteres"].Text = richTxtBText.Text.Length.ToString();
        }

        private void FrmNotepad_Load(object sender, EventArgs e)
        {
            _fileCreate = false;
            richTxtBText.Text = string.Empty;
            statusStrip1.Items["toolStripStatusLabel1"].Text = "Não salvo";
            statusStrip1.Items["toolStripSttLblQntdCaracteres"].Text = richTxtBText.Text.Length.ToString();
        }

        private void richTxtBText_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTxtBText.Text != "")
            {
                if (!_saved || !_fileCreate)
                {
                    MessageSaveChanges();
                }

                _saved = false;
                _fileCreate = false;
                _pathFile = string.Empty;

                richTxtBText.Clear();
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                try
                {
                    ofd.Filter = "All files (*.*)|*.*";
                    ofd.Title = "Abrir arquivo existente";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog().Equals(DialogResult.OK))
                    {
                        _saved = true;
                        fileCreate = true;
                        _pathFile = ofd.FileName;

                        richTxtBText.Text = FileManipulator.Open(_pathFile);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!fileCreate)
            {
                SalvarComo();
            }
            else
            {
                FileManipulator.Save(_pathFile, richTxtBText.Text);
                _saved = true;
                statusStrip1.Items["toolStripStatusLabel1"].Text = "Salvo";

            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalvarComo();
        }

        private void SalvarComo()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (sfd.ShowDialog().Equals(DialogResult.OK))
                {
                    FileManipulator.Save(sfd.FileName, richTxtBText.Text);

                    _pathFile = sfd.FileName;
                    richTxtBText.Text = string.Empty;
                    richTxtBText.Text = FileManipulator.Open(_pathFile);

                    _fileCreate = true;
                    _saved = true;
                    statusStrip1.Items["toolStripStatusLabel1"].Text = "Salvo";

                }
            }
        }
        private void MessageSaveChanges()
        {
            if (MessageBox.Show("Existem mudanças não salvas ainda deseja salvar?", "Salvar alterações",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
            {
                if (_pathFile.Equals(string.Empty))
                {
                    SalvarComo();
                }
                else
                {
                    FileManipulator.Save(_pathFile, richTxtBText.Text);
                    _saved = true;
                    fileCreate = true;
                    statusStrip1.Items["toolStripStatusLabel1"].Text = "Salvo";

                }
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                if (pd.ShowDialog().Equals(DialogResult.OK))
                {
                    MessageBox.Show("Arquivo");
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_saved || !fileCreate)
            {
                MessageSaveChanges();
            }

            Application.Exit();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.Copy();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.Paste();
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.Cut();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.SelectedText = string.Empty;
        }

        private void selecionarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.SelectAll();
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                // Atribuindo a fonte atual ao componente
                fd.Font = richTxtBText.Font;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    richTxtBText.Font = fd.Font;
                }
            }

        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.SelectedText = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        private void horaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.SelectedText = DateTime.Now.ToString("HH:mm:ss");
        }

        private void dataEHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTxtBText.SelectedText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ");
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Criado por: Matheus de Oliveira de Andrade", "Sobre");
        }
    }
}
