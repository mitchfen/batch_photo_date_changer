using System.Net.Mime;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using ExifLibrary;
using System.Dynamic;
using System;
using Serilog;
using System.IO;
using System.Data;

public class PhotoDateChanger
{
    public static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            // Get user inputs
            Console.WriteLine("Enter the full path to the images: ");
            var inputPath = Console.ReadLine();
            Console.WriteLine("Enter the date you'd like to set: ");
            var inputDate = Console.ReadLine();
            DateTime desiredDate;
            DateTime.TryParse(inputDate, out desiredDate);
            
            // Catch some errors
            if (string.IsNullOrEmpty(inputPath))
            {
                throw new NoNullAllowedException("You must provide a directory when prompted.");
            }
            else if (!Directory.Exists(inputPath))
            {
                throw new DirectoryNotFoundException();   
            }

            // Iterate over the images and change the dates
            string[] images = Directory.GetFiles(inputPath);
            foreach (string image in images)
            {
                UpdateMetadata(image, desiredDate);
            }
            Log.Information("Done!");
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }

    public static void UpdateMetadata(string path, DateTime desiredDate)
    {
        var imageFile = ImageFile.FromFile(path);

        Log.Information($"File: {path}");
        var originalDate = imageFile.Properties.Get<ExifDateTime>(ExifTag.DateTime);
        Log.Information($"Old date: {originalDate}");

        Log.Information($"New date: {desiredDate}");

        imageFile.Properties.Set(ExifTag.DateTimeDigitized, desiredDate);
        imageFile.Properties.Set(ExifTag.DateTimeOriginal, desiredDate);
        imageFile.Save(path);
        Console.WriteLine("");
    }
}