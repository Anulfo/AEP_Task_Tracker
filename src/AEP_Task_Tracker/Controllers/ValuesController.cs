using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AEP_Task_Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AEP_Task_Tracker.Models;

namespace AEP_Task_Tracker.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApplicationDbContext context;

        public ValuesController(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> tasks = from chore in context.Chore select chore;
            
            if (tasks == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tasks);
            } 
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Chore chore)
        {
            if (ModelState.IsValid)
            {
                try
                {
                context.Chore.Add(chore);
                context.SaveChanges();
                }
                catch(DbUpdateException)
                {
                    if (TaskExists(chore.Id))
                    {
                        return new StatusCodeResult(StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return CreatedAtRoute("Index", chore);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Chore chore)
        {
            if (id != chore.Id)
            {
                return BadRequest(ModelState);
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Chore.Update(chore);
                context.SaveChanges();
                return Ok(chore);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Chore chore = context.Chore.SingleOrDefault(ch => ch.Id == id);
            if (chore == null)
            {
                return NotFound();
            }
            try
            {
                context.Chore.Remove(chore);
                context.SaveChanges();
                return Ok(chore);
            }
            catch(System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        private bool TaskExists(int id)
        {
            return context.Chore.Count(e => e.Id == id) > 0;
        }
    }
}
