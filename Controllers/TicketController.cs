using AiSupportApi.Data;
using AiSupportApi.Models;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiSupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TicketModel>> GetTickets()
        {
            var tickets = _context.Tickets.ToList();
            return Ok(tickets);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TicketModel> GetTicketById(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado.");
            }

            return Ok(ticket);
        }

        [HttpPost]
        public ActionResult<TicketModel> CreateTicket(TicketModel ticketModel)
        {
            if (ticketModel == null)
            {
                return BadRequest("Ocorreu um erro ao salvar o Ticket");
            }

            _context.Tickets.Add(ticketModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTicketById), new { id = ticketModel.Id }, ticketModel);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<TicketModel> UpdateTicket(TicketModel ticketModel, int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado.");
            }

            ticket.Category = ticketModel.Category;
            ticket.Description = ticketModel.Description;
            ticket.Status = ticketModel.Status;

            _context.Tickets.Update(ticket);
            _context.SaveChanges();

            return Ok(new { mensagem = $"Ticket Id {id} editado com sucesso!" });
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<TicketModel> DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado.");
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return Ok(new { menssagem = $"Ticket Id {id} excluído com sucesso!" });
        }
    }
}
