using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Hafta_StoredProcedure_21._09._2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=.;Database=Ogrenciler;Trusted_Connection=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    while (true)
                    {
                        Console.WriteLine("\n------------------------");
                        Console.WriteLine("1- Öğrenci Ekle");
                        Console.WriteLine("2- Öğrenci Listele");
                        Console.WriteLine("3- Öğrenci Sil");
                        Console.WriteLine("4- Öğrenci Güncelle");
                        Console.WriteLine("5- Çıkış");
                        Console.WriteLine("------------------------");

                        Console.Write("Seçiminizi yapın: ");
                        if (!int.TryParse(Console.ReadLine(), out int secim))
                        {
                            Console.WriteLine("❌ Lütfen geçerli bir sayı girin!");
                            continue;
                        }

                        Console.WriteLine();

                        switch (secim)
                        {
                            case 1: // Öğrenci Ekle
                                using (SqlCommand cmd = new SqlCommand("sp_ogrenciEkle", conn))
                                {
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    Console.WriteLine("Kaç öğrenci eklemek istiyorsunuz?");
                                    if (!int.TryParse(Console.ReadLine(), out int adet) || adet <= 0)
                                    {
                                        Console.WriteLine("❌ Geçerli bir sayı girin!");
                                        break;
                                    }

                                    for (int i = 0; i < adet; i++)
                                    {
                                        Console.Write("Öğrenci adı: ");
                                        string ad = Console.ReadLine()?.Trim();

                                        Console.Write("Öğrenci soyadı: ");
                                        string soyad = Console.ReadLine()?.Trim();

                                        if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad))
                                        {
                                            Console.WriteLine("❌ Ad veya Soyad boş olamaz!");
                                            continue;
                                        }

                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("@Ad", ad);
                                        cmd.Parameters.AddWithValue("@Soyad", soyad);

                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        Console.WriteLine($"✅ {rowsAffected} öğrenci eklendi.");
                                    }
                                }
                                break;

                            case 2: // Öğrenci Listele
                                using (SqlCommand cmd2 = new SqlCommand("sp_OgrenciListeleme", conn))
                                {
                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                    using (SqlDataReader reader = cmd2.ExecuteReader())
                                    {
                                        Console.WriteLine("\n📋 Öğrenci Listesi:");
                                        while (reader.Read())
                                        {
                                            Console.WriteLine($"{reader["OgrenciID"]} - {reader["Ad"]} - {reader["Soyad"]}");
                                        }
                                    }
                                }
                                break;

                            case 3: // Öğrenci Sil
                                Console.Write("Silmek istediğiniz öğrenci ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int ogrenciId))
                                {
                                    Console.WriteLine("❌ Geçerli bir ID girin!");
                                    break;
                                }

                                using (SqlCommand cmd3 = new SqlCommand("sp_ogrencilerSil", conn))
                                {
                                    cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", ogrenciId);

                                    int rowsAffected2 = cmd3.ExecuteNonQuery();
                                    Console.WriteLine($"✅ {rowsAffected2} öğrenci silindi.");
                                }
                                break;

                            case 4: // Öğrenci Güncelle
                                Console.Write("Güncellenecek öğrenci ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int guncelID))
                                {
                                    Console.WriteLine("❌ Geçerli bir ID girin!");
                                    break;
                                }

                                Console.Write("Yeni ad: ");
                                string yeniAd = Console.ReadLine()?.Trim();

                                Console.Write("Yeni soyad: ");
                                string yeniSoyad = Console.ReadLine()?.Trim();

                                if (string.IsNullOrWhiteSpace(yeniAd) || string.IsNullOrWhiteSpace(yeniSoyad))
                                {
                                    Console.WriteLine("❌ Ad veya Soyad boş olamaz!");
                                    break;
                                }

                                using (SqlCommand cmdGuncel = new SqlCommand("sp_OgrenciGuncelle", conn))
                                {
                                    cmdGuncel.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmdGuncel.Parameters.AddWithValue("@OgrenciID", guncelID);
                                    cmdGuncel.Parameters.AddWithValue("@Ad", yeniAd);
                                    cmdGuncel.Parameters.AddWithValue("@Soyad", yeniSoyad);

                                    int rows = cmdGuncel.ExecuteNonQuery();
                                    Console.WriteLine($"✅ {rows} satır güncellendi.");
                                }
                                break;

                            case 5: // Çıkış
                                Console.WriteLine("Programdan çıkılıyor...");
                                return;

                            default:
                                Console.WriteLine("❌ Geçersiz seçim, tekrar deneyin!");
                                break;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("⚠️ SQL Hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Genel Hata: " + ex.Message);
            }
        }
    }
}
