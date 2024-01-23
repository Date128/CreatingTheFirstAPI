
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Создание_первого_API._Взаимодействие_с_API_.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new() { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        public static List<WeatherData> weatherDatas = new()
        {
        new WeatherData() { Id = 1, Data = "21.01.2022", Degree = 10, Location = "Мурманск" },
        new WeatherData() { Id = 23, Data = "21.01.2022", Degree = -20, Location = "Мурманск" },
        new WeatherData() { Id = 24, Data = "21.01.2022", Degree = 15, Location = "Мурманск" },
        new WeatherData() { Id = 25, Data = "21.01.2022", Degree = 0, Location = "Мурманск" },
        new WeatherData() { Id = 30, Data = "21.01.2022", Degree = 3, Location = "Мурманск" },
        };
      
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> GetAll()
        {
            return weatherDatas;// возвращение всех записей списка
        }

        [HttpPost]
            public IActionResult Add(WeatherData data)
            {
                for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива wetherDatas
                {
                    if (weatherDatas[i].Id == data.Id)//в случае, если идентификаторы одинаковые - выполним следующее
                    {
                        return BadRequest("Запись с таким id уже есть");// Возвращает результат "Ошибка" с сообщением
                    }
                }
                weatherDatas.Add(data); // добавляет в список новую запись 
                return Ok();// возвращаем результат "успешно"
            }



        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            if (data.Id < 0)
            {
                return BadRequest();
            }

            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива wetherDatas
            {
                    if (weatherDatas[i].Id == data.Id)//в случае, если идентификаторы одинаковые - выполним следующее
                    {
                        weatherDatas[i] = data;// заменяем значение для данно ячейки массива
                        return Ok();// возвращаем результат "успешно"
                    }
            }
            return BadRequest("Такая запись не обнаружена");// Возвращает результат "Ошибка" с сообщением

        }
    

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           for (int i = 0;i < weatherDatas.Count;i++)//Цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == id)//В случае, если идентифиаторы одинаковые - выполним следующее
                {
                    weatherDatas.RemoveAt(i);//Удаляем элемент из массива по тего индексу (переменная i)
                    return Ok();//Возвращение результата "Успешно"
                }
            }
            return BadRequest("Такая запись не обнаружена");//возвращает результат "Ошибка" с сообщением 
        }


        [HttpGet("{index}")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }

            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name")]
        public IActionResult GetCount(string name)
        {
            if (name == null)
            {
                return BadRequest("Имя не может быть пустым!!!!");
            }

            int count = Summaries.Count(x => x == name);
            return Ok(count);
        }

        [HttpGet("sort")]
        public IActionResult GetAll(int sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            else if (sortStrategy == 1)
            {
                return Ok(Summaries.OrderBy(x => x));
            }
            else if (sortStrategy == -1)
            {
                return Ok(Summaries.OrderByDescending(x => x));
            }
            else
            {
                return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id) 
        {
        for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива wetherDatas
        {
           if (weatherDatas[i].Id == id)//в случае, если идентификаторы одинаковые - выполним следующее
           {
             return Ok(weatherDatas[i]);// возвращаем результат "успешно" с данными о записи
           }
        }
            return BadRequest("Запись с таким id уже есть");// Возвращает результат "Ошибка" с сообщением
        }



        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            if (weatherDatas.Any(x => x.Location == location))
            {
                return Ok("Запись с указанным городом имеется в нашем списке");
            }
            else
            {
                return Ok("Запись с указанным городом не обнаружено");
            }
        }
    }
}






