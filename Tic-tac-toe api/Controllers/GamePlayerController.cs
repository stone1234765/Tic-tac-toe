﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tic_tac_toe_api.Data;
using Tic_tac_toe_api.Models.EntityFramework;

namespace Tic_tac_toe_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayerController : ControllerBase
    {
        private readonly Tic_tac_toeContext _context;

        public GamePlayerController(Tic_tac_toeContext context)
        {
            _context = context;
        }
        public class GamePlayerPrototype
        {
            public Guid Id { get; set; }
            public string Figure { get; set; }
            public bool IsWon { get; set; }
            public string PlayerId { get; set; }
            public Guid GameId { get; set; }
        }
        [HttpPost]
        public async Task<Guid> CreateGamePlayer(GamePlayerPrototype gamePlayerPrototype)
        {
            var gamePlayer = new GamePlayer()
            {
                Id = Guid.NewGuid(),
                Figure = gamePlayerPrototype.Figure == "x" ? FigureType.x : FigureType.o,
                IsWon = gamePlayerPrototype.IsWon,
                PlayerId = gamePlayerPrototype.PlayerId,
                GameId = gamePlayerPrototype.GameId
            };
            _context.GamePlayers.Add(gamePlayer);
            await _context.SaveChangesAsync();
            return gamePlayer.Id;
            //return Guid.NewGuid();
            //return CreatedAtAction(
            //    nameof(GetGamePlayer), gamePlayer.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GamePlayer>> GetGamePlayer(Guid id)
        {
            var gamePlayer = await _context.GamePlayers.FindAsync(id);

            if (gamePlayer == null)
            {
                return NotFound();
            }

            return gamePlayer;
        }
    }
}