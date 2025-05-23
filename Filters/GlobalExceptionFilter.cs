﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.WebAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred");

            context.Result = new ObjectResult("An error occurred while processing your request")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

}