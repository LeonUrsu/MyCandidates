using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCandidates.Models;
using Oracle.ManagedDataAccess.Client;

namespace MyCandidates.Controllers;

public class HomeController : Controller
{
    private readonly string _connectionString;

    public HomeController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleDb") 
            ?? throw new InvalidOperationException("Stringa di connessione OracleDb mancante.");
    }

    public async Task<IActionResult> Index()
    {
        var candidates = new List<Candidate>();

        using (var connection = new OracleConnection(_connectionString))
        {
            await connection.OpenAsync();

            string sql = @"SELECT candidate_id, nome, cognome, email, telefono, citta, disponibilita, note 
                           FROM candidates 
                           ORDER BY candidate_id";

            using (var command = new OracleCommand(sql, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    candidates.Add(new Candidate
                    {
                        CandidateId = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        Email = reader.GetString(3),
                        Telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Citta = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Disponibilita = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Note = reader.IsDBNull(7) ? null : reader.GetString(7)
                    });
                }
            }
        }

        return View(candidates);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}