﻿using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Pong desde la API 🚀");
}