using ImportKokorin.BD;
using ImportKokorin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImportKokorin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Методы импорта
            GenderImport();
            UserImport();
            ImportPhoto();
            ImportProduct();
            ImportStatus();
            ImportOrder();
        }

        private void GenderImport()
        {
            string fileUser = @"C:\Users\Student\Desktop\import_test\importUser.csv";
            string[] rows = File.ReadAllLines(fileUser).Skip(1).ToArray();
            var spl = new char[] { ';' };
            foreach(var row in rows)
            {
                var columns = row.Split(spl);
                if(columns.Length == 5)
                {
                    var g = ImportKokorinContext.GetInstance().Genders.FirstOrDefault(s => s.Name == columns[3]);
                    if(g == null)
                    {
                        var gender = new Gender();
                        gender.Name = columns[3];
                        ImportKokorinContext.GetInstance().Genders.Add(gender);
                        ImportKokorinContext.GetInstance().SaveChanges();
                    }
                }
            }
        }

        private void UserImport()
        {
            string file = @"C:\Users\Student\Desktop\import_test\importUser.csv";
            string[] rows = File.ReadAllLines(file).Skip(1).ToArray();
            var spl = new char[] { ';' };
            foreach(var row in rows)
            {
                var column = row.Split(spl);
                var u = ImportKokorinContext.GetInstance().Users.FirstOrDefault(s => s.FirstName == column[0] && s.LastName == column[1]);
                if(u == null)
                {
                    var user = new User();
                    user.FirstName = column[0];
                    user.LastName = column[1];
                    user.Email = column[2];
                    if (column[3] == "Female")
                    {
                        column[3] = "1";
                    }
                    else
                    {
                        column[3] = "2";
                    }
                    user.GenderId = Convert.ToInt32(column[3]);
                    user.IpAdress = column[4];
                    ImportKokorinContext.GetInstance().Users.Add(user);
                    ImportKokorinContext.GetInstance().SaveChanges();
                }
            }
        }

        private void ImportPhoto()
        {
            string dirImage = @"C:\Users\Student\Desktop\import_test\Product_image\";
            foreach(var file in Directory.EnumerateFiles(dirImage))
            {
                var photo = new Photo();
                photo.Name = file.Split("\\")[6];
                photo.FileName = File.ReadAllBytes(file);
                ImportKokorinContext.GetInstance().Photos.Add(photo);
                ImportKokorinContext.GetInstance().SaveChanges();
            }
        }

        private void ImportProduct()
        {
            string file = @"C:\Users\Student\Desktop\import_test\importProduct.csv";
            string[] rows = File.ReadAllLines(file).Skip(1).ToArray();
            var spl = new char[] { ';' };
            foreach(var row in rows)
            {
                var column = row.Split(spl);
                var p = ImportKokorinContext.GetInstance().Products.FirstOrDefault(s => s.Name == column[0]);
                if(p == null)
                {
                    var product = new Product();
                    product.Name = column[0];
                    product.Count = Convert.ToInt32(column[1]);
                    var photo = ImportKokorinContext.GetInstance().Photos.FirstOrDefault(s => s.Name == column[2]);
                    product.PhotoId = photo.Id;
                    ImportKokorinContext.GetInstance().Products.Add(product);
                    ImportKokorinContext.GetInstance().SaveChanges();
                }
            }
        }

        private void ImportStatus()
        {
            string file = @"C:\Users\Student\Desktop\import_test\importOrder.csv";
            string[] rows = File.ReadAllLines(file).Skip(1).ToArray();
            var spl = new char[] { ';' };
            foreach(var row in rows)
            {
                var column = row.Split(spl);
                var o = ImportKokorinContext.GetInstance().Statuses.FirstOrDefault(s => s.Name == column[3]);
                if(o == null)
                {
                    var status = new Status();
                    status.Name = column[3];
                    ImportKokorinContext.GetInstance().Statuses.Add(status);
                    ImportKokorinContext.GetInstance().SaveChanges();
                }
            }
        }

        private void ImportOrder()
        {
            string file = @"C:\Users\Student\Desktop\import_test\importOrder.csv";
            string[] rows = File.ReadAllLines(file).Skip(1).ToArray();
            var spl = new char[] { ';' };
            foreach(var row in rows)
            {
                var column = row.Split(spl);
                if(column.Length == 4)
                {
                    var order = new Order();
                    order.UserId = ImportKokorinContext.GetInstance().Users.FirstOrDefault(s => s.Email == column[0]).Id;
                    order.Date = Convert.ToDateTime(column[2]);
                    order.StatusId = ImportKokorinContext.GetInstance().Statuses.FirstOrDefault(s => s.Name == column[3]).Id;
                    ImportKokorinContext.GetInstance().Orders.Add(order);
                    ImportKokorinContext.GetInstance().SaveChanges();
                }
            }
        }

        private void ImportOrderProduct()
        {
            //string fileP = @;
        }
    }
}
