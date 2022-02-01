using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Ventas.Models;
using Sistema_de_Ventas.Models.Request;
using Sistema_de_Ventas.Models.Response;

namespace Sistema_de_Ventas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Response oResponse = new Response();
            oResponse.Success = 0;
            try
            {
                using (SistemaVentasContext db = new SistemaVentasContext()) {
                    var lst = db.Clientes.ToList();
                    oResponse.Data = lst;
                    oResponse.Success = 1;
                }
            }catch(Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Response oResponse = new Response();
            try
            {
                using (SistemaVentasContext db = new SistemaVentasContext())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Name;
                    db.Clientes.Add(oCliente);
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }catch(Exception ex)
            {
                oResponse.Message=ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Response oResponse = new Response();
            try
            {
                using (SistemaVentasContext db = new SistemaVentasContext())
                {
                    Cliente oCliente = db.Clientes.Find(oModel.Id);
                    oCliente.Nombre = oModel.Name;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response oResponse = new Response();
            try
            {
                using (SistemaVentasContext db = new SistemaVentasContext())
                {
                    Cliente oCliente = db.Clientes.Find(id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }
    }
}
