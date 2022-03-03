#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace demo_WebAPI.Controllers
{
    [Route("api/[controller]")]// Tạo một Microsoft.AspNetCore.Mvc.RouteAttribute mới với mẫu tuyến đường đã cho.
    [ApiController]
    // ApiController:   Cho biết rằng một loại và tất cả các loại dẫn xuất được sử dụng để phân phát các phản hồi API HTTP.
    //    Các bộ điều khiển được trang trí bằng thuộc tính này được định cấu hình với các tính năng và hành vi nhằm mục đích cải thiện trải nghiệm của nhà phát triển để xây dựng API.
    //    Khi được trang trí trên một Tổ hợp, tất cả các bộ điều khiển trong Tổ hợp sẽ được coi là bộ điều khiển có hành vi API.
    public class PRODUCTS1Controller : ControllerBase
    {
        private readonly DBContextWebAPI _context;

        public PRODUCTS1Controller(DBContextWebAPI context)
        {
            _context = context;
        }

        // GET: api/PRODUCTS1
        [HttpGet(Name = "GetProductSet")]
        public async Task<ActionResult<IEnumerable<PRODUCTS>>> GetProductses()//Bất đồng bộ : cho nhiều User  truy cập cung 1 lúc.
        {
            return await _context.Productses.ToListAsync();
        }

        // GET: api/PRODUCTS1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PRODUCTS>> GetPRODUCTS(int id)
        {
            var pRODUCTS = await _context.Productses.FindAsync(id);

            if (pRODUCTS == null)
            {
                return NotFound();
            }

            return pRODUCTS;
        }

        // PUT: api/PRODUCTS1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPRODUCTS(int id, PRODUCTS pRODUCTS)
        {
            if (id != pRODUCTS.id_Product)
            {
                return BadRequest();
            }

            _context.Entry(pRODUCTS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTSExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PRODUCTS1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PRODUCTS>> PostPRODUCTS(PRODUCTS pRODUCTS)
        {
            _context.Productses.Add(pRODUCTS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPRODUCTS", new { id = pRODUCTS.id_Product }, pRODUCTS);
        }

        // DELETE: api/PRODUCTS1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePRODUCTS(int id)
        {
            var pRODUCTS = await _context.Productses.FindAsync(id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }

            _context.Productses.Remove(pRODUCTS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PRODUCTSExists(int id)
        {
            return _context.Productses.Any(e => e.id_Product == id);
        }
    }
}
