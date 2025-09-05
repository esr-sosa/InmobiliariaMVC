using MySql.Data.MySqlClient;
using System.Collections.Generic;
using InmobiliariaMVC.Models;

public class RepositorioInquilino
{
    private readonly Database _db;
    public RepositorioInquilino(Database db) => _db = db;

    public List<Inquilino> ObtenerTodos()
    {
        var lista = new List<Inquilino>();
        using var conn = _db.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM Inquilinos", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Inquilino
            {
                IdInquilino = reader.GetInt32("IdInquilino"),
                Nombre = reader.GetString("Nombre"),
                Apellido = reader.GetString("Apellido"),
                Dni = reader.GetString("Dni"),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                Email = reader.GetString("Email")
            });
        }
        return lista;
    }

    public Inquilino ObtenerPorId(int id)
    {
        Inquilino? i = null;
        using var conn = _db.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM Inquilinos WHERE IdInquilino=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            i = new Inquilino
            {
                IdInquilino = reader.GetInt32("IdInquilino"),
                Nombre = reader.GetString("Nombre"),
                Apellido = reader.GetString("Apellido"),
                Dni = reader.GetString("Dni"),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                Email = reader.GetString("Email")
            };
        }
        return i;
    }

    public void Alta(Inquilino i)
    {
        using var conn = _db.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand(@"INSERT INTO Inquilinos 
            (Nombre, Apellido, Dni, Telefono, Email) 
            VALUES (@nombre, @apellido, @dni, @telefono, @email)", conn);
        cmd.Parameters.AddWithValue("@nombre", i.Nombre);
        cmd.Parameters.AddWithValue("@apellido", i.Apellido);
        cmd.Parameters.AddWithValue("@dni", i.Dni);
        cmd.Parameters.AddWithValue("@telefono", i.Telefono);
        cmd.Parameters.AddWithValue("@email", i.Email);
        cmd.ExecuteNonQuery();
        i.IdInquilino = (int)cmd.LastInsertedId; // opcional: devuelve el ID insertado
    }

    public bool Editar(Inquilino inquilino)
{
    using var conn = _db.GetConnection();
    var query = "UPDATE Inquilinos SET Nombre=@Nombre, Apellido=@Apellido, Dni=@Dni, Telefono=@Telefono, Email=@Email WHERE IdInquilino=@Id";
    using var cmd = new MySqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
    cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
    cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
    cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
    cmd.Parameters.AddWithValue("@Email", inquilino.Email);
    cmd.Parameters.AddWithValue("@Id", inquilino.IdInquilino);

    int filasAfectadas = cmd.ExecuteNonQuery();
    return filasAfectadas > 0;
}


    public void Eliminar(int id)
    {
        using var conn = _db.GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("DELETE FROM Inquilinos WHERE IdInquilino=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
