using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema3.Commands;
using System.Windows;
using System.Globalization;
using Tema3.Models;

namespace Tema3.Data
{
    public class Datele
    {
        Sql_Injection sql_Injection = new Sql_Injection();
        private string connectionString = "Host=localhost;Username=Alex;Password=1q2w3e;Database=supermarket";

        public bool CheckAdministratorCredentials(string nume, string parola)
        {
            bool isAdmin = false;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT public.check_administrator_credentials(@p_nume, @p_parola)", conn))
                {
                    cmd.Parameters.AddWithValue("p_nume", nume);
                    cmd.Parameters.AddWithValue("p_parola", parola);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        isAdmin = (bool)result;
                    }
                }
            }
            return isAdmin;
        }

        public bool CheckUserCredentials(string nume, string parola)
        {
            bool isUser = false;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT public.check_user_credentials(@p_nume, @p_parola)", conn))
                {
                    cmd.Parameters.AddWithValue("p_nume", nume);
                    cmd.Parameters.AddWithValue("p_parola", parola);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        isUser = (bool)result;
                    }
                }
            }
            return isUser;
        }

        public int GetUserID(string nume, string parola)
        {
            int userID = -1;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT public.get_user_id(@p_nume, @p_parola)", conn))
                {
                    cmd.Parameters.AddWithValue("p_nume", nume);
                    cmd.Parameters.AddWithValue("p_parola", parola);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        userID = (int)result;
                    }
                }
            }
            return userID;
        }

        public bool VerificaDacaExistaProdusul(string nume)
        {
            bool wordExists = false;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT public.check_produs_existance(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();

                    if(result != null && result != DBNull.Value) { wordExists = (bool)result; }
                }
            }

            return wordExists;
        }
        public void DezactiveazaProdusul(string nume)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Call public.deactivate_product(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();
                }
            }
        }
        public void ActiveazaProdusul(string nume)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Call public.activate_product(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();
                }
            }
        }
        public bool AddUser(string nume ,string parola ,string rol)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true || sql_Injection.hasForbiddenWords(parola) == true)
                return false;
            bool wordExists = false;
            
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Select public.user_exists(@user_name)", conn))
                {
                    cmd.Parameters.AddWithValue("user_name", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { wordExists = (bool)result; }
                    if (wordExists == true)
                    { MessageBox.Show("Acest nume exista deja in baza de date"); return false; }
                }
                    using (var cmd = new NpgsqlCommand("Call public.add_user(@utilizator_id , @user_name , @user_password , @user_type)", conn))
                {
                    cmd.Parameters.AddWithValue("utilizator_id", 1);
                    cmd.Parameters.AddWithValue("user_name", nume);
                    cmd.Parameters.AddWithValue("user_password", parola);
                    cmd.Parameters.AddWithValue("user_type", rol);
                    object result = cmd.ExecuteScalar();
                }
            }
            return true;
        }
        public bool DeleteUser(string nume)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true)
                return false;
            bool wordExists = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Select public.user_exists(@user_name)", conn))
                {
                    cmd.Parameters.AddWithValue("user_name", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { wordExists = (bool)result; }
                    if (wordExists == false)
                    { MessageBox.Show("Nu exista numele in baza de date"); return false; }
                }
                using (var cmd = new NpgsqlCommand("Call public.delete_user(@user_name)", conn))
                {
                    cmd.Parameters.AddWithValue("user_name", nume);
                    object result = cmd.ExecuteScalar();
                }
            }
            return true;
        }
        public bool ModifyUser(string nume,string parola , string tip)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true || sql_Injection.hasForbiddenWords(parola))
                return false;
            bool wordExists = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Select public.user_exists(@user_name)", conn))
                {
                    cmd.Parameters.AddWithValue("user_name", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { wordExists = (bool)result; }
                    if (wordExists == false)
                    { MessageBox.Show("Nu exista numele in baza de date"); return false; }
                }
                using (var cmd = new NpgsqlCommand("Call public.update_user(@user_name , @user_password , @user_type)", conn))
                {
                    cmd.Parameters.AddWithValue("user_name", nume);
                    cmd.Parameters.AddWithValue("user_password", parola);
                    cmd.Parameters.AddWithValue("user_type",tip);
                    object result = cmd.ExecuteScalar();
                }
            }
            return true;
        }
        public List<string> GetAllUserNames()
        {
            List<string> userNames = new List<string>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_all_user_names()", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userNames.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return userNames;
        }
        public List<string> GetAllProducerNames()
        {
            List<string> producerNames = new List<string>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_all_producer_names()", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producerNames.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return producerNames;
        }
        public List<string> GetAllCategoryTypes()
        {
            List<string> CategoryTypes = new List<string>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_all_category_types()", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryTypes.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return CategoryTypes;
        }
        public List<string> GetAllProducts()
        {
            List<string> products = new List<string>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_all_product_names()", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return products;
        }
        public List<string> GetAllProducerProducts(string nume)
        {
            List<string> producerProducts = new List<string>();
            nume = nume.ToLower();
            nume = char.ToUpper(nume[0]) + nume.Substring(1);
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_product_names_by_producer(@pnume_producator)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume_producator", nume);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producerProducts.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return producerProducts;
        }
        public List<string> GetAllStockProducts(string nume)
        {
            List<string> stockProducts = new List<string>();
            int temp;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_stock_quantity_by_product_name(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", nume);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp = reader.GetInt32(0);
                            stockProducts.Add(temp.ToString());
                        }
                    }
                }
            }

            return stockProducts;
        }
        public List<string> GetCategoryTotalPrice(string nume)
        {
            List<string> CategoryPrice = new List<string>();
            int temp;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_total_sales_price_by_category_type(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", nume);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp = reader.GetInt32(0);
                            CategoryPrice.Add(temp.ToString());
                        }
                    }
                }
            }

            return CategoryPrice;
        }

        public List<(int id_stock, int pret_achizitie, int pret_vanzare)> GetStockDetailsByProductName(string productName)
        {
            
            var stockDetails = new List<(int id_stock, int pret_achizitie, int pret_vanzare)>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM get_stock_details_by_product_name(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", productName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idStock = reader.GetInt32(0);      // prima coloană (id_stock)
                            int pretAchizitie = reader.GetInt32(1); // a doua coloană (pret_achizitie)
                            int pretVanzare = reader.GetInt32(2);  // a treia coloană (pret_vanzare)
                            stockDetails.Add((idStock, pretAchizitie, pretVanzare));
                        }
                    }
                }
            }

            return stockDetails;
        }

        public void UpdateStockPrice(int idDeStock, int pretDeVanzare)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("CALL update_stock_price(@idDeStock, @pretDeVanzare)", conn))
                {
                    cmd.Parameters.AddWithValue("idDeStock", idDeStock);
                    cmd.Parameters.AddWithValue("pretDeVanzare", pretDeVanzare);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int GetTotalSumByDateAndUser(DateTime selectedDate, string userName)
        {
            int totalSum = 0;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT get_total_sum_by_date_and_user(@selected_date::DATE, @pnume)", conn))
                {
                    string formattedDate = selectedDate.ToString("yyyy-MM-dd");

                    cmd.Parameters.AddWithValue("selected_date", formattedDate);
                    cmd.Parameters.AddWithValue("pnume", userName);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        decimal temp=(decimal)result;
                        totalSum = (int)temp;
                    }
                }
            }
            return totalSum;
        }

        public bool ProductExists(string nume)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true)
            { MessageBox.Show("Sql injection detected"); return false; }
            bool wordExists = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("select product_name_exists(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { wordExists = (bool)result; }
                    if (wordExists == true)
                    { MessageBox.Show("Produsul exista in baza de date"); return false; }
                }
            }
            return true;
        }

        public void addProduct(string nume , string cod , string categorie , string producator , string taraDeOrigine)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true || sql_Injection.hasForbiddenWords(cod)==true || sql_Injection.hasForbiddenWords(categorie) == true || sql_Injection.hasForbiddenWords(producator)==true)
            { MessageBox.Show("Sql injection detected"); return; }
            nume = nume.ToLower();
            categorie=categorie.ToLower();
            categorie = char.ToUpper(categorie[0]) + categorie.Substring(1);
            producator = producator.ToLower();
            producator = char.ToUpper(producator[0]) + producator.Substring(1);
            taraDeOrigine = taraDeOrigine.ToLower();
            taraDeOrigine = char.ToUpper(taraDeOrigine[0]) + taraDeOrigine.Substring(1);
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                int idCategorie = -2;
                int idProd = -2;
                using (var cmd = new NpgsqlCommand("select check_code_exists(@codDB)", conn))
                {
                    cmd.Parameters.AddWithValue("codDB", cod);
                    object result = cmd.ExecuteScalar();
                    bool codeExists = false;
                    if (result != null && result != DBNull.Value) { codeExists = (bool)result; }
                    if (codeExists == true)
                    { MessageBox.Show("Codul de bare deja exista"); return; }
                }

                using (var cmd = new NpgsqlCommand("select check_produs_existance(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();
                    bool nameExists = false;
                    if (result != null && result != DBNull.Value) { nameExists = (bool)result; }
                    if (nameExists == true)
                    { MessageBox.Show("Produsul deja exista"); return; }
                }

                using (var cmd = new NpgsqlCommand("select get_category_id(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", categorie);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { idCategorie = (int)result; }

                }
                if (idCategorie == -1)
                {
                    using (var cmd = new NpgsqlCommand("Call add_category_type(@pnume)", conn))
                    {
                        cmd.Parameters.AddWithValue("pnume", categorie);
                        cmd.ExecuteNonQuery();
                    }
                }
                using (var cmd = new NpgsqlCommand("Select get_producer_id(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", producator);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { idProd = (int)result; }
                }
                if (idProd == -1)
                {
                    using (var cmd = new NpgsqlCommand("Call add_producer(@pnume , @taraprod)", conn))
                    {
                        cmd.Parameters.AddWithValue("pnume", producator);
                        cmd.Parameters.AddWithValue("taraprod", taraDeOrigine);
                        cmd.ExecuteNonQuery();
                    }
                }
                if (idCategorie == -1)
                {
                    using (var cmd = new NpgsqlCommand("select get_category_id(@pnume)", conn))
                    {
                        cmd.Parameters.AddWithValue("pnume", categorie);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value) { idCategorie = (int)result; }

                    }
                }
                if (idProd == -1)
                {
                    using (var cmd = new NpgsqlCommand("Select get_producer_id(@pnume)", conn))
                    {
                        cmd.Parameters.AddWithValue("pnume", producator);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value) { idProd = (int)result; }
                    }
                }
                using (var cmd = new NpgsqlCommand("Call add_product(@pnume,@coddb,@idcategorie,@idproducator)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", nume);
                    cmd.Parameters.AddWithValue("coddb", cod);
                    cmd.Parameters.AddWithValue("idcategorie", idCategorie);
                    cmd.Parameters.AddWithValue("idproducator", idProd);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produs adaugat cu succes");
                }
            }
        }

        public bool AddStock(string nume , int cantitate ,  string unitateDeMasura , string dataDeExpirare , int pretAchizitionare)
        {
            if (sql_Injection.hasForbiddenWords(nume) == true || sql_Injection.hasForbiddenWords(unitateDeMasura) == true || sql_Injection.hasForbiddenWords(dataDeExpirare) == true)
            { MessageBox.Show("Sql injection detected"); return false; }
            if (cantitate < 1 && pretAchizitionare < 1)
            { MessageBox.Show("Pretul sau cantitatea este introdusa gresit"); return false; }
            Verifications verifications = new Verifications();
            nume = nume.ToLower();
            unitateDeMasura = unitateDeMasura.ToLower();
            if(verifications.IsValidDate(dataDeExpirare)==false)
            {
                MessageBox.Show("Data nu este corecta. Respectati formatul : yyyy-MM-dd");
                return false;
            }
            
            int productId=-2;
            int idNewStock = -2;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("Select get_product_id(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { productId = (int)result; }
                    if (productId == -1)
                    { MessageBox.Show("Produsul nu exista in baza de date"); return false; }
                }
                int pretDeVanzare = pretAchizitionare + pretAchizitionare*3 / 10;
                if (pretAchizitionare == pretDeVanzare)
                    pretDeVanzare = pretDeVanzare + 1;
                using (var cmd = new NpgsqlCommand("Select add_new_stock(@cantitaten,@unitatedemasura,@dataexpirare::DATE,@pret_achizitie,@pret_de_vanzare)", conn))
                {
                    cmd.Parameters.AddWithValue("cantitaten", cantitate);
                    cmd.Parameters.AddWithValue("unitatedemasura", unitateDeMasura);
                    cmd.Parameters.AddWithValue("dataexpirare", dataDeExpirare);
                    cmd.Parameters.AddWithValue("pret_achizitie", pretAchizitionare);
                    cmd.Parameters.AddWithValue("pret_de_vanzare", pretDeVanzare);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { idNewStock = (int)result; }
                }
                using (var cmd = new NpgsqlCommand("CALL add_to_stock_produs(@idprodus,@idstock)", conn))
                {
                    cmd.Parameters.AddWithValue("idprodus", productId);
                    cmd.Parameters.AddWithValue("idstock", idNewStock);
                    cmd.ExecuteScalar();
                }
            }
            return true;
        }

        public int MaxReceiptFromADay(DateTime date)
        {
            int value = 0;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT get_max_suma(@pdate::DATE);", conn))
                {
                    string formattedDate = date.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("pdate", formattedDate);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { value = (int)result; }
                }
            }
            return value;
        }

        public List<string> ProductDetails(string nume)
        {
            List<string> list = new List<string>();
            nume = nume.ToLower();
            if (sql_Injection.hasForbiddenWords(nume) == true)
            { MessageBox.Show("Sql injection detected");list.Clear(); return list; }
            bool wordExists = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("select product_name_exists(@product_name)", conn))
                {
                    cmd.Parameters.AddWithValue("product_name", nume);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { wordExists = (bool)result; }
                    if (wordExists == false)
                    { MessageBox.Show("Produsul nu exista in baza de date"); list.Clear(); return list; }
                }
                using (var cmd = new NpgsqlCommand("SELECT * FROM get_product_details(@pnume)", conn))
                {
                    cmd.Parameters.AddWithValue("pnume", nume);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cod = reader.GetString(0);
                            int cantitate = reader.GetInt32(1);
                            int pretVanzare = reader.GetInt32(2);
                            list.Add($"Nume: {nume}, Cantitate: {cantitate}, Pret: {pretVanzare}");
                        }
                    }
                }
            }
            return list;
        }

        public List<string> ProductDetailsByCode(string cod)
        {
            List<string> list = new List<string>();
            if (sql_Injection.hasForbiddenWords(cod) == true)
            { MessageBox.Show("Sql injection detected"); list.Clear(); return list; }
            bool wordExists = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                
                using (var cmd = new NpgsqlCommand("SELECT * FROM get_product_details_by_code(@coddb)", conn))
                {
                    cmd.Parameters.AddWithValue("coddb", cod);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nume = reader.GetString(0);
                            int cantitate = reader.GetInt32(1);
                            int pretVanzare = reader.GetInt32(2);
                            list.Add($"Nume: {nume}, Cantitate: {cantitate}, Pret: {pretVanzare}");
                        }
                    }
                }
            }
            return list;
        }

        public List<string> ProductNamesByCategory(string category)
        {
            List<string> list = new List<string>();
            category = category.ToLower();
            category = char.ToUpper(category[0]) + category.Substring(1);
            if (sql_Injection.hasForbiddenWords(category) == true)
            { MessageBox.Show("Sql injection detected"); list.Clear(); return list; }

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("Select * from get_product_names_by_category(@numecategorie)", conn))
                {
                    cmd.Parameters.AddWithValue("numecategorie", category);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nume = reader.GetString(0);
                            list.Add($"{nume}");
                        }
                    }
                }
            }
            return list;
        }

        public bool FinalizeReceipt(List<Produse> cosProduse, int idCasier , int suma)
        {
            int idBon = 0;
            int idProdus = 0;
            int idStock = 0;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new NpgsqlCommand("Select * from add_bon(@p_id_casier, @p_suma)", conn))
                        {
                            cmd.Parameters.AddWithValue("p_id_casier", idCasier);
                            cmd.Parameters.AddWithValue("p_suma", suma);
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value) { idBon = (int)result; }
                        }
                        foreach (var produs in cosProduse)
                        {
                            using (var cmd = new NpgsqlCommand("Select * from get_product_stock(@p_nume, @p_pret)", conn))
                            {
                                cmd.Parameters.AddWithValue("p_nume", produs.Nume);
                                cmd.Parameters.AddWithValue("p_pret", produs.Pret);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        idProdus = reader.GetInt32(0);
                                        idStock = reader.GetInt32(1);
                                    }
                                }
                                using (var cmdAddBonProdus = new NpgsqlCommand("Select * from add_bon_produs(@p_id_bon, @p_id_produs, @p_id_stock)", conn))
                                {
                                    cmdAddBonProdus.Parameters.AddWithValue("p_id_bon", idBon);
                                    cmdAddBonProdus.Parameters.AddWithValue("p_id_produs", idProdus);
                                    cmdAddBonProdus.Parameters.AddWithValue("p_id_stock", idStock);
                                    cmdAddBonProdus.ExecuteScalar();
                                }
                                using (var cmdAddBonProdus = new NpgsqlCommand("Select * from decrease_stock(@p_id_stock, @p_cantitate)", conn))
                                {
                                    cmdAddBonProdus.Parameters.AddWithValue("p_id_stock", idStock);
                                    cmdAddBonProdus.Parameters.AddWithValue("p_cantitate", produs.Cantitate);
                                    cmdAddBonProdus.ExecuteScalar();
                                }
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"A apărut o eroare: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

    }
}
