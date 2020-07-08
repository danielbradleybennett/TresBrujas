using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TresBrujas.Data;
using TresBrujas.Models;

namespace TresBrujas.Controllers
{
    public class SpellCastersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public SpellCastersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
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



        // GET: SpellCaster
        public async Task<ActionResult> Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //var user = await GetCurrentUserAsync();
                    cmd.CommandText = @"Select Id, Name From SpellCaster";

                    var reader = cmd.ExecuteReader();
                    var spellCaster = new List<SpellCaster>();

                    while (reader.Read())
                    {
                        SpellCaster spellCasters = new SpellCaster
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))


                        };

                        spellCaster.Add(spellCasters);
                    }
                    reader.Close();
                    return View(spellCaster);
                }

            }


        }

        // GET: SpellCaster/Details/5
        public ActionResult Details(int id)
        {
            var SpellCaster = GetSpellCasterById(id);
            return View(SpellCaster);
        }

        // GET: SpellCaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpellCaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpellCaster spellCaster)
        {
            try
            {


                //builds up our new BlogPost using the data submitted from the form, 
                //represented here as "BlogPos"
                var spells = new SpellCaster()
                {
                    Id = spellCaster.Id,
                    Name = spellCaster.Name,

                };
                _context.SpellCaster.Add(spells);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpellCaster/Edit/5
        public ActionResult Edit(int id)
        {
            var spellCaster = GetSpellCasterById(id);
            var viewModel = new SpellCaster()
            {
                Name = spellCaster.Name,
                Id = spellCaster.Id
            };
            return View(viewModel);
        }

        // POST: SpellCaster/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SpellCaster spellCaster)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE SpellCaster
                            SET Name = @name,
                            WHERE Id = @id";

                        cmd.Parameters.Add(new SqlParameter("@name", spellCaster.Name));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();


                    }

                }
                    return RedirectToAction(nameof(Index));

                
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: SpellCaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpellCaster/Delete/5
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

        private SpellCaster GetSpellCasterById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id, s.[Name]
                                      FROM SpellCaster s
                                      WHERE s.Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    var reader = cmd.ExecuteReader();
                    SpellCaster spellCaster = null;

                    while (reader.Read())
                    {
                        if (spellCaster == null)
                        {
                            spellCaster = new SpellCaster()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),

                            };
                        }

                    }

                    reader.Close();
                    return spellCaster;
                }


            }
        }
    }
}