
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB5
{
    public partial class AddProductForm : Form
    {
        public AddProductForm()
        {
            InitializeComponent();

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddMonths(1);

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 100000;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void AddProductForm_Load(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }

        // Save gomb
        private async void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string description = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Add meg a termék nevét!");
                return;
            }

            if (name.Length > 30)
            {
                MessageBox.Show("A termék neve legfeljebb 30 karakter lehet!");
                return;
            }

            if (description.Length > 500)
            {
                MessageBox.Show("A leírás legfeljebb 500 karakter lehet!");
                return;
            }

            DateTime entryDate = dateTimePicker1.Value.Date;
            DateTime expirationDate = dateTimePicker2.Value.Date;
            int quantity = (int)numericUpDown1.Value;

            if (expirationDate < entryDate)
            {
                MessageBox.Show(
                    "A lejárati dátum nem lehet korábbi a belépési dátumnál!");
                return;
            }

            button1.Enabled = false;

            try
            {
                await Task.Run(() =>
                {
                    using var db = new ProductDbContext();

                    var product = new Product
                    {
                        Name = name,
                        Description = description,
                        EntryDate = entryDate,
                        ExpirationDate = expirationDate,
                        Quantity = quantity
                    };

                    db.Products.Add(product);
                    db.SaveChanges();

                    // Visszajelzés a GUI-szálon
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("A termék sikeresen elmentve!");
                        DialogResult = DialogResult.OK;
                        Close();
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hiba a mentéskor:\n" + ex.Message,
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    button1.Enabled = true;
            }
        }

        // Cancel gomb
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
