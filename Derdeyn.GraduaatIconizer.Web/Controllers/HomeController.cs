using Derdeyn.GraduaatIconizer.Web.Extensions;
using Derdeyn.GraduaatIconizer.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Derdeyn.GraduaatIconizer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.NavItem = "Home";
            return View();
        }

        public IActionResult Generate(string module = "", string group = "", bool isRoot = false)
        {
            module = Remove32BitChars(module)?.PadRight(4, ' ').Substring(0, 4).Trim();
            group = Remove32BitChars(group)?.PadRight(4, ' ').Substring(0, 4).Trim();

            string text = $"{module}\n{group}";
            string baseimage = "imgresources/base-blue.png";
            if (isRoot)
                baseimage = "imgresources/base-orange.png";

            baseimage = Path.Combine(_hostingEnvironment.WebRootPath, baseimage);
            string output = Path.Combine(_hostingEnvironment.WebRootPath, "imgresources/output.png");
            string fontpath = Path.Combine(_hostingEnvironment.WebRootPath, "imgresources/fonts/ARLRDBD.TTF"); // Arial Rounded MT Bold

            string base64Result = "";
            byte[] imagebytes = null;

            Font font = new FontCollection().Install(fontpath).CreateFont(10);

            using (var img = Image.Load(baseimage))
            {
                using (var imgResult = img.Clone(ctx => ctx.ApplyScalingWaterMark(font, text, Rgba32.Black, 20, false)))
                {
                    using (var imageStream = new MemoryStream())
                    {
                        imgResult.Save(imageStream, ImageFormats.Png);
                        imagebytes = imageStream.ToArray();
                    }

                    base64Result = imgResult.ToBase64String(ImageFormats.Png);
                }
            }

            return File(imagebytes, "image/png");
        }

        public IActionResult About()
        {
            ViewBag.NavItem = "About";
            return View();
        }

        static string Remove32BitChars(string str)
        {
            if (str == null) return "";

            char[] oldchars = str.ToCharArray();
            List<char> newchars = new List<char>(oldchars.Length);

            for (var i = 0; i < oldchars.Length; i++)
            {
                if(!char.IsControl(oldchars[i]) && !char.IsHighSurrogate(oldchars[i]) && !char.IsLowSurrogate(oldchars[i]))
                {
                    newchars.Add(oldchars[i]);
                }
            }
            return new string(newchars.ToArray());
        }

    }
}
