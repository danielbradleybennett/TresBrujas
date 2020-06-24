using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TresBrujas.Data;
using TresBrujas.Models;

namespace TresBrujas.Controllers
{
    public class SpellsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public SpellsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        /// section for raw Sql cmd
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: Spells
        public async Task<ActionResult> Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //var user = await GetCurrentUserAsync();
                    cmd.CommandText = @"Select Id, Name From Spell";

                    var reader = cmd.ExecuteReader();
                    var spells = new List<Spell>();

                    while (reader.Read())
                    {
                        Spell spell = new Spell
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                           

                        };

                        spells.Add(spell);
                    }
                    reader.Close();
                    return View(spells);
                }

            }
            

        }

        // GET: Spells/Details/5
        public ActionResult Details(int id)
        {
            var Spell = GetSpellById(id);
            return View(Spell);
            
        }

        // GET: Spells/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spells/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Spells/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Spells/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Spells/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Spells/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private Spell GetSpellById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id, s.[Name], st.Type
                                      FROM Spell s
                                      LEFT JOIN SpellType st ON st.Id = s.SpellTypeId
                                      WHERE s.Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    var reader = cmd.ExecuteReader();
                    Spell spell = null;

                    while (reader.Read())
                    {
                        if (spell == null)
                        {
                            spell = new Spell()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                SpellType = new SpellType
                                {
                                    Type = reader.GetString(reader.GetOrdinal("Type"))
                                }
                                
                                
                            };
                        }

                    }

                    reader.Close();
                    return spell;
                }
            }
        }
    }
}
