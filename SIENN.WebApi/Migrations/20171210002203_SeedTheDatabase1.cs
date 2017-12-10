using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SIENN.WebApi.Migrations
{
    public partial class SeedTheDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Seed Categories
            migrationBuilder.Sql("INSERT INTO Categories (Code, Description) VALUES" +
                                 "('Jedzenie', 'Produkty nadające się do spożycia' )," +
                                 "('Pszenne', 'Produkty zawierające przenicę' )," +
                                 "('Dodatki', '' )," +
                                 "('Nabiał', 'Produkty pochodzenia mlecznego lub jajka' )," +
                                 "('Białka', 'Produkty z dużą zawartością białka' )," +
                                 "('Warzywa', '' )," +
                                 "('Mięso', '' )," +
                                 "('Prezenty', 'Produkty nadające się na prezent' )," +
                                 "('Mrożonki', 'Produkty głęboko mrożone' )," +
                                 "('Kwiaty', '' )");

            //Seed Units
            migrationBuilder.Sql("INSERT INTO Unit (Code, Description) VALUES" +
                                 "('Spożywcze', '' )," +
                                 "('Artykuły gospodarstwa domowego', '' )," +
                                 "('Dekoracja', '' )"
                                 );

            //Seed Types
            migrationBuilder.Sql("INSERT INTO Type (Code, Description) VALUES" +
                                 "('Sztuka', 'Produkt sprzedawany na sztuki' )," +
                                 "('Sypkie', 'Produkty sypkie' )," +
                                 "('Płynne', 'Produkty płynne' )," +
                                 "('Waga', 'Produkty sprzedawane na wagę' )"
                                 );

            //Seed Products
            //Would be better to run all below nested SQL queries once, but for the sake of this app it wouldn't make performance difference, so I left it as it is. 
            migrationBuilder.Sql(
                "INSERT INTO Products (Code, DeliveryDate, Description, IsAvailable, Price, TypeId, UnitId) VALUES" +
                $"('Bułka', '{DateTime.UtcNow.AddDays(5).ToString("yyyy-MM-dd")}', '100% bułki w bułce', 1, 0.22, (SELECT Id FROM Type WHERE Code='Sztuka'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Mak', '{DateTime.UtcNow.AddDays(50).ToString("yyyy-MM-dd")}', 'Mak jak się patrzy', 0, 4.11, (SELECT Id FROM Type WHERE Code='Sypkie'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Mleko', '{DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")}', 'Świeże mleko', 1, 2.55, (SELECT Id FROM Type WHERE Code='Płynne'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Fasola', '{DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd")}', 'Po prostu fasola', 0, 5.44, (SELECT Id FROM Type WHERE Code='Sypkie'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Pierś z kurczaka', '{DateTime.UtcNow.AddDays(40).ToString("yyyy-MM-dd")}', 'Piersi zawsze spoko', 1, 12.22, (SELECT Id FROM Type WHERE Code='Waga'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Folia aluminiowa', '{DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")}', 'Nie pomalujesz', 1, 4.22, (SELECT Id FROM Type WHERE Code='Sztuka'), (SELECT Id FROM Unit WHERE Code='Artykuły gospodarstwa domowego')  )," +
                $"('Talerz', '{DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd")}', 'Nie ma fonii', 0, 15.00, (SELECT Id FROM Type WHERE Code='Sztuka'), (SELECT Id FROM Unit WHERE Code='Dekoracja')  )," +
                $"('Mrożona marchew', '{DateTime.UtcNow.AddDays(3).ToString("yyyy-MM-dd")}', 'Chrupka', 1, 3.99, (SELECT Id FROM Type WHERE Code='Sztuka'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )," +
                $"('Róża', '{DateTime.UtcNow.AddDays(4).ToString("yyyy-MM-dd")}', null, 1, 5.00, (SELECT Id FROM Type WHERE Code='Sztuka'), (SELECT Id FROM Unit WHERE Code='Dekoracja')  )," +
                $"('Mrożony królik', '{DateTime.UtcNow.AddDays(5).ToString("yyyy-MM-dd")}', null, 0, 3.42, (SELECT Id FROM Type WHERE Code='Waga'), (SELECT Id FROM Unit WHERE Code='Spożywcze')  )"
                );

            //Seed Categories To Products table
            migrationBuilder.Sql(
                "INSERT INTO CategoryToProduct (CategoryId, ProductId) VALUES" +

                // Bułka - jedzenie, pszenne
                $"((SELECT Id FROM Categories WHERE Code='Jedzenie'), (SELECT Id FROM Products WHERE Code='Bułka'))," +
                $"((SELECT Id FROM Categories WHERE Code='Pszenne'), (SELECT Id FROM Products WHERE Code='Bułka'))," +
            // Mak - jedzenie, dodatki
                $"((SELECT Id FROM Categories WHERE Code='Jedzenie'), (SELECT Id FROM Products WHERE Code='Mak'))," +
                $"((SELECT Id FROM Categories WHERE Code='Dodatki'), (SELECT Id FROM Products WHERE Code='Mak'))," +
            // Mleko - nabiał, białka
                $"((SELECT Id FROM Categories WHERE Code='Nabiał'), (SELECT Id FROM Products WHERE Code='Mleko'))," +
                $"((SELECT Id FROM Categories WHERE Code='Białka'), (SELECT Id FROM Products WHERE Code='Mleko'))," +
            // Fasola - warzywa, białka
                $"((SELECT Id FROM Categories WHERE Code='Warzywa'), (SELECT Id FROM Products WHERE Code='Fasola'))," +
                $"((SELECT Id FROM Categories WHERE Code='Białka'), (SELECT Id FROM Products WHERE Code='Fasola'))," +
            // Pierś - mięso
                $"((SELECT Id FROM Categories WHERE Code='Mięso'), (SELECT Id FROM Products WHERE Code='Pierś z kurczaka'))," +
            // Folia - dodatki
                $"((SELECT Id FROM Categories WHERE Code='Dodatki'), (SELECT Id FROM Products WHERE Code='Folia aluminiowa'))," +
            // talerz - prezenty
                $"((SELECT Id FROM Categories WHERE Code='Prezenty'), (SELECT Id FROM Products WHERE Code='Talerz'))," +
            // marchew - mrożonki, warzywa
                $"((SELECT Id FROM Categories WHERE Code='Mrożonki'), (SELECT Id FROM Products WHERE Code='Mrożona marchew'))," +
                $"((SELECT Id FROM Categories WHERE Code='Warzywa'), (SELECT Id FROM Products WHERE Code='Mrożona marchew'))," +
            // róża - kwiaty, prezenty
                $"((SELECT Id FROM Categories WHERE Code='Kwiaty'), (SELECT Id FROM Products WHERE Code='Róża'))," +
                $"((SELECT Id FROM Categories WHERE Code='Prezenty'), (SELECT Id FROM Products WHERE Code='Róża'))," +
            // królik - mrożonki, białka, mięso
                $"((SELECT Id FROM Categories WHERE Code='Białka'), (SELECT Id FROM Products WHERE Code='Mrożony królik'))," +
                $"((SELECT Id FROM Categories WHERE Code='Mięso'), (SELECT Id FROM Products WHERE Code='Mrożony królik'))," +
                $"((SELECT Id FROM Categories WHERE Code='Mrożonki'), (SELECT Id FROM Products WHERE Code='Mrożony królik'))"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
            migrationBuilder.Sql("DELETE FROM Categories");
            migrationBuilder.Sql("DELETE FROM Unit");
            migrationBuilder.Sql("DELETE FROM Type");
            migrationBuilder.Sql("DELETE FROM CategoryToProduct");
        }
    }
}
