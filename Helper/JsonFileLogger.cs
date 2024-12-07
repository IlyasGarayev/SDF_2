using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonFileLogger
{
    private string _filePath;

    public JsonFileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void LogException(Exception ex)
    {
        try
        {
            var exceptionDetails = new
            {
                Timestamp = DateTime.Now,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                InnerException = ex.InnerException?.Message
            };

            // Əgər fayl mövcud deyilsə, yeni fayl yaradılır
            var exceptionsList = new List<object>();
            if (File.Exists(_filePath))
            {
                var existingContent = File.ReadAllText(_filePath);
                exceptionsList = JsonConvert.DeserializeObject<List<object>>(existingContent) ?? new List<object>();
            }

            // Yeni exception məlumatlarını əlavə et
            exceptionsList.Add(exceptionDetails);

            // Yenilənmiş məlumatları JSON formatında fayla yaz
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(exceptionsList, Formatting.Indented));
        }
        catch (Exception writeEx)
        {
            Console.WriteLine($"Xəta qeydi yazılarkən səhv baş verdi: {writeEx.Message}");
        }
    }
}
