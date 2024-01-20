using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AnimalsManageApp
{
    /// <summary>
    /// MainForm class.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        /// <summary>
        /// The list
        /// </summary>
        private readonly BindingList<AnimalRecord> list = new BindingList<AnimalRecord>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            bindingSource.DataSource = list;
        }

        /// <summary>
        /// Handles the Click event of the BtnLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Multiselect = false, Filter = "CSV file.|*.csv" })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                list.Clear();
                IEnumerable<AnimalRecord> records = File.ReadAllLines(ofd.FileName).Select(AnimalRecord.FromText);
                foreach (AnimalRecord record in records)
                {
                    list.Add(record);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV file.|*.csv" })
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                File.WriteAllLines(sfd.FileName, list.Select(r => r.ToString()));
            }

        }

        /// <summary>
        /// Handles the Click event of the BtnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTag.Text) || string.IsNullOrWhiteSpace(tbUrl.Text))
            {
                return;
            }

            list.Add(new AnimalRecord(tbTag.Text, tbUrl.Text));
        }

        /// <summary>
        /// Handles the Click event of the BtnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            list.RemoveAt(dataGridView.SelectedRows[0].Index);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the DataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                return;
            }


            string url = list[dataGridView.SelectedRows[0].Index].Url;
            webBrowser.Navigate(url);
        }
    }
}
