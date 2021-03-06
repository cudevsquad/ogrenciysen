﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proje.Business.Helper;
using Proje.Entity.Model;
using Proje.Interface;

namespace Proje.AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region service
        private readonly IGenericService<Category> categoryService;
        public CategoryController(IGenericService<Category> categoryservice)
        {
            categoryService = categoryservice;
        }
        #endregion
        [HttpGet]
        [Route("~/Category/GetAll")]
        public IActionResult GetAll()
        {
            var result = categoryService.GetAll();
            var json = JsonConvert.SerializeObject(result);
            return Ok(json);
        }
        [HttpGet]
        [Route("~/Category/Get/{id}")]
        public IActionResult Get(int id)
        {
            var result = categoryService.Get(id);
            var json = JsonConvert.SerializeObject(result);
            return Ok(json);
        }
        [HttpDelete]
        [Route("~/Category/Delete/{id}")]
        public ResultHelper Delete(int id)
        {
            Category Category = categoryService.Get(id);
            if (Category == null)
            {
                return new ResultHelper(true, Category.CategoryID, ResultHelper.UnSuccessMessage);
            }

            categoryService.Delete(Category);
            return new ResultHelper(true, Category.CategoryID, ResultHelper.SuccessMessage);
        }
        [HttpPut]
        [Route("~/Category/Update/{id}")]
        public ResultHelper Put(int id, [FromBody] Category Category)
        {
            if (Category == null)
            {
                return new ResultHelper(true, Category.CategoryID, ResultHelper.UnSuccessMessage);
            }

            categoryService.Set( Category);
            return new ResultHelper(true, Category.CategoryID, ResultHelper.SuccessMessage);

        }
        [HttpPost]
        [Route("~/Category/Add")]
        public ResultHelper Post([FromBody] Category Category)
        {
            if (Category == null)
            {
                return new ResultHelper(true, Category.CategoryID, ResultHelper.UnSuccessMessage);
            }
            categoryService.Create(Category);
            return new ResultHelper(true, Category.CategoryID, ResultHelper.SuccessMessage);
        }
    }
}
