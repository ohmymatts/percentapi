using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PercentAPI.models;

namespace PercentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }
        
        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody] Value value)
        {
            double taxValue = 0;
            string financeType = value.FinanceType;
            int installment = value.Installments;
            double total = value.TotalAmount;
            DateTime firstExpiring = value.FirstExpiry;
            if (total > 1000000)
            {
                return new JsonResult(new {reproved = "maximum Amount 1.000.000,00"});
            }
            switch (financeType)
            {
                case "Credito Direto":
                    taxValue = 0.02;
                    break;
                case "Credito Consignado":
                    taxValue = 0.01;
                    break;
                case "Credito Pessoa Jurídica":
                    if(total >= 15000){
                        taxValue = 0.05;
                    }
                    else
                    {
                        return new JsonResult(new {reproved = "Minimum Amount 15.000,00"});
                    }
                    break;
                case "Credito Pessoa Física":
                    taxValue = 0.03;
                    break;
                case "Credito Imobiliário":
                    taxValue = 0.09;
                    break;
                default:
                    return new JsonResult(new { reproved = "Not a valid type" });
            }

            var dateValidation = firstExpiring.ToUniversalTime().Subtract(DateTime.Today);
            //if ( Int32.Parse(dateValidation) >= 15 || dateValidation > 40)
            //{
              //  return new JsonResult(new { reproved = "Invalid Date" });
            //}

            if (installment <= 4)
            {
                return new JsonResult(new { reproved = "Minimum amount is 5" });
            } else if (installment >= 72)
            {
                return new JsonResult(new { reproved = "Maximum amount is 72" });
            }
            return new JsonResult(new { total = total * Math.Pow((1 + taxValue), installment),
                                        tax = total * Math.Pow((taxValue), installment),
                                        approved = true
            });;
        }
        


    }
}