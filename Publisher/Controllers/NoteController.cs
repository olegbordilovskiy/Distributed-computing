﻿using AutoMapper;
using DC_REST.DTOs.Request;
using DC_REST.DTOs.Response;
using DC_REST.Entities;
using DC_REST.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DC_REST.Controllers
{
	[Route("/api/v1.0/notes")]
	[ApiController]
	public class NoteController : ControllerBase
	{
		private readonly INoteService _noteService;
		private readonly IMapper _mapper;

		public NoteController(INoteService noteService, IMapper mapper)
		{
			_noteService = noteService;
			_mapper = mapper;
		}

		[HttpPost]
		public IActionResult CreateNote(NoteRequestTo noteRequestDTO)
		{
			try
			{
				var noteResponseDTO = _noteService.CreateNote(noteRequestDTO);
				return StatusCode(201, noteResponseDTO);
			}
			catch (DbUpdateException ex)
			{
				var errorMessage = ErrorResponse.CreateErrorResponse(ex.Message, HttpStatusCode.BadRequest); ;
				return StatusCode(403, errorMessage);
			}
			catch (Exception ex)
			{
				var errorMessage = ErrorResponse.CreateErrorResponse(ex.Message, HttpStatusCode.BadRequest); ;
				return BadRequest(errorMessage);
			}
		}

		[HttpGet]
		public IActionResult GetAllNotes()
		{
			var notesResponseDTO = _noteService.GetAllNotes();
			return Ok(notesResponseDTO);
		}

		[HttpGet("{id}")]
		public IActionResult GetNoteById(int id)
		{
			NoteResponseTo? noteResponseDTO;
			try
			{
				noteResponseDTO = _noteService.GetNoteById(id);
			}
			catch (DbUpdateException)
			{
				return BadRequest();
			}

			if (noteResponseDTO == null)
				return NotFound();
			return Ok(noteResponseDTO);
		}

		[HttpPut]
		public IActionResult UpdateNote(NoteRequestTo noteRequestDTO)
		{
			try
			{
				var noteResponseDTO = _noteService.UpdateNote(noteRequestDTO.Id, noteRequestDTO);
				if (noteResponseDTO == null)
					return NotFound();
				return Ok(noteResponseDTO);
			}
			catch (ArgumentException ex)
			{
				var errorMessage = ErrorResponse.CreateErrorResponse(ex.Message, HttpStatusCode.BadRequest); ;

				return BadRequest(errorMessage);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteNote(int id)
		{
			var isDeleted = _noteService.DeleteNote(id);

			if (!isDeleted)
				return NotFound();

			return NoContent();
		}
	}
}
