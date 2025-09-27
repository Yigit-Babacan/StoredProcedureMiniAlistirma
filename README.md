🎓 5. Hafta – Stored Procedure ile Öğrenci Yönetim Sistemi

Bu proje, C# Console uygulaması üzerinden SQL Server Stored Procedure kullanarak basit bir öğrenci yönetim sistemi geliştirmeyi amaçlamaktadır.

🚀 Özellikler

Öğrenci Ekle → Ad ve Soyad girerek yeni öğrenci ekleme.

Öğrenci Listele → Veritabanındaki tüm öğrencileri listeleme.

Öğrenci Sil → ID ile öğrenci kaydını silme.

Öğrenci Güncelle → ID’ye göre öğrenci adı ve soyadını güncelleme.

Hatalı girişlerde uyarı veren try-catch yapısı.

🛠️ Kullanılan Teknolojiler

C# (.NET Framework)

SQL Server

ADO.NET (SqlConnection, SqlCommand, SqlDataReader)

📂 Proje Yapısı
📁 Proje Klasörü
 ┣ 📄 Program.cs        → Ana C# kodu
 ┣ 📄 Stored Procedures → SQL tarafında kullanılan SP’ler
 ┗ 📄 README.md         → Proje açıklaması

🗄️ Gerekli Stored Procedure’ler
-- Öğrenci Ekle
CREATE PROCEDURE sp_ogrenciEkle
    @Ad NVARCHAR(50),
    @Soyad NVARCHAR(50)
AS
BEGIN
    INSERT INTO Ogrenciler_table (Ad, Soyad)
    VALUES (@Ad, @Soyad)
END

-- Öğrenci Listeleme
CREATE PROCEDURE sp_OgrenciListeleme
AS
BEGIN
    SELECT OgrenciID, Ad, Soyad FROM Ogrenciler_table
END

-- Öğrenci Sil
CREATE PROCEDURE sp_ogrencilerSil
    @ID INT
AS
BEGIN
    DELETE FROM Ogrenciler_table WHERE OgrenciID = @ID
END

-- Öğrenci Güncelle
CREATE PROCEDURE sp_OgrenciGuncelle
    @OgrenciID INT,
    @Ad NVARCHAR(50),
    @Soyad NVARCHAR(50)
AS
BEGIN
    UPDATE Ogrenciler_table
    SET Ad = @Ad, Soyad = @Soyad
    WHERE OgrenciID = @OgrenciID
END

🎯 Amaç

Bu proje, Stored Procedure mantığını öğrenmek, C# üzerinden SQL Server bağlantısı kurmak ve temel CRUD (Create, Read, Update, Delete) işlemlerini uygulamak için geliştirilmiştir.
