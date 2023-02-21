using Microsoft.AspNetCore.Mvc;
using wsdl;

namespace appMVC.Controllers
{

    public class CalculatorController : Controller
    {
        private readonly CalculatorSoap _calculator;

        public CalculatorController()
        {
            _calculator = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Calculate(int a, int b, string operation)
        {
            int? result;
            try
            {
                result = await CalculateResultAsync(a, b, operation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return View("Index", result);
        }

        private async Task<int> CalculateResultAsync(int a, int b, string operation)
        {
            switch (operation)
            {
                case "add":
                    return await _calculator.AddAsync(a, b);
                case "subtract":
                    return await _calculator.SubtractAsync(a, b);
                case "multiply":
                    return await _calculator.MultiplyAsync(a, b);
                case "divide":
                    return await _calculator.DivideAsync(a, b);
                default:
                    throw new ArgumentException("Invalid operation");
            }
        }
    }
}