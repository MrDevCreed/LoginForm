using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Controllers
{
    public class UnitConverterController : Controller
    {
        [HttpGet]
        public IActionResult CharToBit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CharToBit(string Text)
        {
            byte[] Bytes = Encoding.ASCII.GetBytes(Text);
            var Bits = new BitArray(Bytes);

            string TextByte = String.Concat(Bytes);
            string TextBits = "";

            for (int i = 0; i < Bits.Count; i++)
            {
                if (Bits.Get(i) == true)
                {
                    TextBits += "1";
                }
                else
                {
                    TextBits += "0";
                }
            }

            ViewBag.Byte = TextByte;
            ViewBag.Bit = TextBits;

            return View();
        }


        [HttpGet]
        public IActionResult BitToChar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BitToChar(string Bit,string Byte)
        {
            byte[] Bytes = new byte[] { byte.Parse(Byte) };

            ViewBag.Byte = Encoding.Default.GetString(Bytes);

            Common.BitArray bitArray = new Common.BitArray(Bit);

            ViewBag.Bit = Encoding.Default.GetString(bitArray.GetBytes());
         
            return View();
        }
    }
}
