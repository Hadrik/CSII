using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Cv6;

[Table("Customer")]
public class Customer
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public override string ToString()
    {
        return $"{Id} | {Name} | {Address}";
    }
}

internal class MyDb : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=C:\Users\trric\Documents\Skola\CS2\Cv6\diffdb.db;");
        optionsBuilder.LogTo(Console.WriteLine);
    }
}

class Program
{
    static void Main(string[] args)
    {
    }

    private void EFC()
    {
        var db = new MyDb();

        db.Customers.Add(new()
        {
            Name = "Standa",
            Address = "Louka"
        });

        foreach (var c in db.Customers
                     .Where(x => x.Name != null))
        {
            Console.WriteLine(c);
        }

        var cust = db.Customers.FirstOrDefault(x => x.Id == 1);
        cust.Name = "Pepa";
        db.SaveChanges();
    }

    private static void Dapper()
    {
        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
        const string connStr = "Data Source=mydb.db;";
        using SqliteConnection conn = new(connStr);
        conn.Open();
        using var tr = conn.BeginTransaction();

        var id = conn.Insert(new Customer()
        {
            Name = "Kastomir",
            Address = "Sulinkov"
        });
        var mira = conn.Get<Customer>(id);
        mira.Address = "Bimbasov";
        conn.Update(mira);

        
        var countSql = "SELECT COUNT(*) FROM Customer";
        Console.WriteLine(conn.ExecuteScalar<long>(countSql, null, tr));

        var selectAllSql = "SELECT * FROM Customer";
        foreach (var c in conn.Query<Customer>(selectAllSql))
        {
            Console.WriteLine(c);
        }
        
        tr.Commit();
        conn.Close();
    }

    private static void Ado()
    {
        const string connStr = "Data Source=mydb.db;";
        using SqliteConnection conn = new(connStr);
        conn.Open();

        var initSql = File.ReadAllText("database-create.sql");
        using(SqliteCommand cmd = new(initSql, conn))
        {
            cmd.ExecuteNonQuery();
        }

        var insertSql = "INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)";
        using (SqliteCommand cmd = new(insertSql, conn))
        {
            cmd.Parameters.AddWithValue("@Name", "Pepa");
            cmd.Parameters.AddWithValue("@Address", "Pičín");
            cmd.ExecuteNonQuery();
        }

        using var tr = conn.BeginTransaction();
        
        var countSql = "SELECT COUNT(*) FROM Customer";
        using (SqliteCommand cmd = new(countSql, conn, tr))
        {
            Console.WriteLine((long)cmd.ExecuteScalar()!);
        }

        var selectAllSql = "SELECT * FROM Customer";
        using (SqliteCommand cmd = new(selectAllSql, conn, tr))
        {
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("Id"));
                var name = reader.GetString(reader.GetOrdinal("Name"));

                string? address = null;
                if (!reader.IsDBNull(reader.GetOrdinal("Address")))
                    address = reader.GetString(reader.GetOrdinal("Address"));

                Console.WriteLine($"{id} | {name} | {address}");
            }
        }
        
        tr.Commit();
        conn.Close();
    }
}