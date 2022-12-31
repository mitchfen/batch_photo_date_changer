
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExifLibrary;
using static PhotoDateChanger;
using System;

namespace PhotoDateChangerTests;

[TestClass]
public class StaticMethodTests
{
    [TestMethod]
    public void TestUpdateMetadata()
    {
        const string testImagePath = "./test_photo.jpg";
        
        DateTime desiredDate = new DateTime(2021, 09, 14);
        
        UpdateMetadata(testImagePath, desiredDate);

        var imageFile = ImageFile.FromFile(testImagePath);
        var newDate = imageFile.Properties.Get(ExifTag.DateTimeDigitized);
        Assert.AreEqual( "2021.09.14 00:00:00", newDate.ToString() );
    }
}
