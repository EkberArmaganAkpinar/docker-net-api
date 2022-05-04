using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Connector;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();
    

     

            [HttpGet]
        public JsonResult Get()
        {
           //BÜTÜN TASKLARI GETİREN GET METHODU
            logger.Info("Tasklar Listelendi");
            Conn mySqlGet = new Conn();
            JsonResult jsonResult = new JsonResult(mySqlGet.Task());

           
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Post(Task task)
        {
            //YENİ BİR TASK EKLEMEK İÇİN KULLANILAN POST METHODU
            logger.Info("Yeni Task Eklendi");
            Conn mySqlGet = new Conn();
            mySqlGet.AddTask(task);
            JsonResult jsonResult = new JsonResult("added succesfully");


            return jsonResult;
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
           //İD'YE GÖRE ARANAN TASKI GETİREN GET METHODU 
            logger.Info("Aranan Task Bulundu");
            Conn mySqlGet = new Conn();
            JsonResult jsonResult = new JsonResult(mySqlGet.SelectedTask(id));

            return jsonResult ;
        }
    }
}
