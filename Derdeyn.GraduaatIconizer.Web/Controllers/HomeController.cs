using Derdeyn.GraduaatIconizer.Web.Extensions;
using Derdeyn.GraduaatIconizer.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Derdeyn.GraduaatIconizer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptionsSnapshot<IconizrSettings> _settings;

        public HomeController(IHostingEnvironment hostingEnvironment, IOptionsSnapshot<IconizrSettings> settings)
        {
            _hostingEnvironment = hostingEnvironment;
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewBag.NavItem = "Home";
            return View();
        }

        public IActionResult Generate(string top = "", string bottom = "", bool isRoot = false)
        {
            top = WebUtility.UrlDecode(top ?? "")
                    .Remove32BitChars()
                    .PadRight(_settings.Value.MaxLength, ' ')
                    .Substring(0, _settings.Value.MaxLength)
                    .Trim();
            
            bottom = WebUtility.UrlDecode(bottom ?? "")
                    .Remove32BitChars()
                    .PadRight(_settings.Value.MaxLength, ' ')
                    .Substring(0, _settings.Value.MaxLength)
                    .Trim();

            string text = $"{top}\n{bottom}";
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
                using (var imgResult = img.Clone(ctx => ctx.ApplyScalingWaterMark(font, text, Rgba32.Black, _settings.Value.DefaultMargin, false)))
                {
                    using (var imageStream = new MemoryStream())
                    {
                        imgResult.Save(imageStream, ImageFormats.Png);
                        imagebytes = imageStream.ToArray();
                    }

                    base64Result = imgResult.ToBase64String(ImageFormats.Png);
                }
            }

            string downloadName = $"{top}-{bottom}.png";
            if (top.Length + bottom.Length == 0) downloadName = "blanco.png";

            return File(imagebytes, "image/png", $"{downloadName}");
        }

        public IActionResult About()
        {
            ViewBag.NavItem = "About";
            return View();
        }

    }
}
