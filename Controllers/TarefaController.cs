using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PloomesAPI.Data;
using PloomesAPI.Models;
using PloomesAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tarefa.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TarefaController : ControllerBase
    {
        //Leitura
        [HttpGet]
        [Route("tarefas/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDataContext context,
            [FromRoute] int id)
        {
            var tarefa = await context
                .Tarefas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return tarefa == null
                ? NotFound()
                : Ok(tarefa);
        }
        
        //Criação
        [HttpPost("tarefas")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDataContext context,
            [FromBody] CreateTarefaViewModel model)
        {
            //Faz a validação dos itens
            if (!ModelState.IsValid)
                return BadRequest();

            var tarefa = new Tarefa
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                //Salva os itens na base de dados
                await context.Tarefas.AddAsync(tarefa);
                await context.SaveChangesAsync();
                return Created($"v1/tarefas/{tarefa.Id}", tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        //Atualizacao
        [HttpPut("tarefas/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDataContext context,
            [FromBody] CreateTarefaViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

            if (tarefa == null)
                return NotFound();

            try
            {
                tarefa.Title = model.Title;
                
                context.Tarefas.Update(tarefa);
                await context.SaveChangesAsync();
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //Remove
        [HttpDelete("tarefas/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDataContext context,
            [FromRoute] int id)
        {
            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Tarefas.Remove(tarefa);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
    }
}