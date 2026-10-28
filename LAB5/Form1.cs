using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.CellDoubleClick +=
                dataGridView1_CellDoubleClick;

            LoadProducts();
        }

        // Termékek betöltése háttérszálon
        private async void LoadProducts(string searchText = "")
        {
            try
            {
                var products = await Task.Run(() =>
                {
                    using var db = new ProductDbContext();

                    db.Database.Migrate();

                    var query = db.Products.AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        query = query.Where(p =>
                            p.Name.Contains(searchText) ||
                            p.Description.Contains(searchText));
                    }

                    return query.ToList();
                });

                // Az eredmény átadása a GUI-szálnak Invoke segítségével
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() =>
                    {
                        dataGridView1.DataSource = products;
                    }));
                }
                else
                {
                    dataGridView1.DataSource = products;
                }
            }
            catch (Exception ex)
            {
                if (!IsDisposed && IsHandleCreated)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show(
                                "Error loading products:\n" + ex.Message);
                        }));
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error loading products:\n" + ex.Message);
                    }
                }
            }
        }

        // Add Product
        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new AddProductForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts(textBox1.Text.Trim());
                }
            }
        }

        // Search
        private void button2_Click(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text.Trim());
        }

        // Clear search
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadProducts();
        }

        // Double-click a product to sell it
        private void dataGridView1_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem
                is not Product selectedProduct)
                return;

            using (var form = new SellProductForm(
                selectedProduct.Id,
                selectedProduct.Name,
                selectedProduct.Quantity))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts(textBox1.Text.Trim());
                }
            }
        }

        private void dataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // New Product menu
        private void newProductToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            using (var form = new AddProductForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts(textBox1.Text.Trim());
                }
            }
        }

        // Increase Existing Stock menu
        private void increaseExistingStockToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            using (var form = new IncreaseStockForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts(textBox1.Text.Trim());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text.Trim());
        }
    }
}