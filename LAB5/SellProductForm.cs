using System;
using System.Windows.Forms;

namespace LAB5
{
    public partial class SellProductForm : Form
    {
        private readonly int productId;
        private readonly string productName;
        private readonly int availableQuantity;

        public SellProductForm(int id, string name, int quantity)
        {
            InitializeComponent();

            productId = id;
            productName = name;
            availableQuantity = quantity;

            lblProductName.Text = productName;
            lblAvailableQuantity.Text = availableQuantity.ToString();

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = availableQuantity;
            numericUpDown1.Value = 1;
        }

        private void SellProductForm_Load(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        // Sell gomb
        private void button1_Click(object sender, EventArgs e)
        {
            int quantityToSell = (int)numericUpDown1.Value;

            try
            {
                using (var db = new ProductDbContext())
                {
                    var product = db.Products.Find(productId);

                    if (product == null)
                    {
                        MessageBox.Show("Product not found.");
                        return;
                    }

                    if (quantityToSell > product.Quantity)
                    {
                        MessageBox.Show("Not enough products in stock.");
                        return;
                    }

                    product.Quantity -= quantityToSell;

                    if (product.Quantity == 0)
                    {
                        db.Products.Remove(product);
                    }

                    db.SaveChanges();
                }

                MessageBox.Show("Sale completed successfully!");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error during sale:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Cancel gomb
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}